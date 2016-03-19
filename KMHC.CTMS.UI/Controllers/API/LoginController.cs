using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using KMHC.CTMS.BLL.PrecisionMedicine;
using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.BLL;

namespace KMHC.CTMS.UI.Controllers.API
{
    public class LoginController : ApiController
    {
        private HttpResponseMessage rsp;
        private UserInfoService _service;
        LoginController()
        {
            rsp = new HttpResponseMessage();
            _service = new UserInfoService();
        }

        #region 属性
        /// <summary>
        /// 缓存字符串
        /// </summary>
        private string CacheString
        {
            get
            {
                return ConfigurationManager.AppSettings["CacheString"];
            }
        }

        /// <summary>
        /// 缓存时间
        /// </summary>
        private int CacheTime
        {
            get
            {
                return ConfigurationManager.AppSettings["CacheTime"].ToInt(30);
            }
        }

        /// <summary>
        /// 登录名
        /// </summary>
        private string LoginName
        {
            get
            {
                return HttpContext.Current.Request.Form["LoginName"];
            }
        }

        /// <summary>
        /// 登录密码
        /// </summary>
        private string LoginPwd
        {
            get
            {
                return HttpContext.Current.Request.Form["LoginPwd"];
            }
        }
        #endregion

        /// <summary>
        /// 提交帐号密码获取令牌
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Post()
        {
            if (string.IsNullOrEmpty(LoginName) || string.IsNullOrEmpty(LoginPwd))//参数错误
                return CreateResponseInfo(1001, string.Empty);

            UserInfo userInfo = _service.Login(LoginName, LoginPwd);

            if(userInfo == null)//密码错误
                return CreateResponseInfo(1002, string.Empty);

            //产生令牌
            string tokenValue = this.GetGuidString();
            _service.CacheInfo(tokenValue + CacheString, tokenValue);

            //产生主站凭证
            CreateInfo(tokenValue, userInfo);
            rsp = CreateResponseInfo(1000, tokenValue);

            HttpCookie tokenCookie = new HttpCookie("Token");
            tokenCookie.Values.Add("Value", tokenValue);
            tokenCookie.Expires = DateTime.Now.AddMinutes(CacheTime);
            tokenCookie.Path = "/";
            HttpContext.Current.Response.AppendCookie(tokenCookie);
            return rsp;
        }

        /// <summary>
        /// 创建响应内容
        /// </summary>
        /// <param name="retCode">响应码</param>
        /// <param name="tokenValue">令牌</param>
        /// <returns></returns>
        private HttpResponseMessage CreateResponseInfo(int retCode, string tokenValue)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new { retCode = retCode, tokenValue = tokenValue }.JsonSerialize());
        }

        /// <summary>
        /// 缓存用户信息
        /// </summary>
        /// <param name="tokenValue"></param>
        /// <param name="userInfo"></param>
        private void CreateInfo(string tokenValue,UserInfo userInfo)
        {
            _service.CacheInfo(tokenValue, userInfo);
        }

        /// <summary>
        /// 通过令牌来取得用户信息
        /// </summary>
        /// <param name="token">令牌</param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetUserInfo(string token)
        {
            UserInfo userInfo = _service.GetLoginInfo(token);
            if (userInfo != null)
                rsp = Request.CreateResponse(HttpStatusCode.OK, userInfo);
            else
                rsp = Request.CreateResponse(HttpStatusCode.Forbidden);
            return rsp;
        }

        /// <summary>
        /// 获取令牌(GUID)
        /// </summary>
        /// <returns></returns>
        private string GetGuidString()
        {
            return Guid.NewGuid().ToString().ToUpper();
        }
	}
}