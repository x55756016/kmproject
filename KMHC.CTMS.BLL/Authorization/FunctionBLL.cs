/*
 * 描述:定义功能实体
 *  
 * 修订历史: 
 * 日期          修改人              内容
 * 20151225      张志明              创建 
 *  
 */

using KMHC.CTMS.Common;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.Authorization;
using KMHC.CTMS.Model.Common;
using KMHC.CTMS.Model.PrecisionMedicine;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.BLL.Authorization
{
    public class FunctionBLL : BaseBLL
    {
          private readonly string logTitle = "访问FunctionBLL类";
        public FunctionBLL()
        {

        }

        /// <summary>
        /// 新增功能
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Add(Function model)
        {
            if (model == null) return string.Empty;
            using (DbContext db = new CRDatabase())
            {
                model.FunctionID = Guid.NewGuid().ToString();
                db.Set<CTMS_SYS_FUNCTION>().Add(ModelToEntity(model));
               
                db.SaveChanges();
                return model.FunctionID;
            }
        }

        /// <summary>
        /// 修改功能
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(Function model)
        {
            if (string.IsNullOrEmpty(model.FunctionID))
            {
                LogService.WriteInfoLog(logTitle, "试图修改为空的Function实体!");
                throw new KeyNotFoundException();
            }
            using (DbContext db = new CRDatabase())
            {
                db.Entry(ModelToEntity(model)).State = EntityState.Modified;
               
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 删除功能
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                LogService.WriteInfoLog(logTitle, "试图删除为空的Function实体!");
                throw new KeyNotFoundException();
            }
            Function model = Get(id);
            if (model != null)
            {
                model.IsDeleted = true;
                return Edit(model);
            }
            return false;
        }


        /// <summary>
        /// 根据ID获取功能
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public Function Get(string id)
        {
            using (DbContext db = new CRDatabase())
            {
                CTMS_SYS_FUNCTION entity = db.Set<CTMS_SYS_FUNCTION>().Find(id);
                if (entity == null || string.IsNullOrEmpty(entity.FUNCTIONID)) return null;
                return EntityToModel(entity);
            }
        }

        /// <summary>
        /// 获取查询列表
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<Function> GetList(string keyWord)
        {
            using (DbContext db = new CRDatabase())
            {
                IEnumerable<CTMS_SYS_FUNCTION> query = null;
                if (!string.IsNullOrEmpty(keyWord))
                {
                    query = db.Set<CTMS_SYS_FUNCTION>().AsNoTracking().Where(o => !o.ISDELETED && (o.FUNCTIONNAME.Contains(keyWord) || o.FUNCTIONCODE.Contains(keyWord))).ToList();
                }
                else
                {
                    query = db.Set<CTMS_SYS_FUNCTION>().AsNoTracking().Where(o => !o.ISDELETED).ToList();
                }
                List<Function> list = (from m in query select EntityToModel(m)).ToList();
                return list;
            }
        }

        /// <summary>
        /// 获取查询列表
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<Function> GetList(Expression<Func<CTMS_SYS_FUNCTION, bool>> predicate = null)
        {
            using (DbContext db = new CRDatabase())
            {
                IEnumerable<CTMS_SYS_FUNCTION> query = null;

                query = db.Set<CTMS_SYS_FUNCTION>().AsNoTracking().Where(predicate).ToList();
                List<Function> list = (from m in query select EntityToModel(m)).ToList();
                return list;
            }
        }

        /// <summary>
        /// 根据userID获取有权限的功能点
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="isMenu">是否为菜单</param>
        /// <returns></returns>
        public List<Function> GetAuthorizedList(string userID ,bool isMenu=false)
        {
            using (DbContext db = new CRDatabase())
            {
                if (string.IsNullOrEmpty(userID))
                    return new List<Function>();

                var RoleIDList = db.Set<CTMS_SYS_USERROLE>()
                    .AsNoTracking()
                    .Where(o => !o.ISDELETED && o.USERID.Equals(userID))
                    .Select(o => o.ROLEID)
                    .ToList();

                var FunctionIDList = db.Set<CTMS_SYS_ROLEFUNCTION>()
                    .AsNoTracking()
                    .Where(o => !o.ISDELETED && RoleIDList.Contains(o.ROLEID))
                    .Select(o => o.FUNCTIONID).ToList();

                return db.Set<CTMS_SYS_FUNCTION>().AsNoTracking()
                     .Where(o => isMenu && !o.ISDELETED && (o.ISPUBLIC || FunctionIDList.Contains(o.FUNCTIONID)))
                     .OrderBy(m => m.SORT)
                     .Select(EntityToModel)
                     .ToList();
            }
        }


        private CTMS_SYS_FUNCTION ModelToEntity(Function model)
        {
            if (model == null) return null;
            return new CTMS_SYS_FUNCTION()
            {
                FUNCTIONID = string.IsNullOrEmpty(model.FunctionID) ? Guid.NewGuid().ToString() : model.FunctionID,
                FUNCTIONNAME = model.FunctionName,
                FUNCTIONCODE = model.FunctionCode,
                PARENTID = model.ParentID,
                STATUS = (int)model.Status,
                ISMENU = model.IsMenu,
                MENUNAME = (model.IsMenu && model.Menu != null) ? model.Menu.Name : "",
                MENUCODE = (model.IsMenu && model.Menu != null) ? model.Menu.Code : "",
                MENUICON = (model.IsMenu && model.Menu != null) ? model.Menu.Icon : "",
                MENUURL = (model.IsMenu && model.Menu != null) ? model.Menu.Url : "",
                ISEXPAND = (model.IsMenu && model.Menu != null) ? model.Menu.IsExpand : false,
                ISPUBLIC = model.IsPublic,
                HELPERTITLE = model.HelperTitle,
                HELPERURL = model.HelperUrl,
                REMARK = model.Remark,
                SORT = model.Sort,
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

        private Function EntityToModel(CTMS_SYS_FUNCTION entity)
        {
            if (entity == null) return null;
            return new Function()
            {
                FunctionID = entity.FUNCTIONID,
                FunctionName = entity.FUNCTIONNAME,
                FunctionCode = entity.FUNCTIONCODE,
                ParentID = entity.PARENTID,
                Status = (FunctionStatus)entity.STATUS,
                IsMenu = entity.ISMENU,
                Menu = entity.ISMENU ? new MenuInfo()
                {
                    ID = entity.FUNCTIONID,
                    Code = entity.MENUCODE,
                    Icon = entity.MENUICON,
                    IsExpand = entity.ISEXPAND,
                    Name = entity.MENUNAME,
                    Url = entity.MENUURL,
                    ParentID = entity.PARENTID,
                    Sort = entity.SORT
                } : null,
                IsPublic = entity.ISPUBLIC,
                HelperTitle = entity.HELPERTITLE,
                HelperUrl = entity.HELPERURL,
                Remark = entity.REMARK,
                Sort = entity.SORT,
                SystemCategory = entity.SYSTEMCATEGORY,

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
