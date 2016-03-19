using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KMHC.CTMS.UI.Dtos;
using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.Model.Repository.Implement;
using KMHC.CTMS.Model.Repository.Implement.CancerRecord;
using KMHC.CTMS.Model.Repository.Interface;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Common.Helper;

namespace KMHC.CTMS.UI.Controllers.API
{
     [RoutePrefix("api/TreatmentHistory")]
    public class TreatmentHistoryController : ApiController
    {
        private IDiseaseHistoryRepository _repository = new EDiseaseHistoryRepository();

        public IHttpActionResult Get(string id)
        {
            try
            {
                var model = _repository.GetTreatmentHistoryList(id);

                if (model == null)
                {
                    return NotFound();
                }
                Response<IEnumerable<object>> response = new Response<IEnumerable<object>>();
                response.Data = model;
                return Ok(response);
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
                return BadRequest(ex.Message);
            }
        }


        public IHttpActionResult Post([FromBody]Request<TreatmentHistory> request)
        {
            try
            {
                var model = request.Data;
                _repository.AddTreatmentHistory(model);
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [Route("{id:long}")]
        public IHttpActionResult Delete(string id)
        {
            try
            {
                _repository.DelTreatmentHistory(id);
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}
