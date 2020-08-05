using DAL;
using IBLL;
using IDAL;
using MODEL;
using System;
using System.Collections.Generic;
using VMODEL;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMMON;

namespace BLL
{
    public class MainOrderBLL : BaseBLL<MainOrder, MainOrderDAL>, IMainOrderBLL
    {
        //public IMemberBLL MemBll { get { return new MemberBLL(); } }
        ISubOrderDAL subOrderDAL = new SubOrderDAL();

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
                //orderVModel.MainOrder.MemberID = MemBll.GetOne(Int32.Parse(id)).ID;
                orderVModel.MainOrder.MemberID = GetOne(Int32.Parse(id)).ID;
                string random = new Random().Next(10000, 999999).ToString();
                string time = DateTime.Now.ToString("yyyyMMddHHmmss");
                orderVModel.MainOrder.OrderNum = time + random;
                orderVModel.MainOrder.OrderStatus = "1";
                orderVModel.MainOrder.ExpressNum = random;
                orderVModel.MainOrder.CreateTime = DateTime.Now;

                var result = 0;
                var tran = dal.BeginTran();
                try
                {
                    dal.Add(orderVModel.MainOrder);
                    result += SaveChange();
                    foreach (var sub in orderVModel.SubOrders)
                    {
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
