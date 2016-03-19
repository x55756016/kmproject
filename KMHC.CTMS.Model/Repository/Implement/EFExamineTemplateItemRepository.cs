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
    public class EFExamineTemplateItemRepository : BaseRepository<CTMS_ADM_EXAMINEITEMS>, IExamineTemplateItemRepository
    {
        public bool AddExamineTemplateItems(Examine.ExamineTemplateItems model)
        {
            CTMS_ADM_EXAMINEITEMS entity = LoadEntityFromModel(model);
            bool result = Insert(entity);
            return result;
        }

        public bool UpdateExamineTemplateItems(Examine.ExamineTemplateItems model)
        {
            CTMS_ADM_EXAMINEITEMS entity = LoadEntityFromModel(model);
            bool result = Update(entity);
            return result;
        }

        public bool DeleteExamineTemplateItemsById(string id)
        {
            var item = FindOne(p => p.ID == id);
            item.ISDELETED = 1;
            return Update(item);
        }

        public bool DeleteExamineTemplateItemByTemplateId(string templateId)
        {
            PageInfo pageInfo = null;
            List<ExamineTemplateItems> list = GetExamineTemplateItemsByTemplateId(templateId, ref pageInfo);
            foreach (ExamineTemplateItems item in list)
            {
                DeleteExamineTemplateItemsById(item.Id);
            }
            return true;
        }

        public List<ExamineTemplateItems> GetExamineTemplateItemsByTemplateId(string templateId,ref PageInfo pageInfo)
        {
            IQueryable<CTMS_ADM_EXAMINEITEMS> list = FindAll(p => p.EXAMINETEMPLATEID == templateId && p.ISDELETED == 0);
            if (pageInfo == null)
                return list.Select(LoadModelFromEntity).OrderBy(p=>p.CreateDateTime).ToList();
            return list.Paging(ref pageInfo).Select(LoadModelFromEntity).OrderBy(p => p.CreateDateTime).ToList();
        }

        public ExamineTemplateItems GetExamineTemplateItemsById(string id)
        {
            CTMS_ADM_EXAMINEITEMS entity = FindOne(p => p.ID == id && p.ISDELETED == 0);
            if (entity == null)
                return null;
            return LoadModelFromEntity(entity);
        }

        protected ExamineTemplateItems LoadModelFromEntity(CTMS_ADM_EXAMINEITEMS entity)
        {
            if (entity == null)
                return null;

            ExamineTemplateItems model = new ExamineTemplateItems();
            model.Id = entity.ID;
            model.ExamineTemplateId = entity.EXAMINETEMPLATEID;
            model.Name = entity.NAME;
            model.Type = (int)entity.TYPE;
            model.CreateUserId = entity.CREATEUSERID;
            model.CreateUserName = entity.CREATEUSERNAME;
            model.CreateDateTime = (DateTime)entity.CREATEDATETIME;
            model.EditUserId = entity.EDITUSERID;
            model.EditUserName = entity.EDITUSERNAME;
            model.EditDateTime = (DateTime)entity.EDITDATETIME;
            model.IsDeleted = (int)entity.ISDELETED;

            return model;
        }

        protected CTMS_ADM_EXAMINEITEMS LoadEntityFromModel(ExamineTemplateItems model)
        {
            if (model == null)
                return null;

            CTMS_ADM_EXAMINEITEMS entity = new CTMS_ADM_EXAMINEITEMS();
            entity.ID = model.Id;
            entity.EXAMINETEMPLATEID = model.ExamineTemplateId;
            entity.NAME = model.Name;
            entity.TYPE = model.Type;
            entity.CREATEUSERID = model.CreateUserId;
            entity.CREATEUSERNAME = model.CreateUserName;
            entity.CREATEDATETIME = model.CreateDateTime;
            entity.EDITUSERID = model.EditUserId;
            entity.EDITUSERNAME = model.EditUserName;
            entity.EDITDATETIME = model.EditDateTime;
            entity.ISDELETED = model.IsDeleted;

            return entity;
        }


        public List<ExamineTemplateItems> GetExamineTemplateItemsByKwd(string parentId,string kwd, ref PageInfo pageInfo)
        {
            IQueryable<CTMS_ADM_EXAMINEITEMS> list = FindAll(p=>p.EXAMINETEMPLATEID == parentId && p.ISDELETED == 0);
            Guid g = new Guid();
            if (Guid.TryParse(kwd, out g))
                return new List<ExamineTemplateItems>() { GetExamineTemplateItemsById(kwd) };
            if (!string.IsNullOrEmpty(kwd))
                list = FindAll(p => (p.EXAMINETEMPLATEID == parentId && p.NAME.Contains(kwd)) && p.ISDELETED == 0);
            if (pageInfo == null)
                return list.Select(LoadModelFromEntity).ToList();
            return list.Paging(ref pageInfo).Select(LoadModelFromEntity).ToList();
        }
    }
}
