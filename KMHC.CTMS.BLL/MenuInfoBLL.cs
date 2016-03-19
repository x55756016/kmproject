/*
 * 描述:定义xxx作为Aggerate的根对外提供访问的接口
 *  
 * 修订历史: 
 * 日期          修改人              内容
 * 20151102      张志明              创建 
 *  
 */

using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.BLL
{
    public class MenuInfoBLL : BaseBLL
    {
        public MenuInfoBLL()
        {

        }

        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Add(MenuInfo model)
        {
            if (model == null) return string.Empty;
            using (DbContext db = new CRDatabase())
            {
                model.ID = Guid.NewGuid().ToString();
                db.Set<CTMS_SYS_FUNCTION>().Add(ModelToEntity(model));

                db.SaveChanges();
                return model.ID;
            }
        }

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(MenuInfo model)
        {
            if (string.IsNullOrEmpty(model.ID))
            {
                LogService.WriteInfoLog("访问MenuInfoBLL类", "试图修改为空的Function实体!");
                throw new KeyNotFoundException();
            }
            using (DbContext db = new CRDatabase())
            {
                db.Entry(ModelToEntity(model)).State = EntityState.Modified;
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 获取查询列表
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<MenuInfo> GetList(string userID)
        {
            using (DbContext db = new CRDatabase())
            {
                if (string.IsNullOrEmpty(userID))
                    return db.Set<CTMS_SYS_FUNCTION>().AsNoTracking()
                    .Where(o => o.ISMENU && !o.ISDELETED && string.IsNullOrEmpty(o.PARENTID) && o.ISPUBLIC)
                    .OrderBy(m => m.SORT).ToList()
                    .Select(m => EntityToModel(m))
                    .ToList();

                var RoleIDList = db.Set<CTMS_SYS_USERROLE>()
                    .AsNoTracking()
                    .Where(o => !o.ISDELETED && o.USERID.Equals(userID))
                    .Select(o => o.ROLEID)
                    .ToList();

                var FunctionIDList = db.Set<CTMS_SYS_ROLEFUNCTION>()
                    .AsNoTracking()
                    .Where(o => !o.ISDELETED && RoleIDList.Contains(o.ROLEID))
                    .Select(o => o.FUNCTIONID).ToList();

                var ParentFunctionIDList = db.Set<CTMS_SYS_FUNCTION>()
                    .AsNoTracking()
                    .Where(o => o.ISMENU && !o.ISDELETED && !string.IsNullOrEmpty(o.PARENTID) && FunctionIDList.Contains(o.FUNCTIONID))
                    .Select(o => o.PARENTID).ToList();

                var query = db.Set<CTMS_SYS_FUNCTION>().AsNoTracking()
                    .Where(o => o.ISMENU && !o.ISDELETED && (o.ISPUBLIC || FunctionIDList.Contains(o.FUNCTIONID) || ParentFunctionIDList.Contains(o.FUNCTIONID)))
                    .OrderBy(m => m.SORT)
                    .ToList();

                List<MenuInfo> list = new List<MenuInfo>();
                foreach (CTMS_SYS_FUNCTION entity in query.Where(o => string.IsNullOrEmpty(o.PARENTID)))
                {
                    MenuInfo model = EntityToModel(entity);
                    model.ChildrenList = query.Where(o => o.PARENTID != null && o.PARENTID.Equals(entity.FUNCTIONID)).Select(EntityToModel).ToList();
                    list.Add(model);
                }
                return list;
            }
        }

        /// <summary>
        /// 获取查询列表
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<MenuInfo> GetList()
        {
            using (DbContext db = new CRDatabase())
            {
                var query = db.Set<CTMS_SYS_FUNCTION>().AsNoTracking()
                     .Where(o => o.ISMENU && !o.ISDELETED && string.IsNullOrEmpty(o.PARENTID))
                     .OrderBy(m => m.SORT).ToList();

                List<MenuInfo> list = new List<MenuInfo>();
                foreach (CTMS_SYS_FUNCTION entity in query)
                {
                    MenuInfo model = EntityToModel(entity);
                    model.ChildrenList = GetChildrenList(model.ID);
                    list.Add(model);
                }
                return list;
            }
        }

        /// <summary>
        /// 获取查询列表
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<MenuInfo> GetList(Expression<Func<CTMS_SYS_FUNCTION, bool>> predicate)
        {
            using (DbContext db = new CRDatabase())
            {
                var query = db.Set<CTMS_SYS_FUNCTION>().AsNoTracking()
                     .Where(predicate);

                List<MenuInfo> list = query.Select(EntityToModel).ToList(); 

                return list;
            }
        }

        /// <summary>
        /// 获取查询列表
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public MenuInfo GetOne(string ID)
        {
            using (DbContext db = new CRDatabase())
            {
                CTMS_SYS_FUNCTION entity = db.Set<CTMS_SYS_FUNCTION>().Find(ID);
                if (entity == null || string.IsNullOrEmpty(entity.FUNCTIONID)) return null;
                return EntityToModel(entity);
            }
        }

        /// <summary>
        /// 获取菜单选择器查询列表
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<MenuInfo> GetChildrenList(string patherID)
        {
            using (DbContext db = new CRDatabase())
            {
                //Todo 权限过滤
                var query = db.Set<CTMS_SYS_FUNCTION>().AsNoTracking().Where(o => o.ISMENU && !o.ISDELETED && o.PARENTID.Equals(patherID)).OrderBy(m => m.SORT).ToList();
                return query.Select(m => EntityToModel(m)).ToList();
            }
        }

        private MenuInfo EntityToModel(CTMS_SYS_FUNCTION entity)
        {
            if (entity == null) return null;
            return new MenuInfo()
            {
                ID = entity.FUNCTIONID,
                Name = entity.MENUNAME,
                Code = entity.MENUCODE,
                Icon = entity.MENUICON,
                ParentID = entity.PARENTID,
                IsExpand = entity.ISEXPAND,
                Url = entity.MENUURL,
                Sort = entity.SORT
            };
        }

        private CTMS_SYS_FUNCTION ModelToEntity(MenuInfo model)
        {
            if (model == null) return null;
            return new CTMS_SYS_FUNCTION()
            {
                FUNCTIONID = model.ID,
                FUNCTIONCODE=model.Code,
                FUNCTIONNAME=model.Name,
                MENUNAME = model.Name,
                MENUCODE = model.Code,
                MENUICON = model.Icon,
                PARENTID = model.ParentID,
                ISEXPAND = model.IsExpand,
                MENUURL = model.Url,
                SORT = model.Sort
            };
        }
    }
}
