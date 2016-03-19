
using KMHC.CTMS.UI.Dtos;
using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.Model.Repository;
using KMHC.CTMS.Model.Repository.Implement;
using KMHC.CTMS.Model.Repository.Implement.CancerRecord;
using KMHC.CTMS.Model.Repository.Interface;
using KMHC.CTMS.DAL.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.UI.Attribute;
using KMHC.CTMS.Common;
using System.Linq.Dynamic;
using KMHC.CTMS.BLL.Authorization;
using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.BLL;
using System.Collections;

namespace KMHC.CTMS.UI.Controllers.API
{
    [System.Web.Http.RoutePrefix("api/cancerUser")]
    public class CancerUserController : ApiController
    {
        CancerUserInfoRepository _repository = new CancerUserInfoRepository();
        IPersonInfoRepository personRepostitory = new EFPersonInfoRepository();
        [ApiAuth(AuthCode = "HR_CNR_USER", Permission = PermissionType.View)]
        public  IHttpActionResult Get(string id)
        {
            try
            {
               var model= _repository.FindOne(o=>o.USERID==id);

                if (model == null)
                {
                    return NotFound();
                }
                return Ok(model);
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
                return BadRequest(ex.Message);
            }
        }
         [ApiAuth(AuthCode = "HR_CNR_USER", Permission = PermissionType.View)]
        public IHttpActionResult Get([FromUri]Request<HR_CNR_USER> request)
        {
            Response<IEnumerable<object>> response = new Response<IEnumerable<object>>();
            try
            {
                var model = _repository.FindAll();

                if (model == null)
                {
                    return NotFound();
                }

                if (!string.IsNullOrEmpty(request.Keyword))
                {
                    model = model.Where(o => o.IDCARD.Equals(request.Keyword));
                }
                if (!string.IsNullOrEmpty(request.UserName))
                {
                    model = model.Where(o => o.NAME.Contains(request.UserName));
                }
                UserInfo user = new UserInfoService().GetCurrentUser();
                if(user==null) return BadRequest("获取不到当前用户信息!");
                ArrayList args = new ArrayList();
                string filterString = new RoleFunctionBLL().GetFilterString(user.UserId, "HR_CNR_USER", PermissionType.View, ref args);
                model = model.where(filterString,args.ToArray());
                List<HR_CNR_USER> list = model.ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    HR_CNR_USER entity = list[i];
                    PersonInfo person = null;
                    if (!string.IsNullOrEmpty(entity.PERSONID))
                    {
                        int personID = 0;
                        int.TryParse(entity.PERSONID, out personID);
                        if(personID>0)
                        { 
                            person = personRepostitory.Get(personID);
                        }
                    }
                    else if (!string.IsNullOrEmpty(entity.IDCARD))
                    {
                        person = personRepostitory.Get(entity.IDCARD);
                    }
                    if (person != null)
                    {
                        if (person.BirthDate.Length <= 4) continue;
                        int birthDateYear = Convert.ToInt32(person.BirthDate.Substring(0, 4));
                        entity.AGE = DateTime.Now.Year - birthDateYear + 1;
                        switch (person.Gender)
                        {
                            case "1":
                                entity.SEX = "男";
                                break;
                            case "2":
                                entity.SEX = "女";
                                break;
                            case "0":
                                entity.SEX = "未知";
                                break;
                            default:
                                entity.SEX = "其他";
                                break;
                        }
                    }
                }
                response.Data = list;
                return Ok(response);
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
                return BadRequest(ex.Message);
            }
        }

