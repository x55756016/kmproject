/*
 * 描述:医生控制页面
 *  
 * 修订历史: 
 * 日期               修改人              Email                  内容
 * 20151230   		  林德力         	takalin@qq.com   		 创建 
 *  
 */
using System;
using System.Collections.Generic;
using System.Linq;
using KMHC.CTMS.Common;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.CancerProcess;
using KMHC.CTMS.Model.Repository.Implement.CancerRecord;
using KMHC.CTMS.Model.Common;
using KMHC.CTMS.Model.PrecisionMedicine;

namespace KMHC.CTMS.BLL.CancerProcess
{
    public class DoctorControlBll
    {
        public string Add(UserApply model)
        {
            if (model == null)
                return string.Empty;

            if (string.IsNullOrEmpty(model.ID)) model.ID = Guid.NewGuid().ToString();
            using (var context = new CRDatabase())
            {
                context.Set<CTMS_USERAPPLY>().Add(ModelToEntity(model));
                context.SaveChanges();
                return model.ID;
            }
        }
        /// <summary>
        /// 获取该医生下的患者列表
        /// </summary>
        public List<UserApply> GetPersonList(PageInfo page ,string keyWord)
        {
            List<UserApply> list = new List<UserApply>();
            using (var context = new CRDatabase())
            {
                CancerUserInfoRepository repository = new CancerUserInfoRepository();
                var userlist = repository.FindAll().ToList();

                (from user in userlist
                 join userapply in context.CTMS_USERAPPLY on user.USERID equals userapply.USERID
                 where (string.IsNullOrEmpty(keyWord) || user.USERNAME.Contains(keyWord))
                 select
                     new
                     {
                         ID = userapply.APPLYID,
                         USERID = user.USERID,
                         NAME = user.USERNAME,
                         CURRENTNODE = userapply.CURRENTNODE,
                         ENTRYDATE = userapply.ENTRYDATE,
                         EXITDATE = userapply.EXITDATE
                     }
                 ).Skip((page.PageIndex - 1) * page.PageSize).Take(page.PageSize)
                 .ToList().ForEach(p =>
                 {
                     var firstOrDefault = context.CTMS_GUIDELINE.FirstOrDefault(k => k.ID == p.CURRENTNODE);
                     list.Add(new UserApply()
                     {
                         //ID = p.ID,
                         USERID = p.USERID,
                         Name = p.NAME,
                         CurrentName = firstOrDefault == null ? "" : firstOrDefault.NAME,
                         CURRENTNODE = p.CURRENTNODE,
                         ENTRYDATE = p.ENTRYDATE,
                         EXITDATE = p.EXITDATE
                     });
                 });

                //(from user in userlist
                //    join userapply in context.CTMS_USERAPPLY on user.USERID equals userapply.USERID into temp
                //    from tt in temp.DefaultIfEmpty()
                //    where ( string.IsNullOrEmpty(keyWord) || user.NAME.Contains(keyWord))
                //    select
                //        new
                //        {
                //            ID=tt==null?"":tt.ID,
                //            USERID = user.USERID,
                //            NAME = user.NAME,
                //            CURRENTNODE = tt == null ? "" : tt.CURRENTNODE,
                //            ENTRYDATE = tt == null ? null : tt.ENTRYDATE,
                //            EXITDATE = tt == null ? null : tt.EXITDATE
                //        }
                //    ).Skip((page.PageIndex - 1)*page.PageSize).Take(page.PageSize)
                //    .ToList().ForEach(p =>
                //    {
                //        var firstOrDefault = context.CTMS_GUIDELINE.FirstOrDefault(k => k.ID == p.CURRENTNODE);

                //            list.Add(new UserApply()
                //            {
                //                ID = p.ID,
                //                USERID = p.USERID,
                //                Name = p.NAME,
                //                CurrentName = firstOrDefault==null?"":firstOrDefault.NAME,
                //            CURRENTNODE = p.CURRENTNODE,
                //                ENTRYDATE = p.ENTRYDATE,
                //                EXITDATE = p.EXITDATE
                //            });
                //    });
            }
            return list;
        }

