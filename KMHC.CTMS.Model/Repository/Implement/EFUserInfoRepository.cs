using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.Model.Repository.Interface;
using KMHC.CTMS.Model.Repository;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Common;

namespace KMHC.CTMS.Model.Repository.Implement
{
    /*
     * 描述:定义IUserInfoRepository的EF实现
     *  
     * 修订历史: 
     * 日期          修改人              内容
     * 20151113      刘佳豪              创建 
     *  
     */
    public class EFUserInfoRepository : BaseRepository<CTMS_SYS_USERINFO>, IUserInfoRepository
    {

        public EFUserInfoRepository() : base(new CRDatabase()) { }

        public UserInfo GetUserInfoByLoginName(string loginName)
        {
            CTMS_SYS_USERINFO userInfo = FindOne(p => p.LOGINNAME == loginName);
            return LoadModelFromEntity(userInfo);
        }

        public bool AddUserInfo(UserInfo user)
        {
            CTMS_SYS_USERINFO userInfo = LoadEntityFromModel(user);
            bool result = Insert(userInfo);
            return result;
        }

        public UserInfo LoadModelFromEntity(CTMS_SYS_USERINFO entity)
        {
            if (entity == null)
                return null;

            UserInfo model = new UserInfo();
            model.UserId = entity.USERID;
            if (!string.IsNullOrEmpty(entity.USERID))
            {
                using (CRDatabase db = new CRDatabase())
                {
                    HR_CNR_USER patient = db.HR_CNR_USER.FirstOrDefault(p => p.USERID == entity.USERID);
                    if(patient != null)
                        model.PatientInfo = patient;

                    List<CTMS_MYHOUSEKEEPER> myHouse = db.CTMS_MYHOUSEKEEPER.Where(p=>p.USERID == entity.USERID).ToList();
                    if (myHouse.Count > 0)
                    {
                        CTMS_MYHOUSEKEEPER temp = myHouse.FirstOrDefault(p => p.OBJECTTYPE == UserType.医生.ToString());
                        if (temp != null)
                        {
                            UserInfo tempUser = LoadModelFromEntity(db.CTMS_SYS_USERINFO.FirstOrDefault(p => p.USERID == temp.OBJECTUSERID));
                            model.MyDoctor = tempUser;
                        }

                        temp = myHouse.FirstOrDefault(p => p.OBJECTTYPE == UserType.客服.ToString());
                        if (temp != null)
                        {
                            UserInfo tempUser = LoadModelFromEntity(db.CTMS_SYS_USERINFO.FirstOrDefault(p => p.USERID == temp.OBJECTUSERID));
                            model.MyService = tempUser;
                        }
                    }
                }
            }
            model.LoginName = entity.LOGINNAME;
            model.LoginPwd = entity.LOGINPWD;
            model.MobilePhone = entity.MOBILEPHONE;
            model.Email = entity.EMAIL;
            model.UserType = entity.USERTYPE;
            model.Status = (UserStatus)entity.USERSTATUS;
            model.MemberID = entity.MEMBERID;
            model.Account = entity.ACCOUNT;
            model.MemberStartDate = entity.MEMBERSTARTDATE;
            model.MemberEndDate = entity.MEMBERENDDATE;
          

            //通用
            model.CreateUserID = entity.CREATEUSERID;
            model.CreateUserName = entity.CREATEUSERNAME;
            model.CreateDateTime = entity.CREATEDATETIME;
            model.EditUserID = entity.EDITUSERID;
            model.EditUserName = entity.EDITUSERNAME;
            model.EditTime = entity.EDITDATETIME;
            model.OwnerID = entity.OWNERID;
            model.OwnerName = entity.OWNERNAME;
            model.IsDeleted = entity.ISDELETED.Value;

            return model;
        }

        protected CTMS_SYS_USERINFO LoadEntityFromModel(UserInfo model)
        {
            if (model == null)
                return null;

            CTMS_SYS_USERINFO entity = new CTMS_SYS_USERINFO();
            entity.USERID
                
                = model.UserId;
            entity.LOGINNAME = model.LoginName;
            entity.LOGINPWD = model.LoginPwd;
            entity.MOBILEPHONE = model.MobilePhone;
            entity.EMAIL = model.Email;
            entity.USERSTATUS = (int)model.Status;
            entity.ACCOUNT = model.Account;
            entity.MEMBERID = model.MemberID;
            entity.MEMBERSTARTDATE = model.MemberStartDate;
            entity.MEMBERENDDATE = model.MemberEndDate;

            //通用
            entity.CREATEUSERID = model.CreateUserID;
            entity.CREATEUSERNAME = model.CreateUserName;
            entity.CREATEDATETIME = model.CreateDateTime;
            entity.EDITUSERID = model.EditUserID;
            entity.EDITUSERNAME = model.EditUserName;
            entity.EDITDATETIME = model.EditTime;
            entity.OWNERID = model.OwnerID;
            entity.OWNERNAME = model.OwnerName;
            entity.ISDELETED = model.IsDeleted;
            entity.USERTYPE = model.UserType;

            return entity;
        }


        public UserInfo GetUserInfoByEmail(string email)
        {
            CTMS_SYS_USERINFO userInfo = FindOne(p => p.EMAIL == email);
            return LoadModelFromEntity(userInfo);
        }

        public UserInfo GetUserInfoByMobilePhone(string mobilePhone)
        {
            CTMS_SYS_USERINFO userInfo = FindOne(p => p.MOBILEPHONE == mobilePhone);
            return LoadModelFromEntity(userInfo);
        }


        public UserInfo GetUserInfoByID(string id)
        {
            CTMS_SYS_USERINFO userInfo = FindOne(p => p.USERID == id);
            return LoadModelFromEntity(userInfo);
        }


        public bool UpdateUserInfo(UserInfo user)
        {
            CTMS_SYS_USERINFO userInfo = LoadEntityFromModel(user);
            bool result = Update(userInfo);
            return result;
        }


        public List<UserInfo> GetUsersListByUserType(int userType)
        {
            var list = FindAll(p => p.USERTYPE == userType).Select(LoadModelFromEntity).ToList();
            return list;
        }
    }
}
