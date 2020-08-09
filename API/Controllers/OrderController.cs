using API.Models;
using BLL;
using COMMON;
using IBLL;
using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.WebPages;
using VMODEL;

namespace API.Controllers
{
    public class OrderController : BaseApiController<MainOrder, IMainOrderBLL>
    {
        public override IMainOrderBLL BLL { get => new MainOrderBLL(); }

        /// <summary>
        /// 查询所有订单
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        //[Route("api/Product/getfull")]
        [AuthFilter]
        public ResponsMessage<OrderVModel> GetAllOrder(string token)
        {
            try
            {
                var id = RedisHelper.Get(token);
                if (id != null)
                {
                    List<SubOrder> subOrders;
                    var mainOrder = BLL.GetOne(id, out subOrders);
                    OrderVModel vModel = new OrderVModel()
                    {
                        MainOrder = mainOrder,
                        SubOrders = subOrders
                    };
                    return Success(vModel);
                }
                return Error<OrderVModel>("在查询订单列表过程中出现异常");
            }
            catch (Exception ex)
            {
                return Error<OrderVModel>("在查询订单列表过程中出现异常" + ex.Message);
            }
        }

        /// <summary>
        /// 创建支付订单
        /// </summary>
        /// <param name="orderVModel"></param>
        /// <returns></returns>
        [Route("api/Order")]
        [AuthFilter]
        [HttpPost]
        public ResponsMessage<string> PostCreateOrder(OrderVModel orderVModel)
        {
            string token = Request.Headers.Authorization.ToString();
            var OrderNum = BLL.Add(token, orderVModel);
            if (OrderNum.IsEmpty())
            {
                return Error<string>("");
            }
            return Success<string>(OrderNum);
        }
    }
}
