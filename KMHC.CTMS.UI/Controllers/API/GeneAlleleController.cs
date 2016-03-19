using KMHC.CTMS.BLL;
using KMHC.CTMS.BLL.PrecisionMedicine;
using KMHC.CTMS.Common.Helper;
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
    public class GeneAlleleController : ApiController
    {
        GeneAlleleService service = new GeneAlleleService();
        public IHttpActionResult Get([FromUri]Request<GeneAllele> request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.ID))
                {
                    Response<IEnumerable<GeneAllele>> response = new Response<IEnumerable<GeneAllele>>();
                    List<GeneAllele> list = service.GetList(request.Keyword);
                    response.Data = list;
                    return Ok(response);
                }
                else
                {
                    Response<GeneAllele> response = new Response<GeneAllele>();
                    response.Data = service.Get(request.ID);
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("GeneAlleleController[Get]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Get(string geneID,int pageIndex, int pageSize = 10)
        {
            try
            {
                PageInfo pageInfo = new PageInfo()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    OrderField = "GeneAlleleName",
                    Order = OrderEnum.asc
                };

                List<GeneAllele> list = service.GetList(geneID, ref pageInfo);

                Response<IEnumerable<GeneAllele>> response = new Response<IEnumerable<GeneAllele>>
                {
                    Data = list,
                    PagesCount = pageInfo.PagesCount
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("GeneAlleleController[Get]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Post([FromBody]Request<GeneAllele> request)
        {
            Response<GeneAllele> response = new Response<GeneAllele>();
            try
            {
                GeneAllele model = request.Data as GeneAllele;
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
                LogService.WriteErrorLog("GeneAlleleController[Post]", ex.ToString());
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
                LogService.WriteErrorLog("GeneAlleleController[Delete]", ex.ToString());
                return BadRequest(ex.Message);
            }
        } 
	}
}