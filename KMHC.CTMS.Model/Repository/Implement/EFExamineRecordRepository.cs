/*
 * 描述:IExamineRecordRepository的EntityFramework实现
 *  
 * 修订历史: 
 * 日期       修改人              Email                   内容
 * 20151016   郝元彦              haoyuanyan@gmail.com    创建 
 *  
 */

using System;
using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.Model.Repository.Interface;
using KMHC.CTMS.DAL.Database;

namespace KMHC.CTMS.Model.Repository.Implement
{
    public class EFExamineRecordRepository : IExamineRecordRepository
    {
        private readonly IBaseRepository<HR_EXAMINERESULT> repository;

        public EFExamineRecordRepository()
        {
            repository = new BaseRepository<HR_EXAMINERESULT>(new CRDatabase());
        }

        public int AddResult(ExamineRecord record, ExamineResult result)
        {
            var maxId = repository.GetMaxId("HR_EXAMINERESULT", "RESULTID");
            var entity = ModelToEntity(result);
            entity.EXAMID = maxId;
            entity.CREATEDATE = DateTime.Now;
            //entity.CreatorUserId
            repository.Insert(entity);
            return maxId;
        }

        public void DeleteResult(int recordId, int resultId)
        {
            repository.Delete(resultId);
        }

        #region 实体模型映射


        private HR_EXAMINERESULT ModelToEntity(ExamineResult model)
        {
            if (model != null)
            {
                var entity = new HR_EXAMINERESULT()
                {
                     RESULTID = model.ResultId,
                     EXAMID = model.ExamId,
                     //ITEMCODE = model.ItemCode,
                     RESULT = model.Result
                };
                return entity;
            }
            return null;

        }

        private ExamineResult EntityToModel(HR_EXAMINERESULT entity)
        {
            if (entity != null)
            {
                var model = new ExamineResult()
                {
                     ResultId = entity.RESULTID,
                     ExamId=entity.EXAMID??0,
                     //ItemCode = entity.ITEMCODE,
                     Result = entity.RESULT,
                     //Remark = ""
                };
                return model;
            }
            return null;
        }

        #endregion
    }
}
