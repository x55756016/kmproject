using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KMHC.CTMS.BLL;
using KMHC.CTMS.BLL.PrecisionMedicine;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.UI.Dtos;
using NPOI.OpenXmlFormats.Dml.Diagram;

namespace KMHC.CTMS.UI.Controllers.API
{
    public class BaseTemplateResultController : ApiController
    {

        public IHttpActionResult Get([FromUri] Request<string> request)
        {
            try
            {
                Response<List<BaseTemplateResult>> response = new Response<List<BaseTemplateResult>>();
                DoctorHistoryBLL dhbll = new DoctorHistoryBLL();
                var list = dhbll.GetBaseOnTemplate(request.ID);
                response.Data = list;
                return Ok(response);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("BaseTemplateResultController[Get]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Post([FromBody] Request<BaseTemplateResult> request)
        {
            try
            {
                Response<bool> rsp = new Response<bool>();
                var flag = false;
                if (request.DataList != null)
                {
                    UserInfoService user = new UserInfoService();
                    DoctorHistoryBLL dhbll = new DoctorHistoryBLL();
                    foreach (var item in request.DataList)
                    {
                        item.CREATEDATETIME = System.DateTime.Now;
                        item.EDITDATETIME = System.DateTime.Now;
                        item.CREATEUSERID = user.GetCurrentUser().UserId;
                        item.EDITUSERID = user.GetCurrentUser().UserId;
                        item.ISDELETED = "0";
                        item.OWNERID = user.GetCurrentUser().UserId;
                    }
                    flag = dhbll.SaveBaseOnTemplate(request.Keyword,request.DataList);
                }
                return Ok(flag);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("BaseTemplateResultController[Post]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }
    }
}