        public bool SaveUserApply(UserApply model)
        {
            using (var context = new CRDatabase())
            {
                //根据model的NextCurrentNode字段来判断是否有选择
                //if (model.NextCurrentNode == "") //当为空的时候，默认就更新几个字段
                //{
                //    var entity = context.CTMS_USERAPPLY.FirstOrDefault(p => p.APPLYID == oldApplyId);
                //    entity.DOCTORSUGGEST = model.DOCTORSUGGEST;
                //    return context.SaveChanges() > 0;
                //}
                //else
                //{
                UserInfo currentUser = new UserInfoService().GetCurrentUser();

                UserEventBLL ueBll = new UserEventBLL();
                UserEvent ueModel = new UserEvent();

                UserEvent oldEvent = ueBll.Get(p => p.EVENTID == model.EventId);

                //更新原先的待办信息
                ueBll.CloseEvent(model.EventId);

                var entity = context.CTMS_USERAPPLY.FirstOrDefault(p => p.APPLYID == model.APPLYID);
                entity.STATUS = ((int) ActionStatus.Complete).ToString();

                var addModel = new CTMS_USERAPPLY();
                addModel.ID = Guid.NewGuid().ToString();
                addModel.APPLYID = model.APPLYID;
                addModel.USERID = entity.USERID;
                addModel.GUIDELINEID = entity.GUIDELINEID;
                addModel.CURRENTNODE = model.NextCurrentNode;
                addModel.STATUS = ((int) ActionStatus.Progress).ToString();
                addModel.DOCTORSUGGEST = model.DOCTORSUGGEST;
                addModel.ENTRYDATE = model.ENTRYDATE;
                addModel.EXITDATE = model.EXITDATE;
                addModel.ISDELETED = false;

                context.CTMS_USERAPPLY.Add(addModel);

                //发送待办任务
                CancerUserInfoRepository _repository = new CancerUserInfoRepository();
                HR_CNR_USER user = _repository.FindOne(p=>p.USERID == entity.USERID);
                ueModel.EventID = Guid.NewGuid().ToString();
                string userEventId = ueModel.EventID;
                ueModel.UserApplyId = addModel.ID;
                ueModel.ActionType = "1";
                ueModel.ActionInfo = "您收到了Doc医生的建议，请查看";// + model.NextCurrentNodeName;
                ueModel.ReceiptTime = System.DateTime.Now;
                ueModel.ActionStatus = ((int) ActionStatus.Progress).ToString();
                ueModel.FromUser = currentUser.UserId;
                ueModel.ToUser = entity.USERID;
                ueModel.CreateTime = System.DateTime.Now;
                ueModel.LinkUrl = "DoTreatmentView"; //用户待办界面
                ueModel.ModelId = oldEvent.ModelId;

                ueBll.AddUserEvent(ueModel);

                #region 推荐产品与待办关联
                string _guidID = model.NextCurrentNode == "" ? model.CURRENTNODE : model.NextCurrentNode;
                GuidelineProductBLL gpBLL = new GuidelineProductBLL();
                IEnumerable<GuidelineProduct> products = gpBLL.GetList(p => p.GUIDELINEID.Equals(_guidID));
                EventProductBLL epdBLL = new EventProductBLL();
                foreach (GuidelineProduct item in products)
                {
                    EventProduct eventProduct = new EventProduct();
                    eventProduct.EventId = ueModel.EventID;
                    eventProduct.ProductID = item.ProductId;
                    eventProduct.ProductName = item.ProductName;
                    eventProduct.ProductPrice = item.ProductPrice;
                    eventProduct.ProductDes = item.Productdes;
                    eventProduct.IsAlreadyBuy = "0";

                    epdBLL.Add(eventProduct);
                }
                #endregion

                #region 发送客服待办
                ueModel = new UserEvent();
                ueModel.EventID = Guid.NewGuid().ToString();
                ueModel.UserApplyId = addModel.ID;
                ueModel.ActionType = "1";
                ueModel.ActionInfo = string.Format("{0}收到了{1}对于病例的处理建议，请跟踪", user.USERNAME, currentUser.LoginName);
                ueModel.ReceiptTime = System.DateTime.Now;
                ueModel.ActionStatus = ((int)ActionStatus.Progress).ToString();
                ueModel.FromUser = currentUser.UserId;
                var toUserId = context.CTMS_SYS_USERINFO.FirstOrDefault(p => p.USERTYPE == (decimal)UserType.客服);
                ueModel.ToUser = toUserId.USERID;
                ueModel.CreateTime = System.DateTime.Now;
                ueModel.LinkUrl = "ServiceTask"; //客服待办
                ueModel.Remarks = userEventId;
                ueModel.ModelId = oldEvent.ModelId;
                ueBll.AddUserEvent(ueModel);
                #endregion

                return context.SaveChanges() > 0;
                //}
            }
        }

