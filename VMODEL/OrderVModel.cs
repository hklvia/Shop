using MODEL;
using System.Collections.Generic;

namespace VMODEL
{
    public class OrderVModel
    {
        public MainOrder MainOrder { get; set; }
        public List<SubOrder> SubOrders { get; set; }
    }
}
