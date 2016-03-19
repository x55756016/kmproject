/*
 * 描述:用户角色DAL
 *  
 * 修订历史: 
 * 日期           修改人              Email                   内容
 * 2015/12/28     邓柏生                                      创建 
 *  
 */

using KMHC.CTMS.DAL.Database;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace KMHC.CTMS.DAL.Authorization
{
    public class RoleDAL : BaseDAL<CTMS_SYS_ROLE>
    {
        /// <summary>
        /// 单条数据
        /// </summary>
        /// <returns></returns>
        public CTMS_SYS_ROLE GetOne(Expression<Func<CTMS_SYS_ROLE, bool>> predicate = null)
        {
            return base.FindOne(predicate);
        }

        /// <summary>
        /// 列表数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<CTMS_SYS_ROLE> Get(Expression<Func<CTMS_SYS_ROLE, bool>> predicate = null)
        {
            return base.FindAll(predicate);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Edit(CTMS_SYS_ROLE entity)
        {
            return base.Update(entity);
        }
    }
}
