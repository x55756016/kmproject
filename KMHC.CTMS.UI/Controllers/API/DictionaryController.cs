using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KMHC.CTMS.BLL;
using KMHC.CTMS.Model.Common;
using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.UI.Dtos;

namespace KMHC.CTMS.UI.Controllers.API
{
    public class DictionaryController : ApiController
    {
        private DictionaryService _dictionary;
        private UserInfoService _user;

        public DictionaryController()
        {
            _dictionary = new DictionaryService();
            _user = new UserInfoService();
        }

        public IHttpActionResult Get([FromUri]Request<string> request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Keyword))
                    return BadRequest("参数错误");

                List<Dictionary> list = _dictionary.GetDictionaryByCategory(request.Keyword);
                Response<List<Dictionary>> rsp = new Response<List<Dictionary>>();
                rsp.Data = list;
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("DictionaryController[Get]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Delete([FromUri]Request<string> req)
        {
            try
            {
                if (string.IsNullOrEmpty(req.Keyword))
                    return BadRequest("参数错误");

                _dictionary.DeleteDictionary(req.Keyword);

                Response<string> rsp = new Response<string>();
                rsp.Data = "删除成功";
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("DictionaryController[Delete]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Post([FromBody]Request<TreeModel> req)
        {
            Response<Dictionary> rsp = new Response<Dictionary>();
            try
            {
                TreeModel tree = req.Data as TreeModel;

                Dictionary model = null;

                UserInfo user = _user.GetCurrentUser();
                if (user == null)
                    return base.Redirect("/User/Login#/Login");

                if (string.IsNullOrEmpty(tree.nodeId))
                {
                    model = new Dictionary();
                    string ID = Guid.NewGuid().ToString();
                    model.nodeId = ID;
                    model.DictionCategory = tree.category;
                    model.DictionCategoryName = tree.category;
                    model.CreateUser = user.LoginName;
                    model.CreateDate = DateTime.Now;
                    model.UpdateUser = user.LoginName;
                    model.UpdateDate = DateTime.Now;
                    model.parentId = tree.parentId == null ? "0" : tree.parentId;
                    model.text = tree.text;
                    model.value = tree.value;
                    model.Remark = "";
                    _dictionary.AddDictionary(model);
                }
                else
                {
                    model = _dictionary.GetDictionaryById(tree.nodeId);
                    if (model == null)
                        return NotFound();
                    model.UpdateDate = DateTime.Now;
                    model.UpdateUser = user.LoginName;
                    model.text = tree.text;
                    model.value = tree.value;
                    _dictionary.EditDictionary(model);
                }
                rsp.Data = model;
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("DictionaryController[Post]", ex.ToString());
                return BadRequest(ex.ToString());
            }
        }
    }

    public class TreeModel
    {
        public string text { get; set; }

        public string value { get; set; }

        public string parentId { get; set; }

        public string nodeId { get; set; }

        public string category { get; set; }
    }
}
