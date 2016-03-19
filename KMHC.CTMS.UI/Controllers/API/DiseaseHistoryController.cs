using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.UI.Dtos;
using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.Model.Repository.Implement;
using KMHC.CTMS.Model.Repository.Interface;

namespace KMHC.CTMS.UI.Controllers.API
{
     [RoutePrefix("api/DiseaseHistory")]
    public class DiseaseHistoryController : ApiController
    {

         private IDiseaseHistoryRepository _repository = new EDiseaseHistoryRepository();


        [Route("")]
        public IHttpActionResult Get([FromUri]Request<DiseaseHistory> request)
        {
            Response<IEnumerable<DiseaseHistory>> response = new Response<IEnumerable<DiseaseHistory>>();
            try
            {
                PageInfo pageInfo = new PageInfo()
                {
                    PageIndex = request.CurrentPage,
                    PageSize = request.PageSize,
                    OrderField = "DISEASEHISTORYID",
                    Order = OrderEnum.desc
                };

                var list = _repository.GetList(pageInfo, request.UserName, request.Keyword);
                response.Data = list;

                response.PagesCount = pageInfo.PagesCount;
            }
            catch (Exception ex)
            {
                //LogHelper.WriteError(ex.ToString());
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }

        [Route("")]
        public virtual IHttpActionResult Post([FromBody]Request<DiseaseHistory> request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Data.DISEASEHISTORYID))
                {
                    _repository.Add(request.Data);
                }
                else
                {
                    _repository.Update(request.Data);
                }
            }
            catch (Exception ex)
            {
                //LogHelper.WriteError(ex.ToString());
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [Route("{id:long}")]
        public IHttpActionResult Delete(string id)
        {
            try
            {
                _repository.Delete(id);
            }
            catch (Exception ex)
            {
                //LogHelper.WriteError(ex.ToString());
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}
