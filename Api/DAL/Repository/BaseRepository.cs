using System;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections.Generic;
using Api.Models;

namespace Api.DAL.Repository
{
    public interface IRepository<T> : IDisposable where T : class
    {
        /// <summary>
        /// Gets all objects from database
        /// </summary>
        IQueryable<T> All();

        /// <summary>
        /// Gets objects from database by filter.
        /// </summary>
        /// <param name="predicate">Specified a filter</param>
        IQueryable<T> Filter(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Gets objects from database with filting and paging.
        /// </summary>
        /// <typeparam name="Key"></typeparam>
        /// <param name="filter">Specified a filter</param>
        /// <param name="total">Returns the total records count of the filter.</param>
        /// <param name="index">Specified the page index.</param>
        /// <param name="size">Specified the page size</param>
        IQueryable<T> Filter<Key>(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 50);

        /// <summary>
        /// Gets the object(s) is exists in database by specified filter.
        /// </summary>
        /// <param name="predicate">Specified the filter expression</param>
        bool Contains(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Find object by keys.
        /// </summary>
        /// <param name="keys">Specified the search keys.</param>
        T Find(params object[] keys);

        /// <summary>
        /// Find object by specified expression.
        /// </summary>
        /// <param name="predicate"></param>
        T FirstOrDefault(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Create a new object to database.
        /// </summary>
        /// <param name="t">Specified a new object to create.</param>
        T Add(T t);

        /// <summary>
        /// Update object. Add to context for tracking without save;
        /// </summary>
        T AddAndSave(T TObject);

        /// <summary>
        /// Create or add object. By primary key
        /// </summary>
        void AddOrUpdate(T TObject);

        /// <summary>
        /// Update object. Add to context for tracking without save;
        /// </summary>
        void Update(T TObject);

        /// <summary>
        /// Delete the object from database.
        /// </summary>
        /// <param name="t">Specified a existing object to delete.</param>        
        void Delete(T t);

        /// <summary>
        /// Delete objects from database by specified filter expression.
        /// </summary>
        /// <param name="predicate"></param>
        int Delete(Expression<Func<T, bool>> predicate);

        int Save();

        Task<int> SaveChangesAsync();

        /// <summary>
        /// Get the total objects count.
        /// </summary>
        int Count { get; }

        EntityState State(T TObject);
    }

    public class BaseRepository<TObject> : IRepository<TObject> where TObject : class
    {
        protected ApplicationDbContext Context = null;
        public BaseRepository()
        {
            Context = new ApplicationDbContext();
        }

        public BaseRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        protected DbSet<TObject> DbSet
        {
            get
            {
                return Context.Set<TObject>();
            }
        }

        public void Dispose()
        {
            if (Context != null)
                Context.Dispose();
        }

        public virtual IQueryable<TObject> All()
        {
            return DbSet.AsQueryable();
        }

        public virtual IQueryable<TObject> AllInclusive<TProperty>(Expression<Func<TObject, TProperty>> path)
        {
            return DbSet.Include(path).AsQueryable();
        }

        public virtual IQueryable<TObject> Filter(Expression<Func<TObject, bool>> predicate)
        {
            return DbSet.Where(predicate).AsQueryable();
        }

        public IQueryable<TObject> Filter<Key>(Expression<Func<TObject, bool>> filter, out int total, int index = 0, int size = 50)
        {
            int skipCount = index * size;
            var _resetSet = filter != null ? DbSet.Where(filter).AsQueryable() :
                DbSet.AsQueryable();
            _resetSet = skipCount == 0 ? _resetSet.Take(size) :
                _resetSet.Skip(skipCount).Take(size);
            total = _resetSet.Count();
            return _resetSet.AsQueryable();
        }
        public bool Contains(Expression<Func<TObject, bool>> predicate)
        {
            return DbSet.Count(predicate) > 0;
        }

        public TObject Find(params object[] keys)
        {
            return DbSet.Find(keys);
        }

        public virtual TObject FirstOrDefault(Expression<Func<TObject, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }

        public virtual TObject Find(object key)
        {
            return DbSet.Find(new object[] { key });
        }

        public virtual TObject Add(TObject TObject)
        {
            var newEntry = DbSet.Add(TObject);
            return newEntry;
        }

        public virtual TObject AddAndSave(TObject TObject)
        {
            var newEntry = DbSet.Add(TObject);
            Context.SaveChanges();

            return newEntry;
        }

        public virtual Task<int> AddAndSaveAsync(TObject TObject)
        {
            var newEntry = DbSet.Add(TObject);
            return Context.SaveChangesAsync();
        }

        public virtual void AddOrUpdate(TObject TObject)
        {
            //надо подумать            
        }

        public virtual T AddReferencedObject<T>(T TEntity) where T : class
        {
            return Context.Set<T>().Add(TEntity);
        }

        public virtual T CreateReferencedObject<T>(T TEntity) where T : class
        {
            return Context.Set<T>().Add(Activator.CreateInstance<T>());
        }

        public virtual TObject CreateReferencedObject()
        {
            return Context.Set<TObject>().Add(Activator.CreateInstance<TObject>());
        }

        public virtual void Update(TObject TObject)
        {
            Context.Entry(TObject).State = EntityState.Modified;
        }

        public virtual EntityState State(TObject TObject)
        {
            return Context.Entry(TObject).State;
        }

        public virtual int Count
        {
            get
            {
                return DbSet.Count();
            }
        }

        public virtual void Remove(TObject TObject)
        {
            DbSet.Remove(TObject);
        }

        public virtual void RemoveReferencedEntity<T>(T TEntity) where T : class
        {
            Context.Entry<T>(TEntity).State = EntityState.Deleted;
        }

        public virtual void RemoveReferencedEntity<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            var genDBSet = Context.Set<T>();
            foreach (var entity in genDBSet.Where(predicate))
            {
                genDBSet.Remove(entity);
            }
        }

        public virtual int Delete(Expression<Func<TObject, bool>> predicate)
        {
            var objects = Filter(predicate);
            foreach (var obj in objects)
                DbSet.Remove(obj);

            return Context.SaveChanges();
        }

        public virtual void Delete(TObject TObject)
        {
            DbSet.Remove(TObject);
            Context.SaveChanges();
        }

        public virtual int Save()
        {
            return Context.SaveChanges();
        }

        public virtual Task<int> SaveChangesAsync()
        {
            return Context.SaveChangesAsync();
        }

        public virtual IList<TObject> CallQueryList(string sql)
        {
            var items = Context.Database.SqlQuery<TObject>(sql).ToList();
            return items;
        }

        public virtual int CallQueryScalar(string sql)
        {
            var result = Context.Database.SqlQuery<int>(sql).First();
            return result;
        }

        public virtual int ExecuteQuery(string sql)
        {
            var result = Context.Database.ExecuteSqlCommand(sql);
            return result;
        }
    }
}