/*
 * 描述:定义功能权限关系
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
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.BLL.Authorization
{
    public class FunctionPermissionBLL : BaseBLL
    {
          private readonly string logTitle = "访问FunctionPermissionBLL类";
        public FunctionPermissionBLL()
        {

        }

        /// <summary>
        /// 新增功能权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Add(FunctionPermission model)
        {
            if (model == null) return string.Empty;
            using (DbContext db = new CRDatabase())
            {
                model.FunctionPermissionID = Guid.NewGuid().ToString();
                db.Set<CTMS_SYS_FUNCTIONPERMISSION>().Add(ModelToEntity(model));
                db.SaveChanges();
                return model.FunctionPermissionID;
            }
        }

        /// <summary>
        /// 修改功能权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(FunctionPermission model)
        {
            if (string.IsNullOrEmpty(model.FunctionPermissionID))
            {
                LogService.WriteInfoLog(logTitle, "试图修改为空的FunctionPermission实体!");
                throw new KeyNotFoundException();
            }
            using (DbContext db = new CRDatabase())
            {
                db.Entry(ModelToEntity(model)).State = EntityState.Modified;
                
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 删除功能权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                LogService.WriteInfoLog(logTitle, "试图删除为空的FunctionPermission实体!");
                throw new KeyNotFoundException();
            }
            FunctionPermission model = Get(id);
            if (model != null)
            {
                model.IsDeleted = true;
                return Edit(model);
            }
            return false;
        }


        /// <summary>
        /// 根据ID获取功能权限
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public FunctionPermission Get(string id)
        {
            using (DbContext db = new CRDatabase())
            {
                CTMS_SYS_FUNCTIONPERMISSION entity = db.Set<CTMS_SYS_FUNCTIONPERMISSION>().Find(id);
                if (entity == null || string.IsNullOrEmpty(entity.FUNCTIONPERMISSIONID)) return null;
                return EntityToModel(entity);
            }
        }

        /// <summary>
        /// 获取查询列表
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<FunctionPermission> GetList()
        {
            using (DbContext db = new CRDatabase())
            {
                IEnumerable<CTMS_SYS_FUNCTIONPERMISSION> query = db.Set<CTMS_SYS_FUNCTIONPERMISSION>().AsNoTracking().Where(o => !o.ISDELETED).ToList();
                List<FunctionPermission> list = (from m in query select EntityToModel(m)).ToList();
                return list;
            }
        }


        private CTMS_SYS_FUNCTIONPERMISSION ModelToEntity(FunctionPermission model)
        {
            if (model == null) return null;
            return new CTMS_SYS_FUNCTIONPERMISSION()
            {
                FUNCTIONPERMISSIONID = string.IsNullOrEmpty(model.FunctionPermissionID) ? Guid.NewGuid().ToString() : model.FunctionPermissionID,
                FUNCTIONID = model.FunctionID,
                PERMISSIONID = model.PermissionID,
                PERMISSIONVALUE = model.PermissionValue,
                CONTROLLERNAME = model.ControllerName,
                ACTIONNAME = model.ActionName,
                REMARK = model.Remark,

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

        private FunctionPermission EntityToModel(CTMS_SYS_FUNCTIONPERMISSION entity)
        {
            if (entity == null) return null;
            return new FunctionPermission()
            {
                FunctionPermissionID = entity.FUNCTIONPERMISSIONID,
                FunctionID = entity.FUNCTIONID,
                PermissionID = entity.PERMISSIONID,
                PermissionValue = entity.PERMISSIONVALUE,
                ControllerName = entity.CONTROLLERNAME,
                ActionName = entity.ACTIONNAME,
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
