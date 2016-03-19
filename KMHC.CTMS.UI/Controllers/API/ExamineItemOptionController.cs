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
    public class ExamineItemOptionController : ApiController
    {
        private ExamineTemplateItemOptionService _etio;
        private ExamineTemplateItemService _eti;
        private ExamineTemplateService _et;
        private UserInfoService _user;

        public ExamineItemOptionController()
        {
            _etio = new ExamineTemplateItemOptionService();
            _user = new UserInfoService();
            _eti = new ExamineTemplateItemService();
            _et = new ExamineTemplateService();
        }
        
        public IHttpActionResult Get([FromUri]Request<string> req)
        {
            try
            {
                Response<ExamineItemOptionModel> rsp = new Response<ExamineItemOptionModel>();
                PageInfo pageInfo = new PageInfo()
                {
                    PageIndex = req.CurrentPage,
                    PageSize = req.PageSize,
                    OrderField = "CREATEDATETIME",
                    Order = OrderEnum.desc
                };
                pageInfo = null;
                List<ExamineTemplateItemOptions> list = _etio.GetExamineTemplateItemOptionsByKwd(req.ParentId, req.Keyword, ref pageInfo);
                ExamineItemOptionModel model = new ExamineItemOptionModel();
                model.Data = list;
                ExamineTemplateItems eti = _eti.GetExamineTemplateItemsById(req.ParentId);
                model.TemplateItem = eti;
                if (eti != null)
                {
                    ExamineTemplates et = _et.GetExamineTemplateById(eti.ExamineTemplateId);
                    model.Template = et;
                }
                rsp.Data = model;
                //rsp.PagesCount = pageInfo.PagesCount;
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("ExamineItemOptionController[Get]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Post([FromBody]Request<ExamineTemplateItemOptions> req)
        {
            Response<ExamineTemplateItemOptions> rsp = new Response<ExamineTemplateItemOptions>();
            try
            {
                UserInfo user = _user.GetCurrentUser();
                if (user == null)
                    return base.Redirect("/User/Login#/Login");

                ExamineTemplateItemOptions et = req.Data as ExamineTemplateItemOptions;

                bool result = false;
                if (string.IsNullOrEmpty(et.Id))
                {
                    ExamineTemplateItemOptions model = new ExamineTemplateItemOptions();
                    string id = Guid.NewGuid().ToString();
                    model.Id = id;
                    model.Description = et.Description;
                    model.ExamineItemId = et.ExamineItemId;
                    model.CreateUserId = user.UserId;
                    model.CreateUserName = user.LoginName;
                    model.CreateDateTime = DateTime.Now;
                    model.EditUserId = user.UserId;
                    model.EditUserName = user.LoginName;
                    model.EditDateTime = DateTime.Now;
                    model.IsDeleted = 0;
                    result = _etio.AddExamineTemplateItemOptions(model);
                    rsp.Data = model;
                }
                else
                {
                    et.EditUserId = user.UserId;
                    et.EditUserName = user.LoginName;
                    et.EditDateTime = DateTime.Now;
                    result = _etio.UpdateExamineTemplateItemOptions(et);
                    rsp.Data = et;
                }
                if (!result)
                    return BadRequest("操作失败");
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("ExamineItemOptionController[Post]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Delete([FromUri]Request<string> req)
        {
            try
            {
                if (string.IsNullOrEmpty(req.Keyword))
                    return BadRequest("参数错误");

                bool result = _etio.DeleteExamineTemplateItemOptionsById(req.Keyword);

                Response<string> rsp = new Response<string>();
                rsp.Data = "删除成功";

                if (!result)
                    return BadRequest("删除失败");
                return Ok(rsp);

            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("ExamineItemOptionController[Delete]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }
    }

    public class ExamineItemOptionModel
    {
        public ExamineTemplates Template { get; set; }

        public ExamineTemplateItems TemplateItem { get; set; }

        public string ItemType
        {
            get
            {
                if (TemplateItem == null)
                    return string.Empty;
                switch (TemplateItem.Type)
                {
                    case 0:
                        return "单选";
                    case 1:
                        return "多选";
                    default:
                        break;
                }
                return string.Empty;
            }
        }

        public List<ExamineTemplateItemOptions> Data { get; set; }
    }
}
