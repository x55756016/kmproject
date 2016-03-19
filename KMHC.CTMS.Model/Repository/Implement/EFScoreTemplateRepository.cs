using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.Model.Repository.Interface;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.Repository.Implement
{
    public class EFScoreTemplateRepository:IScoreTemplateRepository
    {
        private readonly IBaseRepository<HR_SCORETEMPLATE> repository;
        public EFScoreTemplateRepository()
        {
            repository = new BaseRepository<HR_SCORETEMPLATE>(new CRDatabase());
        }

        public string Add(ScoreTemplate score)
        {

            var entity = ModelToEntity(score);
            entity.TEMPLATEID = System.Guid.NewGuid().ToString();
            entity.CREATETIME = DateTime.Now;
            //entity.CreatorUserId
            repository.Insert(entity);
            return entity.TEMPLATEID;
        }

        public bool Edit(ScoreTemplate score)
        {
            var entity = ModelToEntity(score);
            return entity != null && repository.Update(entity);
        }

        public ScoreTemplate Get(string Id)
        {
            return EntityToModel(repository.FindOne(o => o.TEMPLATEID == Id));
        }

        public List<ScoreTemplate> GetAll()
        {
            var templateList = repository.FindAll();
            if (templateList != null && templateList.Count() > 0)
            {
                List<ScoreTemplate> list = new List<ScoreTemplate>();
                foreach (HR_SCORETEMPLATE entity in templateList)
                {
                    list.Add(EntityToModel(entity));
                }
                return list;
            }
            return null;
        }

        private HR_SCORETEMPLATE ModelToEntity(ScoreTemplate model)
        {
            if (model != null)
            {
                var entity = new HR_SCORETEMPLATE()
                {
                    TEMPLATEID = model.TemplateID,
                    ANSWERSJSON = model.AnswersJson,
                    CREATEBY = model.CreateBy,
                    CREATETIME = model.CreateTime,
                    ISSHOWRESULT = model.IsShowResult?1:0,
                    MODIFYBY = model.ModifyBy,
                    MODIFYTIME = model.ModifyTime,
                    QUESTIONSJSON = model.QuestionsJson,
                    SHOWGRADE = model.ShowGrade?1:0,
                    TEMPLATEDESCRIPTION = model.TemplateDescription,
                    TEMPLATELABEL = model.TemplateLabel,
                    TEMPLATETITLE = model.TemplateTitle
                };
                if (model.Questions != null && model.Questions.Count > 0)
                {
                    entity.QUESTIONSJSON = JsonHelper.JsonSerialize(model.Questions);
                }
                if (model.Grades != null && model.Grades.Count > 0)
                {
                    entity.ANSWERSJSON = JsonHelper.JsonSerialize(model.Grades);
                }
                return entity;
            }
            return null;
        }

        private ScoreTemplate EntityToModel(HR_SCORETEMPLATE entity)
        {
            if (entity != null)
            {
                var model = new ScoreTemplate()
                {
                    TemplateID=entity.TEMPLATEID,
                    AnswersJson = entity.ANSWERSJSON,
                    CreateBy = entity.CREATEBY,
                    CreateTime = entity.CREATETIME,
                    IsShowResult = (entity.ISSHOWRESULT.HasValue && entity.ISSHOWRESULT.Value==1),
                    ModifyBy = entity.MODIFYBY,
                    ModifyTime = entity.MODIFYTIME,
                    QuestionsJson = entity.QUESTIONSJSON,
                    ShowGrade = (entity.SHOWGRADE.HasValue && entity.SHOWGRADE.Value == 1),
                    TemplateDescription = entity.TEMPLATEDESCRIPTION,
                    TemplateLabel = entity.TEMPLATELABEL,
                    TemplateTitle = entity.TEMPLATETITLE
                   
                };
                if (!string.IsNullOrEmpty(entity.ANSWERSJSON))
                {
                    model.Questions = JsonHelper.JsonDeserialize<List<ScoreTemplateQuestion>>(entity.ANSWERSJSON);
                }
                if (!string.IsNullOrEmpty(entity.ANSWERSJSON))
                {
                    model.Grades = JsonHelper.JsonDeserialize<List<ScoreTemplateGrade>>(entity.ANSWERSJSON);
                }
                return model;
            }
            return null;
        }


       
    }
}
