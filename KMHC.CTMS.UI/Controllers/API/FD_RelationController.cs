using KMHC.CTMS.UI.Dtos;
using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.Model.Repository.Implement.CancerRecord;
using KMHC.CTMS.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;


namespace KMHC.CTMS.UI.Controllers.API
{
    [System.Web.Http.RoutePrefix("api/familyRelation")]
    public class familyRelationController : ApiController
    {
        EFFD_RelationRepository _repository = new EFFD_RelationRepository();
        public IHttpActionResult Get([FromUri]Request<FD_Relation> request)
        {
            Response<IEnumerable<object>> response = new Response<IEnumerable<object>>();
            try
            {
                var model = _repository.GetAll();

                if (model == null)
                {
                    return NotFound();
                }

                response.Data = model.ToList();
                return Ok(response);
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
                return BadRequest(ex.Message);
            }
        }
    }
}