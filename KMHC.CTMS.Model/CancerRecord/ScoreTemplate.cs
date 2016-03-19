using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.CancerRecord
{
    public class ScoreTemplate
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string TemplateID { get; set; }

        /// <summary>
        /// 模板名称
        /// </summary>
        public string TemplateTitle { get; set; }

        /// <summary>
        /// 模板说明
        /// </summary>
        public string TemplateDescription { get; set; }

        /// <summary>
        /// 评分量表表头提示
        /// </summary>
        public string TemplateLabel { get; set; }

        /// <summary>
        /// 是否以等级方式显示结果
        /// </summary>
        public bool ShowGrade { get; set; }

        /// <summary>
        /// 是否显示结果
        /// </summary>
        public bool IsShowResult { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public Nullable<System.DateTime> CreateTime { get; set; }

        /// <summary>
        /// 创建账号
        /// </summary>
        public string CreateBy { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public Nullable<System.DateTime> ModifyTime { get; set; }

        /// <summary>
        /// 最后修改者
        /// </summary>
        public string ModifyBy { get; set; }

        /// <summary>
        /// 问题集(json格式)
        /// </summary>
        public string QuestionsJson { get; set; }

        /// <summary>
        /// 结果集(json格式)
        /// </summary>
        public string AnswersJson { get; set; }

        public List<ScoreTemplateQuestion> Questions { get; set; }

        public List<ScoreTemplateGrade> Grades { get; set; }
    }

    public class ScoreTemplateQuestion
    {
        public string Question{get;set;}
        public List<ListItem> Options { get; set; }
        public int SelectedValue { get; set; }
    }

    public class ScoreTemplateGrade
    {
        public string Grade{get;set;}
        public int  minValue{get;set;}
        public int  maxValue{get;set;}
        public string  Description{get;set;}
    }

    public class ListItem
    {
        public string Text{get;set;}
        public int Value{get;set;}
    }
}