         [ApiAuth(AuthCode = "HR_CNR_USER", Permission = PermissionType.View)]
         public IHttpActionResult Get(string IDCard,string UserName,string Disease)
         {
             Response<IEnumerable<object>> response = new Response<IEnumerable<object>>();
             try
             {
                 var model = _repository.FindAll();

                 if (model == null)
                 {
                     return NotFound();
                 }

                 if (!string.IsNullOrEmpty(IDCard))
                 {
                     model = model.Where(o => o.IDCARD.Contains(IDCard));
                 }
                 if (!string.IsNullOrEmpty(UserName))
                 {
                     model = model.Where(o => o.NAME.Contains(UserName));
                 }
                 if (!string.IsNullOrEmpty(Disease))
                 {
                     model = model.Where(o => o.DISEASE.Contains(Disease));
                 }
                 UserInfo user = new UserInfoService().GetCurrentUser();
                 if (user == null) return BadRequest("获取不到当前用户信息!");
                 ArrayList args = new ArrayList();
                 string filterString = new RoleFunctionBLL().GetFilterString(user.UserId, "HR_CNR_USER", PermissionType.View, ref args);
                 model = model.where(filterString, args.ToArray());
                 List<HR_CNR_USER> list = model.ToList();
                 for (int i = 0; i < list.Count; i++)
                 {
                     HR_CNR_USER entity = list[i];
                     PersonInfo person = null;
                     if (!string.IsNullOrEmpty(entity.PERSONID))
                     {
                         int personID = 0;
                         int.TryParse(entity.PERSONID, out personID);
                         if (personID > 0)
                         {
                             person = personRepostitory.Get(personID);
                         }
                     }
                     else if (!string.IsNullOrEmpty(entity.IDCARD))
                     {
                         person = personRepostitory.Get(entity.IDCARD);
                     }
                     if (person != null)
                     {
                         if (person.BirthDate.Length <= 4) continue;
                         int birthDateYear = Convert.ToInt32(person.BirthDate.Substring(0, 4));
                         entity.AGE = DateTime.Now.Year - birthDateYear + 1;
                         switch (person.Gender)
                         {
                             case "1":
                                 entity.SEX = "男";
                                 break;
                             case "2":
                                 entity.SEX = "女";
                                 break;
                             case "0":
                                 entity.SEX = "未知";
                                 break;
                             default:
                                 entity.SEX = "其他";
                                 break;
                         }
                     }
                 }
                 response.Data = list;
                 return Ok(response);
             }
             catch (Exception ex)
             {
                 LogHelper.WriteError(ex.ToString());
                 return BadRequest(ex.Message);
             }
         }

        [ApiAuth(AuthCode = "HR_CNR_USER", Permission = PermissionType.Add)]
        public  IHttpActionResult Post([FromBody]Request<HR_CNR_USER> request)
        {
            try
            {
               
                var model= request.Data;
                if (!string.IsNullOrEmpty(model.USERID))
                {
                    model.MODIFYTIME = DateTime.Now;
                    _repository.Update(model);
                }
                else
                {
                    model.USERID = Guid.NewGuid().ToString();
                    model.CREATTIME = DateTime.Now;
                    if (!string.IsNullOrEmpty(model.IDCARD))
                    {
                        PersonInfo person= personRepostitory.Get(model.IDCARD);
                        if (person != null)
                        {
                            model.PERSONID = person.PersonId.ToString();
                        }
                    }
                    _repository.Insert(model);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [ApiAuth(AuthCode = "HR_CNR_USER", Permission = PermissionType.Modify)]
        public IHttpActionResult Post([FromUri]string ID,[FromBody]Request<HR_CNR_USER> request)
        {
            try
            {

                var model = request.Data;
                if (!string.IsNullOrEmpty(model.USERID))
                {
                    model.MODIFYTIME = DateTime.Now;
                    _repository.Update(model);
                }
                else
                {
                    model.USERID = Guid.NewGuid().ToString();
                    model.CREATTIME = DateTime.Now;
                    if (!string.IsNullOrEmpty(model.IDCARD))
                    {
                        PersonInfo person = personRepostitory.Get(model.IDCARD);
                        if (person != null)
                        {
                            model.PERSONID = person.PersonId.ToString();
                        }
                    }
                    _repository.Insert(model);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [ApiAuth(AuthCode = "HR_CNR_USER", Permission = PermissionType.Delete)]
        public IHttpActionResult Delete(string id)
        {
            try
            {
                bool isDeleteSuccess = _repository.DeleteById(id);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        } 
	}
}