/*
 * 描述:
 *  
 * 修订历史: 
 * 日期       修改人              Email                   内容
 * 20111111   郝元彦              haoyuanyan@gmail.com    创建 
 *  
 */


using KMHC.CTMS.Model.CancerRecord;

namespace KMHC.CTMS.Model.Repository.Interface
{
    public interface IExamineRecordRepository
    {
        /// <summary>
        /// 新增检查结果
        /// </summary>
        int AddResult(ExamineRecord record, ExamineResult result);


        /// <summary>
        /// 删除检查结果
        /// </summary>
        void DeleteResult(int recordId, int resultId);


    }
}
