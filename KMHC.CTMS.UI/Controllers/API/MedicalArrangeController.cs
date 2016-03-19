using KMHC.CTMS.BLL;
using KMHC.CTMS.BLL.CancerRecord;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.DAL.Database;
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
    public class MedicalArrangeController : ApiController
    {
        SeeDoctorHistoryBLL bll = new SeeDoctorHistoryBLL();

        public IHttpActionResult Get(int CurrentPage, string Date = "", string Name = "")
        {
            //申明参数
            int _pageSize = 10;
            int count = 1;

            try
            {
                Response<IEnumerable<SeeDoctorHistory>> response = new Response<IEnumerable<SeeDoctorHistory>>();

                PageInfo pageInfo = new PageInfo()
                {
                    PageIndex = CurrentPage,
                    PageSize = _pageSize,
                    OrderField = "DIAGNOSISTIME",
                    Order = OrderEnum.desc
                };

                Expression<Func<HR_SEEDOCTORHISTORY, bool>> predicate = p => true;
                if (!string.IsNullOrEmpty(Date) && string.IsNullOrEmpty(Name))
                {
                    DateTime dt = DateTime.Parse(Date);
                    predicate = p => p.DIAGNOSISTIME == dt;
                }
                else if (string.IsNullOrEmpty(Date) && !string.IsNullOrEmpty(Name))
                {
                    UserInfoService service = new UserInfoService();
                    List<UserInfo> users = service.GetList(Name);
                    if (users == null)
                    {
                        response.Data = null;
                        response.PagesCount = 1;

                        return Ok(response);
                    }

                    IEnumerable<string> ids = (from o in users select o.UserId).ToList();

                    predicate = p => ids.Contains(p.PERSONID);
                }
                else if (!string.IsNullOrEmpty(Date) && !string.IsNullOrEmpty(Name))
                {
                    UserInfoService service = new UserInfoService();
                    List<UserInfo> users = service.GetList(Name);
                    if (users == null)
                    {
                        response.Data = null;
                        response.PagesCount = 1;

                        return Ok(response);
                    }

                    IEnumerable<string> ids = (from o in users select o.UserId).ToList();
                    DateTime dt = DateTime.Parse(Date);

                    predicate = p => ids.Contains(p.PERSONID) && p.DIAGNOSISTIME == dt;
                }

                var list = bll.GetList(pageInfo, predicate);
                count = pageInfo.Total / _pageSize;
                if (pageInfo.Total % _pageSize > 0)
                {
                    count += 1;
                }

                response.Data = list;
                response.PagesCount = count;

                return Ok(response);
            }
            catch (Exception ex)
            {
                LogHelper.WriteInfo(ex.ToString());
                return BadRequest("异常");
            }
        }

        public IHttpActionResult Get(string Id)
        {
            try
            {
                Expression<Func<HR_FILEUPLOAD, bool>> predicate = p => p.FORMID.Equals(Id);

                FileUploadBLL fileBLL = new FileUploadBLL();
                var list = fileBLL.GetList(predicate);

                foreach (FileUpload item in list)
                {
                    switch (item.ModelCode)
                    {
                        case "SeeDoctorHistory":
                            item.ModelCode = "0";
                            break;
                        case "LaboratoryResult":
                            item.ModelCode = "1";
                            break;
                        case "ImageExamination":
                            item.ModelCode = "2";
                            break;
                        default:
                            item.ModelCode = "";
                            break;
                    }

                }

                return Ok(list);
            }
            catch (Exception ex)
            {
                LogHelper.WriteInfo(ex.ToString());
                return BadRequest("异常");
            }
        }

        public IHttpActionResult Post([FromBody]Request<IList<FileUpload>> request)
        {
            try
            {
                IList<FileUpload> files = request.Data;
                FileUploadBLL fileBLL = new FileUploadBLL();
                foreach (FileUpload file in files)
                {
                    switch (file.ModelCode)
                    {
                        case "0":
                            file.ModelName = "病历";
                            file.ModelCode = "SeeDoctorHistory";
                            break;
                        case "1":
                            file.ModelName = "实验室检查结果";
                            file.ModelCode = "LaboratoryResult";
                            break;
                        case "2":
                            file.ModelName = "影像学检查结果";
                            file.ModelCode = "ImageExamination";
                            break;
                        default:
                            break;
                    }

                    fileBLL.Edit(file);
                }

                return Ok("ok");
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
                return BadRequest(ex.Message);
            }
        }
    }
}