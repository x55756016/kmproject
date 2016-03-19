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
    public class GeneAlleleLocusController : ApiController
    {
        GeneAlleleLocusService service = new GeneAlleleLocusService();
        public IHttpActionResult Get([FromUri]Request<GeneAlleleLocus> request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.ID))
                {
                    Response<IEnumerable<GeneAlleleLocus>> response = new Response<IEnumerable<GeneAlleleLocus>>();
                    List<GeneAlleleLocus> list = service.GetList(request.Keyword);
                    response.Data = list;
                    return Ok(response);
                }
                else
                {
                    Response<GeneAlleleLocus> response = new Response<GeneAlleleLocus>();
                    response.Data = service.Get(request.ID);
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("GeneDicGeneController[Get]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Get(string geneAlleleID,int pageIndex, int pageSize = 10)
        {
            try
            {
                PageInfo pageInfo = new PageInfo()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    OrderField = "CreateDateTime",
                    Order = OrderEnum.asc
                };
                List<GeneAlleleLocus> list = service.GetList(geneAlleleID, ref pageInfo);
                Response<IEnumerable<GeneAlleleLocus>> response = new Response<IEnumerable<GeneAlleleLocus>>
                {
                    Data = list,
                    PagesCount = pageInfo.PagesCount
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("GeneDicGeneController[Get]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Post([FromBody]Request<GeneAlleleLocus> request)
        {
            Response<GeneAlleleLocus> response = new Response<GeneAlleleLocus>();
            try
            {
                GeneAlleleLocus model = request.Data as GeneAlleleLocus;
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
                LogService.WriteErrorLog("GeneDicGeneController[Post]", ex.ToString());
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
                LogService.WriteErrorLog("GeneDicGeneController[Delete]", ex.ToString());
                return BadRequest(ex.Message);
            }
        } 
	}
}