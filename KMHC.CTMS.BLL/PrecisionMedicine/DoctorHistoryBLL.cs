/*
 * 描述: 就诊史功能实现
 *  
 * 修订历史: 
 * 日期               修改人              Email                  内容
 * 20151208   		  林德力         	takalin@qq.com   		 创建 
 *  
 */
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using KMHC.CTMS.DAL;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.CancerRecord;
using Newtonsoft.Json;

namespace KMHC.CTMS.BLL.PrecisionMedicine
{  
   public  class DoctorHistoryBLL
    {
       private readonly string logTitle = "访问DoctorHistoryBLL类";


       /// <summary>
       /// 获得就诊史信息
       /// </summary>
       /// <param name="historyId"></param>
       /// <returns></returns>
       public SeeDoctorHistory GetSeeDoctorHistory(string historyId)
       {
           if (string.IsNullOrEmpty(historyId))
           {
               LogService.WriteInfoLog(logTitle, "试图查找ID为空的SeeDoctorHistory实体!");
               return null;
           }
           using (DbContext db = new CRDatabase())
           {
               var model = EntityToModel(db.Set<HR_SEEDOCTORHISTORY>().FirstOrDefault(p => p.HISTORYID == historyId));
               if (model == null)   return null;

               var list = db.Set<HR_SEEDOCHISICD>().Where(p => p.HISTORYID == model.HISTORYID).ToList();

               db.Set<HR_SEEDOCHISICD>().Where(p => p.HISTORYID == model.HISTORYID).ToList().ForEach(k => model.ICDList.Add(EntityToModel(k)));
               return model;
           }
       }


       /// <summary>
       /// 保存就诊史录入
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       public bool SaveSeeDoctoryHis(SeeDoctorHistory model)
       {
           if (model==null)
           {
               LogService.WriteInfoLog(logTitle, "试图查找为空的SeeDoctorHistory实体!");
               return false;
           }

           using (DbContext db = new CRDatabase())
           {
               db.Entry(ModelToEntity(model)).State = EntityState.Modified;
               db.Set<HR_SEEDOCHISICD>()
                   .Where(p => p.HISTORYID == model.HISTORYID)
                   .ForEachAsync(k => db.Set<HR_SEEDOCHISICD>().Remove((k)));

               model.ICDList.ForEach(p => db.Set<HR_SEEDOCHISICD>().Add(EntityToModel(p)));
               return db.SaveChanges() > 0;
           }
       }



       /// <summary>
       /// 获取单个依据模板
       /// </summary>
       /// <param name="baseTemplateID"></param>
       /// <returns></returns>
       public List<BaseTemplateResult> GetBaseOnTemplate(string historyId)
       {
           using (var db = new CRDatabase())
           {
               List<BaseTemplateResult> list = new List<BaseTemplateResult>();
               db.CTMS_BASETEMPLATERESULT.Where(p => p.HISTORYID == historyId).ToList().ForEach(k => list.Add(EntityToModel(k)));
               foreach (var item in list)
               {
                   item.Name = db.CTMS_ADM_EXAMINETEMPLATES.FirstOrDefault(p => p.ID == item.EXAMINETEMPLATESID).NAME;
               }
               return list;
           }
       }

       /// <summary>
       /// 添加依据模板
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       public bool SaveBaseOnTemplate(string historyId,List<BaseTemplateResult> list)
       {
           if (list == null )
           {
               LogService.WriteInfoLog(logTitle, "试图查找为空的CTMS_BASETEMPLATERESULT实体!");
               return false;
           }

           using (var db = DbSessionFactory.GetCurrentDbContext() )
           {
               var oldList = db.Set<CTMS_BASETEMPLATERESULT>().Where(p => p.HISTORYID == historyId).ToList();

               foreach (var item in oldList)
               {
                   db.Set<CTMS_BASETEMPLATERESULT>().Remove(item);
                   db.Set<CTMS_IMAGEEXAMINEREPORTDETAIL>().Where(p => p.REPORTID == item.BASETEMPLATEID)
                       .ForEachAsync(k => db.Set<CTMS_IMAGEEXAMINEREPORTDETAIL>().Remove(k));
               }

               foreach (var baseTemplateResult in list)
               {
                   db.Set<CTMS_BASETEMPLATERESULT>().Add(ModelToEntity(baseTemplateResult));
                   //结果不序列化成json的存放
                   Dictionary<string, string> dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(baseTemplateResult.RESULT);
                   if (dic.Count > 0)
                   {
                       foreach (string key in dic.Keys)
                       {
                           CTMS_IMAGEEXAMINEREPORTDETAIL detail = new CTMS_IMAGEEXAMINEREPORTDETAIL();
                           detail.ID = Guid.NewGuid().ToString();
                           detail.REPORTID = baseTemplateResult.BASETEMPLATEID;
                           detail.OPTIONID = key;
                           detail.VALUE = dic[key];
                           db.Set<CTMS_IMAGEEXAMINEREPORTDETAIL>().Add(detail);
                       }
                   }
               }
               return db.SaveChanges() > 0;
           }
       }



       #region 模型映射

