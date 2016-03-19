using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KMHC.CTMS.BLL;
using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.UI.Dtos;

namespace KMHC.CTMS.UI.Controllers.API
{
    public class AreaController : ApiController
    {

        public IHttpActionResult Get([FromUri]long parentId=0)
        {
            try
            {
                Response<List<Area>> response = new Response<List<Area>>();
                CommonBLL cbll = new CommonBLL();
                var list = cbll.GetArea(parentId);
                response.Data = list;
                return Ok(response);
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
        }



    }
}
