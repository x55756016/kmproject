using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using KMHC.CTMS.BLL;
using KMHC.CTMS.BLL.PrecisionMedicine;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.UI.Dtos;
using NPOI.OpenXmlFormats.Wordprocessing;

namespace KMHC.CTMS.UI.Controllers.API
{
    public class TemplateController : ApiController
    {

        private TemplateManBLL tbll = new TemplateManBLL();

        /// <summary>
        /// 获取模板列表信息
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public IHttpActionResult Get([FromUri]Request<ExamineTemplate> request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.ID))
                {
                    Response<IEnumerable<ExamineTemplate>> response = new Response<IEnumerable<ExamineTemplate>>();
                    PageInfo pageInfo = new PageInfo()
                    {
                        PageIndex = request.CurrentPage,
                        PageSize = request.PageSize,
                        Order = OrderEnum.desc,
                        OrderField = "CREATEDATETIME"
                    };
                    var list = tbll.GetTemplatesList(pageInfo, request.Keyword);
                    response.Data = list;
                    response.PagesCount = pageInfo.PagesCount;
                    return Ok(response);
                }
                else
                {
                    Response<ExamineTemplate> response = new Response<ExamineTemplate>();
                    var list = tbll.GetTemplateInfo(request.ID);
                    response.Data = list;
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Post([FromBody]Request<ExamineTemplate> request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Data.TEMPLATEID))
                {
                    //添加
                    tbll.AddTemplate(request.Data);
                }
                else
                {
                    //编辑
                    tbll.UpdateTemplate(request.Data);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }


        public IHttpActionResult Delete(string id)
        {
            try
            {
                bool isSuccess = tbll.DeleteTemplate(id);
                return Ok();
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("TemplateController[Delete]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }
    }
}
