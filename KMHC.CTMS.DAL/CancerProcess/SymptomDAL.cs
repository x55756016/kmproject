/*
 * 描述:症状体系DAL
 *  
 * 修订历史: 
 * 日期           修改人              Email                   内容
 * 2016/1/5       邓柏生                                      创建 
 *  
 */

using KMHC.CTMS.DAL.Database;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace KMHC.CTMS.DAL.CancerProcess
{
    public class SymptomDAL : BaseDAL<CTMS_SYMPTOM>
    {
        /// <summary>
        /// 新增记录
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public string Add(CTMS_SYMPTOM entity){
            base.Insert(entity);
            return entity.ID;
        }

        /// <summary>
        /// 单条数据
        /// </summary>
        /// <returns></returns>
        public CTMS_SYMPTOM GetOne(Expression<Func<CTMS_SYMPTOM, bool>> predicate = null)
        {
            return base.FindOne(predicate);
        }

        /// <summary>
        /// 列表数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<CTMS_SYMPTOM> Get(Expression<Func<CTMS_SYMPTOM, bool>> predicate = null)
        {
            return base.FindAll(predicate);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Edit(CTMS_SYMPTOM entity)
        {
            return base.Update(entity);
        }
    }
}
