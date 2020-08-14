using System.Collections.Generic;

namespace Constant
{
    public class WebConstants
    {
        public static Dictionary<string, string> ImgPathDicts = new Dictionary<string, string>();
        static WebConstants()
        {
            ImgPathDicts.Add("ProductCategoryImg", "/Upload/Product/CategoryImg/");
            ImgPathDicts.Add("ProductMainImg", "/Upload/Product/ProductMainImg/");
            ImgPathDicts.Add("ProductDetailImg", "/Upload/Product/ProductDetailImg/");
            ImgPathDicts.Add("ProductSlideImg", "/Upload/Product/ProductSlideImg/");
            ImgPathDicts.Add("ProductBrandLogo", "/Upload/Product/ProductBrandLogo/");
            ImgPathDicts.Add("ProductBrandImg", "/Upload/Product/ProductBrandImg/");
            ImgPathDicts.Add("ProductSkuImg", "/Upload/Product/ProductSkuImg/");
        }
    }
}
