using DAL;
using IBLL;
using IDAL;
using MODEL;
using System;
using System.Collections.Generic;
using VMODEL;
using COMMON;

namespace BLL
{
    public class MainOrderBLL : BaseBLL<MainOrder, MainOrderDAL>, IMainOrderBLL
    {
        ISubOrderDAL subOrderDAL = new SubOrderDAL();
        IProductSkuDAL productSkuDal = new ProductSkuDAL();

        public MainOrder GetOne(string id, out List<SubOrder> subOrders)
        {
            var mainOrder = dal.GetOne(Int32.Parse(id));
            subOrders = subOrderDAL.Search(x => x.PID == mainOrder.ID);
            return mainOrder;
        }

        public string Add(string token, OrderVModel orderVModel)
        {
            var id = RedisHelper.Get(token);
            if (id.Trim() != null && id.Trim() != "")
            {
                orderVModel.MainOrder.MemberID = new MemberDAL().GetOne(Int32.Parse(id)).ID;
                orderVModel.MainOrder.OrderNum = new NumberBLL().CreateNumber(1);
                orderVModel.MainOrder.OrderStatus = "1";
                //快递单号，暂时由随机数代替
                orderVModel.MainOrder.ExpressNum = new Random().Next(10000, 999999).ToString();
                orderVModel.MainOrder.CreateTime = DateTime.Now;

                var result = 0;
                var tran = dal.BeginTran();
                try
                {
                    dal.Add(orderVModel.MainOrder);
                    result += SaveChange();
                    foreach (var sub in orderVModel.SubOrders)
                    {
                        //删除库存
                        var Sku = productSkuDal.GetOne(sub.SkuID.Value);
                        if (Sku != null)
                        {
                            Sku.Stock -= sub.Count;
                            productSkuDal.Update(Sku);
                        }
                        //订单详情表添加
                        sub.PID = orderVModel.MainOrder.ID;
                        subOrderDAL.Add(sub);
                    }
                    result += SaveChange();
                    tran.Commit();
                }
                catch (System.Exception ex)
                {
                    tran.Rollback();
                }
                return result > 0 ? orderVModel.MainOrder.OrderNum : null;
            }
            return null;
        }
    }
}
