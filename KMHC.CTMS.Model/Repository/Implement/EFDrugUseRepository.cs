///*
// * 描述:定义IDrugUseRepository的接口实现
// *  
// * 修订历史: 
// * 日期       修改人              Email                  内容
// * 20151116	  林德力         	takalin@qq.com   		 创建 
// *  
// */
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using KMHC.CTMS.Model.PrecisionMedicine;
//using KMHC.CTMS.Model.Repository.Interface;
//using KMHC.CTMS.DAL.Database;

//namespace KMHC.CTMS.Model.Repository.Implement
//{
//    public class EFDrugUseRepository:IDrugUseRepository
//    {
//        private readonly IBaseRepository<PM_DRUGUSE> _repository;


//        public EFDrugUseRepository()
//        {
//            _repository = new BaseRepository<PM_DRUGUSE>(new CRDatabase());
//        }


//        public bool Add(DrugUser model)
//        {
//           return  _repository.Insert(ModelToEntity(model));
//        }

//        public bool Update(DrugUser model)
//        {
//            throw new NotImplementedException();
//        }

//        #region 生活习惯操作
//        public bool AddHabits(Habits mdoel)
//        {
//            var habitsRepository =     new BaseRepository<PM_HABITS>(new CRDatabase());
//            habitsRepository.DeleteById(mdoel.DRUGUSERID);
//            return habitsRepository.Insert(ModelToEntity(mdoel));
//        }


//        #endregion

//        #region 药物作用操作
//        public bool AddDrugEffect(DrugEffect mdoel)
//        {
//            var effectRepository = new BaseRepository<PM_DRUGEFFECT>(new CRDatabase());
//            effectRepository.DeleteById(mdoel.DRUGUSERID);
//            return effectRepository.Insert(ModelToEntity(mdoel));

//        }

//        #endregion

//        #region 实体模型映射

//        private PM_DRUGUSE ModelToEntity(DrugUser model)
//        {
//            if (model != null)
//            {
//                var entity = new PM_DRUGUSE()
//                {
//                    ID = new Guid().ToString(),
//                    USERID = model.UserId,
//                    DISEASEID = model.DiseaseId,
//                    DRUGNAME = model.DrugName,
//                    TREATMENTDISEASES = model.TreatmentDiseases,
//                    STARTTIME = model.StartTime,
//                    ENDTIME = model.EndTime,
//                    DOSE = model.Dose,
//                    UNITS = model.Units,
//                    ISNEWDRUG = model.IsNewDrug,
//                    CREATEDATETIME = model.CreateDateTime,
//                    CREATEUSERID = model.CreateUserId,
//                    EDITDATETIME = model.EditDateTime,
//                    EDITUSERID = model.EditUserId,
//                    ISDELETED = model.IsDeleted
//                };
//                return entity;
//            }
//            return null;
//        }

//        private DrugUser EntityToModel(PM_DRUGUSE entity)
//        {
//            if (entity != null)
//            {
//                var model = new DrugUser()
//                {
//                    Id =  entity.ID,
//                    UserId = entity.USERID,
//                    DiseaseId = entity.DISEASEID,
//                    DrugName = entity.DRUGNAME,
//                    TreatmentDiseases = entity.TREATMENTDISEASES,
//                    StartTime = entity.STARTTIME,
//                    EndTime = entity.ENDTIME,
//                    Dose = entity.DOSE,
//                    Units = entity.UNITS,
//                    IsNewDrug = entity.ISNEWDRUG,
//                    CreateDateTime = entity.CREATEDATETIME,
//                    CreateUserId = entity.CREATEUSERID,
//                    EditDateTime = entity.EDITDATETIME,
//                    EditUserId = entity.EDITUSERID,
//                    IsDeleted = entity.ISDELETED
//                };
//                return model;
//            }
//            return null;
//        }


//        private PM_HABITS ModelToEntity(Habits model)
//        {
//            if (model != null)
//            {
//                var entity = new PM_HABITS()
//                {
//                    DRUGUSERID = model.DRUGUSERID,
//                    HABITSCODE = model.HABITSCODE
//                };
//                return entity;
//            }
//            return null;
//        }

//        private Habits EntityToModel(PM_HABITS entity)
//        {
//            if (entity!=null)
//            {
//                var model = new Habits()
//                {
//                {
//                    DRUGUSERID = entity.DRUGUSERID,
//                    HABITSCODE = entity.HABITSCODE
//                };
//                return model;
//            }
//            return null;
//        }


//        private PM_DRUGEFFECT ModelToEntity(DrugEffect model)
//        {
//            if (model != null)
//            {
//                var entity = new PM_DRUGEFFECT()
//                {
//                      DRUGUSERID =model.DRUGUSERID,
//                      DRUGINTERACTION =model.DRUGINTERACTION,
//                      DRUGGENE =model.DRUGGENE
//                };
//                return entity;
//            }
//            return null;
//        }

//        private DrugEffect EntityToModel(PM_DRUGEFFECT entity)
//        {
//            if (entity != null)
//            {
//                var model = new DrugEffect()
//                {
//                    DRUGUSERID = entity.DRUGUSERID,
//                    DRUGINTERACTION = entity.DRUGINTERACTION,
//                    DRUGGENE = entity.DRUGGENE
//                };
//                return model;
//            }
//            return null;
//        }



//        #endregion

//    }
//}
