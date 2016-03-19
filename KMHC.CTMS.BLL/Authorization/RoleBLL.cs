/*
 * 描述:定义角色实体
 *  
 * 修订历史: 
 * 日期          修改人              内容
 * 20151225      张志明              创建 
 *  
 */

using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.Authorization;
using KMHC.CTMS.Model.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.BLL.Authorization
{
    public class RoleBLL : BaseBLL
    {
          private readonly string logTitle = "访问RoleBLL类";
        public RoleBLL()
        {

        }

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Add(Role model)
        {
            if (model == null) return string.Empty;
            using (DbContext db = new CRDatabase())
            {
                model.RoleID = Guid.NewGuid().ToString();
                db.Set<CTMS_SYS_ROLE>().Add(ModelToEntity(model));
                
                db.SaveChanges();
                return model.RoleID;
            }
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(Role model)
        {
            if (string.IsNullOrEmpty(model.RoleID))
            {
                LogService.WriteInfoLog(logTitle, "试图修改为空的Role实体!");
                throw new KeyNotFoundException();
            }
            using (DbContext db = new CRDatabase())
            {
                db.Entry(ModelToEntity(model)).State = EntityState.Modified;
               
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                LogService.WriteInfoLog(logTitle, "试图删除为空的Role实体!");
                throw new KeyNotFoundException();
            }
            Role model = Get(id);
            if (model != null)
            {
                model.IsDeleted = true;
                return Edit(model);
            }
            return false;
        }


        /// <summary>
        /// 根据ID获取角色
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public Role Get(string id)
        {
            using (DbContext db = new CRDatabase())
            {
                CTMS_SYS_ROLE entity = db.Set<CTMS_SYS_ROLE>().Find(id);
                if (entity == null || string.IsNullOrEmpty(entity.ROLEID)) return null;
                return EntityToModel(entity);
            }
        }

        /// <summary>
        /// 获取查询列表
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<Role> GetList(string keyWord)
        {
            using (DbContext db = new CRDatabase())
            {
                IEnumerable<CTMS_SYS_ROLE> query = null;
                if (!string.IsNullOrEmpty(keyWord))
                {
                    query = db.Set<CTMS_SYS_ROLE>().AsNoTracking().Where(o => !o.ISDELETED && (o.ROLENAME.Contains(keyWord))).ToList();
                }
                else
                {
                    query = db.Set<CTMS_SYS_ROLE>().AsNoTracking().Where(o => !o.ISDELETED).ToList();
                }
                List<Role> list = (from m in query select EntityToModel(m)).ToList();
                return list;
            }
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<Role> GetPageData(PageInfo page, Expression<Func<CTMS_SYS_ROLE, bool>> predicate = null)
        {
            using (DbContext db = new CRDatabase())
            {
                IQueryable<CTMS_SYS_ROLE> query = null;
                query = db.Set<CTMS_SYS_ROLE>().AsNoTracking().Where(predicate);

                List<Role> list = query.Paging(ref page).Select(EntityToModel).ToList(); 
                return list;
            }
        }

        private CTMS_SYS_ROLE ModelToEntity(Role model)
        {
            if (model == null) return null;
            return new CTMS_SYS_ROLE()
            {
                ROLEID = string.IsNullOrEmpty(model.RoleID) ? Guid.NewGuid().ToString() : model.RoleID,
                ROLENAME = model.RoleName,
                REMARK = model.Remark,
                SYSTEMCATEGORY = model.SystemCategory,

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

        private Role EntityToModel(CTMS_SYS_ROLE entity)
        {
            if (entity == null) return null;
            return new Role()
            {
                RoleID = entity.ROLEID,
                RoleName = entity.ROLENAME,
                SystemCategory = entity.SYSTEMCATEGORY,
                Remark = entity.REMARK,

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
