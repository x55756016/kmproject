using KMHC.CTMS.BLL;
using KMHC.CTMS.BLL.CancerProcess;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.CancerProcess;
using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.UI.Dtos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Http;

namespace KMHC.CTMS.UI.Controllers.API
{
    public class DictionaryManageController : ApiController
    {
        DictionaryBLL bll = new DictionaryBLL();

        public IHttpActionResult Get(int CurrentPage=1)
        {
            PageInfo pageInfo = new PageInfo()
            {
                PageIndex = CurrentPage,
                PageSize = 10,
                OrderField = "CreateDate",
                Order = OrderEnum.desc
            };
            Expression<Func<HR_DICTIONARY, bool>> predicate = p => p.ISDELETED == false && p.FATHERID.Equals("0");

            IEnumerable<HrDictionary> list = bll.GetList(pageInfo,predicate);

            Response<IEnumerable<HrDictionary>> response = new Response<IEnumerable<HrDictionary>>
            {
                Data = list,
                PagesCount = pageInfo.PagesCount
            };

            return Ok(response);
        }

        public IHttpActionResult Get(string Id)
        {
            try
            {
                HrDictionary model = bll.GetOne(p => p.DICTIONARYID.Equals(Id));
                if (model != null)
                {
                    HrDictionary father = bll.GetOne(p => p.DICTIONARYID.Equals(model.FatherId));
                    model.FatherId = string.Format("{0}#{1}", father.DictionaryId, father.DictionaryName);
                }

                return Ok(model);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("DictionaryManageController[Get]", ex.ToString());
                return BadRequest("异常！");
            }
        }

        public IHttpActionResult Get(int CurrentPage,int Type,string Name="")
        {
            PageInfo pageInfo = new PageInfo()
            {
                PageIndex = CurrentPage,
                PageSize = 10,
                OrderField = "CreateDate",
                Order = OrderEnum.desc
            };
            Expression<Func<HR_DICTIONARY, bool>> predicate = p => p.ISDELETED == false;
            if (!string.IsNullOrEmpty(Name))
                predicate = p => p.ISDELETED == false && p.DICTIONARYNAME.Contains(Name);

            IEnumerable<HrDictionary> list = bll.GetList(pageInfo, predicate);

            Response<IEnumerable<HrDictionary>> response = new Response<IEnumerable<HrDictionary>>
            {
                Data = list,
                PagesCount = pageInfo.PagesCount
            };

            return Ok(response);
        }

        public IHttpActionResult Post([FromBody]Request<HrDictionary> request)
        {
            try
            {
                HrDictionary model = request.Data;
                bool result = true;
                if (string.IsNullOrEmpty(model.DictionaryId))
                {
                   result= Insert(model);
                }
                else
                {
                    result = Update(model);
                }

                return Ok(result==true?"ok":"false");
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("DictionaryManageController[Post]", ex.ToString());
                return BadRequest(ex.ToString());
            }
        }

        public IHttpActionResult Delete(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return BadRequest("非法请求！");
            }

            try
            {
                HrDictionary model = bll.GetOne(p=>p.DICTIONARYID.Equals(Id));
                if (model == null)
                {
                    return Ok("ok");
                }

                model.IsDeleted = true;
                bll.Edit(model);

                return Ok("ok");
            }
            catch(Exception ex)
            {
                LogService.WriteErrorLog("DictionaryManageController[Delete]", ex.ToString());
                return BadRequest("异常！");
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private bool Insert(HrDictionary model)
        {
            UserInfo currentUser = new UserInfoService().GetCurrentUser();
            string userName = currentUser == null ? "" : currentUser.LoginName;

            model.CreateDate = DateTime.Now;
            model.CreateUser = userName;
            model.SystemNeed = "0";

            if(string.IsNullOrEmpty(bll.Add(model)))
                return false;
            return true;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private bool Update(HrDictionary model)
        {
            UserInfo currentUser = new UserInfoService().GetCurrentUser();
            string userName = currentUser == null ? "" : currentUser.LoginName;

            model.UpdateDate = DateTime.Now;
            model.UpdateUser = userName;

            return bll.Edit(model);
        }
    }
}