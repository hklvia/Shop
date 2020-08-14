using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;

namespace IDAL
{
    public interface IBaseDAL<T>
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        void Add(T model);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">主键</param>
        /// <returns>返回受影响的行数</returns>
        void Update(T model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>受影响的行数</returns>
        void Delete(int id);

        void Delete(T model);

        /// <summary>
        /// 获取修改前的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetOne(int id);

        /// <summary>
        /// 获取所有信息
        /// </summary>
        /// <returns></returns>
        List<T> GetAll();

        List<T> Search(Expression<Func<T, bool>> where);

        //List<T> Search(int pageSize,int pageIndex,bool isDesc, Expression<Func<T, bool>> where);
        List<T> Search<TKey>(int pageSize, int pageIndex, bool isDesc, Func<T, TKey> orderkey, Expression<Func<T, bool>> where, out int count);
        int GetCount(Expression<Func<T, bool>> where);

        int SaveChange();

        DbContextTransaction BeginTran();
    }
}
