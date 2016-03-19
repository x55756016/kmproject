/*
 * 描述:待办事项具体实现DAL
 *  
 * 修订历史: 
 * 日期            修改人              Email                   内容
 * 2015/12/31      邓柏生                                      创建 
 *  
 */

using KMHC.CTMS.DAL.Database;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace KMHC.CTMS.DAL.CancerProcess
{
    public class UserEventDAL:BaseDAL<CTMS_USEREVENT>
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string Add(CTMS_USEREVENT entity)
        {
            base.Insert(entity);
            return entity.EVENTID;
        }

        /// <summary>
        /// 单条数据
        /// </summary>
        /// <returns></returns>
        public CTMS_USEREVENT GetOne(Expression<Func<CTMS_USEREVENT, bool>> predicate = null)
        {
            return base.FindOne(predicate);
        }

        /// <summary>
        /// 列表数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<CTMS_USEREVENT> Get(Expression<Func<CTMS_USEREVENT, bool>> predicate = null)
        {
            return base.FindAll(predicate);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Edit(CTMS_USEREVENT entity)
        {
            return base.Update(entity);
        }
    }
}
