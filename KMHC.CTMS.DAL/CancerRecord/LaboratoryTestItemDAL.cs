/*
 * 描述:
 *  
 * 修订历史: 
 * 日期           修改人              Email                   内容
 * 2015/12/4      邓柏生                                      创建 
 *  
 */

using KMHC.CTMS.DAL.Database;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace KMHC.CTMS.DAL.CancerRecord
{
    public class LaboratoryTestItemDAL : BaseDAL<HR_LABORATORYTESTITEM>
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string Add(HR_LABORATORYTESTITEM entity)
        {
            base.Insert(entity);
            return entity.TESTITEMID;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Edit(HR_LABORATORYTESTITEM entity)
        {
            return base.Update(entity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            return base.DeleteById(id);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public IQueryable<HR_LABORATORYTESTITEM> Get()
        {
            return base.FindAll();
        }

        /// <summary>
        /// 单条数据
        /// </summary>
        /// <returns></returns>
        public HR_LABORATORYTESTITEM Get(Expression<Func<HR_LABORATORYTESTITEM, bool>> predicate = null)
        {
            return base.FindOne(predicate);
        }
    }
}
