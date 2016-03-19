//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web.Http;
//using KMHC.CTMS.UI.Dtos;
//using Microsoft.Data.OData.Query.SemanticAst;

//namespace KMHC.CTMS.UI.Controllers.API
//{
//    [RoutePrefix("api/historyDisease")]
//    public class HistoryDiseaseController : BaseController<HealthHistory>
//    {
//        protected override IQueryable<HealthHistory> CreateQueryable(Request<HealthHistory> request)
//        {
//            var q = base.CreateQueryable(request);
//            if (request.RecordId > 0)
//            {
//                q = q.Where(m => m.RecordId == request.RecordId);
//            }
//            if (request.Keyword != null)
//            {
//                var keyWords = request.Keyword.Split(',');
//                q = q.Where(p => keyWords.Contains(p.Type));
//            }
//            return q;
//        }

//        public override IHttpActionResult Post(Request<HealthHistory> request)
//        {
//            try
//            {
//                //if (request.Data != null)
//                //{
//                //    if (request.Data.ID == 0)
//                //    {
//                //        request.Data.CreationTime = DateTime.Now;
//                //        request.Data.IsDeleted = false;
//                //        dal.Insert(request.Data);
//                //    }
//                //    else
//                //    {
//                //        request.Data.LastModificationTime = DateTime.Now;
//                //        dal.Update(request.Data);
//                //    }
//                //}
//                //else if(request.DataList!=null)
//                //{
//                //    dal.InsertList(request.DataList.Where(p => p.ID == 0));
//                //}

//            }
//            catch (Exception ex)
//            {
//                //LogHelper.WriteError(ex.ToString());
//                return BadRequest(ex.Message);
//            }
//            return Ok();
//        }
//    }
//}
