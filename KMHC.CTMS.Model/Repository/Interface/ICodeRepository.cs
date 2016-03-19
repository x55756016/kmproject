/*
 * 描述:数据字典管理使用接口
 *  
 * 修订历史: 
 * 日期       修改人              Email                   内容
 * 20151016   郝元彦              haoyuanyan@gmail.com    创建 
 *  
 */

using KMHC.CTMS.Model.CancerRecord;
using System.Collections.Generic;

namespace KMHC.CTMS.Model.Repository.Interface
{
    public interface ICodeRepository
    {
        /// <summary>
        /// 获取单个字典
        /// </summary>
        /// <param name="codeId"></param>
        /// <returns></returns>
        Code Get(int codeId);

        /// <summary>
        /// 根据字典编号获取单个字典
        /// </summary>
        /// <param name="itemno"></param>
        /// <returns></returns>
        Code Get(string itemno);


        /// <summary>
        /// 根据字典编号数组获取多个字典
        /// </summary>
        /// <param name="itemnos"></param>
        /// <returns></returns>
        IEnumerable<Code> GetList(string[] itemnos);

    }
}
