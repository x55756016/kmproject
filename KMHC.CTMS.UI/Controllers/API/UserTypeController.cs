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
    public class UserTypeController : ApiController
    {
        private UserTypeRolesService _utr;
        private UserInfoService _user;
        private RoleBLL _role;

        public UserTypeController()
        {
            _utr = new UserTypeRolesService();
            _user = new UserInfoService();
            _role = new RoleBLL();
        }

        public IHttpActionResult Get([FromUri]Request<string> req)
        {
            try
            {
                int userType = -1;
                if (!int.TryParse(req.Keyword, out userType))
                    return BadRequest("参数错误！");

                List<UserTypeRoles> list = _utr.GetUserTypeRoleByUserType(userType);

                Response<List<UserTypeRoles>> rsp = new Response<List<UserTypeRoles>>();
                rsp.Data = list;
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("UserTypeController[Get]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Get(int userType)
        {
            try
            {
                var list = _user.GetUsersByUserType(userType);

                Response<List<UserInfo>> rsp = new Response<List<UserInfo>>();
                rsp.Data = list;
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("UserTypeController[Get]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Post([FromBody]Request<UserTypeRoles> req)
        {
            try
            {
                UserTypeRoles model = req.Data as UserTypeRoles;
                UserInfo user = _user.GetCurrentUser();
                Role role = _role.Get(model.RoleId);
                bool result = false;
                if (string.IsNullOrEmpty(model.UserTypeRoleId))
                {
                    model.CreateDateTime = DateTime.Now;
                    model.CreateUserId = user.UserId;
                    model.CreateUserName = user.PatientInfo == null ? user.LoginName : user.PatientInfo.NAME;
                    model.EditDateTime = DateTime.Now;
                    model.EditUserId = user.UserId;
                    model.EditUserName = user.PatientInfo == null ? user.LoginName : user.PatientInfo.NAME;
                    model.RoleName = role.RoleName;
                    model.UserTypeRoleId = Guid.NewGuid().ToString();
                    result = _utr.AddUserTypeRoles(model);
                }
                else
                {
                    model.EditDateTime = DateTime.Now;
                    model.EditUserId = user.UserId;
                    model.EditUserName = user.PatientInfo == null ? user.LoginName : user.PatientInfo.NAME;
                    model.RoleName = role.RoleName;
                    result = _utr.UpdateUserTypeRoles(model);
                }
                if (!result)
                    return BadRequest("操作失败");

                Response<string> rsp = new Response<string> ();
                rsp.Data = "操作成功";
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("UserTypeController[Post]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Delete([FromUri]Request<UserTypeRoles> req)
        {
            try
            {
                int userType = -1;
                if (!int.TryParse(req.Keyword, out userType))
                    return BadRequest("参数错误");

                Response<string> rsp = new Response<string>();
                List<UserTypeRoles> list = _utr.GetUserTypeRoleByUserType(userType);
                if (list.Count <= 0)
                {
                    rsp.Data = "操作成功";
                    return Ok(rsp);
                }
                foreach (UserTypeRoles item in list)
                {
                    _utr.DeleteUserTypeRolesById(item.UserTypeRoleId);
                }
                
                rsp.Data = "操作成功";
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("UserTypeController[Delete]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }
    }
}
