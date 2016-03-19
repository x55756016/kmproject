
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KMHC.CTMS.BLL.PrecisionMedicine;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.UI.Dtos;


namespace KMHC.CTMS.UI.Controllers.API
{
    [RoutePrefix("api/examitems")]
    public class ExamItemsController : ApiController
    {
        private TemplateManBLL tbll = new TemplateManBLL();

        public  IHttpActionResult Get([FromUri]Request<ExamineItems> request)
        {
            Response<IEnumerable<ExamineItems>> response = new Response<IEnumerable<ExamineItems>>();
            try
            {
                PageInfo pageInfo = new PageInfo()
                {
                    PageIndex = request.CurrentPage,
                    PageSize = request.PageSize,
                    Order = OrderEnum.asc,
                    OrderField = "ITEMID"
                };
                var list = tbll.GetExamItemsList(pageInfo, request.ID, request.Keyword);
                response.Data = list;
                response.PagesCount = pageInfo.PagesCount;
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }
    }
}