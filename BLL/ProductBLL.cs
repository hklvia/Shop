﻿using DAL;
using IBLL;
using IDAL;
using MODEL;
using System.Collections.Generic;
using VMODEL;

namespace BLL
{
    public class ProductBLL : BaseBLL<Product, ProductDAL>, IProductBLL
    {
        IProductSkuDAL skuDal = new ProductSkuDAL();
        IProductAttrDAL attrDal = new ProductAttrDAL();
        IProductSkuImgDAL skuImgDal = new ProductSkuImgDAL();
        IProductAttrKeyBLL attrBll = new ProductAttrKeyBLL();
        public int Add(Product product, List<ProductSku> skuList, List<ProductAttr> attrList, List<ProductSkuImg> productSkuImg)
        {
            var result = 0;
            var tran = dal.BeginTran();
            try
            {
                dal.Add(product);


                result += SaveChange();
                foreach (var skuImg in productSkuImg)
                {
                    skuImg.ProductID = product.ID;
                    skuImgDal.Add(skuImg);
                }

                foreach (var sku in skuList)
                {
                    sku.ProductID = product.ID;
                    skuDal.Add(sku);
                }

                foreach (var attr in attrList)
                {
                    attr.ProductID = product.ID;
                    attrDal.Add(attr);
                }
                result += SaveChange();
                tran.Commit();
            }
            catch (System.Exception)
            {
                tran.Rollback();
            }
            return result;
        }

        public override int Delete(int id)
        {
            dal.Delete(id);
            var skus = skuDal.Search(x => x.ProductID == id);
            foreach (var sku in skus)
            {
                skuDal.Delete(sku);
            }
            var attrs = attrDal.Search(x => x.ProductID == id);
            foreach (var attr in attrs)
            {
                attrDal.Delete(attr);
            }
            return SaveChange();
        }

        public int Update(Product product, List<ProductSku> skuList, List<ProductAttr> attrList)
        {
            //修改商品表
            dal.Update(product);

            //删除属性和sku
            var skus = skuDal.Search(x => x.ProductID == product.ID);
            foreach (var sku in skus)
            {
                skuDal.Delete(sku);
            }
            var attrs = attrDal.Search(x => x.ProductID == product.ID);
            foreach (var attr in attrs)
            {
                attrDal.Delete(attr);
            }

            //添加属性和sku
            foreach (var sku in skuList)
            {
                sku.ProductID = product.ID;
                skuDal.Add(sku);
            }
            foreach (var attr in attrList)
            {
                attr.ProductID = product.ID;
                attrDal.Add(attr);
            }
            return SaveChange();
        }

        public Product GetOne(int id, out List<ProductSku> skus, out List<ProductAttr> attrs)
        {
            var product = dal.GetOne(id);
            skus = skuDal.Search(x => x.ProductID == id);
            attrs = attrDal.Search(x => x.ProductID == id);

            return product;
        }

        public ProductVModel GetFullInfoByID(int id)
        {
            var product = dal.GetOne(id);
            var skus = attrBll.GetByCategoryID(product.ProductCategoryID.Value,true);
            var productSkus = skuDal.Search(x => x.ProductID == product.ID);
            return new ProductVModel()
            {
                Product = product,
                Skus = skus,
                ProductSkus = productSkus
            };
        }
    }
}