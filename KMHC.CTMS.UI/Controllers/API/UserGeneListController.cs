using KMHC.CTMS.BLL;
using KMHC.CTMS.BLL.CancerRecord;
using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.UI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace KMHC.CTMS.UI.Controllers.API
{
    public class UserGeneListController : ApiController
    {
        UserGeneBLL bll = new UserGeneBLL();
        public IHttpActionResult Get([FromUri]Request<UserGene> request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.ID))
                {
                    Response<IEnumerable<UserGene>> response = new Response<IEnumerable<UserGene>>();
                    List<UserGene> list = bll.GetList(request.Keyword);
                    response.Data = list;
                    return Ok(response);
                }
                else
                {
                    Response<UserGene> response = new Response<UserGene>();
                    response.Data = bll.Get(request.ID);
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("UserGeneController[Get]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Post([FromBody]Request<List<UserGene>> request)
        {
            Response<List<UserGene>> response = new Response<List<UserGene>>();
            try
            {
                List<UserGene> modelList = request.Data as List<UserGene>;
                if (modelList == null) return NotFound();
                foreach (UserGene model in modelList)
                {
                    if (string.IsNullOrEmpty(model.ID))
                    {
                        model.ID = Guid.NewGuid().ToString();
                    }
                    bll.Add(model);
                }
                response.Data = modelList;
                return Ok(response);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("UserGeneController[Post]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }
	}
}