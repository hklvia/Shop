using System;
using System.Collections.Generic;

namespace Shop.Models
{
    public class ProductCategoryVModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
        public Nullable<int> PID { get; set; }
        public Nullable<int> OrderNum { get; set; }
        public string KeyWords { get; set; }
        public string Path { get; set; }
        public List<ProductCategoryVModel> children { get; set; }
    }
}