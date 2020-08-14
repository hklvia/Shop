using System.Collections.Generic;

namespace API.Models
{
    public class PageModel<T>
    {
        public int total { get; set; }
        public List<T> data { get; set; }
    }
}