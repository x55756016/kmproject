using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.Model.Repository.Implement;
using KMHC.CTMS.Model.Repository.Interface;
using KMHC.CTMS.Common.Cached;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.BLL.Product;
using KMHC.CTMS.Model.Common;

namespace KMHC.CTMS.BLL
{
    /*
     * 描述:定义用户操作类
     *  
     * 修订历史: 
     * 日期          修改人              内容
     * 20151113      刘佳豪              创建 
     * 20160129      林德力            添加CheckUserPhoneExists方法，检测手机是否已经被注册
     *  
     */
    public class UserInfoService
    {
        private ICached _cached = new LocalCached();

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
        /// 登录
        /// </summary>
        /// <param name="loginName">登录名</param>
        /// <param name="loginPwd">登录密码</param>
        /// <returns></returns>
        public UserInfo Login(string loginName, string loginPwd)
        {
            using (EFUserInfoRepository _repository = new EFUserInfoRepository())
            {
                UserInfo userInfo = _repository.GetUserInfoByLoginName(loginName);
                if (userInfo == null)
                    return userInfo;

                //loginPwd = loginPwd.ToMd5();
                if (!userInfo.LoginPwd.Equals(loginPwd))
                    return null;
                //增加Member属性
                if (!string.IsNullOrEmpty(userInfo.MemberID))
                {
                    userInfo.Member = new MemberBLL().GetMember(userInfo.MemberID);
                }
                return userInfo;
            }            
        }

        public List<UserInfo> GetAll()
        {
            using (EFUserInfoRepository _rsp = new EFUserInfoRepository())
            {
                return _rsp.FindAll().Select(_rsp.LoadModelFromEntity).ToList();
            }
        }

        /// <summary>
        /// 通过id获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserInfo GetUserInfoByID(string id)
        {
            using (EFUserInfoRepository _repository = new EFUserInfoRepository())
            {
                UserInfo userInfo = _repository.GetUserInfoByID(id);
                //增加Member属性
                if (!string.IsNullOrEmpty(userInfo.MemberID))
                {
                    userInfo.Member = new MemberBLL().GetMember(userInfo.MemberID);
                }
                return userInfo;
            }
        }


        /// <summary>
        /// 根据id获取档案信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CnrUser GetCnrUserById(string id)
        {
            using (var context = new CRDatabase())
            {
                var model = EntityToModel(context.HR_CNR_USER.FirstOrDefault(p => p.USERID == id));
                return model;
            }
        }

        /// <summary>
        /// 根据登录名获取用户信息
        /// </summary>
        /// <param name="loginName">登录名</param>
        /// <returns></returns>
        public UserInfo GetUserByLoginName(string loginName)
        {
            using (EFUserInfoRepository _repository = new EFUserInfoRepository())
            {
                UserInfo userInfo = _repository.GetUserInfoByLoginName(loginName);
                return userInfo;
            }
        }

        /// <summary>
        /// 通过邮箱获取用户信息
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public UserInfo GetUserByEmail(string email)
        {
            using (EFUserInfoRepository _repository = new EFUserInfoRepository())
            {
                UserInfo userInfo = _repository.GetUserInfoByEmail(email);
                return userInfo;
            }
        }

        /// <summary>
        /// 通过手机获取用户信息
        /// </summary>
        /// <param name="mobilePhone"></param>
        /// <returns></returns>
        public UserInfo GetUserByMobilePhone(string mobilePhone)
        {
            using (EFUserInfoRepository _repository = new EFUserInfoRepository())
            {
                UserInfo userInfo = _repository.GetUserInfoByMobilePhone(mobilePhone);
                return userInfo;
            }
        }

        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool AddUserInfo(UserInfo user)
        {
            using (EFUserInfoRepository _repository = new EFUserInfoRepository())
            {
                bool result = _repository.AddUserInfo(user);
                return result;
            }
        }

        /// <summary>
        /// 通过id更新用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool UpdateUserInfo(UserInfo user)
        {
            using (EFUserInfoRepository _repository = new EFUserInfoRepository())
            {
                bool result = _repository.UpdateUserInfo(user);
                return result;
            }
        }

        /// <summary>
        /// 更新重置密码记录信息
        /// </summary>
        /// <param name="resetLog"></param>
        /// <returns></returns>
        public bool UpdateResetPasswordLog(ResetPasswordLog resetLog)
        {
            using (EFResetPasswordLogRepository _reset = new EFResetPasswordLogRepository())
            {
                bool result = _reset.UpdateResetPasswordLog(resetLog);
                return result;
            }
        }

