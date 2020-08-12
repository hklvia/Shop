using IBLL;
using MODEL;
using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Models;
using VMODEL;

namespace API.Controllers
{
    public class ProductController : BaseApiController<Product, IProductBLL>
    {
        public override IProductBLL BLL { get => new ProductBLL(); }
        //查询商品列表
        public override ResponsMessage<PageModel<Product>> PostPager(SearchVModel search)
        {
            try
            {
                int count;
                var list = BLL.Search(
                    search.pageSize,
                    search.pageIndex,
                    false,
                    x => x.ID,
                    x => x.KeyWords.Contains(search.keyWord),
                    out count
                    );
                PageModel<Product> page = new PageModel<Product>()
                {
                    total = count,
                    data = list
                };
                return Success(page);
            }
            catch (Exception ex)
            {
                return Error<PageModel<Product>>("在查询分页数据过程中出现异常" + ex.Message);
            }
        }

        //查询商品详情
        [Route("api/Product/getfull")]
        public ResponsMessage<ProductVModel> GetFullIndoByID(int id)
        {
            var product = BLL.GetOne(id, out List<ProductSku> skus, out List<ProductAttr> attrs);
            ProductVModel vModel = new ProductVModel()
            {
                Product = product,
                Attrs = attrs,
                ProductSkus = skus,
            };
            return Success(vModel);
        }
    }
}
