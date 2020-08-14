using MODEL;
using System.Collections.Generic;
using VMODEL;

namespace IBLL
{
    public interface IMainOrderBLL : IBaseBLL<MainOrder>
    {
        MainOrder GetOne(string id, out List<SubOrder> subOrders);
        string Add(string token, OrderVModel orderVModel);
    }
}
