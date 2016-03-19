/*
 * 描述:定义角色功能权限
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
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace KMHC.CTMS.BLL.Authorization
{
    public class RoleFunctionBLL : BaseBLL
    {
        private readonly string logTitle = "访问RoleFunctionBLL类";
        public RoleFunctionBLL()
        {

        }

        /// <summary>
        /// 新增角色功能权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Add(RoleFunction model)
        {
            if (model == null) return string.Empty;
            using (DbContext db = new CRDatabase())
            {
                model.RoleFunctionID = Guid.NewGuid().ToString();
                db.Set<CTMS_SYS_ROLEFUNCTION>().Add(ModelToEntity(model));

                db.SaveChanges();
                return model.RoleFunctionID;
            }
        }

        /// <summary>
        /// 修改角色功能权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(RoleFunction model)
        {
            if (string.IsNullOrEmpty(model.RoleFunctionID))
            {
                LogService.WriteInfoLog(logTitle, "试图修改为空的RoleFunction实体!");
                throw new KeyNotFoundException();
            }
            using (DbContext db = new CRDatabase())
            {
                db.Entry(ModelToEntity(model)).State = EntityState.Modified;

                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 删除角色功能权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                LogService.WriteInfoLog(logTitle, "试图删除为空的RoleFunction实体!");
                throw new KeyNotFoundException();
            }
            RoleFunction model = Get(id);
            if (model != null)
            {
                model.IsDeleted = true;
                return Edit(model);
            }
            return false;
        }

        /// <summary>
        /// 删除角色功能权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool DeleteById(string id)
        {
            using (DbContext db = new CRDatabase())
            {
                var entity = db.Set<CTMS_SYS_ROLEFUNCTION>().Find(id);
                if (entity != null)
                {
                    db.Set<CTMS_SYS_ROLEFUNCTION>().Remove(entity);
                }
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public RoleFunction GetOne(Expression<Func<CTMS_SYS_ROLEFUNCTION, bool>> predicate = null)
        {
            using (DbContext db = new CRDatabase())
            {
                var set = db.Set<CTMS_SYS_ROLEFUNCTION>().AsNoTracking();
                return (predicate == null)
                    ? EntityToModel(set.FirstOrDefault())
                    : EntityToModel(set.FirstOrDefault(predicate));
            }
        }

        /// <summary>
        /// 根据ID获取
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public RoleFunction Get(string id)
        {
            using (DbContext db = new CRDatabase())
            {
                CTMS_SYS_ROLEFUNCTION entity = db.Set<CTMS_SYS_ROLEFUNCTION>().Find(id);
                if (entity == null || string.IsNullOrEmpty(entity.ROLEFUNCTIONID)) return null;
                return EntityToModel(entity);
            }
        }

        /// <summary>
        /// 获取查询列表
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<RoleFunction> GetList()
        {
            using (DbContext db = new CRDatabase())
            {
                IEnumerable<CTMS_SYS_ROLEFUNCTION> query = db.Set<CTMS_SYS_ROLEFUNCTION>().AsNoTracking().Where(o => !o.ISDELETED).ToList();
                List<RoleFunction> list = (from m in query select EntityToModel(m)).ToList();
                return list;
            }
        }

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<RoleFunction> GetList(Expression<Func<CTMS_SYS_ROLEFUNCTION, bool>> predicate = null)
        {
            using (DbContext db = new CRDatabase())
            {
                IEnumerable<CTMS_SYS_ROLEFUNCTION> query = db.Set<CTMS_SYS_ROLEFUNCTION>().AsNoTracking().Where(predicate).ToList();
                List<RoleFunction> list = (from m in query select EntityToModel(m)).ToList();
                return list;
            }
        }

        /// <summary>
        /// 根据userID获取有权限的功能点
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="isMenu">是否为菜单</param>
        /// <returns></returns>
        public List<RoleFunction> GetAuthorizedList(string userID)
        {
            using (DbContext db = new CRDatabase())
            {
                if (string.IsNullOrEmpty(userID))
                    return new List<RoleFunction>();

                var roleIDList = db.Set<CTMS_SYS_USERROLE>()
                    .AsNoTracking()
                    .Where(o => !o.ISDELETED && o.USERID.Equals(userID))
                    .Select(o => o.ROLEID)
                    .ToList();

                return db.Set<CTMS_SYS_ROLEFUNCTION>()
                    .AsNoTracking()
                    .Where(o => !o.ISDELETED && roleIDList.Contains(o.ROLEID))
                    .Select(EntityToModel)
                    .ToList();
            }
        }

        /// <summary>
        /// 根据用户ID，功能编码和权限类型 获取角色功能权限实体
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="authCode">权限编码</param>
        /// <param name="permission">权限类型</param>
        /// <returns></returns>
        public List<RoleFunction> GetAuthorizedList(string userID, string authCode, PermissionType permission)
        {
            List<RoleFunction> roleFunctionList = new List<RoleFunction>();
            if (string.IsNullOrEmpty(authCode) || string.IsNullOrEmpty(authCode))
                return roleFunctionList;
            using (DbContext db = new CRDatabase())
            {
                var function = db.Set<CTMS_SYS_FUNCTION>().AsNoTracking().Where(o => o.FUNCTIONCODE.Equals(authCode)).ToList();
                if (function == null || function.Count() == 0) return roleFunctionList;
                string functionid = function[0].FUNCTIONID;
                var roleIDList = db.Set<CTMS_SYS_USERROLE>()
                   .AsNoTracking()
                   .Where(o => !o.ISDELETED && o.USERID.Equals(userID))
                   .Select(o => o.ROLEID)
                   .ToList();

                roleFunctionList = db.Set<CTMS_SYS_ROLEFUNCTION>()
                    .AsNoTracking()
                    .Where
                    (
                        o => !o.ISDELETED 
                        && roleIDList.Contains(o.ROLEID)
                        && o.FUNCTIONID.Equals(functionid)
                        && o.PERMISSIONVALUE == (int)permission
                     )
                    .Select(EntityToModel)
                    .ToList();
                return roleFunctionList;
            }
        }

        /// <summary>
        /// 判断用户是否有权限
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="authCode">权限编码</param>
        /// <param name="permission">权限类型</param>
        /// <returns></returns>
        public bool IsHavePermission(string userID, string authCode, PermissionType permission)
        {
            return !string.IsNullOrEmpty(userID) && (string.IsNullOrEmpty(authCode) || GetAuthorizedList(userID, authCode, permission).Count > 0);
        }

        /// <summary>
        /// 获取范围筛选的过滤字符串和参数
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="authCode">权限编码</param>
        /// <param name="permission">权限类型</param>
        /// <param name="args">参数值列表</param>
        /// <returns></returns>
        public string GetFilterString(string userID, string authCode, PermissionType permission,ref ArrayList args)
        {
            List<RoleFunction> roleFunctionList = GetAuthorizedList(userID, authCode, permission);
            if (roleFunctionList.Count == 0)
            {
                return string.Empty;
            }
            else if (roleFunctionList.Count == 1)
            {
                return GetFilterByXml(roleFunctionList[0].DataRange, ref args);
            }
            else 
            {
                string filterString = string.Empty;
                foreach (RoleFunction roleFunction in roleFunctionList)
                {
                    string filterExpress=GetFilterByXml(roleFunction.DataRange,ref args);
                    if (string.IsNullOrEmpty(filterExpress)) continue;
                    filterString += string.Format("{0}({1})", string.IsNullOrEmpty(filterString) ? "" : " or", filterExpress);
                }
                return filterString;
            }
            
        }

        /// <summary>
        /// 根据xml获取过滤表达式
        /// </summary>
        /// <param name="xml">范围xml</param>
        /// <returns></returns>
        public string GetFilterByXml(string xml, ref ArrayList args)
        {
            if (string.IsNullOrEmpty(xml)) return " 1=1 ";
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(xml);

                if (doc == null) throw new Exception("错误的xml格式");
                XmlNodeList itemNodeList = doc.SelectNodes("/data/item");
                if (itemNodeList == null || itemNodeList.Count == 0) return " 1=1 ";
                string filterString = string.Empty;
                foreach (XmlNode node in itemNodeList)
                {
                    XmlElement xe = (XmlElement)node;
                    string relationship = xe.SelectNodes("relationship")[0].InnerXml;
                    string column = xe.SelectNodes("column")[0].InnerXml;
                    string operation = xe.SelectNodes("operation")[0].InnerXml;
                    string value = xe.SelectNodes("value")[0].InnerXml;
                    filterString += string.Format(" {0} {1}{2}{3}", string.IsNullOrEmpty(filterString) ? "" : relationship, column, operation, "@" + args.Count);
                    args.Add(value);
                }
                return filterString;
            }
            catch
            {
                throw;
            }
        }

        private CTMS_SYS_ROLEFUNCTION ModelToEntity(RoleFunction model)
        {
            if (model == null) return null;
            return new CTMS_SYS_ROLEFUNCTION()
            {
                ROLEFUNCTIONID = string.IsNullOrEmpty(model.RoleFunctionID) ? Guid.NewGuid().ToString() : model.RoleFunctionID,
                FUNCTIONID = model.FunctionID,
                PERMISSIONVALUE = model.PermissionValue,
                ROLEID = model.RoleID,
                REMARK = model.Remark,
                DATARANGE=model.DataRange,
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

        private RoleFunction EntityToModel(CTMS_SYS_ROLEFUNCTION entity)
        {
            if (entity == null) return null;
            return new RoleFunction()
            {
                RoleFunctionID = entity.ROLEFUNCTIONID,
                RoleID = entity.ROLEID,
                FunctionID = entity.FUNCTIONID,
                PermissionValue = entity.PERMISSIONVALUE,
                Remark = entity.REMARK,
                DataRange=entity.DATARANGE,
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
