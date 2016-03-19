/*
 * 描述:待办事项数据处理API
 *  
 * 修订历史: 
 * 日期        修改人              Email                   内容
 * 2015/12/31  邓柏生                                      创建 
 *  
 */

using KMHC.CTMS.BLL;
using KMHC.CTMS.BLL.Authorization;
using KMHC.CTMS.BLL.CancerProcess;
using KMHC.CTMS.BLL.CancerRecord;
using KMHC.CTMS.Common;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.Authorization;
using KMHC.CTMS.Model.CancerProcess;
using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.UI.Dtos;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Http;

namespace KMHC.CTMS.UI.Controllers.API
{
    [System.Web.Http.RoutePrefix("API/DoTreatment")]
    public class DoTreatmentController : ApiController
    {
        private UserEventBLL bll = new UserEventBLL();

        public IHttpActionResult Get(int CurrentPage, string ActionInfo = "", string ActionStatus = "")
        {
            //申明参数
            int _pageSize = 15;
            int count = 1;
            UserInfo currentUser = new UserInfoService().GetCurrentUser();
            string userId = currentUser == null ? "" : currentUser.UserId;
            try
            {
                Expression<Func<CTMS_USEREVENT, bool>> predicate = p => true;

                predicate =
                    p =>
                        (ActionInfo == "" || p.ACTIONINFO.Contains(ActionInfo)) &&
                        (p.TOUSER == currentUser.UserId) &&
                        (string.IsNullOrEmpty(ActionStatus) || p.ACTIONSTATUS == ActionStatus);

                PageInfo pageInfo = new PageInfo()
                {
                    PageIndex = CurrentPage,
                    PageSize = _pageSize,
                    OrderField = "CREATETIME",
                    Order = OrderEnum.desc
                };
                var list = bll.GetList(pageInfo, predicate);

                count = list.Count();
                count = count / _pageSize;
                if (count % _pageSize > 0)
                {
                    count += 1;
                }
                Response<IEnumerable<UserEvent>> response = new Response<IEnumerable<UserEvent>>
                {
                    Data = list,
                    PagesCount = count
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                LogHelper.WriteInfo(ex.ToString());
                return BadRequest("异常");
            }
        }

        public IHttpActionResult Get(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("非法请求！");
            }

            try
            {
                UserEvent userEvent = bll.Get(p => p.EVENTID.Equals(id));
                if (userEvent == null)
                {
                    return BadRequest("记录不存在！");
                }

                ExtUserEvent extUserEvent = GetExtData(userEvent);

                return Ok(extUserEvent);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("DoTreatmentController[Get]", ex.ToString());
                return BadRequest(ex.ToString());
            }
        }

        public IHttpActionResult Post([FromBody]Request<TreatmentRequest> request)
        {
            try
            {
                IList<FileUpload> files = request.Data.files;
                UserEvent u = request.Data.userEvent;
                string eventID = u.EventID;
                string newEventID = Guid.NewGuid().ToString();
                UserEvent userEvent = bll.Get(p => p.EVENTID.Equals(eventID));
                UserEvent serviceEvent = bll.Get(p => p.REMARKS == eventID && p.USERAPPLYID == userEvent.UserApplyId && p.ACTIONSTATUS == "1");
                if (userEvent == null)
                {
                    return BadRequest("该记录不存在！");
                }

                //更新文件列表
                FileUploadBLL fileBLL = new FileUploadBLL();
                string fromUser = userEvent.ToUser;
                foreach (FileUpload item in files)
                {
                    item.FormId = newEventID;
                    fileBLL.Edit(item);
                }

                userEvent.Remarks = u.Remarks;
                userEvent.ActionType = "1";
                userEvent.ActionStatus = ((int)ActionStatus.Complete).ToString();
                userEvent.EndTime = DateTime.Now;
                bool result = bll.Edit(userEvent);

                #region 结束客服待办
                if (serviceEvent != null)
                {
                    serviceEvent.ActionStatus = ((int)ActionStatus.Complete).ToString();
                    serviceEvent.EndTime = DateTime.Now;
                    bll.Edit(serviceEvent);
                }
                #endregion

                if (result)
                {
                    UserInfo currentUser = new UserInfoService().GetCurrentUser();
                    string _name = currentUser == null ? "" : currentUser.LoginName;

                    UserEvent newUEvent = new UserEvent();
                    newUEvent.EventID = newEventID;
                    newUEvent.UserApplyId = userEvent.UserApplyId;
                    newUEvent.FromUser = fromUser;
                    newUEvent.ActionType = ((int)ActionType.待办事项).ToString();
                    newUEvent.ActionInfo = string.Format("您收到了用户{0}上传的病历资料，请整理", _name);
                    newUEvent.ReceiptTime = DateTime.Now;
                    newUEvent.ActionStatus = ((int)ActionStatus.Progress).ToString();
                    newUEvent.CreateTime = DateTime.Now;
                    newUEvent.LinkUrl = "UserArrange";

                    bll.AddUserEvent(newUEvent, UserType.医学编辑);
                }
                return Ok("ok");
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("DoTreatmentController[Post]", ex.ToString());
                return BadRequest(ex.ToString());
            }
        }

