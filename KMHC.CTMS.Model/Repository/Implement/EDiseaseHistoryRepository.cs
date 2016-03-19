using System;
/*
 * 描述:IDiseaseHistoryRepository的EF实现
 *  
 * 修订历史: 
 * 日期       修改人              Email                   内容
 * 20151102   林德力              takalin@qq.com          创建 
 *  
 */

using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.Model.Repository.CancerRecord;
using KMHC.CTMS.Model.Repository.Interface;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Common.Helper;

namespace KMHC.CTMS.Model.Repository.Implement
{
    public class EDiseaseHistoryRepository:IDiseaseHistoryRepository
    {
        private readonly IBaseRepository<HR_DISEASEHISTORY> _repository;

        public EDiseaseHistoryRepository()
        {
            _repository = new BaseRepository<HR_DISEASEHISTORY>(new CRDatabase());
        }

        public DiseaseHistory Get(string disId)
        {
            var list = _repository.FindAll(u =>u.DISEASEHISTORYID==disId).Select(EntityToModel).FirstOrDefault();
            return list;
        }


        public IEnumerable<DiseaseHistory> GetList(PageInfo page,string userId ,string disName)
        {
            var list = _repository.FindAll(u => u.PERSONID == userId && (disName == null || u.DISEASENAME.Contains(disName))).Paging(ref page).ToList();
            return list.Select(EntityToModel).ToList();
        }

        public bool Add(DiseaseHistory model)
        {
            model.DISEASEHISTORYID = _repository.GetMaxId("HEALTHRECORD", "DISEASEHISTORYID").ToString();
            return _repository.Insert(ModelToEntity(model));
        }

  
        public bool Update(DiseaseHistory model)
        {
            HR_DISEASEHISTORY modelDB = _repository.FindOne(u => u.DISEASEHISTORYID == model.DISEASEHISTORYID);
            if (modelDB!=null)
            {
                modelDB.DISEASENAME = model.DISEASENAME;
                modelDB.TREATPROCESS = model.TREATPROCESS;
                modelDB.CREATTIME = model.CREATTIME;
                modelDB.HOSPITAL = model.HOSPITAL;
                modelDB.STARTTIME = model.STARTTIME;
                modelDB.RECOVERTIME = model.RECOVERTIME;
                modelDB.CONFIRMEDTIME = model.CONFIRMEDTIME;
                modelDB.EDITTIME = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
                modelDB.CONFIRMEDAGE = model.CONFIRMEDAGE;
                modelDB.CONFIRMEDADDRESS = model.CONFIRMEDADDRESS;
                modelDB.CONFIRMEDMODE = model.CONFIRMEDMODE;
                modelDB.CONFIRMEDRELUST = model.CONFIRMEDRELUST;
                modelDB.SYMPTOMDESCRIPTION = model.SYMPTOMDESCRIPTION;
                modelDB.ISCANCER = model.ISCANCER;
                modelDB.CANCERNAME = model.CANCERNAME;
                modelDB.CANCERCODE = model.CANCERCODE;
                modelDB.CANCERPOSITION = model.CANCERPOSITION;
                modelDB.CELLULATYPE = model.CELLULATYPE;
                modelDB.CLINICALSTAGES = model.CLINICALSTAGES;
                modelDB.TUMOR = model.TUMOR;
                modelDB.N = model.N;
                modelDB.M = model.M;
                modelDB.GENE = model.GENE;
                modelDB.ISREAPPEAR = model.ISREAPPEAR;
                return _repository.Update(modelDB);
            }
            return false;
        }


        public IEnumerable<TreatmentHistory> GetTreatmentHistoryList(string disId)
        {
            var treatmentRepository = new BaseRepository<HR_TREATMENTHISTORY>(new CRDatabase());
            var list = treatmentRepository.FindAll(u => u.DISEASEHISTORYID == disId).ToList();
            return list.Select(EntityToModel2).ToList();
        }

        public int AddTreatmentHistory(TreatmentHistory treatment)
        {
            var treatmentRepository = new BaseRepository<HR_TREATMENTHISTORY>(new CRDatabase());
            var maxId = treatmentRepository.GetMaxId("HR_TREATMENTHISTORY", "ID");
            var entity = ModelToEntity(treatment);
            entity.TREATMENTHISTORYID = maxId.ToString();
            treatmentRepository.Insert(entity);
            return maxId;
        }


        public void DelTreatmentHistory(string treatmentId)
        {
            var treatmentRepository = new BaseRepository<HR_TREATMENTHISTORY>(new CRDatabase());
            treatmentRepository.DeleteById(treatmentId);
        }


        public bool Delete(string id)
        {
            return _repository.DeleteById(id);
        }

        #region 实体模型映射

