/*
 * 描述:定义xxx作为Aggerate的根对外提供访问的接口
 *  
 * 修订历史: 
 * 日期          修改人              内容
 * 20151102      张志明              创建 
 *  
 */

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
    public class MyHouseKeeperBLL:BaseBLL
    {
        private readonly string logTitle = "访问MyHouseKeeperBLL类";
        public MyHouseKeeperBLL()
        {

        }

        /// <summary>
        /// 新增管家
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Add(MyHouseKeeper model)
        {
            if (model == null) return string.Empty;
            using (DbContext db = new CRDatabase())
            {
                db.Set<CTMS_MYHOUSEKEEPER>().Add(ModelToEntity(model));
                db.SaveChanges();
                return model.ID;
            }
        }

        /// <summary>
        /// 修改管家
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(MyHouseKeeper model)
        {
            if (string.IsNullOrEmpty(model.ID))
            {
                LogService.WriteInfoLog(logTitle, "试图修改为空的MyHouseKeeper实体!");
                throw new KeyNotFoundException();
            }
            using (DbContext db = new CRDatabase())
            {
                db.Entry(ModelToEntity(model)).State = EntityState.Modified;
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 删除管家
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                LogService.WriteInfoLog(logTitle, "试图删除为空的MyHouseKeeper实体!");
                throw new KeyNotFoundException();
            }
            MyHouseKeeper model = Get(id);
            if (model != null)
            {
                model.IsDeleted = true;
                return Edit(model);
            }
            return false;
        }


        /// <summary>
        /// 根据ID获取管家
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public MyHouseKeeper Get(string id)
        {
            using (DbContext db = new CRDatabase())
            {
                if (string.IsNullOrEmpty(id)) return null;
                CTMS_MYHOUSEKEEPER entity = db.Set<CTMS_MYHOUSEKEEPER>().Find(id);
                if (entity == null) return null;
                return EntityToModel(entity);
            }
        }

        /// <summary>
        /// 获取查询列表
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<MyHouseKeeper> GetList(string userID)
        {
            using (DbContext db = new CRDatabase())
            {
               if(string.IsNullOrEmpty(userID)) return new List<MyHouseKeeper>();
               var user=db.Set<CTMS_SYS_USERINFO>().Find(userID);
               if(user==null)  return new List<MyHouseKeeper>();
               var query = db.Set<CTMS_MYHOUSEKEEPER>().AsNoTracking().Where(o => !o.ISDELETED && o.USERID.Equals(userID)).ToList();
               if (query.Count == 0)   //Todo 临时取第一个医生和客服
               {
                   List<MyHouseKeeper> list = new List<MyHouseKeeper>();
                   var doctor = db.Set<CTMS_SYS_USERINFO>().FirstOrDefault(o => !o.ISDELETED && o.USERTYPE == 1);  //医生
                   var customer = db.Set<CTMS_SYS_USERINFO>().FirstOrDefault(o => !o.ISDELETED && o.USERTYPE == 4); //客户
                   if (doctor != null)
                   {
                       list.Add(new MyHouseKeeper()
                       {
                           ID = Guid.NewGuid().ToString(),
                           LoginName = user.LOGINNAME,
                           ObjectLoginName = doctor.LOGINNAME,
                           ObjectUserID = doctor.USERID,
                           ObjectType = "医生",
                           UserID = user.USERID,
                       });
                   }
                   if (customer != null)
                   {
                       list.Add(new MyHouseKeeper()
                       {
                           ID = Guid.NewGuid().ToString(),
                           LoginName = user.LOGINNAME,
                           ObjectLoginName = customer.LOGINNAME,
                           ObjectUserID = customer.USERID,
                           ObjectType = "客服",
                           UserID = user.USERID,
                       });
                   }
                   return list;
               }
               else
               {
                   return query.Select(EntityToModel).ToList();
               }
            }
        }

        private CTMS_MYHOUSEKEEPER ModelToEntity(MyHouseKeeper model)
        {
            if (model == null) return null;
            return new CTMS_MYHOUSEKEEPER()
            {
                ID = string.IsNullOrEmpty(model.ID) ? Guid.NewGuid().ToString() : model.ID,
                LOGINNAME = model.LoginName,
                OBJECTLOGINNAME = model.ObjectLoginName,
                OBJECTTYPE = model.ObjectType,
                OBJECTUSERID = model.ObjectUserID,
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

        public MyHouseKeeper EntityToModel(CTMS_MYHOUSEKEEPER entity)
        {
            if (entity == null) return null;
            return new MyHouseKeeper()
            {
                ID = entity.ID,
                LoginName = entity.LOGINNAME,
                ObjectLoginName = entity.OBJECTLOGINNAME,
                ObjectType = entity.OBJECTTYPE,
                ObjectUserID = entity.OBJECTUSERID,
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
