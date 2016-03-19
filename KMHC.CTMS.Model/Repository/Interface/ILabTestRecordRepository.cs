/*
 * 描述:检验记录管理
 *  
 * 修订历史: 
 * 日期       修改人              Email                   内容
 * 20151102   郝元彦              haoyuanyan@gmail.com    创建 
 *  
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.Model.CancerRecord;

namespace KMHC.CTMS.Model.Repository.Interface
{
    public interface ILabTestRecordRepository
    {
        /// <summary>
        /// 新增检验记录
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        int Add(LabTestRecord record);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        bool Edit(LabTestRecord record);

        /// <summary>
        /// 获取检验记录
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        LabTestRecord Get(int recordId);
        /// <summary>
        /// 根据检验序号获取检验记录
        /// </summary>
        /// <param name="recordNo"></param>
        /// <returns></returns>
        LabTestRecord Get(string recordNo);

    }
}
