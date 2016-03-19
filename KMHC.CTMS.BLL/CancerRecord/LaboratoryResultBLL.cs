/*
 * 描述:
 *  
 * 修订历史: 
 * 日期           修改人              Email                   内容
 * 2015/12/3      邓柏生                                      创建 
 *  
 */

using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.DAL.CancerRecord;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.CancerRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.BLL.CancerRecord
{
    public class LaboratoryResultBLL
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Add(LaboratoryResult model)
        {
            if (model == null)
                return string.Empty;

            using (LaboratoryResultDAL dal = new LaboratoryResultDAL())
            {
                HR_LABORATORYRESULT entity = ModelToEntity(model);
                entity.LABRESULTID = string.IsNullOrEmpty(model.LabresultId) ? Guid.NewGuid().ToString("N") : model.LabresultId;

                return dal.Add(entity);
            }
        }

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <returns></returns>
        public LaboratoryResult Get(Expression<Func<HR_LABORATORYRESULT, bool>> predicate = null)
        {
            using (LaboratoryResultDAL dal = new LaboratoryResultDAL())
            {
                HR_LABORATORYRESULT entity = dal.Get(predicate);
                return EntityToModel(entity);
            }
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LaboratoryResult> Get()
        {
            using (LaboratoryResultDAL dal = new LaboratoryResultDAL())
            {
                List<HR_LABORATORYRESULT> entitys = dal.Get().ToList();
                List<LaboratoryResult> list = new List<LaboratoryResult>();
                foreach (HR_LABORATORYRESULT item in entitys)
                {
                    list.Add(EntityToModel(item));
                }
                return list;
            }
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="HistoryID">就诊史ID</param>
        /// <returns></returns>
        public IEnumerable<LaboratoryResult> GetList(PageInfo page, string HistoryID)
        {
            using (LaboratoryResultDAL dal = new LaboratoryResultDAL())
            {
                var list = dal.Get();
                if(!string.IsNullOrEmpty(HistoryID))
                {
                    list = list.Where(p => p.HISTORYID == HistoryID);
                }

                return list.Paging(ref page).Select(EntityToModel).ToList();
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(LaboratoryResult model)
        {
            if (model == null) return false;
            using (LaboratoryResultDAL dal = new LaboratoryResultDAL())
            {
                HR_LABORATORYRESULT entitys = ModelToEntity(model);

                return dal.Edit(entitys);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return false;
            using (LaboratoryResultDAL dal = new LaboratoryResultDAL())
            {
                return dal.Delete(id);
            }
        }

        /// <summary>
        /// Model转Entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private HR_LABORATORYRESULT ModelToEntity(LaboratoryResult model)
        {
            if (model != null)
            {
                var entity = new HR_LABORATORYRESULT()
                {
                    LABRESULTID = model.LabresultId,
                    HISTORYID = model.HistoryId,
                    DIAGNOSISTIME = model.DiagnosisTime,
                    DIAGNOSIS = model.Diagnosis,
                    HOSPITAL = model.Hospital,
                    DIAGNOSISREPORT = model.DiagnosisReport,
                    CI = model.CI,
                    MEDICALHISATTACHMENT = model.MedicalhisAttachment,
                    LABMODELID = model.LabmodelId,
                    LABTABELNAME = model.LabtabelName,
                    TESTSTANDARD = model.Teststandard
                };

                return entity;
            }
            return null;
        }

        /// <summary>
        /// Entity转Model
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private LaboratoryResult EntityToModel(HR_LABORATORYRESULT entity)
        {
            if (entity != null)
            {
                var model = new LaboratoryResult()
                {
                    LabresultId = entity.LABRESULTID,
                    HistoryId = entity.HISTORYID,
                    DiagnosisTime = entity.DIAGNOSISTIME,
                    Diagnosis = entity.DIAGNOSIS,
                    Hospital = entity.HOSPITAL,
                    DiagnosisReport = entity.DIAGNOSISREPORT,
                    CI = entity.CI,
                    MedicalhisAttachment = entity.MEDICALHISATTACHMENT,
                    LabmodelId = entity.LABMODELID,
                    LabtabelName = entity.LABTABELNAME,
                    Teststandard = entity.TESTSTANDARD
                };

                return model;
            }
            return null;
        }
    }
}
