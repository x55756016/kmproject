using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KMHC.CTMS.BLL;
using KMHC.CTMS.BLL.PrecisionMedicine;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.UI.Dtos;

namespace KMHC.CTMS.UI.Controllers.API
{
    public class EMRInfoController : ApiController
    {

        private DoctorHistoryBLL dhbll = new DoctorHistoryBLL();
        public IHttpActionResult Get(string historyId)
        {
            //异常检测
            if (string.IsNullOrEmpty(historyId))
            {
                return BadRequest("请求异常！");
            }
            Response<SeeDoctorHistory> response = new Response<SeeDoctorHistory>();

            try
            {
                var model = dhbll.GetSeeDoctorHistory(historyId);
                response.Data = model;
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
                throw;
            }
            return Ok(response);
        }


        public IHttpActionResult Post([FromBody]Request<SeeDoctorHistory> request )
        {
            Response<bool> response = new Response<bool>();
            try
            {
                foreach (var model in request.Data.ICDList)
                {
                    model.INFOID = System.Guid.NewGuid().ToString("N");
                    model.HISTORYID = request.Data.HISTORYID;
                }
                bool flag = dhbll.SaveSeeDoctoryHis(request.Data);
                response.Data = flag;
                return Ok(response);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("EMRInfoConatroller[Post]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }


    }
}
