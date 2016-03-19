using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using KMHC.CTMS.BLL;
using KMHC.CTMS.BLL.CancerProcess;
using KMHC.CTMS.Common;
using KMHC.CTMS.Model.CancerProcess;
using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.UI.Dtos;

namespace KMHC.CTMS.UI.Controllers.API
{
    public class UserApplyController : ApiController
    {
        private DoctorControlBll dcbll = new DoctorControlBll();

        public IHttpActionResult Get([FromUri]Request<UserApply> request)
        {
            try
            {
                Response<UserApply> response = new Response<UserApply>();
                var list = dcbll.GetModelUserApply(request.ID);
                response.Data = list;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Post([FromBody] Request<UserApply> request)
        {
            try
            {
                dcbll.SaveUserApply(request.Data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
                throw;
            }
            return Ok();
        }

    }
}
