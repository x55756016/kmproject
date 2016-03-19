/*
 * 描述:定义FD_Relation作为Aggerate的根对外提供访问的接口
 *  
 * 修订历史: 
 * 日期          修改人              Email              内容
 * 20151102      张志明              			创建 
 *  
 */

using KMHC.CTMS.Model.CancerRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.Repository.Interface
{
    public interface IFD_RelationRepository
    {
        /// <summary>
        /// 新增家族史关系
        /// </summary>
        /// <param name="relation"></param>
        /// <returns></returns>
        string Add(FD_Relation relation);

        /// <summary>
        /// 修改家族史关系
        /// </summary>
        /// <param name="relation"></param>
        /// <returns></returns>
        bool Edit(FD_Relation relation);

        /// <summary>
        /// 根据ID删除家族关系
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(string id);

        /// <summary>
        /// 获取家族关系
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        List<FD_Relation> GetAll();
        
    }
}
