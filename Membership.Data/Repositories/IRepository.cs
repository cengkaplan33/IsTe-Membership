using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Membership.Data.Repositories
{
    public interface IRepository<T>
    {
        T Insert(T entity);

        T Update(T entity);

        void Delete(T entity);

        T FindOne(Expression<Func<T, bool>> predicate);

        List<T> Find(Expression<Func<T, bool>> predicate);

        List<T> FindAll();

        DbRawSqlQuery<T1> SqlQuery<T1>(string sql, params object[] parameters);

        Task<T> InsertAsync(T entity);

        Task<T> UpdateAsync(T entity);

        void DeleteAsync(T entity);

        Task<T> FindOneAsync(Expression<Func<T, bool>> predicate);

        Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);

        Task<List<T>> FindAllAsync();        
    }
}