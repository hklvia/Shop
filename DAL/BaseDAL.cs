using MODEL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

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
            return entities.Database.BeginTransaction();
        }
    }
}