using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDemo1.Models
{
    public class ProductCategory
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
        public int PID { get; set; }
        public int OrderNum { get; set; }
        public string KeyWords { get; set; }
        public string Path { get; set; }
    }
}
