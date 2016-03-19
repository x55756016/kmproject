using KMHC.CTMS.BLL;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.UI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using KMHC.CTMS.BLL.CancerRecord;
using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.Common;
using KMHC.CTMS.DAL.Database;
using System.Data.Entity;

namespace KMHC.CTMS.UI.Controllers.API
{
    public class UserManageController:ApiController
    {
        private UserInfoService _user;
        private MyHouseKeeperBLL _myhouse;

        public UserManageController()
        {
            _user = new UserInfoService();
            _myhouse = new MyHouseKeeperBLL();
        }

        public IHttpActionResult Get(int pageIndex,  string keyWord, int pageSize=10)
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

                List<UserInfo> list = new UserInfoService().GetList(ref pageInfo, keyWord);

                Response<IEnumerable<UserInfo>> response = new Response<IEnumerable<UserInfo>>
                {
                    Data = list,
                    PagesCount = pageInfo.PagesCount
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                LogHelper.WriteInfo(ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Get(string uid)
        {
            try
            {
                UserInfo User = new UserInfoService().GetUserInfoByID(uid);
                Response<UserInfo> response = new Response<UserInfo>
                {
                    Data = User,
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                LogHelper.WriteInfo(ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Get()
        {
            try
            {
                UserInfo User = new UserInfoService().GetCurrentUser();
                Response<UserInfo> response = new Response<UserInfo>
                {
                    Data = User,
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                LogHelper.WriteInfo(ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Post([FromBody]Request<UserInfo> request)
        {
            try
            {
                UserInfo user = request.Data as UserInfo;

                UserInfo currUser = _user.GetCurrentUser();

                #region 处理“我的医生”
                if (user.MyDoctor != null)
                {
                    if (!string.IsNullOrEmpty(user.MyDoctor.UserId))
                    {
                        if (user.MyDoctor != null)
                        {
                            using (DbContext db = new CRDatabase())
                            {
                                var list = db.Set<CTMS_MYHOUSEKEEPER>().AsNoTracking().Where(o => !o.ISDELETED && o.USERID.Equals(user.UserId)).Select(_myhouse.EntityToModel).ToList();
                                string strDoctor = ((UserType)user.MyDoctor.UserType).ToString();
                                MyHouseKeeper model = list.FindAll(p => p.ObjectType.Trim() == strDoctor.Trim()).FirstOrDefault();
                                if (model == null)
                                {
                                    model = new MyHouseKeeper();
                                    model.ID = Guid.NewGuid().ToString();
                                    model.UserID = user.UserId;
                                    model.LoginName = user.LoginName;
                                    model.ObjectType = strDoctor;
                                    model.ObjectUserID = user.MyDoctor.UserId;
                                    model.ObjectLoginName = user.MyDoctor.LoginName;
                                    model.CreateUserID = currUser.UserId;
                                    model.CreateUserName = currUser.Name;
                                    model.CreateDateTime = DateTime.Now;
                                    model.EditUserID = currUser.UserId;
                                    model.EditUserName = currUser.Name;
                                    model.EditTime = DateTime.Now;
                                    model.OwnerID = "";
                                    model.OwnerName = "";
                                    model.IsDeleted = false;
                                    _myhouse.Add(model);
                                }
                                else
                                {
                                    model.ObjectUserID = user.MyDoctor.UserId;
                                    model.ObjectLoginName = user.MyDoctor.LoginName;
                                    model.EditUserID = currUser.UserId;
                                    model.EditUserName = currUser.Name;
                                    model.EditTime = DateTime.Now;
                                    _myhouse.Edit(model);
                                }
                            }
                        }
                    }
                }
                #endregion

                #region 处理“我的医生”
                if (user.MyService != null)
                {
                    if (!string.IsNullOrEmpty(user.MyService.UserId))
                    {
                        if (user.MyService != null)
                        {
                            using (DbContext db = new CRDatabase())
                            {
                                var list = db.Set<CTMS_MYHOUSEKEEPER>().AsNoTracking().Where(o => !o.ISDELETED && o.USERID.Equals(user.UserId)).Select(_myhouse.EntityToModel).ToList();
                                string strDoctor = ((UserType)user.MyService.UserType).ToString();
                                MyHouseKeeper model = list.FindAll(p => p.ObjectType.Trim() == strDoctor.Trim()).FirstOrDefault();
                                if (model == null)
                                {
                                    model = new MyHouseKeeper();
                                    model.ID = Guid.NewGuid().ToString();
                                    model.UserID = user.UserId;
                                    model.LoginName = user.LoginName;
                                    model.ObjectType = strDoctor;
                                    model.ObjectUserID = user.MyService.UserId;
                                    model.ObjectLoginName = user.MyService.LoginName;
                                    model.CreateUserID = currUser.UserId;
                                    model.CreateUserName = currUser.Name;
                                    model.CreateDateTime = DateTime.Now;
                                    model.EditUserID = currUser.UserId;
                                    model.EditUserName = currUser.Name;
                                    model.EditTime = DateTime.Now;
                                    model.OwnerID = "";
                                    model.OwnerName = "";
                                    model.IsDeleted = false;
                                    _myhouse.Add(model);
                                }
                                else
                                {
                                    model.ObjectUserID = user.MyService.UserId;
                                    model.ObjectLoginName = user.MyService.LoginName;
                                    model.EditUserID = currUser.UserId;
                                    model.EditUserName = currUser.Name;
                                    model.EditTime = DateTime.Now;
                                    _myhouse.Edit(model);
                                }
                            }
                        }
                    }
                }
                #endregion

                if (user != null)
                {
                    new UserInfoService().UpdateUserInfo(user);
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