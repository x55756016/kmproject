using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KMHC.CTMS.BLL.PrecisionMedicine;
using KMHC.CTMS.UI.Dtos;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.Model.PrecisionMedicine;


namespace KMHC.CTMS.UI.Controllers.API
{
    [RoutePrefix("api/examcategories")]
    public class ExamCategoriesController : ApiController
    {
        private TemplateManBLL tbll = new TemplateManBLL();

        public IHttpActionResult Get([FromUri]Request<ExamineCategory> request)
        {
            Response<IEnumerable<ExamineCategory>> response = new Response<IEnumerable<ExamineCategory>>();
            try
            {
                var list = tbll.GetExamCatetoryList();
                response.Data = list;
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
