using API.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using COMMON;
using IBLL;
using BLL;
using JWT.Algorithms;
using JWT;
using JWT.Serializers;
using MODEL;
using Newtonsoft.Json.Linq;
using VMODEL;

namespace API.Controllers
{
    public class AuthController : ApiController
    {
        public IMemberBLL Bll { get { return new MemberBLL(); } }

        [Route("api/auth/getToken")]
        [HttpPost]
        public ResponsMessage<TokenVModel> GetToken(MemberVModel memberVModel)
        {
            HttpWebResponse response = null;
            Stream Responsestream = null;
            StreamReader streamReader = null;
            try
            {
                string openIdUrl = $"https://api.weixin.qq.com/sns/jscode2session?appid={ConfigurationManager.AppSettings["AppID"]}&secret={ConfigurationManager.AppSettings["AppSecret"]}&js_code={memberVModel.Code}&grant_type=authorization_code";
                //服务器发起http请求（爬虫）
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(openIdUrl);
                //获取返回头
                response = (HttpWebResponse)request.GetResponse();
                request.Method = "GET";
                //获取响应流
                Responsestream = response.GetResponseStream();
                streamReader = new StreamReader(Responsestream, System.Text.Encoding.UTF8);
                string retString = streamReader.ReadToEnd();

                if (JObject.Parse(retString)["openid"] == null)
                {
                    return new ResponsMessage<TokenVModel>()
                    {
                        Code = 500,
                        Message = "code不正确"
                    };
                }

                string openId = JObject.Parse(retString)["openid"].ToString();
                //判断数据库中是否存在该用户（根据openId）
                var member = Bll.Search(x => x.OpenId == openId).First();
                if (member == null)
                {
                    memberVModel.UserInfo.OpenId = openId;
                    Bll.Add(memberVModel.UserInfo);
                    member = memberVModel.UserInfo;
                }

                //生成token
                string token, refreshToken;
                DateTime expire = DateTime.Now.AddDays(7);
                DateTime now = DateTime.Now;
                CreateToken(member.ID.ToString(), out token, out refreshToken);
                //将token存入redis(tkoen设置时间不宜过长,refreshToken过期时间一般是token的两倍)
                var tokenSet = RedisHelper.Set(token, (member.ID), expire - now);
                var refreshTokenSet = RedisHelper.Set(refreshToken, (member.ID), expire.AddDays(7) - now);
                if (!tokenSet && !refreshTokenSet)
                {
                    return new ResponsMessage<TokenVModel>()
                    {
                        Code = 500,
                        Message = "获取token失败，请重新授权"
                    };
                }


                return new ResponsMessage<TokenVModel>()
                {
                    Code = 200,
                    Data = new TokenVModel()
                    {
                        Token = token,
                        RefreshToken = refreshToken,
                        Expire = (int)(expire - now).TotalMilliseconds
                    }
                };
            }
            catch (Exception ex)
            {
                return new ResponsMessage<TokenVModel>()
                {
                    Code = 500,
                    Message = "获取个人信息失败"
                };
            }
            finally
            {
                if (streamReader != null)
                {
                    streamReader.Close();
                }
                if (Responsestream != null)
                {
                    Responsestream.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
            }
        }

        [HttpGet]
        [Route("api/auth/getTokenByRefreshToken")]
        public ResponsMessage<TokenVModel> GetTokenByRefreshToken(string rToken) {
            var uid = RedisHelper.Get(rToken);
            if (uid==null)
            {
                return new ResponsMessage<TokenVModel>()
                {
                    Code = 500,
                    Message = "refreshToken失效，请重新授权"
                };
            }
            string token, refreshToken;
            CreateToken(uid, out token, out refreshToken);
            DateTime expire = DateTime.Now.AddDays(7);
            DateTime now = DateTime.Now;
            //将token存入redis(tkoen设置时间不宜过长,refreshToken过期时间一般是token的两倍)
            var tokenSet= RedisHelper.Set(token, Int32.Parse(uid), expire - now);
            var refreshTokenSet = RedisHelper.Set(refreshToken, Int32.Parse(uid), expire.AddDays(7) - now);
            if (!tokenSet&& !refreshTokenSet)
            {
                return new ResponsMessage<TokenVModel>()
                {
                    Code = 500,
                    Message= "获取token失败，请重新授权"
                };
            }
            return new ResponsMessage<TokenVModel>()
            {
                Code = 200,
                Data = new TokenVModel()
                {
                    Token = token,
                    RefreshToken = refreshToken,
                    Expire = (int)(expire - now).TotalMilliseconds
                }
            };
        }

        /// <summary>
        /// 生成token
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="token"></param>
        /// <param name="refreshToken"></param>
        private void CreateToken(string uid, out string token, out string refreshToken)
        {
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            //token
            var payload = new Dictionary<string, object>
            {
                { "UserName", uid+Guid.NewGuid().ToString("N")}
            };
            token = encoder.Encode(payload, ConfigurationManager.AppSettings["JwtSecret"]);
            //refreshToken
            payload = new Dictionary<string, object>
            {
                { "UserName", uid+new Random().Next(10000, 999999).ToString()+Guid.NewGuid().ToString("N")}
            };
            refreshToken = encoder.Encode(payload, ConfigurationManager.AppSettings["JwtSecret"]);
        }
    }
}
