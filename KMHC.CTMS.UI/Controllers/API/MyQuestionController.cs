using KMHC.CTMS.BLL;
using KMHC.CTMS.BLL.CancerRecord;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.UI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace KMHC.CTMS.UI.Controllers.API
{
    public class MyQuestionController : ApiController
    {
        MyQuestionBLL bll = new MyQuestionBLL();
        public IHttpActionResult Get([FromUri]Request<MyQuestion> request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.ID))
                {
                    Response<IEnumerable<MyQuestion>> response = new Response<IEnumerable<MyQuestion>>();
                    UserInfo user = new UserInfoService().GetCurrentUser();
                    if (user == null || string.IsNullOrEmpty(user.UserId)) return BadRequest("查询不到当前用户");
                    List<MyQuestion> list = bll.GetList(request.Keyword, user.UserId);
                    response.Data = list;
                    return Ok(response);
                }
                else
                {
                    Response<MyQuestion> response = new Response<MyQuestion>();
                    if (!string.IsNullOrEmpty(request.ID))
                    {
                        response.Data = bll.Get(request.ID);
                        return Ok(response);
                    }
                    else
                    {
                        return NotFound();
                    }

                }
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("MetaDataController[Get]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Get(string Keyword, int pageIndex)
        {
            Response<IEnumerable<MyQuestion>> response = new Response<IEnumerable<MyQuestion>>();
            UserInfo user = new UserInfoService().GetCurrentUser();
            if (user == null || string.IsNullOrEmpty(user.UserId)) return BadRequest("查询不到当前用户");
            PageInfo pager = new PageInfo() { PageSize = 10, PageIndex = pageIndex, OrderField = "CreateDateTime", Order = OrderEnum.desc };
            List<MyQuestion> list = bll.GetList(Keyword, ref pager, user.UserId);
            response.Data = list;
            response.PagesCount = pager.PagesCount;
            return Ok(response);
        }

        public IHttpActionResult Post([FromBody]Request<MyQuestion> request)
        {
            try
            {
                Response<MyQuestion> response = new Response<MyQuestion>();
                MyQuestion model = request.Data as MyQuestion;
                if (model == null) return NotFound();
                if (string.IsNullOrEmpty(model.ID))
                {
                    model.CreateDateTime = DateTime.Now;
                    model.ID = Guid.NewGuid().ToString();
                    string ID = bll.Add(model);
                }
                else
                {
                    model.EditTime = DateTime.Now;
                    bool isEditSuccess = bll.Edit(model);
                }
                response.Data = model;
                return Ok(response);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("MetaDataController[Post]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Post([FromUri]string eventID,[FromBody]Request<MyQuestion> request)
        {
            try
            {
                Response<MyQuestion> response = new Response<MyQuestion>();
                MyQuestion model = request.Data as MyQuestion;
                if (model == null) return NotFound();
                if (string.IsNullOrEmpty(model.ID))
                {
                    return BadRequest("修改的Model ID为空!");
                }
                else
                {
                    model.EditTime = DateTime.Now;
                    bool isEditSuccess = bll.Edit(model,eventID);
                }
                response.Data = model;
                return Ok(response);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("MetaDataController[Post]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Delete(string id)
        {
            try
            {
                bool isDeleteSuccess = bll.Delete(id);
                return Ok();

            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("MetaDataController[Delete]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }
    }
}