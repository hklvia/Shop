using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using IDAL;
using MODEL;

namespace DAL
{
    public class BaseDAL<T> where T : BaseModel, new()
    {
        public ShopEntities entities = DbContextFactory.GetEntities();

        public virtual void Add(T model)
        {
            DbEntityEntry entityEntry = entities.Entry(model);
            entityEntry.State = EntityState.Added;
        }

        public virtual void Update(T model)
        {
            DbEntityEntry entityEntry = entities.Entry(model);
            entityEntry.State = EntityState.Modified;
        }

        public virtual void Delete(int id)
        {
            T model = new T() { ID = id };
            DbEntityEntry entityEntry = entities.Entry(model);
            entityEntry.State = EntityState.Deleted;
        }

        public virtual void Delete(T model)
        {
            DbEntityEntry entityEntry = entities.Entry(model);
            entityEntry.State = EntityState.Deleted;
        }

        public virtual T GetOne(int id)
        {
            return entities.Set<T>().FirstOrDefault(x => x.ID == id);
        }

        public virtual List<T> Search(Expression<Func<T, bool>> where)
        {
            return entities.Set<T>().Where(where).ToList();
        }

        //public virtual List<T> Search(int pageSize, int pageIndex, bool isDesc, Expression<Func<T, bool>> where)
        //{
        //    if (isDesc)
        //    {
        //        return entities.Set<T>().Where(where).OrderByDescending(x => x.ID).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        //    }
        //    else
        //    {
        //        return entities.Set<T>().Where(where).OrderBy(x => x.ID).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        //    }
        //}

        //public virtual List<T> Search<TKey>(int pageSize, int pageIndex, bool isDesc, Func<T, TKey> orderKey, Expression<Func<T, bool>> where)
        //{
        //    count = entities.Set<T>().Where<where>.Count();
        //    if (isDesc)
        //    {
        //        return entities.Set<T>().Where(where).OrderByDescending(x => x.ID).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        //    }
        //    else
        //    {
        //        return entities.Set<T>().Where(where).OrderBy(x => x.ID).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        //    }
        //}

        public virtual List<T> Search<TKey>(int pageSize, int pageIndex, bool isDesc, Func<T, TKey> orderkey, Expression<Func<T, bool>> where, out int count)
        {
            //离开当前方法必须给out参数赋值
            count = entities.Set<T>().Where(where).Count();
            if (isDesc)
            {
                return entities.Set<T>().Where(where).OrderByDescending(orderkey).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                return entities.Set<T>().Where(where).OrderBy(orderkey).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }

        }

        public virtual int GetCount(Expression<Func<T, bool>> where)
        {
            return entities.Set<T>().Where(where).Count();
        }

        public virtual List<T> GetAll()
        {
            return entities.Set<T>().ToList();
        }

        public int SaveChange()
        {
            return entities.SaveChanges();
        }

        public DbContextTransaction BeginTran()
        {
            var tran = entities.Database.BeginTransaction();
            return tran;
        }

        /// <summary>
        ///生成订单编号
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string CreateNumber(int type)
        {
            //ShopEntities entities = new ShopEntities();
            //1-商品销售订单
            int currentNumber = 1;
            var number = entities.Number.Where(x => x.Type == type).FirstOrDefault();
            if (number != null)
            {
                currentNumber = number.CurrentNumber.Value + 1;
                number.CurrentNumber = currentNumber;
                entities.SaveChanges();//执行update
            }
            else
            {
                Number number1 = new Number();
                number.Type = type;
                number.CurrentNumber = currentNumber;
                entities.Number.Add(number1);
                entities.SaveChanges();
            }
            string dh = DateTime.Now.ToString("yyyyMMddHHmmssfffff") + new Random().Next(10000, 999999) + currentNumber.ToString().PadLeft(5, '0');
            return dh;
        }
    }
}