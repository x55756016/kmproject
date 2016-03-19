using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.Repository.Implement
{
    public class EFScoreRepository
    {
         private readonly IBaseRepository<HR_SCORE> repository;
        public EFScoreRepository()
        {
            repository = new BaseRepository<HR_SCORE>(new CRDatabase());
        }

         /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public string Add(Score score)
        {
            var entity = ModelToEntity(score);
            entity.SCOREID = System.Guid.NewGuid().ToString();
            entity.CREATETIME = DateTime.Now;
            //entity.CreatorUserId
            repository.Insert(entity);
            return entity.SCOREID;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        public bool Edit(Score score)
        {
            var entity = ModelToEntity(score);
            return entity != null && repository.Update(entity);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public Score Get(string Id)
        {
            return EntityToModel(repository.FindOne(o => o.SCOREID == Id));
        }


        private HR_SCORE ModelToEntity(Score model)
        {
            if (model != null && model.template!=null)
            {
                var entity = new HR_SCORE()
                {
                   ANSWERSJSON=model.template.AnswersJson,
                   ISSHOWRESULT=model.template.IsShowResult?1:0,
                   QUESTIONSJSON=model.template.QuestionsJson,
                   SHOWGRADE=model.template.ShowGrade?1:0,
                   TEMPLATEDESCRIPTION = model.template.TemplateDescription,
                   TEMPLATETITLE=model.template.TemplateTitle,
                   TEMPLATELABEL=model.template.TemplateLabel,
                   TEMPLATEID=model.template.TemplateID,        
                };
                entity.TOTALSCORE = model.template.Questions.Sum(o => o.SelectedValue);
                for (int i = 0; i < model.template.Grades.Count;i++ )
                {
                    if (model.TotalScore >= model.template.Grades[i].minValue && model.TotalScore <= model.template.Grades[i].maxValue)
                    {
                        entity.GRADE = model.template.Grades[i].Grade;
                        entity.DESCRIPTION = model.template.Grades[i].Description;
                        break;
                    }
                }
                return entity;
            }
            return null;
        }

        private Score EntityToModel(HR_SCORE entity)
        {
            if (entity != null)
            {
                var model = new Score()
                {
                    template = new ScoreTemplate() { 
                       Grades=JsonHelper.JsonDeserialize<List<ScoreTemplateGrade>>(entity.ANSWERSJSON),
                       IsShowResult=true,
                       Questions = JsonHelper.JsonDeserialize<List<ScoreTemplateQuestion>>(entity.QUESTIONSJSON),
                       ShowGrade=(entity.SHOWGRADE.HasValue && entity.SHOWGRADE.Value==1),
                       TemplateDescription=entity.TEMPLATEDESCRIPTION,
                       TemplateLabel=entity.TEMPLATELABEL,
                       TemplateTitle=entity.TEMPLATETITLE
                    },
                   TotalScore=entity.TOTALSCORE.HasValue?Convert.ToInt32(entity.TOTALSCORE.Value):0,
                   Grade=entity.GRADE,
                   Description=entity.DESCRIPTION,
                   UserID=entity.USERID
                };
                return model;
            }
            return null;
        }
    }
}
