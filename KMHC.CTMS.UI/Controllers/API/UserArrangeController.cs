using KMHC.CTMS.BLL;
using KMHC.CTMS.BLL.CancerProcess;
using KMHC.CTMS.BLL.CancerRecord;
using KMHC.CTMS.Common;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.CancerProcess;
using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.UI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KMHC.CTMS.UI.Controllers.API
{
    [System.Web.Http.RoutePrefix("api/UserArrange")]
    public class UserArrangeController : ApiController
    {
        FileUploadBLL bll = new FileUploadBLL();

        public IHttpActionResult Get(int CurrentPage, string ID, string Date = "")
        {
            //申明参数
            int _pageSize = 10;
            int count = 1;

            try
            {
                string _creater = string.Empty;
                UserEventBLL userBLL = new UserEventBLL();
                UserEvent userEvent = userBLL.Get(p => p.EVENTID.Equals(ID));
                if (userEvent == null)
                {
                    return BadRequest("该记录不存在！");
                }
                _creater = userEvent.FromUser;

                PageInfo pageInfo = new PageInfo()
                {
                    PageIndex = CurrentPage,
                    PageSize = _pageSize,
                    OrderField = "CreatTime",
                    Order = OrderEnum.desc
                };
                Expression<Func<HR_FILEUPLOAD, bool>> predicate = p => true;
                if (string.IsNullOrEmpty(Date))
                {
                    predicate = p => p.CREATBY.Equals(_creater)&&(p.FORMID.Equals(ID)||string.IsNullOrEmpty(p.FORMID));
                }
                else
                {
                    DateTime dt_start = DateTime.Parse(Date);
                    DateTime dt_end = dt_start.AddHours(24);
                    predicate = p => p.CREATBY.Equals(_creater) && p.CREATTIME >= dt_start && p.CREATTIME <= dt_end && (p.FORMID.Equals(ID)||string.IsNullOrEmpty(p.FORMID));
                }

                var list = bll.GetList(pageInfo, predicate);
                count = pageInfo.Total / _pageSize;
                if (pageInfo.Total % _pageSize > 0)
                {
                    count += 1;
                }

                SeeDoctorHistoryBLL doctorBLL = new SeeDoctorHistoryBLL();
                List<FileUserArrange> data = new List<FileUserArrange>();
                foreach (FileUpload item in list)
                {
                    FileUserArrange model = new FileUserArrange();
                    model.FileId = item.FileUploadid;
                    model.CreatTime = item.CreatTime;
                    model.FileName = item.FileName;
                    model.FilePath = item.FilePath;
                    model.FormId = item.FormId;
                    model.Remark = item.Remark;
                    switch (item.ModelCode)
                    {
                        case "SeeDoctorHistory":
                            model.Type = "0";
                            break;
                        case "LaboratoryResult":
                            model.Type = "1";
                            break;
                        case "ImageExamination":
                            model.Type = "2";
                            break;
                        default:
                            model.Type = "";
                            break;
                    }

                    SeeDoctorHistory doctorHModel = doctorBLL.GetOne(p => p.HISTORYID.Equals(item.FormId));
                    if (doctorHModel != null)
                    {
                        model.Date = doctorHModel.DIAGNOSISTIME == null ? "" : ((DateTime)doctorHModel.DIAGNOSISTIME).ToString("yyyy-MM-dd");
                        model.Hosiptal = doctorHModel.HOSPITAL;
                    }

                    data.Add(model);
                }

                Response<IEnumerable<FileUserArrange>> response = new Response<IEnumerable<FileUserArrange>>
                {
                    Data = data,
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

        public IHttpActionResult Get(int CurrentPage, string CreateBy, string ModelCode, string Date = "")
        {
            //申明参数
            int _pageSize = 10;
            int count = 1;

            try
            {
                PageInfo pageInfo = new PageInfo()
                {
                    PageIndex = CurrentPage,
                    PageSize = _pageSize,
                    OrderField = "CreatTime",
                    Order = OrderEnum.desc
                };
                Expression<Func<HR_FILEUPLOAD, bool>> predicate = p => true;
                if (string.IsNullOrEmpty(Date))
                {
                    predicate = p => p.CREATBY.Equals(CreateBy) && p.MODELCODE.Equals(ModelCode);
                }
                else
                {
                    DateTime dt_start = DateTime.Parse(Date);
                    DateTime dt_end = dt_start.AddHours(24);
                    predicate = p => p.CREATBY.Equals(CreateBy) && p.CREATTIME >= dt_start && p.CREATTIME <= dt_end && p.MODELCODE.Equals(ModelCode);
                }

                var list = bll.GetList(pageInfo, predicate);
                count = pageInfo.Total / _pageSize;
                if (pageInfo.Total % _pageSize > 0)
                {
                    count += 1;
                }

                SeeDoctorHistoryBLL doctorBLL = new SeeDoctorHistoryBLL();
                List<FileUserArrange> data = new List<FileUserArrange>();
                foreach (FileUpload item in list)
                {
                    FileUserArrange model = new FileUserArrange();
                    model.FileId = item.FileUploadid;
                    model.CreatTime = item.CreatTime;
                    model.FileName = item.FileName;
                    model.FilePath = item.FilePath;
                    model.FormId = item.FormId;
                    model.Remark = item.Remark;
                    switch (item.ModelCode)
                    {
                        case "SeeDoctorHistory":
                            model.Type = "0";
                            break;
                        case "LaboratoryResult":
                            model.Type = "1";
                            break;
                        case "ImageExamination":
                            model.Type = "2";
                            break;
                        default:
                            model.Type = "";
                            break;
                    }

                    SeeDoctorHistory doctorHModel = doctorBLL.GetOne(p => p.HISTORYID.Equals(item.FormId));
                    if (doctorHModel != null)
                    {
                        model.Date = doctorHModel.DIAGNOSISTIME == null ? "" : ((DateTime)doctorHModel.DIAGNOSISTIME).ToString("yyyy-MM-dd");
                        model.Hosiptal = doctorHModel.HOSPITAL;
                    }

                    data.Add(model);
                }

                Response<IEnumerable<FileUserArrange>> response = new Response<IEnumerable<FileUserArrange>>
                {
                    Data = data,
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

        public IHttpActionResult Post([FromBody]Request<UserArrangeItem> request)
        {
            try
            {
                //获取数据
                UserArrangeItem item = request.Data;
                string eventID = item.EventID;
                string historyID = item.HistoryId;
                string uid=string.Empty;
                bool createNew = item.CreateNew;
                string remark = item.Remark;
                IList<FileUserArrange> files = item.Files;

                //申明返回
                string content = string.Empty;

                UserEventBLL userBLL = new UserEventBLL();             
                UserEvent userEvent = userBLL.Get(p => p.EVENTID.Equals(eventID));
                if (userEvent == null)
                {
                    return Json(new { Result = "no", Msg = "参数错误，\n请输入用户信息" });
                }

                SeeDoctorHistoryBLL doctorBLL = new SeeDoctorHistoryBLL();
                SeeDoctorHistory doctorHModel = new SeeDoctorHistory();
                if (createNew)
                {
                    doctorHModel.PERSONID = userEvent.FromUser;
                    doctorHModel.REMARK = remark;
                    historyID = doctorBLL.Add(doctorHModel);
                }
                else
                {
                    doctorHModel = doctorBLL.GetOne(p => p.HISTORYID.Equals(historyID));
                    doctorHModel.REMARK = remark;
                    doctorBLL.Edit(doctorHModel);
                }

                foreach (FileUserArrange file in files)
                {
                    //申明参数
                    string _formId = string.Empty;
                    string _modelName = string.Empty;
                    string _modelCode = string.Empty;
                    switch (file.Type)
                    {
                        case "0":
                            _modelName = "病历";
                            _modelCode = "SeeDoctorHistory";
                            break;
                        case "1":
                            _modelName = "实验室检查结果";
                            _modelCode = "LaboratoryResult";
                            break;
                        case "2":
                            _modelName = "影像学检查结果";
                            _modelCode = "ImageExamination";
                            break;
                        default:
                            break;
                    }

                    //获取数据
                    FileUpload model = bll.Get(p => p.FILEUPLOADID.Equals(file.FileId));
                    if (model == null)
                    {
                        return Json(new { Result ="false",ID=""});
                    }

                    model.ModelCode = _modelCode;
                    model.ModelName = _modelName;
                    model.FormId = historyID;
                    bll.Edit(model);
                }
                
                SendEvent(userEvent, historyID);
                return Json(new { Result = "ok", ID = historyID,Uid= userEvent.FromUser});
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public bool SendEvent(UserEvent userEvent, string FormID)
        {
            try
            {
                UserEventBLL userBLL = new UserEventBLL();
                userBLL.CloseEvent(userEvent.EventID);

                UserInfoService _service = new UserInfoService();
                UserInfo currentUser = _service.GetCurrentUser();
                string _fromUser = currentUser == null ? "" : currentUser.UserId;
                string _applyId = userEvent == null ? "" : userEvent.UserApplyId;

                string _userID = userEvent.FromUser;
                UserInfo createUser = _service.GetUserInfoByID(_userID);
                string _name = createUser == null ? "" : createUser.LoginName;

                UserEvent newUEvent = new UserEvent();
                newUEvent.EventID = Guid.NewGuid().ToString("N");
                newUEvent.UserApplyId = _applyId;
                newUEvent.FromUser = _fromUser;
                newUEvent.ActionType = ((int)ActionType.待办事项).ToString();
                newUEvent.ActionInfo = string.Format("您收到了{0}的病历报告，请查看", _name);
                newUEvent.ReceiptTime = DateTime.Now;
                newUEvent.ActionStatus = ((int)ActionStatus.Progress).ToString();
                newUEvent.CreateTime = DateTime.Now;
                newUEvent.ModelType = "DoctorHistory";
                newUEvent.ModelId = FormID;
                newUEvent.LinkUrl = "EventDetailForDoc";

                userBLL.AddUserEvent(newUEvent, UserType.医生);

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
                return false;
            }
        }

        public IHttpActionResult Get(int CurrentPage )
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
            return Ok();
        }
    }
}