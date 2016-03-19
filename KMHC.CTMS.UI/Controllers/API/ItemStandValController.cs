using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using KMHC.CTMS.BLL;
using KMHC.CTMS.BLL.PrecisionMedicine;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.Model.Common;
using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.UI.Dtos;

namespace KMHC.CTMS.UI.Controllers.API
{
    public class ItemStandValController : ApiController
    {
        private TemplateManBLL tbll = new TemplateManBLL();



        public IHttpActionResult Get(string id)
        {
            Response<ItemStandVal> response = new Response<ItemStandVal>();
            try
            {
                var model = tbll.GetItemsStandVal(id);
                if (model==null)
                {
                    return NotFound();
                }
                response.Data = model;
                return Ok(response);
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 获取模板值返回列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IHttpActionResult Get([FromUri] Request<ItemStandVal> request)
        {
            try
            {
                Response<IEnumerable<ItemStandVal>> response = new Response<IEnumerable<ItemStandVal>>();
                PageInfo pageInfo = new PageInfo()
                {
                    PageIndex = request.CurrentPage,
                    PageSize = request.PageSize,
                    Order = OrderEnum.desc,
                    OrderField = "ID"
                };
                var list = tbll.GetItemsStandValList(pageInfo, request.Keyword);
                response.Data = list;
                response.PagesCount = pageInfo.PagesCount;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        public IHttpActionResult Post([FromBody]Request<ItemStandVal> request)
        {
            BaseResult br = new BaseResult();
            try
            {
                var cookie = HttpContext.Current.Request.Cookies["Token"].Value;

                if (string.IsNullOrEmpty(request.Data.ID))
                {
                    //添加
                    request.Data.ID = Guid.NewGuid().ToString();
                    request.Data.CREATEUSERID = new UserInfoService().GetLoginInfo(cookie).UserId;
                    request.Data.CREATEDATETIME = System.DateTime.Now;
                    request.Data.EDITDATETIME = System.DateTime.Now;
                    request.Data.EDITUSERID = new UserInfoService().GetLoginInfo(cookie).UserId;
                    request.Data.OWNERID = new UserInfoService().GetLoginInfo(cookie).UserId;
                    request.Data.ISDELETED = "0";
                    br =  tbll.AddItemStandVal(request.Data);
                }
                else
                {
                    //编辑
                    request.Data.EDITDATETIME = System.DateTime.Now;
                    request.Data.EDITUSERID = new UserInfoService().GetLoginInfo(cookie).UserId;
                    br= tbll.UpdateItemStandVal(request.Data);
                }
            }
            catch (Exception ex)
            {
                br.Succeeded = false;
                br.Error = ex.Message;
            }
            return Ok(br);
        }

        public IHttpActionResult Delete(string id)
        {
            BaseResult br = new BaseResult(){Succeeded = true};
            try
            {
                br = tbll.DeleteItemStandVal(id);
            }
            catch (Exception e)
            {
                br.Succeeded = false;
                br.Error = e.Message;
                throw;
            }
            return Ok(br);
        }

    }
}
