/*
 * 描述:定义FD_Member的实现
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
    public class EFFD_MemberRepository:IFD_MemberRepository
    {
        private readonly BaseRepository<HR_FD_MEMBER> repository;
        private readonly EFFD_DiseaseRepository diseaseRepository;
        public EFFD_MemberRepository()
        {
            repository = new BaseRepository<HR_FD_MEMBER>(new CRDatabase());
            diseaseRepository = new EFFD_DiseaseRepository();
        }

        /// <summary>
        /// 添加家族成员
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public string Add(FD_Member model)
        {
            HR_FD_MEMBER member = new HR_FD_MEMBER();
            LoadModelToEntity(model,member);
            member.ID = string.IsNullOrEmpty(model.ID)?Guid.NewGuid().ToString():model.ID;
            member.CREATEDTIME = DateTime.Now;
            repository.Insert(member);
            if (model.Diseases != null && model.Diseases.Count > 0)
            {
                foreach (FD_Disease disease in model.Diseases)
                {
                    disease.MemberID = member.ID;
                    diseaseRepository.Add(disease);
                }
            }
            return member.ID;
        }

        /// <summary>
        /// 编辑家族成员
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public bool Edit(FD_Member model)
        {
            HR_FD_MEMBER member = repository.FindOne(o => o.ID.Equals(model.ID));
            LoadModelToEntity(model, member);
            member.MODIFIEDTIME = DateTime.Now;
            bool isUpdateSucess=repository.Update(member);
            if (isUpdateSucess)
            {
                //先删除
                foreach (FD_Disease disease in diseaseRepository.GetByMemberID(model.ID))
                {
                    diseaseRepository.Delete(disease.ID);
                }
                //再新增
                if (model.Diseases != null && model.Diseases.Count > 0)
                {
                    foreach (FD_Disease disease in model.Diseases)
                    {
                        disease.MemberID = member.ID;
                        diseaseRepository.Add(disease);
                    }
                }
            }
            return isUpdateSucess;
            
        }

        /// <summary>
        /// 删除家族成员
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public bool Delete(string ID)
        {
            return repository.DeleteById(ID);
        }

        /// <summary>
        /// 根据用户账号获取家族成员
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<FD_Member> GetByUserID(string userID)
        {
            List<FD_Member> list = new List<FD_Member>();
            var data = repository.FindAll(o => o.USERID.Equals(userID));
            foreach(HR_FD_MEMBER entity in data)
            {
                FD_Member model = new FD_Member();
                LoadEntityToModel(entity, model);
                list.Add(model);
            }
            return list;
        }

        /// <summary>
        /// 批量添加家族成员
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool AddList(List<FD_Member> list)
        {
            foreach (FD_Member model in list)
            {
                Add(model);
            }
            return true;
        }

        /// <summary>
        /// 业务模型转数据库模型
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="model"></param>
        protected static void LoadEntityToModel(HR_FD_MEMBER entity, FD_Member model)
        {
            if (entity == null || model == null) return;
            model.DeadReason = entity.DEADREASON;
            model.ID = entity.ID;
            model.IsAlive = (entity.ISALIVE.HasValue && entity.ISALIVE.Value.Equals(1));
            model.IsIdentical = (entity.ISIDENTICAL.HasValue && entity.ISIDENTICAL.Value.Equals(1));
            model.IsTwins = (entity.ISTWINS.HasValue && entity.ISTWINS.Value.Equals(1));
            model.Name = entity.MEMBERNAME;
            model.RelationID = entity.RELATIONSHIP;
            model.RelationShip = new EFFD_RelationRepository().Get(entity.RELATIONSHIP);
            model.Sex = entity.SEX.HasValue ? entity.SEX.Value.ToString() : "0";
            model.UserID = entity.USERID;
            model.Diseases = new EFFD_DiseaseRepository().GetByMemberID(entity.ID);
        }

        /// <summary>
        /// 数据库模型转业务模型
        /// </summary>
        /// <param name="model"></param>
        /// <param name="entity"></param>
        protected static void LoadModelToEntity(FD_Member model, HR_FD_MEMBER entity)
        {
            if (entity == null || model == null) return;
            entity.DEADREASON = model.DeadReason;
            entity.ID = model.ID;
            entity.ISALIVE = model.IsAlive ? 1 : 0;
            entity.ISTWINS = model.IsTwins ? 1 : 0;
            entity.ISIDENTICAL = model.IsIdentical ? 1 : 0;
            entity.MEMBERNAME = model.Name;
            entity.RELATIONSHIP = model.RelationShip == null ? (model.RelationID) : model.RelationShip.ID;
            entity.SEX = string.IsNullOrEmpty(model.Sex) ? 0 : int.Parse(model.Sex);
            entity.USERID = model.UserID;
        }
    }
}
