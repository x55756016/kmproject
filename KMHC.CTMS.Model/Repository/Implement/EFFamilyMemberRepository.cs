//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using KMHC.CTMS.Model.CancerRecord;
//using KMHC.CTMS.Model.Repository.Interface;
//using KMHC.CTMS.DAL.Database;
//using KMHC.CTMS.Common.Helper;

//namespace KMHC.CTMS.Model.Repository.Implement
//{
//    public class EFFamilyMemberRepository : IFamilyMemberRepository
//    {
//        private readonly IBaseRepository<HR_FamilyMember> repository;

//        public EFFamilyMemberRepository()
//        {
//            repository = new BaseRepository<HR_FamilyMember>(new CRDatabase());
//        }
//        public int Add(FamilyMember familyMember)
//        {
//            var maxId = repository.GetMaxId("HR_FamilyMember", "FMemberId");
//            var entity = ModelToEntity(familyMember);
//            entity.FMemberId = maxId;
//            entity.CreationTime = DateTime.Now;
//            //entity.CreatorUserId
//            repository.Insert(entity);
//            return maxId;
//        }

//        public bool Edit(FamilyMember familyMember)
//        {
//            var entity = ModelToEntity(familyMember);
//            entity.LastModificationTime = DateTime.Now;
//            //entity.LastModifierUserId
//            return entity != null && repository.Update(entity);
//        }

//        public FamilyMember Get(int fMemberId)
//        {
//            return EntityToModel(repository.FindOne(o => o.FMemberId == fMemberId));
//        }

//        public IEnumerable<FamilyMember> GetFMemberList(int familyId)
//        {
//            var list = repository.FindAll(o => o.FamilyId == familyId);
//            var familys = list.Select(EntityToModel).ToList();
//            return familys;
//        }

//        public void Delete(int id)
//        {
//            repository.Delete(id);
//        }


//        #region 实体模型映射


//        private HR_FamilyMember ModelToEntity(FamilyMember model)
//        {
//            if (model != null)
//            {
//                var entity = new HR_FamilyMember()
//                {
//                    FMemberId = model.FMemberId,
//                    FamilyId = model.FamilyId,
//                    CardNo = model.CardNo,
//                    MemberName = model.MemberName,
//                    RelateId = model.RelateId
//                };
//                return entity;
//            }
//            return null;

//        }
//        private FamilyMember EntityToModel(HR_FamilyMember model)
//        {
//            if (model != null)
//            {
//                var entity = new FamilyMember()
//                {
//                    FMemberId = model.FMemberId,
//                    FamilyId = model.FamilyId,
//                    CardNo = model.CardNo,
//                    MemberName = model.MemberName,
//                    RelateId = model.RelateId
//                };
//                return entity;
//            }
//            return null;
//        }

//        #endregion
//    }
//}
