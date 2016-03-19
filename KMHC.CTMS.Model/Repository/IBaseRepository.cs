/*
 * 描述:
 *  
 * 修订历史: 
 * 日期       修改人              Email                   内容
 * 20151016   郝元彦              haoyuanyan@gmail.com    创建 
 *  
 */

using System;
using System.Linq;
using System.Linq.Expressions;

namespace KMHC.CTMS.Model.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : class 
    {

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        bool Insert(TEntity entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);


        /// <summary>
        /// 删除(临时用，后期删除)
        /// </summary>
        /// <param name="id"></param>
        bool DeleteById(string id);


        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        bool Update(TEntity entity);

        /// <summary>
        /// 根据ID获取指定项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Find(params object[] keyValues);

        /// <summary>
        /// 按条件查询数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity FindOne(Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// 返回数据集
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate = null);


        int GetMaxId(string table, string keyId);


    }
}