        private HR_DISEASEHISTORY ModelToEntity(DiseaseHistory model)
        {
            if (model != null)
            {
                var entity = new HR_DISEASEHISTORY()
                {
                    DISEASEHISTORYID = model.DISEASEHISTORYID,
                    PERSONID = model.PERSONID,
                    DISEASENAME = model.DISEASENAME,
                    ANNUALINCOME = model.ANNUALINCOME,
                    TREATPROCESS = model.TREATPROCESS,
                    CREATTIME = model.CREATTIME,
                    HOSPITAL = model.HOSPITAL,
                    CREATUSER = model.CREATUSER,
                    EDITUSER = model.EDITUSER,
                    EDITTIME = model.EDITTIME,
                    STARTTIME = model.STARTTIME,
                    RECOVERTIME = model.RECOVERTIME,
                    CONFIRMEDTIME = model.CONFIRMEDTIME,
                    CONFIRMEDAGE = model.CONFIRMEDAGE,
                    CONFIRMEDADDRESS = model.CONFIRMEDADDRESS,
                    CONFIRMEDMODE = model.CONFIRMEDMODE,
                    CONFIRMEDRELUST = model.CONFIRMEDRELUST,
                    SYMPTOMDESCRIPTION = model.SYMPTOMDESCRIPTION,
                    ISCANCER = model.ISCANCER,
                    CANCERCODE = model.CANCERCODE,
                    CANCERNAME = model.CANCERNAME,
                    CANCERPOSITION = model.CANCERPOSITION,
                    CELLULATYPE = model.CELLULATYPE,
                    CLINICALSTAGES = model.CLINICALSTAGES,
                    TUMOR = model.TUMOR,
                    N = model.N,
                    M = model.M,
                    GENE = model.GENE,
                    ISREAPPEAR = model.ISREAPPEAR
                };
                return entity;
            }
            return null;
        }


        private DiseaseHistory EntityToModel(HR_DISEASEHISTORY entity)
        {
            if (entity != null)
            {
                var model = new DiseaseHistory()
                {
                    DISEASEHISTORYID = entity.DISEASEHISTORYID,
                    PERSONID = entity.PERSONID,
                    DISEASENAME = entity.DISEASENAME,
                    ANNUALINCOME = entity.ANNUALINCOME,
                    TREATPROCESS = entity.TREATPROCESS,
                    CREATTIME = entity.CREATTIME,
                    HOSPITAL = entity.HOSPITAL,
                    CREATUSER = entity.CREATUSER,
                    EDITUSER = entity.EDITUSER,
                    EDITTIME = entity.EDITTIME,
                    STARTTIME = entity.STARTTIME,
                    RECOVERTIME = entity.RECOVERTIME,
                    CONFIRMEDTIME = entity.CONFIRMEDTIME,
                    CONFIRMEDAGE = entity.CONFIRMEDAGE,
                    CONFIRMEDADDRESS = entity.CONFIRMEDADDRESS,
                    CONFIRMEDMODE = entity.CONFIRMEDMODE,
                    CONFIRMEDRELUST = entity.CONFIRMEDRELUST,
                    SYMPTOMDESCRIPTION = entity.SYMPTOMDESCRIPTION,
                    ISCANCER = entity.ISCANCER,
                    CANCERCODE = entity.CANCERCODE,
                    CANCERNAME = entity.CANCERNAME,
                    CANCERPOSITION = entity.CANCERPOSITION,
                    CELLULATYPE = entity.CELLULATYPE,
                    CLINICALSTAGES = entity.CLINICALSTAGES,
                    TUMOR = entity.TUMOR,
                    N = entity.N,
                    M = entity.M,
                    GENE = entity.GENE,
                    ISREAPPEAR = entity.ISREAPPEAR
                };
                return model;
            }
            return null;
        }


        private HR_TREATMENTHISTORY ModelToEntity(TreatmentHistory model)
        {
            if (model!=null)
            {
                var entity = new HR_TREATMENTHISTORY()
                {
                    TREATMENTHISTORYID = model.TREATMENTHISTORYID,
                    DISEASEHISTORYID = model.DISEASEHISTORYID,
                    TREATMENTTIME = model.TREATMENTTIME,
                    TREATMENTHOSPITAL = model.TREATMENTHOSPITAL,
                    TREATMENTTYPE = model.TREATMENTTYPE,
                    OPERATIONTYPE = model.OPERATIONTYPE,
                    OPERATIONRESLUT = model.OPERATIONRESLUT,
                    RADIOTHERAPYDOSE = model.RADIOTHERAPYDOSE,
                    RADIOTHERAPYRESLUT = model.RADIOTHERAPYRESLUT,
                    CHEMOTHERAPYPROJECT = model.CHEMOTHERAPYPROJECT,
                    CHEMOTHERAPYDRUG = model.CHEMOTHERAPYDRUG,
                    CHEMOTHERAPYRESLUT = model.CHEMOTHERAPYRESLUT
                };
                return entity;
            }
            return null;
        }


        private TreatmentHistory EntityToModel2(HR_TREATMENTHISTORY entity)
        {
            if (entity != null)
            {
                var model = new TreatmentHistory()
                {
                    TREATMENTHISTORYID = entity.TREATMENTHISTORYID,
                    DISEASEHISTORYID = entity.DISEASEHISTORYID,
                    TREATMENTTIME = entity.TREATMENTTIME,
                    TREATMENTHOSPITAL = entity.TREATMENTHOSPITAL,
                    TREATMENTTYPE = entity.TREATMENTTYPE,
                    OPERATIONTYPE = entity.OPERATIONTYPE,
                    OPERATIONRESLUT = entity.OPERATIONRESLUT,
                    RADIOTHERAPYDOSE = entity.RADIOTHERAPYDOSE,
                    RADIOTHERAPYRESLUT = entity.RADIOTHERAPYRESLUT,
                    CHEMOTHERAPYPROJECT = entity.CHEMOTHERAPYPROJECT,
                    CHEMOTHERAPYDRUG = entity.CHEMOTHERAPYDRUG,
                    CHEMOTHERAPYRESLUT = entity.CHEMOTHERAPYRESLUT
                };
                return model;
            }
            return null;
        }


        #endregion




    }
}
