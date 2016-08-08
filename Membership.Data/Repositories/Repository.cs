using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Membership.Core.Domain;
using Membership.Core.Enum;
using Membership.Core.Helper;

namespace Membership.Data.Repositories
{
    public class Repository<T> : DbFactory, IRepository<T> where T : DomainBase
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _entities;

        public Repository()
        {
            _context = DbInstance;
            _entities = _context.Set<T>();
        }

        public T Insert(T entity)
        {
            try
            {
                if (entity == null)
                    ExceptionHelper.ThrowIfNull(() => entity);

                entity.CreateTime = DateTime.Now;
                entity.IsDeleted = (byte)GeneralEnum.IsDeleted.No;

                _context.Set<T>().Add(entity);

                _context.SaveChanges();

                return entity;
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = dbEx.EntityValidationErrors.SelectMany(validationErrors => validationErrors.ValidationErrors).
                    Aggregate(string.Empty,
                        (current, validationError) =>
                            current + $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}" +
                            Environment.NewLine);

                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public T Update(T entity)
        {
            try
            {
                if (entity == null)
                    ExceptionHelper.ThrowIfNull(() => entity);

                entity.IsDeleted = (byte)GeneralEnum.IsDeleted.No;
                entity.UpdateTime = DateTime.Now;

                _context.Set<T>().Attach(entity);

                _context.Entry(entity).State = EntityState.Modified;

                _context.SaveChanges();

                return entity;
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = dbEx.EntityValidationErrors.SelectMany(validationErrors => validationErrors.ValidationErrors).
                    Aggregate(string.Empty,
                        (current, validationError) =>
                            current + Environment.NewLine +
                            $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");

                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public void Delete(T entity)
        {
            try
            {
                if (entity == null)
                    ExceptionHelper.ThrowIfNull(() => entity);

                entity.IsDeleted = (byte)GeneralEnum.IsDeleted.Yes;
                entity.DeleteTime = DateTime.Now;

                _context.Set<T>().Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;

                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = dbEx.EntityValidationErrors.SelectMany(validationErrors =>
                    validationErrors.ValidationErrors)
                    .Aggregate(string.Empty,
                        (current, validationError) =>
                            current + Environment.NewLine +
                            $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");

                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public List<T> Find(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return _entities.Where(predicate).ToList();
            }
            catch
            {
                return null;
            }
        }

        public T FindOne(Expression<Func<T, bool>> predicate)
        {
            return _entities.FirstOrDefault(predicate);
        }

        public List<T> FindAll()
        {
            return _entities.ToList();
        }

        public DbRawSqlQuery<T1> SqlQuery<T1>(string sql, params object[] parameters)
        {
            return _context.Database.SqlQuery<T1>(sql, parameters);
        }

        public async Task<T> InsertAsync(T entity)
        {
            try
            {
                if (entity == null)
                    ExceptionHelper.ThrowIfNull(() => entity);

                entity.CreateTime = DateTime.Now;
                entity.IsDeleted = (byte)GeneralEnum.IsDeleted.No;

                _context.Set<T>().Add(entity);

                await _context.SaveChangesAsync();

                return entity;
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = dbEx.EntityValidationErrors.SelectMany(validationErrors => validationErrors.ValidationErrors).
                    Aggregate(string.Empty,
                        (current, validationError) =>
                            current + $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}" +
                            Environment.NewLine);

                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public async Task<T> UpdateAsync(T entity)
        {
            try
            {
                if (entity == null)
                    ExceptionHelper.ThrowIfNull(() => entity);

                entity.IsDeleted = (byte)GeneralEnum.IsDeleted.No;
                entity.UpdateTime = DateTime.Now;

                _context.Set<T>().Attach(entity);

                _context.Entry(entity).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return entity;
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = dbEx.EntityValidationErrors.SelectMany(validationErrors => validationErrors.ValidationErrors).
                    Aggregate(string.Empty,
                        (current, validationError) =>
                            current + Environment.NewLine +
                            $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");

                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public async void DeleteAsync(T entity)
        {
            try
            {
                if (entity == null)
                    ExceptionHelper.ThrowIfNull(() => entity);

                entity.IsDeleted = (byte)GeneralEnum.IsDeleted.Yes;
                entity.DeleteTime = DateTime.Now;

                _context.Set<T>().Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = dbEx.EntityValidationErrors.SelectMany(validationErrors =>
                    validationErrors.ValidationErrors)
                    .Aggregate(string.Empty,
                        (current, validationError) =>
                            current + Environment.NewLine +
                            $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");

                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public async Task<T> FindOneAsync(Expression<Func<T, bool>> predicate)
        {
            return await _entities.FirstOrDefaultAsync(predicate);
        }

        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await _entities.Where(predicate).ToListAsync();
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<T>> FindAllAsync()
        {
            return await _entities.ToListAsync();
        }
    }
}