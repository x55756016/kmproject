/*
 * 描述:就诊史BLL
 *  
 * 修订历史: 
 * 日期                    修改人              Email                   内容
 * 2015/12/24 14:41:46     邓柏生                                      创建 
 * 20160125                林德力                                  添加就诊史列表
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

namespace KMHC.CTMS.BLL.CancerRecord
{
    public class SeeDoctorHistoryBLL
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Add(SeeDoctorHistory model)
        {
            if (model == null)
                return string.Empty;

            using (SeeDoctorHistoryDAL dal = new SeeDoctorHistoryDAL())
            {
                HR_SEEDOCTORHISTORY entity = ModelToEntity(model);
                entity.HISTORYID = string.IsNullOrEmpty(model.HISTORYID) ? Guid.NewGuid().ToString("N") : model.HISTORYID;

                return dal.Add(entity);
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(SeeDoctorHistory model)
        {
            if (model == null) return false;

            using (SeeDoctorHistoryDAL dal = new SeeDoctorHistoryDAL())
            {
                HR_SEEDOCTORHISTORY entity = ModelToEntity(model);
                return dal.Edit(entity);
            }
        }
        
        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public SeeDoctorHistory GetOne(Expression<Func<HR_SEEDOCTORHISTORY, bool>> predicate = null)
        {
            using (SeeDoctorHistoryDAL dal = new SeeDoctorHistoryDAL())
            {
                return EntityToModel(dal.GetOne(predicate));
            }
        }

        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<SeeDoctorHistory> GetList(PageInfo pageInfo, Expression<Func<HR_SEEDOCTORHISTORY, bool>> predicate = null)
        {
            using (var db = new CRDatabase())
            {
                List<SeeDoctorHistory> list = new List<SeeDoctorHistory>();
                db.HR_SEEDOCTORHISTORY.Where(predicate)
                   .Paging(ref pageInfo)
                   .ToList().ForEach(k => list.Add(EntityToModel(k)));

                list.ForEach(k => k.PERSONID =GetUserName(db.CTMS_SYS_USERINFO.Where(p => p.USERID.Equals(k.PERSONID)).FirstOrDefault()));

                return list;
            }
        }

        /// <summary>
        /// 返回就诊史列表
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<SeeDoctorHistory> GetSeeDoctorHistories(PageInfo pageInfo, string keyWord,string personId)
        {
            using (var db = new CRDatabase())
            {
                List<SeeDoctorHistory> list = new List<SeeDoctorHistory>();

               db.HR_SEEDOCTORHISTORY.Where(p => (string.IsNullOrEmpty(keyWord) || p.HOSPITAL.Contains(keyWord)) && p.PERSONID==personId)
                    .Paging(ref pageInfo)
                    .ToList().ForEach(k=>list.Add(EntityToModel(k)));

               list.ForEach(k => k.ICD10 = GetIcdSting(db.HR_SEEDOCHISICD.Where(p => p.HISTORYID == k.HISTORYID).ToList()));
                return list;
            }
        }

        /// <summary>
        /// Entity转Model
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private SeeDoctorHistory EntityToModel(HR_SEEDOCTORHISTORY entity)
        {
            if (entity != null)
            {
                SeeDoctorHistory model = new SeeDoctorHistory()
                {
                    HISTORYID = entity.HISTORYID,
                    PERSONID = entity.PERSONID,
                    DIAGNOSISTIME = entity.DIAGNOSISTIME,
                    DIAGNOSIS = entity.DIAGNOSIS,
                    ICD10 = entity.ICD10,
                    HOSPITAL = entity.HOSPITAL,
                    DIAGNOSISREPORT = entity.DIAGNOSISREPORT,
                    MEDICALHISATTACHMENT = entity.MEDICALHISATTACHMENT,
                    MAINDISEASE = entity.MAINDISEASE,
                    PERSONHISTORY = entity.PERSONHISTORY,
                    OBSTETRICALHISTORY = entity.OBSTETRICALHISTORY,
                    MENSTRUALHISTORY = entity.MENSTRUALHISTORY,
                    PHYSICALEXAM = entity.PHYSICALEXAM,
                    SPECIALISTCASE = entity.SPECIALISTCASE,
                    AUXILIARYEXAM = entity.AUXILIARYEXAM,
                    DISEASETYPE = entity.DISEASETYPE,
                    ISRELAPSE = entity.ISRELAPSE,
                    T = entity.T,
                    M = entity.M,
                    N = entity.N,
                    POSITION = entity.POSITION,
                    STAGE = entity.STAGE,
                    CYTOTYPE = entity.CYTOTYPE,
                    GENOTYPING = entity.GENOTYPING,
                    TRANSFER = entity.TRANSFER,
                    //REMARK = entity.REMARK,
                    HISTOLOGICALLY = entity.HISTOLOGICALLY,
                    MOLECULARPATHOLOGIC = entity.MOLECULARPATHOLOGIC
                };

                return model;
            }

            return null;
        }

        /// <summary>
        /// Model转Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private HR_SEEDOCTORHISTORY ModelToEntity(SeeDoctorHistory model)
        {
            if (model != null)
            {
                HR_SEEDOCTORHISTORY entity = new HR_SEEDOCTORHISTORY()
                {
                    HISTORYID = model.HISTORYID,
                    PERSONID = model.PERSONID,
                    DIAGNOSISTIME = model.DIAGNOSISTIME,
                    DIAGNOSIS = model.DIAGNOSIS,
                    ICD10 = model.ICD10,
                    HOSPITAL = model.HOSPITAL,
                    DIAGNOSISREPORT = model.DIAGNOSISREPORT,
                    MEDICALHISATTACHMENT = model.MEDICALHISATTACHMENT,
                    MAINDISEASE=model.MAINDISEASE,
                    PERSONHISTORY=model.PERSONHISTORY,
                    OBSTETRICALHISTORY=model.OBSTETRICALHISTORY,
                    MENSTRUALHISTORY=model.MENSTRUALHISTORY,
                    PHYSICALEXAM=model.PHYSICALEXAM,
                    SPECIALISTCASE=model.SPECIALISTCASE,
                    AUXILIARYEXAM=model.AUXILIARYEXAM,
                    DISEASETYPE=model.DISEASETYPE,
                    ISRELAPSE=model.ISRELAPSE,
                    T=model.T,
                    M=model.M,
                    N=model.N,
                    POSITION=model.POSITION,
                    STAGE=model.STAGE,
                    CYTOTYPE=model.CYTOTYPE,
                    GENOTYPING=model.GENOTYPING,
                    TRANSFER=model.TRANSFER,
                    //REMARK=model.REMARK,
                    HISTOLOGICALLY=model.HISTOLOGICALLY,
                    MOLECULARPATHOLOGIC=model.MOLECULARPATHOLOGIC
                };

                return entity;
            }

            return null;
        }

        /// <summary>
        /// 根据HistoryID获取ICD10名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetICD10(string id)
        {
            string icd10 = string.Empty;
            using (var db = new CRDatabase())
            {
                icd10= GetIcdSting(db.HR_SEEDOCHISICD.Where(p => p.HISTORYID == id).ToList());
            }

            return icd10;
        }

        private string GetIcdSting(List<HR_SEEDOCHISICD> list)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var hrSeedochisicd in list)
            {
                sb.Append(hrSeedochisicd.ICDNAME+",");
            }

            if (sb.ToString().Length>0)
            {
                return sb.ToString().Substring(0, sb.ToString().Length - 1);
            }
            
            return sb.ToString();
        }

        private string GetUserName(CTMS_SYS_USERINFO user)
        {
            string name = string.Empty;
            if (user != null)
                name = user.LOGINNAME;
            
            return name;
        }
    }
}
