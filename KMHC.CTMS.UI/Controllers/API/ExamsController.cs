using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EntityFramework.Extensions;
using System.Data.Entity;
using KMHC.CTMS.UI.Dtos;
using KMHC.CTMS.Model.CancerRecord;


namespace KMHC.CTMS.UI.Controllers.API
{
    [RoutePrefix("api/exams")]
    public class ExamsController : BaseController<ExamineRecord>
    {
        ////Repository<ExamResult> resultDal = new Repository<ExamResult>();
        //Repository<Examine>  _repo = new Repository<Examine>();
        //public override IHttpActionResult Get(int id)
        //{
        //    Response<Examine> response = new Response<Examine>();
        //    try
        //    {
        //        CRDatabase db = new CRDatabase();
        //        var model = from a in db.ExamResults
        //            where a.ExamId == (from b in  db.Exams.Where(p => p.RecordId == id).OrderByDescending(p => p.ExamNo) select b).FirstOrDefault().ID && a.Result.Length>0 && !a.IsDeleted
        //            select a;;
                
        //        if (model == null)
        //        {
        //            return NotFound();
        //        }
        //        return Ok(model);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.WriteError(ex.ToString());
        //        return BadRequest(ex.Message);
        //    }
        //}
        //public override IHttpActionResult Get([FromUri]Request<Examine> request)
        //{
        //    Response<IEnumerable<ExamListDto>> response = new Response<IEnumerable<ExamListDto>>();
        //    try
        //    {
        //        CRDatabase db = new CRDatabase();
        //        var q = from a in db.Exams join b in db.Templates on a.ID equals b.ID select new ExamListDto { ID = a.ID, ExamTemplateID = a.ID, ExamDate = a.ExamDate, ExamTemplateName = b.TEMPLATENAME, IsDeleted = a.IsDeleted };
        //        if (!request.ContainsDeleted)
        //        {
        //            q = q.Where(m => m.IsDeleted == false);
        //        }
        //        int count = q.Count();
        //        if (request.PageSize > 0)
        //        {
        //            q = q.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize);
        //            count = count / request.PageSize;
        //            if (count % request.PageSize > 0)
        //            {
        //                count = +1;
        //            }
        //        }
        //        else
        //        {
        //            count = 1;
        //        }
        //        response.PagesCount = count;
        //        response.Data = q.ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.WriteError(ex.ToString());
        //        return BadRequest(ex.Message);
        //    }
        //    return Ok(response);
        //}
        //public override IHttpActionResult Post([FromBody]Request<Examine> request)
        //{
        //    try
        //    {
        //        var examine = request.Data;
        //        examine.ExamNo = DateTime.Now.ToString("yyyymmddhhmmss");
        //        examine.ExamDate = DateTime.Now.ToString("yyyymmdd");

        //        _repo.InsertOrUpdate(examine);
        //        //var id = dal.InsertOrUpdate(request.Data).ID;
        //        //foreach (var item in request.Data.Results)
        //        //{
        //        //    item.ID = id;
        //        //    resultDal.InsertOrUpdate(item);
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.WriteError(ex.ToString());
        //        return BadRequest(ex.Message);
        //    }
        //    return Ok();
        //}
    }
}
