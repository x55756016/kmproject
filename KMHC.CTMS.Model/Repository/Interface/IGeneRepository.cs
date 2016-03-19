/*
 * 描述:定义Gene作为Aggerate的根对外提供访问的接口
 *  
 * 修订历史: 
 * 日期          修改人              内容
 * 20151113      张志明              创建 
 *  
 */

using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.DAL.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.Repository.Interface
{
    public interface IGeneRepository : IBaseRepository<GN_GENE>
    {
        /// <summary>
        /// 添加基因字典
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        string Add(GN_GENE entity);

        /// <summary>
        /// 编辑基因字典
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        bool Edit(GN_GENE entity);

        /// <summary>
        /// 删除基因字典
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        bool Delete(string ID);


        /// <summary>
        /// 获取等位基因
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        GN_GENE Get(string ID);


    }
}
