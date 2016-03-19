/*
 * 描述:用于验证是否登陆成功
 *  
 * 修订历史: 
 * 日期       修改人              Email                  内容
 * 20151113	  林德力         	takalin@qq.com   		 创建 
 *  
 */
using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KMHC.CTMS.BLL;
using KMHC.CTMS.Model.PrecisionMedicine;

namespace KMHC.CTMS.UI.Attribute
{
    public class AuthorizedAttribute : System.Web.Mvc.AuthorizeAttribute
    {

        private UserInfoService _userInfoService = new UserInfoService();

        public string ResourceKey
        {
            get;
            set;
        }

        private bool IsLogin = false;

        private bool HasAccess()
        {
            var result = true;

            if (!string.IsNullOrEmpty(ResourceKey))
            {
               //判断是否有相应权限
                //httpContext.Response.StatusCode = 401;//无权限状态码
                result = false;
            }
            return result;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            var pass = false;
            string controllerName = httpContext.Request.RequestContext.RouteData.Values["controller"].ToString();
            if (controllerName == "User")
                return true;

            if (_userInfoService.IsLogin())
            {
                IsLogin = true;
                if (this.HasAccess())
                    pass = true;
            }
            return pass;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                if (!IsLogin)
                {
                    filterContext.HttpContext.Response.StatusCode = 401;
                    return;
                }
            }
            if (!IsLogin)
            {
                filterContext.Result = new RedirectResult("/User/Login#/Login");
                return;
            }
            base.HandleUnauthorizedRequest(filterContext);
            if (filterContext.HttpContext.Response.StatusCode == 401)
            {
                filterContext.Result = new RedirectResult("/");
            }
        }
    }
}