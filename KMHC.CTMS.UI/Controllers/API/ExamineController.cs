using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KMHC.CTMS.BLL;
using KMHC.CTMS.BLL.Examine;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.Model.Examine;
using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.UI.Dtos;

namespace KMHC.CTMS.UI.Controllers.API
{
    public class ExamineController : ApiController
    {
        private ExamineTemplateService _et;
        private ExamineTemplateItemService _eti;
        private ExamineTemplateItemOptionService _etio;
        private UserInfoService _user;

        public ExamineController()
        {
            _et = new ExamineTemplateService();
            _user = new UserInfoService();
            _eti = new ExamineTemplateItemService();
            _etio = new ExamineTemplateItemOptionService();
        }

        //[Route("api/{controller}/{Keyword}/{DicType}/{day}")]
        //public IHttpActionResult Get([FromUri]Request<ExamineTemplates> req)
        public IHttpActionResult Get(int CurrentPage, int PageSize, string Keyword,
            string DicType = null)
        {
            try
            {
                Response<List<ExamineTemplates>> rsp = new Response<List<ExamineTemplates>>();
                PageInfo pageInfo = new PageInfo()
                {
                    PageIndex = CurrentPage,
                    PageSize = PageSize,
                    OrderField = "CREATEDATETIME",
                    Order = OrderEnum.desc
                };
                //List<ExamineTemplates> list = _et.GetExamineTemplateByKwd(req.Keyword, ref pageInfo);
                List<ExamineTemplates> list = _et.GetExamineTemplateList(Keyword,DicType, pageInfo);
                rsp.Data = list;
                rsp.PagesCount = pageInfo.PagesCount;
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("ExamineController[Get]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Get(string templateCode)
        {
            try
            {
                ExamineTemplates model = _et.GetExamineTemplateById(templateCode);
                PageInfo pageInfo = null;
                List<ExamineTemplateItems> items = _eti.GetExamineTemplateItemsByTemplateId(model.Id, ref pageInfo);
                items.ForEach(delegate(ExamineTemplateItems item)
                {
                    List<ExamineTemplateItemOptions> options = _etio.GetExamineTemplateItemOptionsByTemplateItemId(item.Id, ref pageInfo);
                    item.Options = options;
                });
                model.ItemTypes = items;
                Response<ExamineTemplates> rsp = new Response<ExamineTemplates>();
                rsp.Data = model;
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("ExamineController[Get]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Get(int templateType)
        {
            try
            {
                List<ExamineTemplates> list = _et.GetExamineTemplateByType(templateType);
                Response<List<ExamineTemplates>> rsp = new Response<List<ExamineTemplates>>();
                rsp.Data = list;
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("ExamineController[Get]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Post([FromBody]Request<ExamineTemplates> req)
        {
            Response<ExamineTemplates> rsp = new Response<ExamineTemplates>();
            try
            {
                UserInfo user = _user.GetCurrentUser();
                if (user == null)
                    return base.Redirect("/User/Login#/Login");

                ExamineTemplates et = req.Data as ExamineTemplates;

                bool result = false;
                if (string.IsNullOrEmpty(et.Id))
                {
                    ExamineTemplates model = new ExamineTemplates();
                    string id = Guid.NewGuid().ToString();
                    model.Id = id;
                    model.Name = et.Name;
                    model.Description = et.Description;
                    model.Type = et.Type;
                    model.TemplateCode = et.TemplateCode ?? "";
                    model.CreateUserId = user.UserId;
                    model.CreateUserName = user.LoginName;
                    model.CreateDateTime = DateTime.Now;
                    model.EditUserId = user.UserId;
                    model.EditUserName = user.LoginName;
                    model.EditDateTime = DateTime.Now;
                    model.IsDeleted = 0;
                    result = _et.AddExamineTemplates(model);
                    rsp.Data = model;
                }
                else
                {
                    et.EditUserId = user.UserId;
                    et.EditUserName = user.LoginName;
                    et.EditDateTime = DateTime.Now;
                    result = _et.UpdateExamineTemplates(et);
                    rsp.Data = et;
                }
                if (!result)
                    return BadRequest("操作失败");
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("ExamineController[Post]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Delete([FromUri]Request<string> req)
        {
            try
            {
                if (string.IsNullOrEmpty(req.Keyword))
                    return BadRequest("参数错误");

                bool result = _et.DeleteExamineTemplates(req.Keyword);

                Response<string> rsp = new Response<string>();
                rsp.Data = "删除成功";

                if (!result)
                    return BadRequest("删除失败");
                return Ok(rsp);

            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("ExamineController[Delete]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }
    }
}
