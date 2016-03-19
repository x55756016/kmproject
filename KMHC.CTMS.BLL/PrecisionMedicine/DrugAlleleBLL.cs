/*
 * 描述:定义基因对用药影响的业务类
 *  
 * 修订历史: 
 * 日期          修改人              内容
 * 20151123      张志明              创建 
 *  
 */

using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.PrecisionMedicine;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.BLL.PrecisionMedicine
{
    public class DrugAlleleBLL
    {
        private readonly string logTitle = "访问DrugAlleleBLL类";
        public DrugAlleleBLL()
        {

        }

        /// <summary>
        /// 添加用户基因
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Add(DrugAllele model)
        {
            if (model == null) return string.Empty;
            if (string.IsNullOrEmpty(model.ID)) model.ID = Guid.NewGuid().ToString();
            using (DbContext db = new CRDatabase())
            {
                db.Set<GN_DRUGALLELE>().Add(ModelToEntity(model));
                db.SaveChanges();
                return model.ID;
            }
        }

        /// <summary>
        /// 编辑用户基因
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(DrugAllele model)
        {
            if (model == null)
            {
                LogService.WriteInfoLog(logTitle, "试图修改为空的DrugAllele实体!");
                return false;
            }
            using (DbContext db = new CRDatabase())
            {
                db.Entry(ModelToEntity(model)).State = EntityState.Modified;
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 根据ID获取用户基因信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DrugAllele Get(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                LogService.WriteInfoLog(logTitle, "试图查找ID为空的DrugAllele实体!");
                return null;
            }
            using (DbContext db = new CRDatabase())
            {
                GN_DRUGALLELE entity = db.Set<GN_DRUGALLELE>().Find(id);
                if (entity == null) return null;
                return EntityToModel(entity);
            }
        }

        /// <summary>
        /// 删除用户基因
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                LogService.WriteInfoLog(logTitle, "试图删除ID为空的DrugAllele实体!");
                return false;
            }
            using (DbContext db = new CRDatabase())
            {
                GN_DRUGALLELE entity = db.Set<GN_DRUGALLELE>().Find(id);
                if (entity != null)
                {
                    db.Set<GN_DRUGALLELE>().Remove(entity);
                }
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 根据用户ID获取基因
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<DrugAllele> GetList(string drugBankID)
        {
            List<DrugAllele> list = new List<DrugAllele>();
            if (string.IsNullOrEmpty(drugBankID)) return list;
            using (DbContext db = new CRDatabase())
            {
                var entityList = db.Set<GN_DRUGALLELE>().AsNoTracking().Where(o => o.DRUGBANKID.Equals(drugBankID));
                foreach (GN_DRUGALLELE entity in entityList)
                {
                    DrugAllele DrugAllele = EntityToModel(entity);
                    list.Add(DrugAllele);
                }
            }
            return list;
        }

        private GN_DRUGALLELE ModelToEntity(DrugAllele model)
        {
            if (model == null) return null;
            return new GN_DRUGALLELE()
            {
                ID = model.ID,
                GENEALLELEID = model.GeneAlleleID,
                DRUGBANKID = model.DrugBankID,
                EFFECTTYPE = model.EffectType,
                EFFECT = model.Effect,

                CREATEDATETIME = model.CreateDateTime,
                CREATEUSERID = model.CreateUserID,
                CREATEUSERNAME = model.CreateUserName,
                EDITDATETIME = model.EditTime,
                EDITUSERID = model.EditUserID,
                EDITUSERNAME = model.EditUserName,
                OWNERID = model.OwnerID,
                OWNERNAME = model.OwnerName,
                ISDELETED = model.IsDeleted
            };
        }

        private DrugAllele EntityToModel(GN_DRUGALLELE entity)
        {
            if (entity == null) return null;
            return new DrugAllele()
            {
                ID = entity.ID,
                GeneAlleleID=entity.GENEALLELEID,
                DrugBankID=entity.DRUGBANKID,
                EffectType=entity.EFFECTTYPE,
                Effect=entity.EFFECT,

                CreateDateTime = entity.CREATEDATETIME,
                CreateUserID = entity.CREATEUSERID,
                CreateUserName = entity.CREATEUSERNAME,
                EditTime = entity.EDITDATETIME,
                EditUserID = entity.EDITUSERID,
                EditUserName = entity.EDITUSERNAME,
                OwnerID = entity.OWNERID,
                OwnerName = entity.OWNERNAME,
                IsDeleted = entity.ISDELETED
            };
        }
    }
}
