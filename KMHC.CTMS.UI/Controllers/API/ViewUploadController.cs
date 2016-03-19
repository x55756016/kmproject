using KMHC.CTMS.BLL;
using KMHC.CTMS.BLL.CancerProcess;
using KMHC.CTMS.BLL.CancerRecord;
using KMHC.CTMS.Common;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.Model.CancerProcess;
using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.UI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace KMHC.CTMS.UI.Controllers.API
{
    [System.Web.Http.RoutePrefix("api/ViewUpload")]
    public class ViewUploadController : ApiController
    {
        private UserEventBLL bll = new UserEventBLL();

        public IHttpActionResult Post([FromBody]Request<IList<FileUpload>> request)
        {
            try
            {
                //获取参数
                string eventID = request.ID;
                IList<FileUpload> list = request.Data;
                string newEventID=Guid.NewGuid().ToString();

                //更新文件列表
                FileUploadBLL fileBLL = new FileUploadBLL();
                string fromUser = string.Empty;
                foreach (FileUpload item in list)
                {
                    item.FormId = newEventID;
                    fromUser = item.CreatBy;
                    fileBLL.Edit(item);
                }

                bool result = bll.CloseEvent(eventID);
                if (result)
                {
                    UserInfo currentUser = new UserInfoService().GetCurrentUser();
                    string _name = currentUser == null ? "" : currentUser.LoginName;

                    UserEvent userEvent = bll.Get(p => p.EVENTID.Equals(eventID));
                    string _applyId = userEvent == null ? "" : userEvent.UserApplyId;

                    UserEvent newUEvent = new UserEvent();
                    newUEvent.EventID = newEventID;
                    newUEvent.UserApplyId = _applyId;
                    newUEvent.FromUser = fromUser;
                    newUEvent.ActionType = ((int)ActionType.待办事项).ToString();
                    newUEvent.ActionInfo = string.Format("您收到了用户{0}上传的病历资料，请整理",_name);
                    newUEvent.ReceiptTime = DateTime.Now;
                    newUEvent.ActionStatus = ((int)ActionStatus.Progress).ToString();
                    newUEvent.CreateTime = DateTime.Now;
                    newUEvent.LinkUrl = "UserArrange";

                    bll.AddUserEvent(newUEvent,UserType.医学编辑);
                }

                return Ok("ok");
            }
            catch (Exception ex)
            {
                LogHelper.WriteInfo(ex.ToString());
                return BadRequest("异常");
            }
        }
    }
}
