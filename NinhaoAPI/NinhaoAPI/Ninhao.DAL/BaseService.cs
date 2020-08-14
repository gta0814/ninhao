using Ninhao.IDAL;
using Ninhao.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ninhao.DAL
{
    public class BaseService<T> : IDisposable where T : BaseEntity, new()
    {
        protected readonly NinhaoContext _db;
        public BaseService(NinhaoContext db)
        {
            _db = db;
        }
        public async Task CreateAsync(T model, bool saved = true)
        {
            _db.Set<T>().Add(model);
            if (saved) await _db.SaveChangesAsync();
        }
        public async Task CreateListAsync(List<T> models, bool saved = true)
        {
            _db.Set<T>().AddRange(models);
            if (saved)
            {
                await _db.SaveChangesAsync();
            }
        }

        public async Task EditAsync(T model, bool saved = true)
        {
            _db.Configuration.ValidateOnSaveEnabled = false;
            _db.Entry(model).State = EntityState.Modified;
            if (saved)
            {
                await SaveAsync();
            }
        }
        public async Task RemoveAsync(Guid id, bool saved = true)
        {
            _db.Configuration.ValidateOnSaveEnabled = false;
            var t = new T() { Id = id };
            _db.Entry(t).State = EntityState.Unchanged;
            t.IsRemoved = true;
            if (saved)
            {
                await SaveAsync();
            }
        }

        public async Task RemoveAsync(T model, bool saved = true)
        {
            await RemoveAsync(model.Id, saved);
        }

        /// <summary>
        /// return all undeleted data (没有真的执行query语句)
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            return _db.Set<T>().Where(m => !m.IsRemoved).AsNoTracking();
        }
        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }
        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate, bool asc)
        {
            var data = GetAll(predicate);
            data = asc ? data.OrderBy(m => m.CreateTime) : data.OrderByDescending(m => m.CreateTime);
            return data;
        }
        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate, bool asc, int pageSize, int pageIndex = 0)
        {
            return GetAll(predicate, asc).Skip(pageSize * pageIndex).Take(pageSize);
        }
        #region old GetAll() 
        //public IQueryable<T> GetAllByPage(int pageSize = 10, int pageIndex = 0)
        //{
        //    return GetAll().Skip(pageSize * pageIndex).Take(pageSize);
        //}

        //public IQueryable<T> GetAllByPageInOrder(int pageSize = 10, int pageIndex = 0, bool asc = true)
        //{
        //    return GetAllInOrder(asc).Skip(pageSize * pageIndex).Take(pageSize);
        //}

        //public IQueryable<T> GetAllInOrder(bool asc = true)
        //{
        //    var data = GetAll();
        //    data = asc ? data.OrderBy(m => m.CreateTime) : data.OrderByDescending(m => m.CreateTime);
        //    return data;
        //} 
        #endregion

        public async Task<T> GetOneByIdAsync(Guid id)
        {
            return await GetAll().FirstAsync(m => m.Id == id);
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
            _db.Configuration.ValidateOnSaveEnabled = true;
        }
        public void Dispose()
        {
            _db.Dispose();
        }

        /// <summary>
        /// InsertOrUpdate pattern
        /// </summary>
        //public void InsertOrUpdate(Blog blog)
        //{
        //    using (var context = new BloggingContext())
        //    {
        //        context.Entry(blog).State = blog.BlogId == 0 ?
        //                                   EntityState.Added :
        //                                   EntityState.Modified;

        //        context.SaveChanges();
        //    }
        //}
    }
}