        /// <summary>
        /// 添加重置密码记录信息
        /// </summary>
        /// <param name="resetLog"></param>
        /// <returns></returns>
        public bool AddResetPasswordLog(ResetPasswordLog resetLog)
        {
            using (EFResetPasswordLogRepository _reset = new EFResetPasswordLogRepository())
            {
                bool result = _reset.AddResetPasswordLog(resetLog);
                return result;
            }
        }

        /// <summary>
        /// 通过id获取重置记录信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResetPasswordLog GetResetPasswordLogByID(string id)
        {
            using (EFResetPasswordLogRepository _reset = new EFResetPasswordLogRepository())
            {
                ResetPasswordLog resetPasswordLog = _reset.GetResetPasswordLogByID(id);
                return resetPasswordLog;
            }
        }

        /// <summary>
        /// 缓存信息
        /// </summary>
        /// <param name="tokenValue"></param>
        /// <param name="userInfo"></param>
        public void CacheInfo(string tokenValue, object userInfo)
        {
            _cached.Set(tokenValue, userInfo, CacheTime);
        }

        /// <summary>
        /// 根据令牌获取用户登录信息(没有则返回null)
        /// </summary>
        /// <param name="tokenValue">令牌</param>
        /// <returns></returns>
        public UserInfo GetLoginInfo(string tokenValue)
        {
            if (string.IsNullOrEmpty(tokenValue))
                return null;
            UserInfo userInfo = _cached.Get(tokenValue) as UserInfo;
            return userInfo;
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        public bool Logout()
        {
            try
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["Token"];
                if (cookie == null)
                    return true;
                _cached.Remove(cookie.Value + CacheString);
                _cached.Remove(cookie.Value);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }            
        }

        /// <summary>
        /// 判断当前是否已登录
        /// </summary>
        /// <returns></returns>
        public bool IsLogin()
        {
            bool isLogin = false;
            HttpCookie cookie = HttpContext.Current.Request.Cookies["Token"];
            if (cookie != null)
            {
                UserInfo user = GetLoginInfo(cookie.Value);
                if (user != null)
                {
                    isLogin = true;
                }
            }
            return isLogin;
        }

        public UserInfo GetCurrentUser()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["Token"];
            if (cookie == null || string.IsNullOrEmpty(cookie.Value))
            {
                return null;
            }
            return GetLoginInfo(cookie.Value);
        }

