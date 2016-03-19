using KMHC.CTMS.BLL;
using KMHC.CTMS.BLL.Authorization;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.Model.Authorization;
using KMHC.CTMS.UI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace KMHC.CTMS.UI.Controllers.API
{
    public class UserRoleController:ApiController
    {
        public IHttpActionResult Get(string uid)
        {
            try
            {
                List<UserRole> list = new UserRoleBLL().GetListByUserID(uid);
                Response<List<UserRole>> response = new Response<List<UserRole>>
                {
                    Data = list,
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                LogHelper.WriteInfo(ex.ToString());
                return BadRequest("异常");
            }
        }

        public IHttpActionResult Post([FromBody]Request<List<Role>> request)
        {
            try
            {
                List<Role> roleList = request.Data as List<Role>;
                if (roleList != null && !string.IsNullOrEmpty(request.ID))
                {
                    new UserRoleBLL().UpdateUserRole(request.ID, roleList);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                LogHelper.WriteInfo(ex.ToString());
                return BadRequest(ex.Message);
            }
        }
    }
}