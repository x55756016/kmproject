﻿using KMHC.CTMS.DAL.Database;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace KMHC.CTMS.Model.Repository
{
    /// <summary>
    /// 数据层基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class BaseRepository<TEntity> : IDisposable, IBaseRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;


        public BaseRepository()   
        {
            _context = new CRDatabase();
        }

        public BaseRepository(DbContext context)
        {
            //Contract.Requires<ArgumentNullException>(context != null);
            //Check.IsNotNull(context);
            _context = context;
           
        }

        public bool Insert(TEntity model)
        {
            _context.Set<TEntity>().Add(model);
            return _context.SaveChanges()>0;

        }

        public void Delete(int id)
        {
            var entity = Find(id);
            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
                _context.SaveChanges();
            }
        }

        public bool DeleteById(string id)
        {
            var entity = Find(id);
            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
            }
            
            return _context.SaveChanges() > 0;
        }

        public bool Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return _context.SaveChanges() > 0;
        }

        public TEntity Find(params object[] keyValues)
        {
            return _context.Set<TEntity>().Find(keyValues);
        }


        public TEntity FindOne(Expression<Func<TEntity, bool>> predicate = null)
        {
            var set = _context.Set<TEntity>().AsNoTracking();
            return (predicate == null)
                ? set.FirstOrDefault()
                : set.FirstOrDefault(predicate);
        }

        public IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            var set = _context.Set<TEntity>().AsNoTracking();
            return (predicate == null) ? set : set.Where(predicate);
        }

        public int GetMaxId(string table,string keyId)
        {
            return _context.Database.SqlQuery<int>(string.Format("select {0}_{1}.nextval from dual ",table, keyId)).FirstOrDefault();
        }

         void IDisposable.Dispose()
        {
            _context.Dispose();
        }
    }
}
