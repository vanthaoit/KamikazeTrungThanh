using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace KamikazeTrungThanh.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        T Add(T entity);

        void Update(T entity);

        T Delete(T entity);

        T Delete(int id);

        void DeleteMulti(Expression<Func<T, bool>> where);

        T GetSingleById(int id);

        T GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null);

        IEnumerable<T> GetAll(string[] includes = null);

        IEnumerable<T> GetMulti(Expression<Func<T, bool>> expression, string[] includes = null);

        IEnumerable<T> GetMultiPaging(Expression<Func<T, bool>> expression, out int total, int index, int size = 50, string[] includes = null);

        int Count(Expression<Func<T, bool>> expression);

        bool CheckContains(Expression<Func<T, bool>> expression);
    }
}