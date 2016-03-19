/*
 * 描述:定义FD_Disease的实现
 *  
 * 修订历史: 
 * 日期          修改人              Email              内容
 * 20151102      张志明              			创建 
 *  
 */

using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.Model.Repository.Interface;
using KMHC.CTMS.DAL.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.Repository.Implement.CancerRecord
{
    public class EFFD_DiseaseRepository:IFD_DiseaseRepository
    {
        private readonly BaseRepository<HR_FD_DISEASE> repository;
        public EFFD_DiseaseRepository()
        {
            repository = new BaseRepository<HR_FD_DISEASE>(new CRDatabase());
        }

        /// <summary>
        /// 添加家族疾病
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public string Add(FD_Disease disease)
        {
            var entity = new HR_FD_DISEASE();
            LoadModelToEntity(disease, entity);
            entity.ID = string.IsNullOrEmpty(disease.ID) ? Guid.NewGuid().ToString() : disease.ID;
            repository.Insert(entity);
            return entity.ID;
        }

        /// <summary>
        /// 编辑家族疾病
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public bool Edit(FD_Disease disease)
        {
            return false;
        }

        /// <summary>
        /// 删除家族疾病
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public bool Delete(string ID)
        {
            return repository.DeleteById(ID);
        }

        /// <summary>
        /// 根据家族成员ID获取家族疾病
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<FD_Disease> GetByMemberID(string memberID)
        {
            var entityList = repository.FindAll(o => o.MEMBERID.Equals(memberID));
            List<FD_Disease> list = new List<FD_Disease>();
            foreach (HR_FD_DISEASE disease in entityList)
            {
                FD_Disease model = new FD_Disease();
                LoadEntityToModel(disease, model);
                list.Add(model);
            }
            return list;
        }

        /// <summary>
        /// 批量添加家族疾病
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool AddList(List<FD_Disease> list)
        {
            return false;
        }
        
        /// <summary>
        /// 数据库模型转业务模型
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="model"></param>
        protected static void LoadEntityToModel(HR_FD_DISEASE entity, FD_Disease model)
        {
            if (entity == null || model == null) return;
            model.AttackDate = entity.ATTACKDATA;
            model.DiagnosisAge = entity.DIAGNOSISAGE.HasValue ? Convert.ToInt32(entity.DIAGNOSISAGE.Value) : 0;
            model.DiseaseCode = entity.DISEASECODE;
            model.DiseaseName = entity.DISEASENAME;
            model.ID = entity.ID;
            model.IsInfectious = entity.ISINFECTIOUS.HasValue && entity.ISINFECTIOUS.Value == 1;
            model.MemberID = entity.MEMBERID;
            model.Treatment = entity.TREATMENT;
            model.TreatmentHospital = entity.TREATMENTHOSPITAL;
            model.TreatmentResult = entity.TREATMENTRESULT;
        }

        /// <summary>
        /// 业务模型转数据库模型
        /// </summary>
        /// <param name="model"></param>
        /// <param name="entity"></param>
        protected static void LoadModelToEntity(FD_Disease model, HR_FD_DISEASE entity)
        {
            if (entity == null || model == null) return;
            entity.ATTACKDATA = model.AttackDate;
            entity.DIAGNOSISAGE = model.DiagnosisAge;
            entity.DISEASECODE = model.DiseaseCode;
            entity.DISEASENAME = model.DiseaseName;
            entity.ID = model.ID;
            entity.ISINFECTIOUS = model.IsInfectious ? 1 : 0;
            entity.MEMBERID = model.MemberID;
            entity.TREATMENT = model.Treatment;
            entity.TREATMENTHOSPITAL = model.TreatmentHospital;
            entity.TREATMENTRESULT = model.TreatmentResult;
        }
    }
}
