/*
 * 描述:待办事项具体实现BLL
 *  
 * 修订历史: 
 * 日期            修改人              Email                   内容
 * 2015/12/31      邓柏生                                      创建 
 *  
 */

using KMHC.CTMS.Common;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.DAL.CancerProcess;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.CancerProcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace KMHC.CTMS.BLL.CancerProcess
{
    public class UserEventBLL
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Add(UserEvent model)
        {
            if (model == null)
                return string.Empty;

            using (UserEventDAL dal = new UserEventDAL())
            {
                CTMS_USEREVENT entity = ModelToEntity(model);
                entity.EVENTID = string.IsNullOrEmpty(model.EventID) ? Guid.NewGuid().ToString("N") : model.EventID;

                return dal.Add(entity);
            }
        }

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public UserEvent Get(Expression<Func<CTMS_USEREVENT, bool>> predicate = null)
        {
            using (UserEventDAL dal = new UserEventDAL())
            {
                return EntityToModel(dal.GetOne(predicate));
            }
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<UserEvent> GetList(PageInfo page, Expression<Func<CTMS_USEREVENT, bool>> predicate = null)
        {
            using (UserEventDAL dal = new UserEventDAL())
            {
                var list = dal.Get(predicate);

                return list.Paging(ref page).Select(EntityToModel).ToList();
            }
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<UserEvent> GetList(Expression<Func<CTMS_USEREVENT, bool>> predicate = null)
        {


            using (UserEventDAL dal = new UserEventDAL())
            {
                var list = dal.Get(predicate);

                return list.Select(EntityToModel).ToList();
            }




        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(UserEvent model)
        {
            if (model == null) return false;
            using (UserEventDAL dal = new UserEventDAL())
            {
                CTMS_USEREVENT entitys = ModelToEntity(model);

                return dal.Edit(entitys);
            }
        }

        /// <summary>
        /// 添加用户待办信息
        /// </summary>
        public void AddUserEvent(UserEvent model,UserType userType)
        {
            using (var context = new CRDatabase())
            {
                //1.根据UserTypetype找到人
               var toUserId = context.CTMS_SYS_USERINFO.FirstOrDefault(p => p.USERTYPE == (decimal)userType);

                //2.将事件类型ActionInfo插入表中
                model.ToUser = toUserId.USERID;
                context.CTMS_USEREVENT.Add(ModelToEntity(model));

                context.SaveChanges();
            }
        }


        /// <summary>
        /// 添加用户待办信息
        /// </summary>
        public void AddUserEvent(UserEvent model)
        {
            using (var context = new CRDatabase())
            {
                context.CTMS_USEREVENT.Add(ModelToEntity(model));
                context.SaveChanges();
            }
        }



        /// <summary>
        /// 将上面一条设置成已完成
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns>更新结果</returns>
        public bool CloseEvent(string eventId)
        {
            using (var context = new CRDatabase())
            {
                var entity = context.CTMS_USEREVENT.FirstOrDefault(p => p.EVENTID == eventId);
                if (entity==null)
                {
                    return false;
                }
                entity.ACTIONSTATUS = ((int)ActionStatus.Complete).ToString();
                entity.ENDTIME = System.DateTime.Now;
                return context.SaveChanges()>0;
            }
        }

        public bool CloseApply(string applyId)
        {
            using (var context = new CRDatabase())
            {
                var entity = context.CTMS_USERAPPLY.FirstOrDefault(p => p.ID == applyId);
                if (entity==null)
                {
                    return false;
                }
                entity.STATUS = "3";
                return context.SaveChanges()>0;
            }
        }

        /// <summary>
        /// Model转Entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private CTMS_USEREVENT ModelToEntity(UserEvent model)
        {
            if (model != null)
            {
                var entity = new CTMS_USEREVENT()
                {
                    EVENTID = model.EventID,
                    USERAPPLYID = model.UserApplyId,
                    ACTIONTYPE = model.ActionType,
                    ACTIONINFO = model.ActionInfo,
                    RECEIPTTIME = model.ReceiptTime,
                    ENDTIME = model.EndTime,
                    ACTIONSTATUS = model.ActionStatus,
                    FROMUSER = model.FromUser,
                    TOUSER = model.ToUser,
                    CREATETIME = model.CreateTime,
                    MODELTYPE = model.ModelType,
                    MODELID = model.ModelId,
                    REMARKS = model.Remarks,
                    LINKURL=model.LinkUrl
                };

                return entity;
            }
            return null;
        }

        /// <summary>
        /// Entity转Model
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private UserEvent EntityToModel(CTMS_USEREVENT entity)
        {
            if (entity != null)
            {
                var model = new UserEvent()
                {
                    EventID = entity.EVENTID,
                    UserApplyId = entity.USERAPPLYID,

                    ActionType =entity.ACTIONTYPE,

                    ActionInfo = entity.ACTIONINFO,
                    ReceiptTime = entity.RECEIPTTIME,
                    EndTime = entity.ENDTIME,

                    ActionStatus = entity.ACTIONSTATUS==null?"": EnumHelper.GetDescription((ActionStatus)int.Parse(entity.ACTIONSTATUS)),
                   
                    FromUser = entity.FROMUSER,
                    ToUser = entity.TOUSER,
                    CreateTime = entity.CREATETIME,
                    ModelType = entity.MODELTYPE,
                    ModelId = entity.MODELID,
                    Remarks = entity.REMARKS,
                    LinkUrl = entity.LINKURL
                };

                return model;
            }
            return null;
        }
    }
}
