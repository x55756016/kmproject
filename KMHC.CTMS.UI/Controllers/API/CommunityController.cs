using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using KMHC.CTMS.UI.Dtos;
using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.Model.Repository;
using KMHC.CTMS.Model.Repository.Interface;
using KMHC.CTMS.Common.Helper;

namespace KMHC.CTMS.UI.Controllers.API
{
    [System.Web.Http.RoutePrefix("api/Community")]
    public class CommunityController : BaseController<Community>//  ApiController
    {
        //public IPersonInfoRepository pr = new EFPersonRepository();
        protected override IQueryable<Community> CreateQueryable(Request<Community> request)
        {
            //var d=pr.Get(1);
            var q = base.CreateQueryable(request);
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                q = q.Where(m => m.CommunityName.Contains(request.Keyword) || m.CommunityAddress.Contains(request.Keyword));
            }
            return q;
        }


        public override IHttpActionResult Post([FromBody]Request<Community> request)
        {
            try
            {
                //if (request.Data != null)
                //{
                //    if (request.Data.ID == 0)
                //    {
                //        request.Data.CreationTime = DateTime.Now;
                //        request.Data.IsDeleted = false;
                //        dal.Insert(request.Data);
                //    }
                //    else
                //    {
                //        request.Data.LastModificationTime = DateTime.Now;
                //        dal.Update(request.Data);
                //    }
                //}
                //else if (request.DataList != null)
                //{
                //    dal.InsertList(request.DataList.Where(p => p.ID == 0));
                //}

            }
            catch (Exception ex)
            {
                //LogHelper.WriteError(ex.ToString());
                return BadRequest(ex.Message);
            }
            return Ok();
        }





        public override IHttpActionResult Get([FromUri]Request<Community> request)
        {
            Response<IEnumerable<object>> response = new Response<IEnumerable<object>>();
            try
            {
                if (request.CurrentPage == 0)
                {
                }
                else
                {
                    var q = this.CreateQueryable(request);
                    if (!request.ContainsDeleted)
                    {
                        //q = q.Where(m => m.IsDeleted == false);
                    }
                    response.Data = q.ToList();
                }
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
