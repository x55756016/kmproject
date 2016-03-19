/*
 * 描述:
 *  
 * 修订历史: 
 * 日期       修改人              Email                   内容
 * 2015/12/9 16:02:17     邓柏生                                      创建 
 *  
 */

using KMHC.CTMS.DAL.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.DAL.PrecisionMedicine
{
    public class CnDrugDAL : BaseDAL<DUG_CNDRUG>
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string Add(DUG_CNDRUG entity)
        {
            base.Insert(entity);
            return entity.ID.ToString();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Edit(DUG_CNDRUG entity)
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
        public IQueryable<DUG_CNDRUG> Get()
        {
            return base.FindAll();
        }

        /// <summary>
        /// 单条数据
        /// </summary>
        /// <returns></returns>
        public DUG_CNDRUG Get(Expression<Func<DUG_CNDRUG, bool>> predicate = null)
        {
            return base.FindOne(predicate);
        }
    }
}
