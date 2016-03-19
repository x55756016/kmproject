using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KMHC.CTMS.BLL;
using KMHC.CTMS.Model.Common;
using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.UI.Dtos;

namespace KMHC.CTMS.UI.Controllers.API
{
    /// <summary>
    /// 页面地址: 
    /// 实现功能:提供接口给用户修改个人信息
    /// </summary>
    public class UserInfoController : ApiController
    {
        /// <summary>
        /// 取出当前缓存信息
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            var userService = new UserInfoService();
            Response<UserInfoForView> response = new Response<UserInfoForView>();

            //取的时候从缓存中取，更新字段的时候更新缓存
            var model = userService.GetUserInfoForView(userService.GetCurrentUser().UserId);


            model.UserInfo.LoginPwd = "";


            response.Data = model;
            return Ok(response);
        }

        public IHttpActionResult Post([FromBody]Request<UserInfoForView> request)
        {
            Response<BaseResult> response = new Response<BaseResult>();
            try
            {
                var userService = new UserInfoService();
                var flag = userService.UpdaetUserInfo(userService.GetCurrentUser().UserId, request.Data);
                response.Data = flag;
                return Ok(response);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("UserInfoController[Post]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }

    }
}
