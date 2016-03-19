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
    public class ExamineItemController : ApiController
    {
        private ExamineTemplateItemService _eti;
        private UserInfoService _user;
        private ExamineTemplateService _et;

        public ExamineItemController()
        {
            _eti = new ExamineTemplateItemService();
            _user = new UserInfoService();
            _et = new ExamineTemplateService();
        }

        public IHttpActionResult Get([FromUri]Request<string> req)
        {
            try
            {
                Response<ExamineItemModel> rsp = new Response<ExamineItemModel>();
                PageInfo pageInfo = new PageInfo()
                {
                    PageIndex = req.CurrentPage,
                    PageSize = req.PageSize,
                    OrderField = "CREATEDATETIME",
                    Order = OrderEnum.desc
                };
                List<ExamineTemplateItems> list = _eti.GetExamineTemplateItemsByKwd(req.ParentId,req.Keyword, ref pageInfo);
                ExamineItemModel eim = new ExamineItemModel();
                eim.Data = list;
                ExamineTemplates p = _et.GetExamineTemplateById(req.ParentId);
                eim.TemplateType = p.Type;
                eim.Template = p;
                rsp.Data = eim;
                rsp.PagesCount = pageInfo.Total / pageInfo.PageSize;
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("ExamineItemController[Get]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Post([FromBody]Request<ExamineTemplateItems> req)
        {
            Response<ExamineTemplateItems> rsp = new Response<ExamineTemplateItems>();
            try
            {
                UserInfo user = _user.GetCurrentUser();
                if (user == null)
                    return base.Redirect("/User/Login#/Login");

                ExamineTemplateItems et = req.Data as ExamineTemplateItems;

                bool result = false;
                if (string.IsNullOrEmpty(et.Id))
                {
                    ExamineTemplateItems model = new ExamineTemplateItems();
                    string id = Guid.NewGuid().ToString();
                    model.Id = id;
                    model.Name = et.Name;
                    model.ExamineTemplateId = et.ExamineTemplateId;
                    model.Type = et.Type;
                    model.CreateUserId = user.UserId;
                    model.CreateUserName = user.LoginName;
                    model.CreateDateTime = DateTime.Now;
                    model.EditUserId = user.UserId;
                    model.EditUserName = user.LoginName;
                    model.EditDateTime = DateTime.Now;
                    model.IsDeleted = 0;
                    result = _eti.AddExamineTemplateItems(model);
                    rsp.Data = model;
                }
                else
                {
                    et.EditUserId = user.UserId;
                    et.EditUserName = user.LoginName;
                    et.EditDateTime = DateTime.Now;
                    result = _eti.UpdateExamineTemplateItems(et);
                    rsp.Data = et;
                }
                if (!result)
                    return BadRequest("操作失败");
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("ExamineItemController[Post]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Delete([FromUri]Request<string> req)
        {
            try
            {
                if (string.IsNullOrEmpty(req.Keyword))
                    return BadRequest("参数错误");

                bool result = _eti.DeleteExamineTemplateItemsById(req.Keyword);

                Response<string> rsp = new Response<string>();
                rsp.Data = "删除成功";

                if (!result)
                    return BadRequest("删除失败");
                return Ok(rsp);

            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("ExamineItemController[Delete]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }
    }

    public class ExamineItemModel
    {
        public List<ExamineTemplateItems> Data { get; set; }

        public ExamineItemModel()
        {
            Data = new List<ExamineTemplateItems>();
        }

        /// <summary>
        /// 模版类型
        /// </summary>
        public int TemplateType { get; set; }

        public ExamineTemplates Template { get; set; }
    }
}
