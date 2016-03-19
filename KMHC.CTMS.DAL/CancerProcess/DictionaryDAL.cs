/*
 * 描述:
 *  
 * 修订历史: 
 * 日期       修改人              Email                   内容
 * 2016/2/16  邓柏生                                      创建 
 *  
 */

using KMHC.CTMS.DAL.Database;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace KMHC.CTMS.DAL.CancerProcess
{
    public class DictionaryDAL : BaseDAL<HR_DICTIONARY>
    {
        /// <summary>
        /// 新增记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string Add(HR_DICTIONARY entity)
        {
            base.Insert(entity);
            return entity.DICTIONARYID;
        }

        /// <summary>
        /// 单条数据
        /// </summary>
        /// <returns></returns>
        public HR_DICTIONARY GetOne(Expression<Func<HR_DICTIONARY, bool>> predicate = null)
        {
            return base.FindOne(predicate);
        }

        /// <summary>
        /// 列表数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<HR_DICTIONARY> Get(Expression<Func<HR_DICTIONARY, bool>> predicate = null)
        {
            return base.FindAll(predicate);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Edit(HR_DICTIONARY entity)
        {
            return base.Update(entity);
        }
    }
}
