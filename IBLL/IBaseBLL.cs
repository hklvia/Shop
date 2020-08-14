using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;

namespace IBLL
{
    public interface IBaseBLL<T>
    {
        int Add(T model);

        int Delete(int id);

        int Delete(T model);

        List<T> GetAll();

        List<T> Search(Expression<Func<T, bool>> where);

        //List<T> Search(int pageSize, int pageIndex, bool isDesc, Expression<Func<T, bool>> where);
        List<T> Search<TKey>(int pageSize, int pageIndex, bool isDesc, Func<T, TKey> orderkey, Expression<Func<T, bool>> where, out int count);

        int GetCount(Expression<Func<T, bool>> where);

        int Update(T model);

        T GetOne(int id);

        int SaveChange();

        DbContextTransaction BeginTran();
    }
}