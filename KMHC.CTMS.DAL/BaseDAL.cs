/*
 * 描述:数据操作层的基类
 *  
 * 修订历史: 
 * 日期           修改人                内容
 * 2015/12/3      邓柏生                创建 
 * 2016/02/17     林德力      将context由直接生成更改成由DbSessionFactory生成
 */

using KMHC.CTMS.DAL.Database;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace KMHC.CTMS.DAL
{
    /// <summary>
    /// 数据层基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class BaseDAL<TEntity> : IDisposable where TEntity : class
    {
        private readonly DbContext _context=  DbSessionFactory.GetCurrentDbContext();

        public BaseDAL()
        {
            //_context = new CRDatabase();
        }

        //public BaseDAL(DbContext context)
        //{
        //    _context = context;
        //}

        public bool Insert(TEntity model)
        {
            _context.Set<TEntity>().Add(model);
            return _context.SaveChanges() > 0;

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

        public int GetMaxId(string table, string keyId)
        {
            return _context.Database.SqlQuery<int>(string.Format("select {0}_{1}.nextval from dual ", table, keyId)).FirstOrDefault();
        }


        /// <summary>
        /// 实现对数据的分页查询
        /// </summary>
        /// <typeparam name="S">按照某个类进行排序</typeparam>
        /// <param name="pageIndex">当前第几页</param>
        /// <param name="pageSize">一页显示多少条数据</param>
        /// <param name="total">总条数</param>
        /// <param name="whereLambda">取得排序的条件</param>
        /// <param name="isAsc">如何排序，根据倒叙还是升序</param>
        /// <param name="orderByLambda">根据那个字段进行排序</param>
        /// <returns>返回一个实体类型的IQueryable集合</returns>
        public IQueryable<TEntity> LoadPageEntities<S>(int pageIndex, int pageSize, out int total,
            Expression<Func<TEntity, bool>> whereLambda,
            bool isAsc, Expression<Func<TEntity, S>> orderByLambda)
        {
            var temp = _context.Set<TEntity>().Where<TEntity>(whereLambda);
            total = temp.Count(); //得到总的条数
            //排序,获取当前页的数据
            if (isAsc)
            {
                temp = temp.OrderBy<TEntity, S>(orderByLambda)
                    .Skip<TEntity>(pageSize*(pageIndex - 1)) //越过多少条
                    .Take<TEntity>(pageSize).AsQueryable(); //取出多少条
            }
            else
            {
                temp = temp.OrderByDescending<TEntity, S>(orderByLambda)
                    .Skip<TEntity>(pageSize*(pageIndex - 1)) //越过多少条
                    .Take<TEntity>(pageSize).AsQueryable(); //取出多少条
            }
            return temp.AsQueryable();
        }



        void IDisposable.Dispose()
        {
            //_context.Dispose();
        }
      
    }
}
