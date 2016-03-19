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
    public class EFExamineTemplateItemOptionsRepository : BaseRepository<CTMS_ADM_EXAMINEITEMOPTIONS>, IExamineTemplateItemOptionsRepository
    {
        public bool AddExamineTemplateItemOptions(ExamineTemplateItemOptions model)
        {
            CTMS_ADM_EXAMINEITEMOPTIONS entity = LoadEntityFromModel(model);
            bool result = Insert(entity);
            return result;
        }

        public bool UpdateExamineTemplateItemOptions(ExamineTemplateItemOptions model)
        {
            CTMS_ADM_EXAMINEITEMOPTIONS entity = LoadEntityFromModel(model);
            bool result = Update(entity);
            return result;
        }

        public bool DeleteExamineTemplateItemOptionsById(string id)
        {
            var item = FindOne(p => p.ID == id);
            item.ISDELETED = 1;
            return Update(item);
        }

        public bool DeleteExamineTemplateItemByTemplateItemId(string templateItemId)
        {
            PageInfo pageInfo = null;
            List<ExamineTemplateItemOptions> list = GetExamineTemplateItemOptionsByTemplateItemId(templateItemId, ref pageInfo);
            list.ForEach(p => DeleteExamineTemplateItemOptionsById(p.Id));
            return true;
        }

        public List<ExamineTemplateItemOptions> GetExamineTemplateItemOptionsByTemplateItemId(string templateItemId, ref PageInfo pageInfo)
        {
            IQueryable<CTMS_ADM_EXAMINEITEMOPTIONS> list = FindAll(p => p.EXAMINEITEMID == templateItemId && p.ISDELETED == 0).OrderBy(p=>p.CREATEDATETIME);
            if (pageInfo == null)
                return list.Select(LoadModelFromEntity).ToList();
            return list.Paging(ref pageInfo).Select(LoadModelFromEntity).ToList();
        }

        public ExamineTemplateItemOptions GetExamineTemplateItemOptionsById(string id)
        {
            CTMS_ADM_EXAMINEITEMOPTIONS entity = FindOne(p => p.ID == id && p.ISDELETED == 0);
            if (entity == null)
                return null;
            return LoadModelFromEntity(entity);
        }

        public List<ExamineTemplateItemOptions> GetExamineTemplateItemOptionsByKwd(string templateItemId, string kwd, ref PageInfo pageInfo)
        {
            IQueryable<CTMS_ADM_EXAMINEITEMOPTIONS> list = FindAll(p => p.EXAMINEITEMID == templateItemId && p.ISDELETED == 0).OrderByDescending(p=>p.CREATEDATETIME);
            Guid g = new Guid();
            if (Guid.TryParse(kwd, out g))
                return new List<ExamineTemplateItemOptions>() { GetExamineTemplateItemOptionsById(kwd) };
            if (!string.IsNullOrEmpty(kwd))
                list = FindAll(p => (p.EXAMINEITEMID == templateItemId && p.DESCRIPTION.Contains(kwd)) && p.ISDELETED == 0);
            if (pageInfo == null)
                return list.Select(LoadModelFromEntity).ToList();
            return list.Paging(ref pageInfo).Select(LoadModelFromEntity).ToList();
        }

        protected ExamineTemplateItemOptions LoadModelFromEntity(CTMS_ADM_EXAMINEITEMOPTIONS entity)
        {
            if (entity == null)
                return null;

            ExamineTemplateItemOptions model = new ExamineTemplateItemOptions();
            model.Id = entity.ID;
            model.ExamineItemId = entity.EXAMINEITEMID;
            model.Description = entity.DESCRIPTION;
            model.CreateUserId = entity.CREATEUSERID;
            model.CreateUserName = entity.CREATEUSERNAME;
            model.CreateDateTime = (DateTime)entity.CREATEDATETIME;
            model.EditUserId = entity.EDITUSERID;
            model.EditUserName = entity.EDITUSERNAME;
            model.EditDateTime = (DateTime)entity.EDITDATETIME;
            model.IsDeleted = (int)entity.ISDELETED;

            return model;
        }

        protected CTMS_ADM_EXAMINEITEMOPTIONS LoadEntityFromModel(ExamineTemplateItemOptions model)
        {
            if (model == null)
                return null;

            CTMS_ADM_EXAMINEITEMOPTIONS entity = new CTMS_ADM_EXAMINEITEMOPTIONS();
            entity.ID = model.Id;
            entity.EXAMINEITEMID = model.ExamineItemId;
            entity.DESCRIPTION = model.Description;
            entity.CREATEUSERID = model.CreateUserId;
            entity.CREATEUSERNAME = model.CreateUserName;
            entity.CREATEDATETIME = model.CreateDateTime;
            entity.EDITUSERID = model.EditUserId;
            entity.EDITUSERNAME = model.EditUserName;
            entity.EDITDATETIME = model.EditDateTime;
            entity.ISDELETED = model.IsDeleted;

            return entity;
        }
    }
}
