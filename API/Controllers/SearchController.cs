using API.Models;
using BLL;
using IBLL;
using MODEL;
using System;
using System.Collections.Generic;

namespace API.Controllers
{
    public class SearchController : BaseApiController<Product, IProductBLL>
    {
        public IProductBLL Bll { get { return new ProductBLL(); } }

        public ResponsMessage<List<Product>> Get(string query)
        {
            try
            {
                var Data = Bll.Search(x => x.KeyWords.Contains(query));
                return new ResponsMessage<List<Product>>()
                {
                    Code = 200,
                    Message = "请求成功",
                    Data = Data,
                    Total = Data.Count
                };
            }
            catch (Exception)
            {
                return Error<List<Product>>("在搜索商品名称时出现异常");
            }
        }
    }
}
