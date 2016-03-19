/*
 * 描述:定义xxx作为Aggerate的根对外提供访问的接口
 *  
 * 修订历史: 
 * 日期          修改人              内容
 * 20151102      张志明              创建 
 *  
 */

using KMHC.CTMS.Common.Helper;
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
    public class MyQuestionBLL
    {
        private readonly string logTitle = "访问MyQuestionBLL类";
        public MyQuestionBLL()
        {

        }

        /// <summary>
        /// 新增咨询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Add(MyQuestion model)
        {
            if (model == null) return string.Empty;
            using (DbContext db = new CRDatabase())
            {
                db.Set<CTMS_MYQUESTION>().Add(ModelToEntity(model));
                //待办
                db.Set<CTMS_USEREVENT>().Add(new CTMS_USEREVENT()
                {
                    EVENTID = Guid.NewGuid().ToString(),
                    USERAPPLYID = "",
                    ACTIONTYPE = "1",
                    ACTIONINFO = string.Format("您收到了{0}的咨询，请回复", model.LoginName),
                    RECEIPTTIME = model.CreateDateTime,
                    ACTIONSTATUS = "1",
                    FROMUSER = model.UserID,
                    TOUSER = model.ObjectUserID,
                    CREATETIME = model.CreateDateTime,
                    MODELID = model.ID,
                    LINKURL = "MyQuestion"
                });
                db.SaveChanges();
                return model.ID;
            }
        }

        /// <summary>
        /// 修改咨询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(MyQuestion model,string eventID="")
        {
            if (string.IsNullOrEmpty(model.ID))
            {
                LogService.WriteInfoLog(logTitle, "试图修改为空的MyQuestion实体!");
                throw new KeyNotFoundException();
            }
            using (DbContext db = new CRDatabase())
            {
                db.Entry(ModelToEntity(model)).State = EntityState.Modified;
                if (!string.IsNullOrEmpty(eventID))
                {
                    CTMS_USEREVENT userEvent=db.Set<CTMS_USEREVENT>().Find(eventID);
                    if (userEvent != null)
                    {
                        userEvent.ACTIONSTATUS = "3";
                        userEvent.ENDTIME = model.EditTime;
                        db.Entry(userEvent).State = EntityState.Modified;
                    }
                }
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 删除咨询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                LogService.WriteInfoLog(logTitle, "试图删除为空的MyQuestion实体!");
                throw new KeyNotFoundException();
            }
            MyQuestion model = Get(id);
            if (model != null)
            {
                model.IsDeleted = true;
                return Edit(model);
            }
            return false;
        }


        /// <summary>
        /// 根据ID获取咨询
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public MyQuestion Get(string id)
        {
            using (DbContext db = new CRDatabase())
            {
                if (string.IsNullOrEmpty(id)) return null;
                CTMS_MYQUESTION entity = db.Set<CTMS_MYQUESTION>().Find(id);
                if (entity == null) return null;
                return EntityToModel(entity);
            }
        }

        /// <summary>
        /// 获取查询列表
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<MyQuestion> GetList(string keyWord, string userID = "")
        {
            using (DbContext db = new CRDatabase())
            {
                var query = db.Set<CTMS_MYQUESTION>().AsNoTracking().Where(o => !o.ISDELETED);
                if (!string.IsNullOrEmpty(userID)) 
                {
                    query = query.Where(o => o.USERID.Equals(userID));
                }
                if(!string.IsNullOrEmpty(keyWord))
                {
                    query=query.Where(o=>o.QUESTION.Contains(keyWord));
                }
                List<MyQuestion> list = query.ToList().Select(EntityToModel).ToList();
                return list;
            }
        }
        /// <summary>
        /// 获取查询列表
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<MyQuestion> GetList(string keyWord, ref PageInfo pager, string userID)
        {
            using (DbContext db = new CRDatabase())
            {
                var query = db.Set<CTMS_MYQUESTION>().AsNoTracking().Where(o => !o.ISDELETED);
                if (!string.IsNullOrEmpty(userID))
                {
                    query = query.Where(o => o.USERID.Equals(userID));
                }
                if (!string.IsNullOrEmpty(keyWord))
                {
                    query = query.Where(o => o.QUESTION.Contains(keyWord));
                }
                if (pager != null)
                {
                    query = query.Paging(ref pager);
                }
                List<MyQuestion> list = query.ToList().Select(EntityToModel).ToList();
                return list;
            }
        }

        private CTMS_MYQUESTION ModelToEntity(MyQuestion model)
        {
            if (model == null) return null;
            return new CTMS_MYQUESTION()
            {
                ID = string.IsNullOrEmpty(model.ID) ? Guid.NewGuid().ToString() : model.ID,
                LOGINNAME = model.LoginName,
                OBJECTLOGINNAME = model.ObjectLoginName,
                OBJECTTYPE = model.ObjectType,
                OBJECTUSERID = model.ObjectUserID,
                USERID = model.UserID,
                QUESTION = model.Question,
                ANSWER = model.Answer,

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

        private MyQuestion EntityToModel(CTMS_MYQUESTION entity)
        {
            if (entity == null) return null;
            return new MyQuestion()
            {
                ID = entity.ID,
                LoginName = entity.LOGINNAME,
                ObjectLoginName = entity.OBJECTLOGINNAME,
                ObjectType = entity.OBJECTTYPE,
                ObjectUserID = entity.OBJECTUSERID,
                UserID = entity.USERID,
                Question = entity.QUESTION,
                Answer = entity.ANSWER,

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