       public SeeDoctorHistory EntityToModel(HR_SEEDOCTORHISTORY entity)
       {
           if (entity != null)
           {
               var model = new SeeDoctorHistory()
               {
                   HISTORYID = entity.HISTORYID,
                   PERSONID = entity.PERSONID,
                   DIAGNOSISTIME = entity.DIAGNOSISTIME ,
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
                   CYTOTYPE = entity.CYTOTYPE,
                   GENOTYPING = entity.GENOTYPING,
                   POSITION = entity.POSITION,
                   STAGE = entity.STAGE,
                   TRANSFER = entity.TRANSFER,
                   T = entity.T,
                   M = entity.M,
                   N = entity.N,
                   HISTOLOGICALLY = entity.HISTOLOGICALLY,
                   MOLECULARPATHOLOGIC = entity.MOLECULARPATHOLOGIC
               };
               return model;
           }
           return null;
       }


       public HR_SEEDOCTORHISTORY ModelToEntity(SeeDoctorHistory model)
       {
           if (model!=null)
           {
               var entity = new HR_SEEDOCTORHISTORY()
               {
                   HISTORYID = model.HISTORYID,
                   PERSONID = model.PERSONID,
                   DIAGNOSISTIME = model.DIAGNOSISTIME,
                   DIAGNOSIS = model.DIAGNOSIS,
                   ICD10 = model.ICD10,
                   HOSPITAL = model.HOSPITAL,
                   DIAGNOSISREPORT = model.DIAGNOSISREPORT,
                   MEDICALHISATTACHMENT = model.MEDICALHISATTACHMENT,
                   MAINDISEASE = model.MAINDISEASE,
                   PERSONHISTORY = model.PERSONHISTORY,
                   OBSTETRICALHISTORY = model.OBSTETRICALHISTORY,
                   MENSTRUALHISTORY = model.MENSTRUALHISTORY,
                   PHYSICALEXAM = model.PHYSICALEXAM,
                   SPECIALISTCASE = model.SPECIALISTCASE,
                   AUXILIARYEXAM = model.AUXILIARYEXAM,
                   DISEASETYPE = model.DISEASETYPE,
                   ISRELAPSE = model.ISRELAPSE,
                   CYTOTYPE = model.CYTOTYPE,
                   GENOTYPING = model.GENOTYPING,
                   POSITION = model.POSITION,
                   STAGE = model.STAGE,
                   TRANSFER = model.TRANSFER,
                   T = model.T,
                   M = model.M,
                   N = model.N,
                   HISTOLOGICALLY = model.HISTOLOGICALLY,
                   MOLECULARPATHOLOGIC = model.MOLECULARPATHOLOGIC
               };
               return entity;
           }
           return null;
       }

       public SeeDocHisICD EntityToModel(HR_SEEDOCHISICD entity)
       {
           if (entity != null)
           {
               var model = new SeeDocHisICD()
               {
                   INFOID = entity.INFOID,
                   HISTORYID = entity.HISTORYID,
                   ICDCODE = entity.ICDCODE,
                   ICDNAME = entity.ICDNAME,
                   DETAILS = entity.DETAILS,
                   ISMAIN = entity.ISMAIN
               };
               return model;
           }
           return null;
       }


       public HR_SEEDOCHISICD EntityToModel(SeeDocHisICD model)
       {
           if (model != null)
           {
               var entity = new HR_SEEDOCHISICD()
               {
                   INFOID = model.INFOID,
                   HISTORYID = model.HISTORYID,
                   ICDCODE = model.ICDCODE,
                   ICDNAME = model.ICDNAME,
                   DETAILS = model.DETAILS,
                   ISMAIN = model.ISMAIN
               };
               return entity;
           }
           return null;
       }



       public CTMS_BASETEMPLATERESULT ModelToEntity(BaseTemplateResult model)
       {
           if (model!=null)
           {
               var entity = new CTMS_BASETEMPLATERESULT()
               {
                     BASETEMPLATEID = model.BASETEMPLATEID,
                     HISTORYID = model.HISTORYID,
                     EXAMINETEMPLATESID = model.EXAMINETEMPLATESID,
                     RESULT = model.RESULT,
                     CREATEUSERID = model.CREATEUSERID,
                     CREATEDATETIME = model.CREATEDATETIME,
                     EDITUSERID = model.EDITUSERID,
                     EDITDATETIME = model.EDITDATETIME,
                     ISDELETED = model.ISDELETED,
                     OWNERID = model.OWNERID
               };
               return entity;
           }
           return null;
       }


       public BaseTemplateResult EntityToModel(CTMS_BASETEMPLATERESULT entity)
       {
           if (entity != null)
           {
               var model = new BaseTemplateResult()
               {
                   BASETEMPLATEID = entity.BASETEMPLATEID,
                   HISTORYID = entity.HISTORYID,
                   EXAMINETEMPLATESID = entity.EXAMINETEMPLATESID,
                   RESULT = entity.RESULT,
                   CREATEUSERID = entity.CREATEUSERID,
                   CREATEDATETIME = entity.CREATEDATETIME,
                   EDITUSERID = entity.EDITUSERID,
                   EDITDATETIME = entity.EDITDATETIME,
                   ISDELETED = entity.ISDELETED,
                   OWNERID = entity.OWNERID
               };
               return model;
           }
           return null;
       }

        #endregion



    }
}
