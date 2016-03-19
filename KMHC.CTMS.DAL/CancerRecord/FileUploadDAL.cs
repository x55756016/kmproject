/*
 * 描述:公共文件上传DAL
 *  
 * 修订历史: 
 * 日期       修改人              Email                   内容
 * 2015/12/22 15:42:19     邓柏生                                      创建 
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
    public class FileUploadDAL : BaseDAL<HR_FILEUPLOAD>
    {
        /// <summary>
        /// 单条数据
        /// </summary>
        /// <returns></returns>
        public HR_FILEUPLOAD GetOne(Expression<Func<HR_FILEUPLOAD, bool>> predicate = null)
        {
            return base.FindOne(predicate);
        }

        /// <summary>
        /// 列表数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<HR_FILEUPLOAD> Get(Expression<Func<HR_FILEUPLOAD, bool>> predicate = null)
        {
            return base.FindAll(predicate);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Edit(HR_FILEUPLOAD entity)
        {
            return base.Update(entity);
        }
    }
}
