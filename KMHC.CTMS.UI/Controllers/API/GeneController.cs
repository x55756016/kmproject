using KMHC.CTMS.BLL;
using KMHC.CTMS.BLL.CancerProcess;
using KMHC.CTMS.BLL.PrecisionMedicine;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.Model.CancerProcess;
using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.UI.Attribute;
using KMHC.CTMS.UI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace KMHC.CTMS.UI.Controllers.API
{
    [System.Web.Http.RoutePrefix("api/Gene")]
    public class GeneController : ApiController
    {
        GeneService service = new GeneService();
        //[ApiAuth(AuthCode="")]
        public IHttpActionResult Get([FromUri]Request<Gene> request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.ID))
                {
                    Response<IEnumerable<Gene>> response = new Response<IEnumerable<Gene>>();
                    List<Gene> list = service.GetList(request.Keyword);
                    response.Data = list;
                    return Ok(response);
                }
                else
                {
                    Response<Gene> response = new Response<Gene>();
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
        public IHttpActionResult Get(int pageIndex, string keyWord, int pageSize = 10)
        {
            try
            {
                ////Todo 删除 测试满足进入条件的guideLine
                //List<GuideLine> matchList = new GuideLineBLL().GetMatchList(new List<string>() { "UserID" }, new List<string>() { "d155f081-2772-4218-b9a8-857630e2ee95" });
                ////Todo 删除 测试

                PageInfo pageInfo = new PageInfo()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    OrderField = "GeneName",
                    Order = OrderEnum.asc
                };

                List<Gene> list = service.GetList(keyWord, ref pageInfo);

                Response<IEnumerable<Gene>> response = new Response<IEnumerable<Gene>>
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
        
        //[ApiAuth(AuthCode = "gn_gene")]
        public IHttpActionResult Post([FromBody]Request<Gene> request)
        {
            Response<Gene> response = new Response<Gene>();
            try
            {
                Gene model = request.Data as Gene;
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
                LogService.WriteErrorLog("GeneDicGeneController[Post]",ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Post([FromUri]string ID,[FromBody]Request<Gene> request)
        {
            Response<Gene> response = new Response<Gene>();
            try
            {
                Gene model = request.Data as Gene;
                if (model == null) return NotFound();
                if (string.IsNullOrEmpty(model.ID))
                {
                    string id = service.Add(model);
                    model.ID = id;
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

        //[ApiAuth(AuthCode = "gn_gene")]
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