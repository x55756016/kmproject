using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.Examine;
using KMHC.CTMS.Model.Repository.Interface;

namespace KMHC.CTMS.Model.Repository.Implement
{
    public class EFImageExamineReportRepository : BaseRepository<CTMS_IMAGEEXAMINEREPORT>, IImageExamineReportRepository
    {
        public bool AddImageExamineReport(ImageExamineReport model)
        {
            return Insert(LoadEntityFromModel(model));
        }

        public bool UpdateImageExamineReport(ImageExamineReport model)
        {
            return Update(LoadEntityFromModel(model));
        }

        public bool DeleteImageExamineReport(string id)
        {
            CTMS_IMAGEEXAMINEREPORT entity = FindOne(p => p.ID == id);
            if (entity == null)
                return true;
            entity.ISDELETED = 1;
            return Update(entity);
        }

        public ImageExamineReport GetImageExamineReportById(string id)
        {
            return LoadModelFromEntity(FindOne(p => p.ID == id && p.ISDELETED == 0));
        }

        public List<ImageExamineReport> GetImageExamineReportByUserId(string userId)
        {
            return FindAll(p => p.USERID == userId && p.ISDELETED == 0).Select(LoadModelFromEntity).ToList();
        }

        #region 模型转换
        protected ImageExamineReport LoadModelFromEntity(CTMS_IMAGEEXAMINEREPORT entity)
        {
            if (entity == null)
                return null;

            ImageExamineReport model = new ImageExamineReport();
            model.Id = entity.ID;
            model.TemplateType = entity.TEMPLATETYPE;
            model.CheckDate = entity.CHECKDATE;
            model.Dept = entity.DEPT;
            model.Part = entity.PART;
            model.Diagnose = entity.DIAGNOSE;
            model.See = entity.SEE;
            model.CreateDateTime = entity.CREATEDATETIME;
            model.CreateUserId = entity.CREATEUSERID;
            model.CreateUserName = entity.CREATEUSERNAME;
            model.EditDateTime = entity.EDITDATETIME;
            model.EditUserId = entity.EDITUSERID;
            model.EditUserName = entity.EDITUSERNAME;
            model.IsDeleted = (int)entity.ISDELETED;
            model.ReportDoctor = entity.REPORTDOCTOR;
            model.CheckDoctor = entity.CHECKDOCTOR;
            model.ReportDetail = Encoding.UTF8.GetString(entity.REPORTDETAIL);
            model.Remark = entity.REMARK;
            model.UserId = entity.USERID;

            return model;
        }
        protected CTMS_IMAGEEXAMINEREPORT LoadEntityFromModel(ImageExamineReport model)
        {
            if (model == null)
                return null;

            CTMS_IMAGEEXAMINEREPORT entity = new CTMS_IMAGEEXAMINEREPORT();
            entity.ID = model.Id;
            entity.TEMPLATETYPE = model.TemplateType;
            entity.CHECKDATE = model.CheckDate;
            entity.DEPT = model.Dept;
            entity.PART = model.Part;
            entity.DIAGNOSE = model.Diagnose;
            entity.SEE = model.See;
            entity.CREATEDATETIME = model.CreateDateTime;
            entity.CREATEUSERID = model.CreateUserId;
            entity.CREATEUSERNAME = model.CreateUserName;
            entity.EDITDATETIME = model.EditDateTime;
            entity.EDITUSERID = model.EditUserId;
            entity.EDITUSERNAME = model.EditUserName;
            entity.ISDELETED = model.IsDeleted;
            entity.REPORTDOCTOR = model.ReportDoctor;
            entity.CHECKDOCTOR = model.CheckDoctor;
            entity.REPORTDETAIL = Encoding.UTF8.GetBytes( model.ReportDetail);
            entity.REMARK = model.Remark;
            entity.USERID = model.UserId;

            return entity;
        }
        #endregion
    }
}
