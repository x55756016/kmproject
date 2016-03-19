using KMHC.CTMS.BLL.Authorization;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.Model.Authorization;
using KMHC.CTMS.UI.Attribute;
using KMHC.CTMS.UI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace KMHC.CTMS.UI.Controllers.API
{
    public class PermissionController : ApiController
    {
        public IHttpActionResult Get()
        {
            try
            {
                List<Permission> list = new PermissionBLL().GetList();
                Response<List<Permission>> response = new Response<List<Permission>>
                {
                    Data = list,
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                LogHelper.WriteInfo(ex.ToString());
                return BadRequest(ex.Message);
            }
        }
    }
}