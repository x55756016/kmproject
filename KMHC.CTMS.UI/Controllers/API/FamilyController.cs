using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KMHC.CTMS.UI.Dtos;
using KMHC.CTMS.Model.CancerRecord;


namespace KMHC.CTMS.UI.Controllers.API
{
    public class FamilyController : BaseController<FamilyInfo>
    {
        //IFamilyService service=new FamilyService();
        public override IHttpActionResult Post([FromBody]Request<FamilyInfo> request)
        {
            //try
            //{
            //    if (request.Data != null)
            //    {
            //        if (request.Data.ID == 0)
            //        {
            //            request.Data.CreationTime = DateTime.Now;
            //            request.Data.IsDeleted = false;
            //            dal.Insert(request.Data);
            //        }
            //        else
            //        {
            //            request.Data.LastModificationTime = DateTime.Now;
            //            dal.Update(request.Data);
            //        }
            //    }
            //    else if (request.DataList != null)
            //    {
            //        dal.InsertList(request.DataList.Where(p => p.ID == 0));
            //    }

            //}
            //catch (Exception ex)
            //{
            //    LogHelper.WriteError(ex.ToString());
            //    return BadRequest(ex.Message);
            //}
            return Ok();
        }

        public override IHttpActionResult Get([FromUri]Request<FamilyInfo> request)
        {
            //Response<IEnumerable<Family>> response = new Response<IEnumerable<Family>>();
            //try
            //{
            //    IQueryable<Family> q = this.CreateQueryable(request);
            //    if (!request.ContainsDeleted)
            //    {
            //        q = q.Where(m => m.IsDeleted == false);
            //    }
            //    if (!string.IsNullOrEmpty(request.Keyword))
            //    {
            //        q = q.Where(m => m.HouseholderName.Contains(request.Keyword));
            //    }
            //    int count = q.Count();
            //    request.PageSize = 10;
            //    if (request.PageSize > 0)
            //    {
            //        q = q.OrderBy(m => m.ID).Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize);
            //        count = count / request.PageSize;
            //        if (count % request.PageSize > 0)
            //        {
            //            count += 1;
            //        }
            //    }
            //    else
            //    {
            //        count = 1;
            //    }
            //    response.PagesCount = count;
            //    response.Data = q.ToList();
            //}
            //catch (Exception ex)
            //{
            //    LogHelper.WriteError(ex.ToString());
            //    return BadRequest(ex.Message);
            //}
            return Ok();
        }

    }
}
