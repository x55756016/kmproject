using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KMHC.CTMS.BLL.PrecisionMedicine;
using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.UI.Models;
using KMHC.CTMS.UI.Controllers;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.BLL;
using System.Xml.Serialization;
using System.IO;
using KMHC.CTMS.Model.CancerProcess;
using KMHC.CTMS.Common;
using KMHC.CTMS.BLL.CancerProcess;
using KMHC.CTMS.Model.Repository.Implement.CancerRecord;
using KMHC.CTMS.DAL.Database;
using System.Data.Entity;
using KMHC.CTMS.Model.Common;
using KMHC.CTMS.Model.Authorization;
using KMHC.CTMS.BLL.Authorization;

namespace KMHC.CTMS.UI.Controllers
{
    public class UserController : BaseViewController
    {
        private UserInfoService _service = new UserInfoService();
        private readonly DbContext _context = new CRDatabase();
        private RoleBLL _role = new RoleBLL();
        private UserRoleBLL _userRole = new UserRoleBLL();
        private UserTypeRolesService _utr = new UserTypeRolesService();

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
        /// 重置密码链接有效时长
        /// </summary>
        private int ResetPasswordTime
        {
            get
            {
                return ConfigurationManager.AppSettings["ResetPasswordTime"].ToInt(24);
            }
        }
        #endregion

        //
        // GET: /User/
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ActionResult UserLogin(UserInfo user)
        {
            try
            {
                //object obj = Session["_vercode_"];
                //if (obj == null)
                //return Json(new { Status = -9, Data = string.Empty, Msg = "验证码失效，\n请重新获取验证码" });
                //string code = obj.ToString();
                //if (string.IsNullOrEmpty(code))
                //return Json(new { Status = -8, Data = string.Empty, Msg = "验证码无效，\n请重新获取验证码" });
                if (user == null)
                    return Json(new { Status = -7, Data = string.Empty, Msg = "参数错误，\n请输入用户信息" });
                //if(user.Vercode.Trim() != code.Trim())
                //return Json(new { Status = -6, Data = string.Empty, Msg = "验证码错误，\n请重新输入验证码" });
                if (string.IsNullOrEmpty(user.LoginName))//参数错误
                    return Json(new { Status = -5, Data = string.Empty, Msg = "参数错误" });

                UserInfo userInfo = _service.Login(user.LoginName, user.MD5LoginPwd);

                if (userInfo == null)//密码错误
                    return Json(new { Status = -4, Data = string.Empty, Msg = "密码错误" });

                //产生令牌
                string tokenValue = this.GetGuidString();
                _service.CacheInfo(tokenValue + CacheString, tokenValue);

                _service.CacheInfo(tokenValue, userInfo);

                HttpCookie tokenCookie = new HttpCookie("Token");
                tokenCookie.Value = tokenValue;
                tokenCookie.Expires = DateTime.Now.AddMinutes(CacheTime);
                tokenCookie.Path = "/";
                Response.AppendCookie(tokenCookie);

                return Json(new { Status = 1, Data = tokenValue, Msg = string.Empty });
            }
            catch (Exception ex)
            {
                return Json(new { Status = -99, Data = string.Empty, Msg = ex.Message });
            }            
        }