        public void ReflashUserInfo()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["Token"];
            if (cookie != null)
            {
                if (!string.IsNullOrEmpty(cookie.Value))
                {
                    UserInfo user = GetLoginInfo(cookie.Value);
                    if (user != null)
                    {
                        CacheInfo(cookie.Value, user);
                    }
                }
            }
        }

        public List<UserInfo> GetList(ref PageInfo pager, string keyWord)
        {
            using (EFUserInfoRepository _repository = new EFUserInfoRepository())
            {
                var list = _repository.FindAll();
                if (!string.IsNullOrEmpty(keyWord))
                {
                    list = list.Where(u => u.LOGINNAME.Contains(keyWord));
                }
                return list.Paging(ref pager).ToList().Select(u => _repository.LoadModelFromEntity(u)).ToList(); 
            }
        }

        /// <summary>
        /// 根据用户名获取数据列表
        /// </summary>
        /// <param name="loginname"></param>
        /// <returns></returns>
        public List<UserInfo> GetList(string loginname)
        {
            using (EFUserInfoRepository _repository = new EFUserInfoRepository())
            {
                var list = _repository.FindAll(p => p.LOGINNAME.Contains(loginname)).ToList();

                return list.Select(u => _repository.LoadModelFromEntity(u)).ToList();
            }
        }

        /// <summary>
        /// 根据用户类型获取用户列表
        /// </summary>
        /// <param name="userType"></param>
        /// <returns></returns>
        public List<UserInfo> GetUsersByUserType(int userType)
        {
            using (EFUserInfoRepository _repository = new EFUserInfoRepository())
            {
                return _repository.GetUsersListByUserType(userType);
            }
        }

        /// <summary>
        /// 检查该手机是否已经被使用
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public BaseResult CheckUserPhoneExists(string phone)
        {
            using (var context = new CRDatabase())
            {
                BaseResult br = new BaseResult(){Succeeded = true,Error = ""};

                var list = context.Set<CTMS_SYS_USERINFO>().Where(p => p.MOBILEPHONE == phone).ToList();
                if (list.Any())
                {
                    br.Succeeded = false;
                    br.Error = "该手机已被注册";
                }
                return br;
            }
        }

        #region 用户信息管理页面

        public UserInfoForView GetUserInfoForView(string userId)
        {
            using (var db = new CRDatabase())
            {
                UserInfoForView model = new UserInfoForView();
                model.UserInfo = GetUserInfoByID(userId);
                model.CnrUser = EntityToModel(db.HR_CNR_USER.FirstOrDefault(p => p.USERID == userId));
                return model;
            }
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userinfoModel"></param>
        /// <returns></returns>
        public BaseResult UpdaetUserInfo(string userId,UserInfoForView userinfoModel)
        {
            BaseResult br = new BaseResult(){Succeeded = false,Error = ""};
            using (var context = new CRDatabase())
            {
                var entity = context.CTMS_SYS_USERINFO.FirstOrDefault(p => p.USERID == userId);

                //更新用户基本信息
                if (entity != null)
                {
                    //1.验证手机是否被更改
                    if (entity.MOBILEPHONE != userinfoModel.UserInfo.MobilePhone)
                    {
                        var vercodeList = context.Set<HPN_SENDVERCODELOG>()
                            .Where(p => p.MOBILEPHONE == userinfoModel.UserInfo.MobilePhone && p.STATUS == 0)
                            .OrderByDescending(p => p.INPUTTIME).FirstOrDefault();
                        if (vercodeList.VERCODE != userinfoModel.VerCode)
                        {
                            br.Succeeded = false;
                            br.Error = "验证码有误";
                            return br;
                        }
                        else
                        {
                            entity.MOBILEPHONE = userinfoModel.UserInfo.MobilePhone;
                        }
                    }

                    if (userinfoModel.UserInfo.LoginPwd!="")//判断密码是否更改了
                    {
                        entity.LOGINPWD = userinfoModel.UserInfo.LoginPwd;
                    }

                    entity.EMAIL = userinfoModel.UserInfo.Email;
                    entity.MOBILEPHONE = userinfoModel.UserInfo.MobilePhone;

                    //更新用户的档案信息(只有用户才有档案)
                    var recordEntity = context.HR_CNR_USER.FirstOrDefault(p => p.USERID == userId);
                    if (recordEntity!=null)
                    {
                        //recordEntity.NAME = userinfoModel.CnrUser.NAME;
                        recordEntity.SEX = userinfoModel.CnrUser.SEX;
                        recordEntity.BIRTHDATE = userinfoModel.CnrUser.BIRTHDATE;
                        recordEntity.LOCALISM = userinfoModel.CnrUser.LOCALISM;
                        recordEntity.CITY = userinfoModel.CnrUser.CITY;
                        recordEntity.DISEASE = userinfoModel.CnrUser.DISEASE;
                        recordEntity.DOCTORNAME = userinfoModel.CnrUser.DOCTORNAME;
                    }
                    br.Succeeded = context.SaveChanges() > 0;
                }
                else
                {
                    br.Succeeded = false;
                    br.Error = "尝试更改不存在的用户";
                }
            }
            return br;
        }

        #region 模型映射


        public CnrUser EntityToModel(HR_CNR_USER entity)
        {
            if (entity!=null)
            {
                var model = new CnrUser()
                {
                    USERID = entity.USERID,
                    PERSONID = entity.PERSONID,
                    LOCALISM = entity.LOCALISM,
                    AREA = entity.AREA,
                    DISEASE = entity.DISEASE,
                    CREATTIME = entity.CREATTIME,
                    CREATBY = entity.CREATBY,
                    MODIFYTIME = entity.MODIFYTIME,
                    MODIFYBY = entity.MODIFYBY,
                    IDCARD = entity.IDCARD,
                    NAME = entity.USERNAME,
                    AGE = entity.AGE,
                    SEX = entity.SEX,
                     
                    BIRTHDATE = entity.BIRTHDATE,
                    CITY = entity.CITY,
                    DOCTORNAME = entity.DOCTORNAME
                };
                return model;
            }
            return null;
        }

        #endregion
        #endregion
    }
}