        /// <summary>
        /// 获取该医生下的患者列表
        /// </summary>
        public UserApply GetModelUserApply(string applyId)
        {
            using (var context = new CRDatabase())
            {
                var model = EntityToModel(context.CTMS_USERAPPLY.FirstOrDefault(p => p.ID == applyId));
                if (model!=null)
                {

                    var nameModel=context.CTMS_GUIDELINE.FirstOrDefault(k => k.ID == model.CURRENTNODE);

                    model.CurrentName = nameModel == null ? "" : nameModel.NAME;


                    model.CnrUser = new UserInfoService().GetCnrUserById(model.USERID);

                    var eventModel= context.CTMS_USEREVENT.FirstOrDefault(p => p.USERAPPLYID == applyId && p.MODELID!=null);
                    if (eventModel!=null)
                    {
                        model.DiseaseInfoId = eventModel.MODELID;
                    }
                }
                return model;
            }
        }

        public CTMS_USERAPPLY ModelTpEntity(UserApply model)
        {
            //using (var context = new CRDatabase())
            //{
            //    var eneity = context.CTMS_USERAPPLY.FirstOrDefault(p => p.ID==model.ID);

            //    if (eneity!=null)
            //    {
            //        CTMS_USERAPPLY addEntity = new CTMS_USERAPPLY();
            //        addEntity.ID = Guid.NewGuid().ToString();
            //    }
            //}
            return null;
        }

        public UserApply EntityToModel(CTMS_USERAPPLY entity)
        {
            if (entity!=null)
            {
                var model = new UserApply()
                {
                    ID=entity.ID,
                    APPLYID = entity.APPLYID,
                    USERID = entity.USERID,
                    GUIDELINEID = entity.GUIDELINEID,
                    CURRENTNODE = entity.CURRENTNODE,
                    STATUS = entity.STATUS,
                    DOCTORSUGGEST = entity.DOCTORSUGGEST,
                    ENTRYDATE = entity.ENTRYDATE,
                    EXITDATE = entity.EXITDATE,
                    CREATEUSERID = entity.CREATEUSERID,
                    CREATEDATETIME = entity.CREATEDATETIME,
                    EDITUSERID = entity.EDITUSERID,
                    EDITDATETIME = entity.EDITDATETIME,
                    ISDELETED = entity.ISDELETED,
                    OWNERID = entity.OWNERID
                };
                return model;
            }
            return null;
        }


        public CTMS_USERAPPLY ModelToEntity(UserApply model)
        {
            if (model != null)
            {
                var entity = new CTMS_USERAPPLY()
                {
                    ID=model.ID,
                    APPLYID = model.APPLYID,
                    USERID = model.USERID,
                    GUIDELINEID = model.GUIDELINEID,
                    CURRENTNODE = model.CURRENTNODE,
                    STATUS = model.STATUS,
                    DOCTORSUGGEST = model.DOCTORSUGGEST,
                    ENTRYDATE = model.ENTRYDATE,
                    EXITDATE = model.EXITDATE,
                    CREATEUSERID = model.CREATEUSERID,
                    CREATEDATETIME = model.CREATEDATETIME,
                    EDITUSERID = model.EDITUSERID,
                    EDITDATETIME = model.EDITDATETIME,
                    ISDELETED = model.ISDELETED,
                    OWNERID = model.OWNERID
                };
                return entity;
            }
            return null;
        }
    }
}
