using API.Models;
using IBLL;
using MODEL;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace API.Controllers
{
    public class BaseApiController<T, B> : ApiController
        where T : BaseModel, new()
        where B : IBaseBLL<T>
    {
        public virtual B BLL { get; set; }

        //获取全部数据
        public virtual ResponsMessage<List<T>> Get()//泛型方法
        {
            try
            {
                return Success(BLL.GetAll());
            }
            catch (Exception)
            {
                return Error<List<T>>("在查询全部数据过程中出现异常");
            }
        }

        //根据主键获取单个数据
        public virtual ResponsMessage<T> Get(int id)
        {
            try
            {
                return Success(BLL.GetOne(id));
            }
            catch (Exception)
            {
                return Error<T>("在查询单条数据过程中出现异常");
            }
        }

        public virtual ResponsMessage<PageModel<T>> PostPager(SearchVModel search)
        {
            try
            {
                int count;
                var list = BLL.Search(
                    search.pageSize,
                    search.pageIndex,
                    false,
                    x => x.ID,
                    x => 1 == 1,
                    out count
                    );
                PageModel<T> page = new PageModel<T>()
                {
                    total = count,
                    data = list
                };
                return Success(page);
            }
            catch (Exception ex)
            {
                return Error<PageModel<T>>("在查询分页数据过程中出现异常");
            }
        }

        //成功方法封装
        [NonAction]
        public ResponsMessage<D> Success<D>(D data)
        {
            return new ResponsMessage<D>()
            {
                Code = 200,
                Message = "请求成功",
                Data = data
            };
        }

        //失败方法封装
        [NonAction]
        public ResponsMessage<D> Error<D>(string message)
        {
            return new ResponsMessage<D>()
            {
                Code = 500,
                Message = "请求失败" + message
            };
        }
        ////查询所有数据
        //public virtual List<T> Get()
        //{
        //    return Bll.GetAll();
        //}

        ////根据主键查询单条数据
        //public virtual T Get(int id)
        //{
        //    return Bll.GetOne(id);
        //}
    }
}
