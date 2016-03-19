using KMHC.CTMS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.Product;

namespace KMHC.CTMS.Model.PrecisionMedicine
{
    public class UserInfo : BaseModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string LoginPwd { get; set; }

        /// <summary>
        /// 已加密的密码
        /// </summary>
        public string MD5LoginPwd { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string MobilePhone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 确认密码
        /// </summary>
        public string ConfirmLoginPwd { get; set; }

        /// <summary>
        /// 是否医生
        /// </summary>
        public bool IsDoctor { get; set; }

        /// <summary>
        /// 用户类型(1、普通用户；2、医生)
        /// </summary>
        public decimal? UserType { get; set; }

        /// <summary>
        /// 重置密码方式(0：邮箱；1：手机)
        /// </summary>
        public int ResetType { get; set; }

        /// <summary>
        /// 重置密码时使用到的token
        /// </summary>
        public string ResetToken { get; set; }

        /// <summary>
        /// 账号状态
        /// </summary>
        public UserStatus Status { get; set; }

        /// <summary>
        /// 账号状态
        /// </summary>
        public string StatusText { get { return EnumHelper.GetDescription(Status); } }

        /// <summary>
        /// 验证码
        /// </summary>
        public string Vercode { get; set; }

        /// <summary>
        /// 账户余额
        /// </summary>
        public decimal Account { get; set; }

        private string _memberID = "b5a65c11-e6cb-4da5-a757-ee1dfaf32ce3";
        /// <summary>
        /// 会员级别
        /// </summary>
        public string MemberID { get { return _memberID;} set { _memberID=value;} }

        /// <summary>
        /// 会员开始时间
        /// </summary>
        public DateTime? MemberStartDate { get; set; }

        /// <summary>
        /// 会员结束时间
        /// </summary>
        public DateTime? MemberEndDate { get; set; }

        /// <summary>
        /// 会员结束时间
        /// </summary>
        public MemberModel Member { get; set; }

        /// <summary>
        /// 患者信息
        /// </summary>
        public HR_CNR_USER PatientInfo { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name
        {
            get
            {
                if (PatientInfo == null)
                {
                    return LoginName;
                }
                return PatientInfo.USERNAME;
            }
        }

        public string SexText
        {
            get
            {
                if (PatientInfo == null)
                    return "";
                if (PatientInfo.SEX == null)
                    return "";
                if (PatientInfo.SEX.Trim() == "1")
                    return "男";
                if (PatientInfo.SEX.Trim() == "0")
                    return "女";
                return "";
            }
        }

        public UserInfo MyDoctor { get; set; }

        public UserInfo MyService { get; set; }

        public string IdCard { get; set; }
    }





    public class CnrUser
    {
        public string USERID { get; set; }
        public string PERSONID { get; set; }
        public string LOCALISM { get; set; }
        public string AREA { get; set; }
        public string DISEASE { get; set; }
        public Nullable<System.DateTime> CREATTIME { get; set; }
        public string CREATBY { get; set; }
        public Nullable<System.DateTime> MODIFYTIME { get; set; }
        public string MODIFYBY { get; set; }
        public string IDCARD { get; set; }
        public string NAME { get; set; }
        public Nullable<decimal> AGE { get; set; }


        public string SexName
        {
            get
            {
                switch (SEX)
                {
                    case "0": return "女";   
                    case "1": return "男"; 
                    default: return "";  
                }
            }
        }


        public string SEX { get; set; }

        public Nullable<System.DateTime> BIRTHDATE { get; set; }
        public string CITY { get; set; }
        public string DOCTORNAME { get; set; }
    }



    /// <summary>
    /// 用户
    /// </summary>
    public class UserInfoForView
    {
        public UserInfo UserInfo { get; set; }
        public CnrUser CnrUser { get; set; }


        public string VerCode { get; set; }


    }




}
