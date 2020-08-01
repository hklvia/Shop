﻿using JWT;
using JWT.Algorithms;
using JWT.Exceptions;
using JWT.Serializers;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using COMMON;
using System.Net;
using System.Collections.Generic;

namespace API.Models
{
    public class AuthFilterAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            IEnumerable<string> headValues;
            string token = null;
            //读取请求中是否包含token
            if (actionContext.Request.Headers.TryGetValues("Authorization",out headValues))
            {
                token = headValues.FirstOrDefault();
            }

            if (token != null)
            {
                try
                {
                    //验证jwt的合法性
                    IJsonSerializer serializer = new JsonNetSerializer();
                    IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                    IDateTimeProvider provider = new UtcDateTimeProvider();
                    IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
                    IJwtValidator validator = new JwtValidator(serializer, provider);
                    IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);
                    var json = decoder.Decode(token, ConfigurationManager.AppSettings["JwtSecret"], verify: true);

                    //验证redis是否过期
                    if (RedisHelper.Get(token) == null)
                    {
                        HttpResponseMessage httpResponse = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                        httpResponse.Content = new StringContent("签名已过期");
                        actionContext.Response = httpResponse;
                    }
                }
                catch (SignatureVerificationException)
                {
                    HttpResponseMessage httpResponse = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                    httpResponse.Content = new StringContent("签名无效");
                    actionContext.Response = httpResponse;
                }
            }
        }
    }
}