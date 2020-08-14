using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDemo1.Models
{
    public class ProductAttrKey
    {
        public int ID { get; set; }
        public string AttrName { get; set; }
        public int ProductCategoryID { get; set; }
        public int IsSku { get; set; }
        public int EnterType { get; set; }
        public int OrderNum { get; set; }
        public int IsImg { get; set; }
    }
}
