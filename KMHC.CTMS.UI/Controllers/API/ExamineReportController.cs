using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KMHC.CTMS.BLL;
using KMHC.CTMS.BLL.Examine;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.Examine;
using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.UI.Dtos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KMHC.CTMS.UI.Controllers.API
{
    public class ExamineReportController : ApiController
    {
        private ImageExamineReportService _ier;
        private UserInfoService _user;
        private readonly DbContext _db = new CRDatabase();

        private UserInfo User
        {
            get
            {
                return _user.GetCurrentUser();
            }
        }

        public ExamineReportController()
        {
            _ier = new ImageExamineReportService();
            _user = new UserInfoService();
        }

        public IHttpActionResult Get(string examineNo,string userId)
        {
            try
            {
                if (string.IsNullOrEmpty(examineNo))
                {
                    List<ImageExamineReport> list = _ier.GetImageExamineReportByUserId(userId.ToLower());
                    Response<List<ImageExamineReport>> rsp = new Response<List<ImageExamineReport>>();
                    rsp.Data = list;
                    return Ok(rsp);
                }
                else
                {
                    ImageExamineReport model = _ier.GetImageExamineReportById(examineNo);
                    Response<ImageExamineReport> rsp = new Response<ImageExamineReport>();
                    rsp.Data = model;
                    return Ok(rsp);
                }                
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("ExamineReportController[Get]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Post([FromBody]Request<ImageExamineReport> req)
        {
            try
            {
                ImageExamineReport model = req.Data;
                Response<ImageExamineReport> rsp = new Response<ImageExamineReport> ();
                if (string.IsNullOrEmpty(model.Id))
                {
                    //新增
                    string id = Guid.NewGuid().ToString();
                    model.Id = id;
                    model.CreateUserId = User.UserId;
                    model.CreateUserName = User.LoginName;
                    model.CreateDateTime = model.CreateDateTime == null ? DateTime.Now : model.CreateDateTime;
                    model.IsDeleted = 0;
                    model.EditUserName = User.LoginName;
                    model.EditUserId = User.UserId;
                    model.EditDateTime = DateTime.Now;
                    model.UserId = Guid.Parse(model.UserId).ToString();
                    model.Remark = "";
                    bool result = _ier.AddImageExamineReport(model);
                    rsp.Data = model;
                    if (result)
                    {
                        Dictionary<string, string> dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(model.ReportDetail);
                        if (dic.Count > 0)
                        {
                            var detailList = (from x in _db.Set<CTMS_IMAGEEXAMINEREPORTDETAIL>() where x.REPORTID == model.Id select x).ToList();
                            if (detailList.Count > 0)
                            {
                                _db.Set<CTMS_IMAGEEXAMINEREPORTDETAIL>().RemoveRange(detailList);
                            }
                            foreach (string key in dic.Keys)
                            {
                                CTMS_IMAGEEXAMINEREPORTDETAIL detail = new CTMS_IMAGEEXAMINEREPORTDETAIL();
                                detail.ID = Guid.NewGuid().ToString();
                                detail.REPORTID = model.Id;
                                detail.OPTIONID = key;
                                detail.VALUE = dic[key];
                                _db.Set<CTMS_IMAGEEXAMINEREPORTDETAIL>().Add(detail);
                            }
                            _db.SaveChanges();
                        }
                        return Ok(rsp);
                    }
                }
                else
                {
                    //修改
                    ImageExamineReport m = _ier.GetImageExamineReportById(model.Id);
                    if (m != null)
                    {
                        if (model.CheckDate != null)
                            m.CheckDate = model.CheckDate;
                        if (!string.IsNullOrEmpty(model.Dept))
                            m.Dept = model.Dept;
                        if (!string.IsNullOrEmpty(model.Part))
                            m.Part = model.Part;
                        if (!string.IsNullOrEmpty(model.Diagnose))
                            m.Diagnose = model.Diagnose;
                        if (!string.IsNullOrEmpty(model.See))
                            m.See = model.See;
                        if (!string.IsNullOrEmpty(model.ReportDoctor))
                            m.ReportDoctor = model.ReportDoctor;
                        if (!string.IsNullOrEmpty(model.ReportDetail))
                            m.ReportDetail = model.ReportDetail;
                        if (!string.IsNullOrEmpty(model.CheckDoctor))
                            m.CheckDoctor = model.CheckDoctor;
                        bool result = _ier.UpdateImageExamineReport(m);
                        rsp.Data = m;
                        if (result)
                        {
                            Dictionary<string, string> dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(model.ReportDetail);
                            if (dic.Count > 0)
                            {
                                var detailList = (from x in _db.Set<CTMS_IMAGEEXAMINEREPORTDETAIL>() where x.REPORTID == model.Id select x).ToList();
                                if (detailList.Count > 0)
                                {
                                    _db.Set<CTMS_IMAGEEXAMINEREPORTDETAIL>().RemoveRange(detailList);
                                }
                                foreach (string key in dic.Keys)
                                {
                                    CTMS_IMAGEEXAMINEREPORTDETAIL detail = new CTMS_IMAGEEXAMINEREPORTDETAIL();
                                    detail.ID = Guid.NewGuid().ToString();
                                    detail.REPORTID = model.Id;
                                    detail.OPTIONID = key;
                                    detail.VALUE = dic[key];
                                    _db.Set<CTMS_IMAGEEXAMINEREPORTDETAIL>().Add(detail);
                                }
                                _db.SaveChanges();
                            }
                            return Ok(rsp);
                        }
                    }
                }
                return BadRequest("操作失败");
            }
            catch (DbEntityValidationException ex)
            {
                LogService.WriteErrorLog("ExamineReportController[Post]", ex.ToString());
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("ExamineReportController[Post]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Delete([FromUri]Request<string> req)
        {
            try
            {
                if (string.IsNullOrEmpty(req.Keyword))
                    return BadRequest("参数错误");

                bool result = _ier.DeleteImageExamineReport(req.Keyword);

                Response<string> rsp = new Response<string>();
                rsp.Data = "删除成功";

                if (!result)
                    return BadRequest("删除失败");
                return Ok(rsp);

            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("ExamineController[Delete]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }
    }
}
