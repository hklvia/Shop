using IDAL;
using MODEL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;

namespace BLL
{
    /// <summary>
    /// T:MODEL类型 D：DAL类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="D"></typeparam>
    public class BaseBLL<T, D>
       where T : BaseModel, new()
       where D : IBaseDAL<T>, new()
    {
        public D dal = new D();

        public virtual int Add(T model)
        {
            dal.Add(model);
            return SaveChange();
        }

        public virtual int Delete(int id)
        {
            dal.Delete(id);
            return SaveChange();
        }

        public virtual int Delete(T model)
        {
            dal.Delete(model);
            return SaveChange();
        }

        public virtual List<T> GetAll()
        {
            return dal.GetAll();
        }

        // 泛型，T为参数类型，bool为返回值类型 where为参数
        public virtual List<T> Search(Expression<Func<T, bool>> where)
        {
            return dal.Search(where);
        }

        public virtual List<T> Search<TKey>(int pageSize, int pageIndex, bool isDesc, Func<T, TKey> orderkey, Expression<Func<T, bool>> where, out int count)
        {
            return dal.Search<TKey>(pageSize, pageIndex, isDesc, orderkey, where, out count);
        }

        public virtual int GetCount(Expression<Func<T, bool>> where)
        {
            return dal.GetCount(where);
        }

        public virtual T GetOne(int id)
        {
            return dal.GetOne(id);
        }

        public virtual int Update(T model)
        {
            dal.Update(model);
            return SaveChange();
        }

        public int SaveChange()
        {
            return dal.SaveChange();
        }


        public DbContextTransaction BeginTran()
        {
            return dal.BeginTran();
        }
    }
}