        public IHttpActionResult Get(string epid,string pid)
        {
            try
            {
                EventProductBLL epdBLL = new EventProductBLL();

                EventProduct model = epdBLL.Get(p => p.EVENTPRODUCTID.Equals(epid) && p.PRODUCTID.Equals(pid));
                model.IsAlreadyBuy = "1";
                epdBLL.Edit(model);

                return Ok("ok");
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("DoTreatmentController[Post]", ex.ToString());
                return BadRequest(ex.ToString());
            }
        }

        private ExtUserEvent GetExtData(UserEvent userEvent)
        {
            ExtUserEvent extUserEvent = new ExtUserEvent();
            extUserEvent.EventID = userEvent.EventID;
            extUserEvent.ActionStatus = userEvent.ActionStatus;
            extUserEvent.Remarks = userEvent.Remarks;
            List<UserEvent> tracks = bll.GetList(p => p.USERAPPLYID.Equals(userEvent.UserApplyId)).OrderBy(p => p.CreateTime).ToList();
            UserInfoService userService = new UserInfoService();
            UserRoleBLL userRoleBLL = new UserRoleBLL();
            RoleBLL roleBLL = new RoleBLL();
            for (int i = 0; i < tracks.Count; i++)
            {
                string _userID = tracks[i].ToUser;
                UserInfo userInfo = userService.GetUserInfoByID(_userID);
                if (userInfo == null)
                {
                    tracks[i].ToUser = "";
                }
                else
                {
                    tracks[i].ToUser = userInfo.LoginName;
                }

                UserRole userrole = userRoleBLL.GetOne(p => p.USERID.Equals(_userID));
                if (userrole == null)
                {
                    tracks[i].Remarks = "";
                }
                else
                {
                    Role role = roleBLL.Get(userrole.RoleID);
                    tracks[i].Remarks = role == null ? "" : role.RoleName;
                }
            }


            //todo:20160119演示用，临时去了客服的代办信息
            extUserEvent.Tracks = tracks.Where(p => !p.Remarks.Contains("客服")).ToList();
            //extUserEvent.Tracks = tracks;

            DoctorControlBll dcBLL = new DoctorControlBll();
            UserApply userApply = dcBLL.GetModelUserApply(userEvent.UserApplyId);
            if (userApply != null)
            {
                extUserEvent.DoctorSuggest = userApply.DOCTORSUGGEST;

                GuideLineBLL gdBLL = new GuideLineBLL();
                GuideLine guidline = gdBLL.GetSimpleModel(userApply.CURRENTNODE);
                if (guidline != null)
                {
                    extUserEvent.CurrentNodeName = guidline.Name;
                    extUserEvent.RecommendProcess = guidline.RecommendProcess;
                }
            }

            EventProductBLL epdBLL = new EventProductBLL();
            extUserEvent.Products = epdBLL.GetList(p => p.EVENTID.Equals(userEvent.EventID)).ToList();

            return extUserEvent;
        }
    }

    public class TreatmentRequest
    {
        public UserEvent userEvent { get; set; }
        public IList<FileUpload> files { get; set; }
    }
}