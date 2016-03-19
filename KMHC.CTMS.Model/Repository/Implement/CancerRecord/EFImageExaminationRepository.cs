/*
 * 描述:IImageExaminationRepository的EntityFramework实现
 *  
 * 修订历史: 
 * 日期       修改人              Email                   内容
 * 2015/11/6  邓柏生                                      创建 
 *  
 */

using KMHC.CTMS.Model.Repository.Interface;
using KMHC.CTMS.DAL.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.Model.CancerRecord;

namespace KMHC.CTMS.Model.Repository.Implement.CancerRecord
{
    public class EFImageExaminationRepository : IImageExaminationRepository
    {
        private readonly IBaseRepository<HR_IMAGEEXAMINATION> repository;

        public EFImageExaminationRepository()
        {
            repository = new BaseRepository<HR_IMAGEEXAMINATION>(new CRDatabase());
        }

        public string Add(ImageExamination imageExam)
        {
            var entity = ModelToEntity(imageExam);
            entity.IMAGEEXAMID = imageExam.ImageExamID == null ? System.Guid.NewGuid().ToString() : imageExam.ImageExamID;
            repository.Insert(entity);
            return entity.IMAGEEXAMID;
        }

        public ImageExamination Get(string historyID)
        {
            return EntityToModel(repository.FindOne(p => p.HISTORYID == historyID));
        }

        public bool Edit(ImageExamination imageExam)
        {
            var entity = ModelToEntity(imageExam);
            return entity != null && repository.Update(entity);
        }

        private HR_IMAGEEXAMINATION ModelToEntity(ImageExamination model)
        {
            if (model != null)
            {
                var entity = new HR_IMAGEEXAMINATION()
                {
                    IMAGEEXAMID = model.ImageExamID,
                    HISTORYID = model.HistoryID,
                    CHECKTYPE = model.CheckType,
                    CHECKTIME = model.CheckTime,
                    DEPARTMENT = model.Department,
                    CHECKPOSITION = model.CheckPosition,
                    REPORTCONTENT = model.ReportContent,
                    REPORTTIME = model.ReportTime,
                    REPORTDOCTOR = model.ReportDoctor,
                    AUDITDOCTOR = model.AuditDoctor,
                    REMARK = model.ImageUrl,
                };

                return entity;
            }
            return null;
        }

        private ImageExamination EntityToModel(HR_IMAGEEXAMINATION entity)
        {
            if (entity != null)
            {
                var model = new ImageExamination()
                {
                    ImageExamID = entity.IMAGEEXAMID,
                    HistoryID = entity.HISTORYID,
                    CheckType = entity.CHECKTYPE,
                    CheckTime = entity.CHECKTIME,
                    Department = entity.DEPARTMENT,
                    CheckPosition = entity.CHECKPOSITION,
                    ReportContent = entity.REPORTCONTENT,
                    ReportTime = entity.REPORTTIME,
                    ReportDoctor = entity.REPORTDOCTOR,
                    AuditDoctor = entity.AUDITDOCTOR,
                    ImageUrl = entity.REMARK,
                };

                return model;
            }
            return null;
        }
    }
}
