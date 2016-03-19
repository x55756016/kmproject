
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KMHC.CTMS.UI.Dtos;


namespace KMHC.CTMS.UI.Controllers.API
{
    //[RoutePrefix("api/examtemplates")]
    //public class ExamTemplatesController : BaseController<Template>
    //{

    //    //public override IHttpActionResult Get([FromUri]Request<Template> request)
    //    //{
    //    //    Response<IEnumerable<Template>> response = new Response<IEnumerable<Template>>();
    //    //    try
    //    //    {
    //    //        IQueryable<Template> q = this.CreateQueryable(request);
    //    //        //var q = from a in dal.Set select new { ID = a.ID, TemplateCode = a.TemplateCode, TEMPLATENAME = a.TEMPLATENAME, Categories = a.Categories, IsDeleted = a.IsDeleted };
    //    //        if (!request.ContainsDeleted)
    //    //        {
    //    //            q = q.Where(m => m.IsDeleted == false);
    //    //        }
    //    //        if (!string.IsNullOrEmpty(request.Keyword))
    //    //        {
    //    //            q = q.Where(m => m.TemplateCode.Contains(request.Keyword) || m.TEMPLATENAME.Contains(request.Keyword));
    //    //        }
    //    //        int count = q.Count();
    //    //        request.PageSize = 10;
    //    //        if (request.PageSize > 0)
    //    //        {
    //    //            q = q.OrderBy(m => m.TemplateCode).Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize);
    //    //            count = count / request.PageSize;
    //    //            if (count % request.PageSize > 0)
    //    //            {
    //    //                count += 1;
    //    //            }
    //    //        }
    //    //        else
    //    //        {
    //    //            count = 1;
    //    //        }
    //    //        response.PagesCount = count;
    //    //        response.Data = q.ToList();
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //        LogHelper.WriteError(ex.ToString());
    //    //        return BadRequest(ex.Message);
    //    //    }
    //    //    return Ok(response);
    //    //}
    //}
}
