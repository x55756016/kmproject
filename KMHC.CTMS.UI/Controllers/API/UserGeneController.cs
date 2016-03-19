using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using KMHC.CTMS.BLL.CancerRecord;
using KMHC.CTMS.UI.Dtos;
using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.BLL;

namespace KMHC.CTMS.UI.Controllers.API
{
    public class UserGeneController : ApiController
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

        public IHttpActionResult Post([FromBody]Request<UserGene> request)
        {
            Response<UserGene> response = new Response<UserGene>();
            try
            {
                UserGene model = request.Data as UserGene;
                if (model == null) return NotFound();
                if (string.IsNullOrEmpty(model.ID))
                {
                    string ID = bll.Add(model);
                    model.ID = ID;
                }
                else
                {
                    bool isEditSuccess = bll.Edit(model);
                }
                response.Data = model;
                return Ok(response);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("UserGeneController[Post]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }


        public IHttpActionResult Delete(string id)
        {
            try
            {
                bool isDeleteSuccess = bll.Delete(id);
                return Ok();

            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("UserGeneController[Delete]", ex.ToString());
                return BadRequest(ex.Message);
            }
        } 
        
	}
}