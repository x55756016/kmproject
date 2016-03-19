/*
 * 描述:定义用户基因信息的业务类
 *  
 * 修订历史: 
 * 日期          修改人              内容
 * 20151123      张志明              创建 
 *  
 */

using KMHC.CTMS.BLL.PrecisionMedicine;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.CancerRecord;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.BLL.CancerRecord
{
    public class UserGeneBLL
    {
        private readonly string logTitle = "访问UserGeneBLL类";
        public UserGeneBLL()
        {

        }

        /// <summary>
        /// 添加用户基因
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Add(UserGene model)
        {
            if (model == null) return string.Empty;
            if (string.IsNullOrEmpty(model.ID)) model.ID = Guid.NewGuid().ToString();
            using (DbContext db = new CRDatabase())
            {
                db.Set<HR_USERGENE>().Add(ModelToEntity(model));
                db.SaveChanges();
                return model.ID;
            }
        }

        /// <summary>
        /// 编辑用户基因
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(UserGene model)
        {
            if (model == null)
            {
                LogService.WriteInfoLog(logTitle, "试图修改为空的UserGene实体!");
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
        public UserGene Get(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                LogService.WriteInfoLog(logTitle, "试图查找ID为空的UserGene实体!");
                return null;
            }
            using (DbContext db = new CRDatabase())
            {
                HR_USERGENE entity = db.Set<HR_USERGENE>().Find(id);
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
                LogService.WriteInfoLog(logTitle, "试图删除ID为空的UserGene实体!");
                return false;
            }
            using (DbContext db = new CRDatabase())
            {
                HR_USERGENE entity = db.Set<HR_USERGENE>().Find(id);
                if (entity != null)
                {
                    db.Set<HR_USERGENE>().Remove(entity);
                }
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 根据用户ID获取基因
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<UserGene> GetList(string userID)
        {
            List<UserGene> list = new List<UserGene>();
            if (string.IsNullOrEmpty(userID)) return list;
            using (DbContext db = new CRDatabase())
            {
                var entityList = db.Set<HR_USERGENE>().AsNoTracking().Where(o => o.USERID.Equals(userID));
                foreach (HR_USERGENE entity in entityList)
                {
                    UserGene userGene = EntityToModel(entity);
                    userGene.GeneModel = string.IsNullOrEmpty(entity.GENEID) ? null : new GeneService().Get(entity.GENEID);
                    userGene.Allele1Model = string.IsNullOrEmpty(entity.ALLELE1ID) ? null : new GeneAlleleService().Get(entity.ALLELE1ID);
                    userGene.Allele2Model = string.IsNullOrEmpty(entity.ALLELE2ID) ? null : new GeneAlleleService().Get(entity.ALLELE2ID);
                    list.Add(userGene);
                }
            }
            return list;
        }

        private HR_USERGENE ModelToEntity(UserGene model)
        {
            if (model == null) return null;
            return new HR_USERGENE()
            {
                ID = model.ID,
                USERID = model.UserID,
                GENEID = model.GeneID,
                ALLELE1ID = model.Allele1ID,
                ALLELE2ID = model.Allele2ID,
                CREATEBY = model.CreateBy,
                CREATETIME = model.CreateTime,
                MODIFYBY = model.ModifyBy,
                MODIFYTIME = model.ModifyTime,
                COPYNUMBER1=model.CopyNumber1,
                COPYNUMBER2=model.CopyNumber2
            };
        }

        private UserGene EntityToModel(HR_USERGENE entity)
        {
            if (entity == null) return null;
            return new UserGene()
            {
                ID = entity.ID,
                UserID = entity.USERID,
                GeneID = entity.GENEID,
                Allele1ID = entity.ALLELE1ID,
                Allele2ID = entity.ALLELE2ID,
                CreateBy = entity.CREATEBY,
                CreateTime = entity.CREATETIME,
                ModifyBy = entity.MODIFYBY,
                ModifyTime = entity.MODIFYTIME,
                CopyNumber1=entity.COPYNUMBER1,
                CopyNumber2=entity.COPYNUMBER2
            };
        }
    }
}
