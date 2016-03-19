/*
 * 描述:定义GeneAllele作为Aggerate的根对外提供访问的接口
 *  
 * 修订历史: 
 * 日期          修改人              内容
 * 20151113      张志明              创建 
 *  
 */

using KMHC.CTMS.DAL.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.Repository.Interface
{
    public interface IGeneAlleleRepository : IBaseRepository<GN_GENEALLELE>
    {
        /// <summary>
        /// 添加等位基因
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        string Add(GN_GENEALLELE model);

        /// <summary>
        /// 编辑等位基因
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        bool Edit(GN_GENEALLELE model);

        /// <summary>
        /// 删除等位基因
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        bool Delete(string ID);

        /// <summary>
        /// 获取等位基因
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        GN_GENEALLELE Get(string ID);

        /// <summary>
        /// 获取等位基因列表
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        List<GN_GENEALLELE> GetList(Expression<Func<GN_GENEALLELE, bool>> predicate = null);
    }
}
