using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KMHC.CTMS.BLL;
using KMHC.CTMS.BLL.CancerProcess;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.Model.CancerProcess;
using KMHC.CTMS.Model.Repository.CancerRecord;
using KMHC.CTMS.UI.Dtos;

namespace KMHC.CTMS.UI.Controllers.API
{
    public class DoctorControlController : ApiController
    {
        private DoctorControlBll dcBll = new DoctorControlBll();

        public IHttpActionResult Get([FromUri] Request<UserApply> request)
        {
            try
            {
                Response<IEnumerable<UserApply>> response = new Response<IEnumerable<UserApply>>();
                PageInfo pageInfo = new PageInfo()
                {
                    PageIndex = request.CurrentPage,
                    PageSize = request.PageSize
                };
                var list = dcBll.GetPersonList(pageInfo, request.Keyword);
                response.Data = list;
                response.PagesCount = pageInfo.PagesCount;
                return Ok(response);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("DoctorControlController[Get]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }


        public IHttpActionResult Post([FromUri] Request<UserApply> request)
        {
            return Ok();
        }
    }
}
