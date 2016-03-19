/*
 * 描述:
 *  
 * 修订历史: 
 * 日期       修改人              Email                   内容
 * 2015/12/24 14:45:32     邓柏生                                      创建 
 *  
 */

using KMHC.CTMS.DAL.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.DAL.CancerRecord
{
    public class SeeDoctorHistoryDAL : BaseDAL<HR_SEEDOCTORHISTORY>
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string Add(HR_SEEDOCTORHISTORY entity)
        {
            base.Insert(entity);
            return entity.HISTORYID;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Edit(HR_SEEDOCTORHISTORY entity)
        {
          return base.Update(entity);
        }

        /// <summary>
        /// 单条数据
        /// </summary>
        /// <returns></returns>
        public HR_SEEDOCTORHISTORY GetOne(Expression<Func<HR_SEEDOCTORHISTORY, bool>> predicate = null)
        {
            return base.FindOne(predicate);
        }

        /// <summary>
        /// 列表数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<HR_SEEDOCTORHISTORY> Get(Expression<Func<HR_SEEDOCTORHISTORY, bool>> predicate = null)
        {
            return base.FindAll(predicate);
        }
    }
}
