using KMHC.CTMS.BLL;
using KMHC.CTMS.BLL.PrecisionMedicine;
using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.UI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace KMHC.CTMS.UI.Controllers.API
{
    public class DrugAlleleController : ApiController
    {
        DrugAlleleBLL service = new DrugAlleleBLL();
        public IHttpActionResult Get([FromUri]Request<DrugAllele> request)    
        {
            try
            {
                if (string.IsNullOrEmpty(request.ID))
                {
                    Response<IEnumerable<DrugAllele>> response = new Response<IEnumerable<DrugAllele>>();
                    List<DrugAllele> list = service.GetList(request.Keyword);
                    response.Data = list;
                    return Ok(response);
                }
                else
                {
                    Response<DrugAllele> response = new Response<DrugAllele>();
                    response.Data = service.Get(request.ID);
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("DrugAlleleController[Get]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Post([FromBody]Request<DrugAllele> request)
        {
            Response<DrugAllele> response = new Response<DrugAllele>();
            try
            {
                DrugAllele model = request.Data as DrugAllele;
                if (model == null) return NotFound();
                if (string.IsNullOrEmpty(model.ID))
                {
                    string ID = service.Add(model);
                    model.ID = ID;
                }
                else
                {
                    bool isEditSuccess = service.Edit(model); 
                }
                response.Data = model;
                return Ok(response);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("DrugAlleleController[Post]",ex.ToString());
                return BadRequest(ex.Message);
            }
        }


        public IHttpActionResult Delete(string id)
        {
            try
            {
                bool isDeleteSuccess = service.Delete(id);
                return Ok();

            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("DrugAlleleController[Delete]", ex.ToString());
                return BadRequest(ex.Message);
            }
        } 
	}
}