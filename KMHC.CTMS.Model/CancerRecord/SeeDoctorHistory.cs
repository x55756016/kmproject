/*
 * 描述:
 *  
 * 修订历史: 
 * 日期            修改人              Email                   内容
 * 2015/11/24      邓柏生                                      创建 
 *  
 */

using System;
using System.Collections.Generic;

namespace KMHC.CTMS.Model.CancerRecord
{
    public class SeeDoctorHistory
    {

        public SeeDoctorHistory()
        {
            ICDList = new List<SeeDocHisICD>();
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        public string HISTORYID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string PERSONID { get; set; }

        /// <summary>
        /// 诊断时间
        /// </summary>
        public Nullable<System.DateTime> DIAGNOSISTIME { get; set; }

        /// <summary>
        /// 诊断
        /// </summary>
        public string DIAGNOSIS { get; set; }

        /// <summary>
        /// ICD10
        /// </summary>
        public string ICD10 { get; set; }

        /// <summary>
        /// 就诊医院
        /// </summary>
        public string HOSPITAL { get; set; }

        /// <summary>
        /// 检验报告附件
        /// </summary>
        public string DIAGNOSISREPORT { get; set; }

        /// <summary>
        /// 病历附件
        /// </summary>
        public string MEDICALHISATTACHMENT { get; set; }

        /// <summary>
        /// 主诉
        /// </summary>
        public string MAINDISEASE { get; set; }

        /// <summary>
        /// 个人史
        /// </summary>
        public string PERSONHISTORY { get; set; }

        /// <summary>
        /// 婚育史
        /// </summary>
        public string OBSTETRICALHISTORY { get; set; }

        /// <summary>
        /// 月经史
        /// </summary>
        public string MENSTRUALHISTORY { get; set; }

        /// <summary>
        /// 体格检查
        /// </summary>
        public string PHYSICALEXAM { get; set; }

        /// <summary>
        /// 专科情况
        /// </summary>
        public string SPECIALISTCASE { get; set; }

        /// <summary>
        /// 辅助检查
        /// </summary>
        public string AUXILIARYEXAM { get; set; }

        public List<SeeDocHisICD> ICDList { get; set; }

        /// <summary>
        /// 疾病类型： 0：未知，1：疑似肿瘤，2：肿瘤
        /// </summary>
        public int DISEASETYPE { get; set; }

        /// <summary>
        /// 是否原发
        /// </summary>
        public bool ISRELAPSE { get; set; }

        /// <summary>
        /// TNM分期：肿瘤
        /// </summary>
        public string T { get; set; }

        /// <summary>
        /// TNM分期：淋巴结
        /// </summary>
        public string M { get; set; }

        /// <summary>
        /// TNM分期：转移
        /// </summary>
        public string N { get; set; }

        /// <summary>
        /// 位置
        /// </summary>
        public string POSITION { get; set; }

        /// <summary>
        /// 临床分期
        /// </summary>
        public string STAGE { get; set; }

        /// <summary>
        /// 细胞分型
        /// </summary>
        public string CYTOTYPE { get; set; }

        /// <summary>
        /// 基因分型
        /// </summary>
        public string GENOTYPING { get; set; }

        /// <summary>
        /// 是否远处转移
        /// </summary>
        public string TRANSFER { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string REMARK { get; set; }


        /// <summary>
        /// 组织病理学检查
        /// </summary>
        public string HISTOLOGICALLY { get; set; }
        /// <summary>
        /// 分子病理学检查
        /// </summary>
        public string MOLECULARPATHOLOGIC { get; set; }
    }

    public class SeeDocHisICD
    {
        public string INFOID { get; set; }
        public string HISTORYID { get; set; }
        public string ICDCODE { get; set; }
        public string ICDNAME { get; set; }
        public string DETAILS { get; set; }
        public bool ISMAIN { get; set; }
    }

    /// <summary>
    /// 用户上传整理Model
    /// </summary>
    public class UserArrangeItem
    {
        public string EventID { get; set; }
        public string HistoryId { get; set; }
        public bool  CreateNew{ get; set; }
        public string Remark { get; set; }
        public IList<FileUserArrange> Files { get; set; }
    }
}
