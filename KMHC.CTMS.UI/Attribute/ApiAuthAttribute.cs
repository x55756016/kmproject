using KMHC.CTMS.BLL;
using KMHC.CTMS.BLL.Authorization;
using KMHC.CTMS.Common;
using KMHC.CTMS.Model.PrecisionMedicine;
/*
 * 描述:创建基于webapi的filter
 *  
 * 修订历史: 
 * 日期               修改人              Email                  内容
 * 20160105   		  林德力         	takalin@qq.com   		 创建 
 *  
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace KMHC.CTMS.UI.Attribute
{
    public class ApiAuthAttribute : AuthorizeAttribute
    {
        //
        // Summary:
        //     Indicates whether the specified control is authorized.
        //
        // Parameters:
        //   actionContext:
        //     The context.
        //
        // Returns:
        //     true if the control is authorized; otherwise, false.
        public string AuthCode
        {
            get;
            set;
        }

        public PermissionType Permission
        {
            get;
            set;
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            return true;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (!string.IsNullOrEmpty(AuthCode))
            {
                var operates = new List<string>() {"post", "delete"};

                UserInfo user = new UserInfoService().GetCurrentUser();
                if (user == null || string.IsNullOrEmpty(user.UserId)) //获取不到当前用户
                {

                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.NoContent);



                    //actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                }
                else if (!new RoleFunctionBLL().IsHavePermission(user.UserId, AuthCode, Permission)) //无权限
                {
                    if (operates.Contains(actionContext.Request.Method.Method.ToLower())) //增删改
                    {
                        actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden); //前台处理，弹出提示
                    }
                    else
                    {
                        actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized); //查询直接跳转到无权限页面
                    }
                }
            }
            else
            {
                UserInfoService _service = new UserInfoService();
                UserInfo user = _service.GetCurrentUser();
                if (user == null || string.IsNullOrEmpty(user.UserId)) //获取不到当前用户
                {

                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.NoContent);

                    return;
                    

                    //actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                }
                _service.ReflashUserInfo();
                
            }
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            base.HandleUnauthorizedRequest(actionContext);
        }
    }
}