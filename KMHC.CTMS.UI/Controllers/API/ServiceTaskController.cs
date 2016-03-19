using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KMHC.CTMS.BLL;
using KMHC.CTMS.BLL.CancerProcess;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.CancerProcess;
using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.Model.Repository.Implement.CancerRecord;
using KMHC.CTMS.UI.Dtos;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.BLL.Product;

namespace KMHC.CTMS.UI.Controllers.API
{
    public class ServiceTaskController : ApiController
    {
        private UserInfoService _user;
        private UserEventBLL _event;
        private DoctorControlBll _apply;
        private ServiceTraceInfoService _service;
        private CancerUserInfoRepository _repository;
        private MemberBLL _member;

        public ServiceTaskController()
        {
            _user = new UserInfoService();
            _event = new UserEventBLL();
            _apply = new DoctorControlBll();
            _service = new ServiceTraceInfoService();
            _repository = new CancerUserInfoRepository();
            _member = new MemberBLL();
        }

        public IHttpActionResult Get([FromUri]Request<string> req)
        {
            try
            {
                UserInfo user = _user.GetCurrentUser();
                List<ServiceTraceInfo> list = _service.GetServiceTraceInfoByCreateUserId(user.UserId);
                if (!string.IsNullOrEmpty(req.Data))
                {
                    list = list.Where(p => p.TraceType == (Common.TraceType)req.Data.ToInt()).ToList<ServiceTraceInfo>();
                }
                List<TraceHistoryModel> historyList = new List<TraceHistoryModel>();
                list.ForEach(delegate(ServiceTraceInfo info)
                {
                    UserEvent e = _event.Get(p => p.EVENTID == info.ForEventId);
                    HR_CNR_USER u = _repository.FindOne(p => p.USERID == e.ToUser);
                    UserInfo toUser = _user.GetUserInfoByID(e.ToUser);
                    UserEvent s = _event.Get(p => p.REMARKS == e.EventID.ToLower());
                    TraceHistoryModel model = new TraceHistoryModel();
                    model.TraceInfo = info;
                    model.User = u;
                    model.UserEvent = e;
                    model.ServiceEvent = s;
                    if (toUser != null)
                    {
                        model.MemberLevel = toUser.Member.MEMBERNAME;
                    }
                    model.IsClose = e == null ? "" : e.ActionStatus == "完成" ? "是" : "否";

                    historyList.Add(model);
                });
                Response<List<TraceHistoryModel>> rsp = new Response<List<TraceHistoryModel>>();
                rsp.Data = historyList;
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("ServiceTaskController[Get]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Get(string eventId)
        {
            try
            {
                UserEvent e = _event.Get(p=>p.EVENTID == eventId);
                UserEvent userEvent = _event.Get(p => p.EVENTID == e.Remarks);
                UserInfo user = _user.GetUserInfoByID(userEvent.ToUser);
                UserApply apply = _apply.GetModelUserApply(e.UserApplyId);
                UserEventModel model = new UserEventModel();
                model.User = user;
                model.Event = userEvent;
                model.Apply = apply;
                Response<UserEventModel> rsp = new Response<UserEventModel>();
                rsp.Data = model;
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("ServiceTaskController[Get]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Post([FromBody]Request<ServiceTraceInfo> req)
        {
            try
            {
                ServiceTraceInfo model = req.Data;
                UserInfo user = _user.GetCurrentUser();
                if (string.IsNullOrEmpty(model.Id))
                {
                    model.CreateDateTime = DateTime.Now;
                    model.CreateUserId = user.UserId;
                    model.CreateUserName = user.LoginName;
                    model.IsValid = 1;
                    string id = Guid.NewGuid().ToString();
                    model.Id = id;
                    bool result = _service.AddServiceTraceInfo(model);
                    if (!result)
                        return BadRequest("操作失败");
                    return Ok("操作成功");
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("ServiceTaskController[Post]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }
    }

    public class UserEventModel
    {
        public UserInfo User { get; set; }

        public UserEvent Event { get; set; }

        public UserApply Apply { get; set; }
    }

    public class TraceHistoryModel
    {
        public ServiceTraceInfo TraceInfo { get; set; }

        public HR_CNR_USER User { get; set; }

        public UserEvent UserEvent { get; set; }

        public UserEvent ServiceEvent { get; set; }

        public string IsClose { get; set; }

        public string MemberLevel { get; set; }
    }
}
