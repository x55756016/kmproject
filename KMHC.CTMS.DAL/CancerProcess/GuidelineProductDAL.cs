/*
 * 描述:
 *  
 * 修订历史: 
 * 日期           修改人              Email                   内容
 * 2016/1/22      邓柏生                                      创建 
 *  
 */

using KMHC.CTMS.DAL.Database;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace KMHC.CTMS.DAL.CancerProcess
{
    public class GuidelineProductDAL:BaseDAL<CTMS_GUIDELINEPRODUCT>
    {
        /// <summary>
        /// 新增记录
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public string Add(CTMS_GUIDELINEPRODUCT entity)
        {
            base.Insert(entity);
            return entity.GUIDELINEPRODUCTID;
        }

        /// <summary>
        /// 单条数据
        /// </summary>
        /// <returns></returns>
        public CTMS_GUIDELINEPRODUCT GetOne(Expression<Func<CTMS_GUIDELINEPRODUCT, bool>> predicate = null)
        {
            return base.FindOne(predicate);
        }

        /// <summary>
        /// 列表数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<CTMS_GUIDELINEPRODUCT> Get(Expression<Func<CTMS_GUIDELINEPRODUCT, bool>> predicate = null)
        {
            return base.FindAll(predicate);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Edit(CTMS_GUIDELINEPRODUCT entity)
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
    }
}
