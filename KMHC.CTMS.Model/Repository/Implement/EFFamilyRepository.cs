//using System;
//using System.Collections.Generic;
//using System.Linq;
//using KMHC.CTMS.Model.CancerRecord;
//using KMHC.CTMS.Model.Repository.Interface;
//using KMHC.CTMS.DAL.Database;
//using KMHC.CTMS.Common.Helper;

//namespace KMHC.CTMS.Model.Repository
//{
//    public class EFFamilyRepository : IFamilyInfoRepository
//    {
//        private readonly IBaseRepository<HR_Family> repository;



//        public EFFamilyRepository()
//        {
//            repository = new BaseRepository<HR_Family>(new CRDatabase());
//        }

//        public IEnumerable<FamilyInfo> GetFamilyList(PageInfo page, string name)
//        {
//            var list=repository.FindAll(o => o.HouseholderName == name).Paging(ref page).ToList();
//            var familys = list.Select(EntityToModel).ToList();
//            return familys;
//        }

//        public void Delete(int id)
//        {
//            repository.Delete(id);
//        }

//        public FamilyInfo Get(int id)
//        {
//            return EntityToModel(repository.FindOne(o=>o.FamilyId==id));
//        }


//        public int Add(FamilyInfo model)
//        {
//            var maxId = repository.GetMaxId("HR_Family", "FamilyId");
//            var entity = ModelToEntity(model);
//            entity.FamilyId = maxId;
//            entity.CreationTime = DateTime.Now;
//            //entity.CreatorUserId
//            repository.Insert(entity);
//            return maxId;
//        }

//        public bool Edit(FamilyInfo model)
//        {
//            var entity=ModelToEntity(model);
//            entity.LastModificationTime=DateTime.Now;
//            //entity.LastModifierUserId
//            return entity != null && repository.Update(entity);
//        }


//        #region 实体模型映射


//        private HR_Family ModelToEntity(FamilyInfo model)
//        {
//            if (model != null)
//            {
//                var entity = new HR_Family()
//                {
//                    FamilyId = model.FamilyId,
//                    HouseholderName = model.HouseholderName,
//                    HouseholderId = model.HouseholderId,
//                    CardNo = model.CardNo,
//                    CommunityId = model.CommunityId,
//                    Nationality = model.Nationality,
//                    ProvinceId = model.ProvinceId,
//                    CityId = model.CityId,
//                    CountryId = model.CountryId,
//                    TownId = model.TownId,
//                    Address = model.Address,
//                    ZipCode = model.ZipCode
//                };
//                return entity;
//            }
//            return null;

//        }
//        private FamilyInfo EntityToModel(HR_Family model)
//        {
//            if (model != null)
//            {
//                var entity = new FamilyInfo()
//                {
//                    FamilyId = model.FamilyId,
//                    HouseholderName = model.HouseholderName,
//                    HouseholderId = model.HouseholderId,
//                    CardNo = model.CardNo,
//                    CommunityId = model.CommunityId,
//                    Nationality = model.Nationality,
//                    ProvinceId = model.ProvinceId,
//                    CityId = model.CityId,
//                    CountryId = model.CountryId,
//                    TownId = model.TownId,
//                    Address = model.Address,
//                    ZipCode = model.ZipCode
//                };
//                return entity;
//            }
//            return null;
//        }

//        #endregion
//    }
//}
