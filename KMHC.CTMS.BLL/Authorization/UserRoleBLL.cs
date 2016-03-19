/*
 * 描述:定义用户角色关系实体
 *  
 * 修订历史: 
 * 日期          修改人              内容
 * 20151225      张志明              创建 
 *  
 */

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
    public class UserRoleBLL : BaseBLL
    {
        private readonly string logTitle = "访问UserRoleBLL类";
        public UserRoleBLL()
        {

        }

        /// <summary>
        /// 新增用户角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Add(UserRole model)
        {
            if (model == null) return string.Empty;
            using (DbContext db = new CRDatabase())
            {
                model.UserRoleID = Guid.NewGuid().ToString();
                db.Set<CTMS_SYS_USERROLE>().Add(ModelToEntity(model));

                db.SaveChanges();
                return model.UserRoleID;
            }
        }

        /// <summary>
        /// 修改用户角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(UserRole model)
        {
            if (string.IsNullOrEmpty(model.UserRoleID))
            {
                LogService.WriteInfoLog(logTitle, "试图修改为空的UserRole实体!");
                throw new KeyNotFoundException();
            }
            using (DbContext db = new CRDatabase())
            {
                db.Entry(ModelToEntity(model)).State = EntityState.Modified;

                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 修改用户角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateUserRole(string  userid, List<Role> roleList)
        {
            if (string.IsNullOrEmpty(userid) || roleList==null)
            {
                LogService.WriteInfoLog(logTitle, "用户ID或实体列表为空!");
                return false;
            }
            using (DbContext db = new CRDatabase())
            {
                List<CTMS_SYS_USERROLE> preUserRoleList=db.Set<CTMS_SYS_USERROLE>().Where(o => !o.ISDELETED && o.USERID.Equals(userid)).ToList();
                List<string> preRoleIDList=preUserRoleList.Select(p=>p.ROLEID).ToList();
                List<string> afterRoleIDList = roleList.Select(o => o.RoleID).ToList() ;
                foreach (CTMS_SYS_USERROLE entity in preUserRoleList)
                {
                    entity.ISDELETED = (preUserRoleList.Find(o => afterRoleIDList.Contains(entity.ROLEID))==null);
                    db.Entry(entity).State = EntityState.Modified;
                }
                foreach (string roleID in afterRoleIDList.Where(o => !preRoleIDList.Contains(o)))
                {
                    db.Set<CTMS_SYS_USERROLE>().Add(new CTMS_SYS_USERROLE() { USERROLEID = Guid.NewGuid().ToString(), ROLEID = roleID, USERID = userid });
                }
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 删除用户角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                LogService.WriteInfoLog(logTitle, "试图删除为空的UserRole实体!");
                throw new KeyNotFoundException();
            }
            UserRole model = Get(id);
            if (model != null)
            {
                model.IsDeleted = true;
                return Edit(model);
            }
            return false;
        }


        /// <summary>
        /// 根据ID获取用户角色
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public UserRole Get(string id)
        {
            using (DbContext db = new CRDatabase())
            {
                CTMS_SYS_USERROLE entity = db.Set<CTMS_SYS_USERROLE>().Find(id);
                if (entity == null || string.IsNullOrEmpty(entity.USERROLEID)) return null;
                return EntityToModel(entity);
            }
        }

        /// <summary>
        /// 根据条件获取用户角色
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public UserRole GetOne(Expression<Func<CTMS_SYS_USERROLE, bool>> predicate = null)
        {
            using (DbContext db = new CRDatabase())
            {
                CTMS_SYS_USERROLE entity = db.Set<CTMS_SYS_USERROLE>().FirstOrDefault(predicate);
                if (entity == null || string.IsNullOrEmpty(entity.USERROLEID)) return null;
                return EntityToModel(entity);
            }
        }

        /// <summary>
        /// 获取查询列表
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<UserRole> GetListByUserID(string userID)
        {
            using (DbContext db = new CRDatabase())
            {
                IEnumerable<CTMS_SYS_USERROLE> query = db.Set<CTMS_SYS_USERROLE>().AsNoTracking().Where(o => !o.ISDELETED && o.USERID.Equals(userID)).ToList();
                List<UserRole> list = (from m in query select EntityToModel(m)).ToList();
                return list;
            }
        }


        private CTMS_SYS_USERROLE ModelToEntity(UserRole model)
        {
            if (model == null) return null;
            return new CTMS_SYS_USERROLE()
            {
                USERROLEID = string.IsNullOrEmpty(model.UserRoleID) ? Guid.NewGuid().ToString() : model.UserRoleID,
                ROLEID = model.RoleID,
                USERID = model.UserID,

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

        private UserRole EntityToModel(CTMS_SYS_USERROLE entity)
        {
            if (entity == null) return null;
            return new UserRole()
            {
                UserRoleID = entity.USERROLEID,
                RoleID = entity.ROLEID,
                UserID = entity.USERID,

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
