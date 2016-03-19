/*
 * 描述:
 *  
 * 修订历史: 
 * 日期           修改人              Email                   内容
 * 2016/1/23      邓柏生                                      创建 
 *  
 */

using KMHC.CTMS.DAL.Database;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace KMHC.CTMS.DAL.CancerProcess
{
    public class EventProductDAL : BaseDAL<CTMS_EVENTPRODUCT>
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string Add(CTMS_EVENTPRODUCT entity)
        {
            base.Insert(entity);
            return entity.EVENTPRODUCTID;
        }

        /// <summary>
        /// 单条数据
        /// </summary>
        /// <returns></returns>
        public CTMS_EVENTPRODUCT GetOne(Expression<Func<CTMS_EVENTPRODUCT, bool>> predicate = null)
        {
            return base.FindOne(predicate);
        }

        /// <summary>
        /// 列表数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<CTMS_EVENTPRODUCT> Get(Expression<Func<CTMS_EVENTPRODUCT, bool>> predicate = null)
        {
            return base.FindAll(predicate);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Edit(CTMS_EVENTPRODUCT entity)
        {
            return base.Update(entity);
        }
    }
}
