/*
 * 描述:定义xxx作为Aggerate的根对外提供访问的接口
 *  
 * 修订历史: 
 * 日期          修改人              内容
 * 20151102      张志明              创建 
 *  
 */

using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.PrecisionMedicine;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.BLL
{
    public class BaseBLL
    {
        public BaseBLL()
        {

        }

        /// <summary>
        /// 获取表的最大ID(自增长)
        /// </summary>
        /// <param name="table"></param>
        /// <param name="keyId"></param>
        /// <returns></returns>
        public virtual int GetMaxId(string table, string keyId)
        {
            return ExcuteScalar<int>(string.Format("select {0}_{1}.nextval from dual ", table, keyId));
        }

        /// <summary>
        /// 查询第一行第一列
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public virtual T ExcuteScalar<T>(string sql)
        {
            using (DbContext db = new CRDatabase())
            {
                return db.Database.SqlQuery<T>(sql).FirstOrDefault();
            }
        }

        /// <summary>
        /// 固定字段赋值-将数据库实体转化为模型
        /// </summary>
        /// <typeparam name="M"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <param name="model"></param>
        /// <param name="entity"></param>
        public virtual void EntityToModel<E, M>(E entity,M model) where M : BaseModel
        {
            if (entity == null || model == null) return;
            Type type = typeof(E);
            //创建者ID
            PropertyInfo pCreateUserID = type.GetProperty("CREATEUSERID");
            if (pCreateUserID != null)
            {
                model.CreateUserID = pCreateUserID.GetValue(entity, null) as string;
            }
            //创建者姓名
            PropertyInfo pCreateUserName = type.GetProperty("CREATEUSERNAME");
            if (pCreateUserName != null)
            {
                model.CreateUserName = pCreateUserName.GetValue(entity, null) as string;
            }
            //创建时间
            PropertyInfo pCreateDateTime = type.GetProperty("CREATEDATETIME");
            if (pCreateDateTime != null)
            {
                model.CreateDateTime = pCreateDateTime.GetValue(entity, null) as DateTime?;
            }
            //修改者ID
            PropertyInfo pEditUserID = type.GetProperty("EDITUSERID");
            if (pEditUserID != null)
            {
                model.EditUserID = pEditUserID.GetValue(entity, null) as string;
            }
            //修改者姓名
            PropertyInfo pEditUserName = type.GetProperty("EDITUSERNAME");
            if (pEditUserName != null)
            {
                model.EditUserName = pEditUserName.GetValue(entity, null) as string;
            }
            //修改时间
            PropertyInfo pEditTime = type.GetProperty("EDITTIME");
            if (pEditTime != null)
            {
                model.EditTime = pEditTime.GetValue(entity, null) as DateTime?;
            }
            //所有者ID
            PropertyInfo pOwnerID = type.GetProperty("OWNERID");
            if (pOwnerID != null)
            {
                model.OwnerID = pOwnerID.GetValue(entity, null) as string;
            }
            //所有者姓名
            PropertyInfo pOwnerName = type.GetProperty("OWNERNAME");
            if (pOwnerName != null)
            {
                model.OwnerName = pOwnerName.GetValue(entity, null) as string;
            }
            //是否删除
            PropertyInfo pIsDeleted = type.GetProperty("ISDELETE"); //bool
            if (pIsDeleted != null)
            {
                model.IsDeleted = (bool)pIsDeleted.GetValue(entity, null);
            }
        }

        /// <summary>
        /// 固定字段赋值-将模型转化为数据库实体
        /// </summary>
        /// <typeparam name="M"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <param name="model"></param>
        /// <param name="entity"></param>
        public virtual void ModelToEntity<M, E>(M model,E entity) where M : BaseModel
        {
            Type type = typeof(E);
            //创建者ID
            PropertyInfo pCreateUserID = type.GetProperty("CREATEUSERID");
            if (pCreateUserID != null)
            {
                pCreateUserID.SetValue(entity, model.CreateUserID);
            }
            //创建者姓名
            PropertyInfo pCreateUserName = type.GetProperty("CREATEUSERNAME");
            if (pCreateUserName != null)
            {
                pCreateUserName.SetValue(entity, model.CreateUserName);
            }
            //创建时间
            PropertyInfo pCreateDateTime = type.GetProperty("CREATEDATETIME");
            if (pCreateDateTime != null)
            {
                pCreateDateTime.SetValue(entity, model.CreateDateTime);
            }
            //修改者ID
            PropertyInfo pEditUserID = type.GetProperty("EDITUSERID");
            if (pEditUserID != null)
            {
                pEditUserID.SetValue(entity, model.EditUserID);
            }
            //修改者姓名
            PropertyInfo pEditUserName = type.GetProperty("EDITUSERNAME");
            if (pEditUserName != null)
            {
                pEditUserName.SetValue(entity, model.EditUserName);
            }
            //修改时间
            PropertyInfo pEditTime = type.GetProperty("EDITTIME");
            if (pEditTime != null)
            {
                pEditTime.SetValue(entity, model.EditTime);
            }
            //所有者ID
            PropertyInfo pOwnerID = type.GetProperty("OWNERID");
            if (pOwnerID != null)
            {
                pOwnerID.SetValue(entity, model.OwnerID);
            }
            //所有者姓名
            PropertyInfo pOwnerName = type.GetProperty("OWNERNAME");
            if (pOwnerName != null)
            {
                pOwnerName.SetValue(entity, model.OwnerName);
            }
            //是否删除
            PropertyInfo pIsDeleted = type.GetProperty("ISDELETE"); //bool
            if (pIsDeleted != null)
            {
                pIsDeleted.SetValue(entity, model.IsDeleted);
            }
        }
    }
}