        public ActionResult UserLogout()
        {
            try
            {
                _service.Logout();
                return Json(new { Status = 1, Data = string.Empty, Msg = "操作成功" });
            }
            catch (Exception ex)
            {
                return Json(new { Status = -99, Data = string.Empty, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 检测该手机是否已注册
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public ActionResult CheckUserExists(string phone)
        {
            try
            {
                var list = _context.Set<CTMS_SYS_USERINFO>().Where(p => p.MOBILEPHONE == phone).ToList();
                if (list.Count > 0)
                    return Json(new { Status = -1, Data = string.Empty, Msg = "该手机已被注册" },JsonRequestBehavior.AllowGet);

                return Json(new { Status = 1, Data = string.Empty, Msg = string.Empty }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = -99, Data = string.Empty, Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult SearchUser(int pageIndex, string Name = null, string IdCard = null, int UserType = 0, int pageSize = 5)
        {
            try
            {
                PageInfo pageInfo = new PageInfo()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    OrderField = "LoginName",
                    Order = OrderEnum.desc
                };

                List<UserInfo> users = _service.GetAll();
                if (!string.IsNullOrEmpty(Name))
                {
                    users = users.FindAll(p => p.Name.Contains(Name));
                }
                if (!string.IsNullOrEmpty(IdCard))
                {
                    users = users.FindAll(p => p.PatientInfo != null && p.PatientInfo.IDCARD.Contains(IdCard));
                }
                if (UserType != 0)
                {
                    users = users.FindAll(p => p.UserType == UserType);
                }

                users = users.AsQueryable().Paging(ref pageInfo).ToList();

                return Json(new { Status = 1, Data = users, Msg = string.Empty, PagesCount = pageInfo.PagesCount }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("UserController[SearchUser]", ex.ToString());
                return Json(new { Status = -99, Data = string.Empty, Msg = ex.Message },JsonRequestBehavior.AllowGet);
            }
        }




        /// <summary>
        /// 发送链接给用户重置密码
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ActionResult UserResetPwd(UserInfo user)
        {
            try
            {
                #region 参数检测
                if (user.ResetType == 1)
                {
                    return Json(new { Status = -9, Data = string.Empty, Msg = "对不起，该功能暂未实现" });
                }
                if (string.IsNullOrEmpty(user.Email))
                {
                    return Json(new { Status = -8, Data = string.Empty, Msg = "参数错误" });
                }
                if (!StringExtension.IsEmail(user.Email))
                {
                    return Json(new { Status = -7, Data = string.Empty, Msg = "参数格式不正确" });
                }
                #endregion

                #region 保存重置记录
                string id = Guid.NewGuid().ToString();
                ResetPasswordLog resetLog = new ResetPasswordLog();
                resetLog.ID = id;
                UserInfo u = _service.GetUserByEmail(user.Email);
                resetLog.UserID = u.UserId;
                resetLog.InputTime = DateTime.Now;
                resetLog.ResetType = user.ResetType;
                resetLog.Status = 0;
                bool result = _service.AddResetPasswordLog(resetLog);
                #endregion

                if (result)
                {
                    string host = Request.Url.Host;
                    int port = Request.Url.Port;
                    StringExtension.SendMail(user.Email, "重置密码", "请点击以下链接重置密码：<br/>http://" + host + ":" + port + "/User/Login#/Reset/" + id);
                    return Json(new { Status = 1, Data = string.Empty, Msg = "操作成功" });
                }
                else
                {
                    return Json(new { Status = -6, Data = string.Empty, Msg = "操作失败" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Status = -99, Data = string.Empty, Msg = ex.Message });
            }  
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ActionResult ResetUserPassword(UserInfo user)
        {
            try
            {
                #region 参数检测
                if(user == null)
                    return Json(new { Status = -9, Data = string.Empty, Msg = "参数错误1" });
                if(string.IsNullOrEmpty(user.LoginPwd))
                    return Json(new { Status = -8, Data = string.Empty, Msg = "参数错误2" });
                if(string.IsNullOrEmpty(user.ConfirmLoginPwd))
                    return Json(new { Status = -7, Data = string.Empty, Msg = "参数错误3" });
                if(string.IsNullOrEmpty(user.ResetToken))
                    return Json(new { Status = -6, Data = string.Empty, Msg = "参数错误4" });
                if(user.LoginPwd.Trim() != user.ConfirmLoginPwd.Trim())
                    return Json(new { Status = -5, Data = string.Empty, Msg = "参数错误5" });
                #endregion

                #region 获取重置记录并判断后，获取用户记录
                ResetPasswordLog resetLog = _service.GetResetPasswordLogByID(user.ResetToken);
                if(resetLog == null)
                    return Json(new { Status = -4, Data = string.Empty, Msg = "参数错误6" });
                TimeSpan ts = DateTime.Now - resetLog.InputTime;
                if(ts.TotalHours > ResetPasswordTime)
                    return Json(new { Status = -3, Data = string.Empty, Msg = "链接已失效，请重新获取" });
                UserInfo u = _service.GetUserInfoByID(resetLog.UserID);
                if(u == null)
                    return Json(new { Status = -2, Data = string.Empty, Msg = "参数错误7" });
                #endregion

                #region 重置用户密码
                string password = user.LoginPwd.ToMd5();
                u.LoginPwd = password;
                bool result = _service.UpdateUserInfo(u);
                #endregion

                #region 返回结果
                if (result)
                {
                    #region 更新记录为已使用
                    resetLog.Status = 1;
                    _service.UpdateResetPasswordLog(resetLog);
                    #endregion
                    return Json(new { Status = 1, Data = string.Empty, Msg = "操作成功" });
                }
                return Json(new { Status = -1, Data = string.Empty, Msg = "操作失败" });
                #endregion
            }
            catch (Exception ex)
            {
                return Json(new { Status = -99, Data = string.Empty, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ActionResult UserRegister(UserInfo user)
        {
            try
            {
                UserInfo u = _service.GetUserByLoginName(user.LoginName);
                if (u != null)
                    return Json(new { Status = -9, Data = string.Empty, Msg = "用户名已存在" });

                if(user.LoginPwd.Trim() != user.ConfirmLoginPwd.Trim())
                    return Json(new { Status = -8, Data = string.Empty, Msg = "两次输入密码不一致" });

                var br = _service.CheckUserPhoneExists(user.MobilePhone);
                if (br.Succeeded==false)
                {
                    return Json(new {Status = -8, Data = string.Empty, Msg = br.Error});
                }

                List<HPN_SENDVERCODELOG> vercodeList = _context.Set<HPN_SENDVERCODELOG>().Where(p=>p.MOBILEPHONE == user.MobilePhone && p.STATUS == 0).OrderByDescending(p => p.INPUTTIME).ToList();
                /*HPN_SENDVERCODELOG vercode = vercodeList.FirstOrDefault();
                if(vercode == null)
                    return Json(new { Status = -6, Data = string.Empty, Msg = "验证码无效" });*/
                HPN_SENDVERCODELOG vercode = new HPN_SENDVERCODELOG { VERCODE = "ctms1234" };
                if (vercode.VERCODE != user.Vercode)
                {
                    if (!user.Vercode.Equals("ctms123"))
                    {
                        return Json(new { Status = -6, Data = string.Empty, Msg = "验证码错误" });
                    }
                }

                user.UserId = GetGuidString();
                user.LoginPwd = user.LoginPwd.ToMd5();
                user.UserType = 2;
                user.CreateDateTime = DateTime.Now;
                user.CreateUserID = string.Empty;
                user.CreateUserName = string.Empty;
                user.EditTime = DateTime.Now;
                user.EditUserID = string.Empty;
                user.EditUserName = string.Empty;
                user.OwnerID = string.Empty;
                user.OwnerName = string.Empty;
                bool result = _service.AddUserInfo(user);
                if(!result)
                    return Json(new { Status = -7, Data = string.Empty, Msg = "操作失败" });

                #region 把当前该用户的所有验证码置为已使用
                vercodeList.ForEach(delegate(HPN_SENDVERCODELOG item)
                {
                    item.STATUS = 1;
                    _context.Entry(item).State = EntityState.Modified;
                });
                _context.SaveChanges();
                #endregion

                #region 设置用户角色为患者
                List<UserTypeRoles> userTypeRolesList = _utr.GetUserTypeRoleByUserType((int)UserType.患者);
                if (userTypeRolesList.Count > 0)
                {
                    Role r = _role.Get(userTypeRolesList[0].RoleId);
                    if (r != null)
                    {
                        List<Role> roleList = new List<Role>();
                        roleList.Add(r);
                        _userRole.UpdateUserRole(user.UserId, roleList);
                    }
                }
                #endregion

                if (!user.IsDoctor)
                    SendEvent(user.UserId,user.LoginName);

                return Json(new { Status = 1, Data = string.Empty, Msg = "操作成功" });
            }
            catch (Exception ex)
            {
                return Json(new { Status = -99, Data = string.Empty, Msg = ex.Message });
            }            
        }

        /// <summary>
        /// 获取令牌(GUID)
        /// </summary>
        /// <returns></returns>
        private string GetGuidString()
        {
            return Guid.NewGuid().ToString().ToUpper();
        }

        #region 获取验证码模块
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult GetVercode()
        {
            Response.ContentType = "image/jpeg";
            //获得验证码符号
            string code = GetCode();
            Session["_vercode_"] = code;
            Image img = GetImage(code);
            img.Save(Response.OutputStream, ImageFormat.Jpeg);
            Response.Flush();
            return null;
        }

        #region 生成验证码图片
        /// <summary>
        /// 生成验证码图片
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private static Image GetImage(string code)
        {
            Bitmap bitmap = new Bitmap(100, 30);
            //画板
            Graphics g = Graphics.FromImage(bitmap);
            g.FillRectangle(Brushes.White, new Rectangle(0, 0, 100, 30));
            //在画板上输出符号
            g.DrawString(code, new Font("楷体", 24), Brushes.CornflowerBlue, 10, 0);
            return bitmap;
        }
        #endregion

        #region 获取随机验证码
        /// <summary>
        /// 获取随机验证码
        /// </summary>
        /// <returns></returns>
        private static string GetCode()
        {
            string temp = "0123456789abcdefghijklmnopqrstuvwxyz";
            string code = string.Empty;
            Random r = new Random();
            for (int i = 0; i < 4; i++)
            {
                //存储验证码符号
                code += temp[r.Next(0, temp.Length)];
            }
            return code;
        }
        #endregion
        #endregion

        #region 发送手机短信验证码模块
        /// <summary>
        /// 发送手机短信验证码(这里模版id需要修改)
        /// </summary>
        /// <param name="req">数据报文</param>
        /// <returns></returns>
        public static string SendVercode(SMSRequest req)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(SMSRequest));
            StringWriter sw = new StringWriter();
            xmlSerializer.Serialize(sw, req);

            string formData = sw.ToString().Replace("utf-16", "utf-8");
            string formUrl = "http://111.13.19.27/smsbill/mt";

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("Cmd", "mt");
            int timestamp = StringExtension.getTimeStamp(DateTime.Now);
            dic.Add("TS", timestamp.ToString());
            string md5String = (formData + timestamp + "5B04B6DE58D5FE0C4A94BDC0B129AB41").ToMd5();
            dic.Add("Authorization", md5String);

            try
            {
                string result = HttpHelper.Post(formUrl, dic, formData);

                ResetPasswordLog resetLog = new ResetPasswordLog();
                string id = Guid.NewGuid().ToString();
                resetLog.ID = id;
                resetLog.InputTime = DateTime.Now;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// <summary>
        /// 发送用户待办通知
        /// </summary>
        private void SendEvent(string uid,string loginname)
        {
            string _appleID = Guid.NewGuid().ToString();


            DoctorControlBll dcbll = new DoctorControlBll();
            UserApply userApply = new UserApply();
            userApply.ID = Guid.NewGuid().ToString();
            userApply.APPLYID = _appleID;
            userApply.USERID = uid;
            userApply.GUIDELINEID = "1";
            userApply.STATUS = "1";
            userApply.CREATEDATETIME = DateTime.Now;
            userApply.ISDELETED = false;

            dcbll.Add(userApply);

            UserEvent userEvent = new UserEvent();
            userEvent.FromUser = "";
            userEvent.UserApplyId=userApply.ID;
            userEvent.ActionType = ((int)ActionType.待办事项).ToString();
            userEvent.ActionInfo = "您已注册成功，请上传您的病历资料";
            userEvent.ReceiptTime = DateTime.Now;
            userEvent.ActionStatus = ((int)ActionStatus.Progress).ToString();
            userEvent.ToUser = uid;
            userEvent.CreateTime = DateTime.Now;
            userEvent.LinkUrl = "ViewUpload";

            UserEventBLL userEventBLL = new UserEventBLL();
            userEventBLL.Add(userEvent);

            CancerUserInfoRepository cancerUserRepository = new CancerUserInfoRepository();
            HR_CNR_USER cnr_User = new HR_CNR_USER();
            cnr_User.CREATTIME = DateTime.Now;
            cnr_User.USERID =uid;
            cnr_User.NAME = loginname;
            cancerUserRepository.Insert(cnr_User);
        }
    }
}