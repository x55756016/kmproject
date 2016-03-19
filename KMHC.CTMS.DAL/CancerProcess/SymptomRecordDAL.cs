/*
 * 描述:
 *  
 * 修订历史: 
 * 日期           修改人              Email                   内容
 * 2016/1/20      邓柏生                                      创建 
 *  
 */

using KMHC.CTMS.DAL.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.DAL.CancerProcess
{
    public class SymptomRecordDAL : BaseDAL<CTMS_SYMPTOMRECORDS>
    {
        /// <summary>
        /// 新增记录
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public string Add(CTMS_SYMPTOMRECORDS entity)
        {
            base.Insert(entity);
            return entity.ID;
        }

        /// <summary>
        /// 单条数据
        /// </summary>
        /// <returns></returns>
        public CTMS_SYMPTOMRECORDS GetOne(Expression<Func<CTMS_SYMPTOMRECORDS, bool>> predicate = null)
        {
            return base.FindOne(predicate);
        }

        /// <summary>
        /// 列表数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<CTMS_SYMPTOMRECORDS> Get(Expression<Func<CTMS_SYMPTOMRECORDS, bool>> predicate = null)
        {
            return base.FindAll(predicate);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Edit(CTMS_SYMPTOMRECORDS entity)
        {
            return base.Update(entity);
        }
    }
}
