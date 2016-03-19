/*
 * 描述:
 *  
 * 修订历史: 
 * 日期       修改人              Email                   内容
 * 2015/12/3 15:39:47     邓柏生                                      创建 
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
    public class LaboratoryResultDAL : BaseDAL<HR_LABORATORYRESULT>
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string Add(HR_LABORATORYRESULT entity)
        {
            base.Insert(entity);
            return entity.LABRESULTID;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Edit(HR_LABORATORYRESULT entity)
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
        public IQueryable<HR_LABORATORYRESULT> Get()
        {
            return base.FindAll();
        }

        /// <summary>
        /// 单条数据
        /// </summary>
        /// <returns></returns>
        public HR_LABORATORYRESULT Get(Expression<Func<HR_LABORATORYRESULT, bool>> predicate = null)
        {
            return base.FindOne(predicate);
        }
    }
}
