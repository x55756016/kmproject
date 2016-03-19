using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.Examine;
using KMHC.CTMS.Model.Repository.Interface;

namespace KMHC.CTMS.Model.Repository.Implement
{
    public class EFExamineTemplateRepository : BaseRepository<CTMS_ADM_EXAMINETEMPLATES>, IExamineTemplateRepository
    {

        protected ExamineTemplates LoadModelFromEntity(CTMS_ADM_EXAMINETEMPLATES entity)
        {
            if (entity == null)
                return null;

            ExamineTemplates model = new ExamineTemplates();
            model.Id = entity.ID;
            model.Name = entity.NAME;
            model.Description = entity.DESCRIPTION;
            model.Type = (int)entity.TYPE;
            model.TemplateCode = entity.TEMPLATECODE;
            model.CreateUserId = entity.CREATEUSERID;
            model.CreateUserName = entity.CREATEUSERNAME;
            model.CreateDateTime = (DateTime)entity.CREATEDATETIME;
            model.EditUserId = entity.EDITUSERID;
            model.EditUserName = entity.EDITUSERNAME;
            model.EditDateTime = (DateTime)entity.EDITDATETIME;
            model.IsDeleted = (int)entity.ISDELETED;

            return model;
        }

        protected CTMS_ADM_EXAMINETEMPLATES LoadEntityFromModel(ExamineTemplates model)
        {
            if (model == null)
                return null;

            CTMS_ADM_EXAMINETEMPLATES entity = new CTMS_ADM_EXAMINETEMPLATES();
            entity.ID = model.Id;
            entity.NAME = model.Name;
            entity.DESCRIPTION = model.Description;
            entity.TYPE = model.Type;
            entity.TEMPLATECODE = model.TemplateCode;
            entity.CREATEUSERID = model.CreateUserId;
            entity.CREATEUSERNAME = model.CreateUserName;
            entity.CREATEDATETIME = model.CreateDateTime;
            entity.EDITUSERID = model.EditUserId;
            entity.EDITUSERNAME = model.EditUserName;
            entity.EDITDATETIME = model.EditDateTime;
            entity.ISDELETED = model.IsDeleted;

            return entity;
        }

        public List<ExamineTemplates> GetExamineTemplatesByName(int type,string name,ref PageInfo pageInfo)
        {
            IQueryable<CTMS_ADM_EXAMINETEMPLATES> list = FindAll(p => p.NAME.Contains(name) && p.TYPE == type);
            if(pageInfo == null)
                return list.Select(LoadModelFromEntity).ToList();
            return list.Paging(ref pageInfo).Select(LoadModelFromEntity).ToList();
        }

        public List<ExamineTemplates> GetExamineTemplatesByDescription(int type,string description, ref PageInfo pageInfo)
        {
            IQueryable<CTMS_ADM_EXAMINETEMPLATES> list = FindAll(p => p.DESCRIPTION.Contains(description) && p.ISDELETED == 0 && p.TYPE == type);
            if (pageInfo == null)
                return list.Select(LoadModelFromEntity).ToList();
            return list.Paging(ref pageInfo).Select(LoadModelFromEntity).ToList();
        }

        public ExamineTemplates GetExamineTemplateById(string id)
        {
            CTMS_ADM_EXAMINETEMPLATES entity = FindOne(p => p.ID == id && p.ISDELETED == 0);
            if (entity == null)
                return null;
            return LoadModelFromEntity(entity);
        }

        public List<ExamineTemplates> GetExamineTemplates(int type,ref PageInfo pageInfo)
        {
            IQueryable<CTMS_ADM_EXAMINETEMPLATES> list = FindAll(p=>p.ISDELETED == 0 && p.TYPE == type);
            if (pageInfo == null)
                return list.Select(LoadModelFromEntity).ToList();
            return list.Paging(ref pageInfo).Select(LoadModelFromEntity).ToList();
        }

        public List<ExamineTemplates> GetExamineTemplatesByKwd(string kwd, ref PageInfo pageInfo)
        {
            IQueryable<CTMS_ADM_EXAMINETEMPLATES> list = FindAll(p=>p.ISDELETED == 0);
            Guid g = new Guid();
            if (Guid.TryParse(kwd, out g))
                return new List<ExamineTemplates>(){GetExamineTemplateById(kwd)};
            if(!string.IsNullOrEmpty(kwd))
                list = FindAll(p => (p.NAME.Contains(kwd) || p.DESCRIPTION.Contains(kwd)) && p.ISDELETED == 0);
            if (pageInfo == null)
                return list.Select(LoadModelFromEntity).ToList();
            return list.Paging(ref pageInfo).Select(LoadModelFromEntity).ToList();
        }


        public bool AddExamineTemplates(ExamineTemplates model)
        {
            CTMS_ADM_EXAMINETEMPLATES cae = LoadEntityFromModel(model);
            bool result = Insert(cae);
            return result;
        }

        public bool UpdateExamineTemplates(ExamineTemplates model)
        {
            CTMS_ADM_EXAMINETEMPLATES cae = LoadEntityFromModel(model);
            bool result = Update(cae);
            return result;
        }


        public bool DeleteExamineTemplates(string id)
        {
            CTMS_ADM_EXAMINETEMPLATES entity = FindOne(p => p.ID == id);
            if (entity == null)
                return true;
            entity.ISDELETED = 1;
            return Update(entity);
        }


        public List<ExamineTemplates> GetExamineTemplatesByCode(string code)
        {
            return FindAll(p => p.TEMPLATECODE == code).Select(LoadModelFromEntity).ToList();
        }


        public List<ExamineTemplates> GetExamineTemplateByType(int type)
        {
            return FindAll(p => p.TYPE == type && p.ISDELETED == 0).Select(LoadModelFromEntity).Distinct(new ExamineTemplateCompare()).ToList();
        }
    }

    public class ExamineTemplateCompare : IEqualityComparer<ExamineTemplates>
    {
        public bool Equals(ExamineTemplates x, ExamineTemplates y)
        {
            return x.TemplateCode == y.TemplateCode;
        }

        public int GetHashCode(ExamineTemplates obj)
        {
            return obj.GetHashCode();
        }
    }
}
