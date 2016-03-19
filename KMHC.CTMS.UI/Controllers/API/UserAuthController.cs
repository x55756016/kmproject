using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KMHC.CTMS.BLL;
using KMHC.CTMS.BLL.Authorization;
using KMHC.CTMS.Model.Authorization;
using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.UI.Dtos;

namespace KMHC.CTMS.UI.Controllers.API
{
    public class UserAuthController : ApiController
    {
        private UserAuthorizationBLL uaBll = new UserAuthorizationBLL();

        public IHttpActionResult Get(string funId)
        {
            try
            {
                Response<List<UserFunction>> response = new Response<List<UserFunction>>();
                UserInfo user =new UserInfoService().GetCurrentUser();
                response.Data = uaBll.GetUserAuthByCode(user.UserId, funId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("UserAuthController[Get]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }
    }
}
