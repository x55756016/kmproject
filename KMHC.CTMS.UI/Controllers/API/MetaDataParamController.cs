using KMHC.CTMS.BLL;
using KMHC.CTMS.BLL.CancerProcess;
using KMHC.CTMS.Model.CancerProcess;
using KMHC.CTMS.UI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace KMHC.CTMS.UI.Controllers.API
{
    public class MetaDataParamController : ApiController
    {
        MetaDataParamBLL bll = new MetaDataParamBLL();
        public IHttpActionResult Get([FromUri]Request<MetaDataParam> request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.ID))
                {
                    Response<IEnumerable<MetaDataParam>> response = new Response<IEnumerable<MetaDataParam>>();
                    int id = 0;
                    if (int.TryParse(request.Keyword, out id))
                    {
                        if (id <= 0) return NotFound();
                        List<MetaDataParam> list = bll.GetList(id);
                        response.Data = list;
                        return Ok(response);
                    }
                    return NotFound();
                }
                else
                {
                    Response<MetaDataParam> response = new Response<MetaDataParam>();
                    int id = 0;
                    if (int.TryParse(request.ID, out id))
                    {
                        if (id <= 0) return NotFound();
                        response.Data = bll.Get(id);
                        return Ok(response);
                    }
                    else
                    {
                        return NotFound();
                    }

                }
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("MetaDataParamController[Get]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Post([FromBody]Request<MetaDataParam> request)
        {
            try
            {
                Response<MetaDataParam> response = new Response<MetaDataParam>();
                MetaDataParam model = request.Data as MetaDataParam;
                if (model == null) return NotFound();
                if (model.ID <= 0)
                {
                    int ID = bll.Add(model);
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
                LogService.WriteErrorLog("MetaDataParamController[Post]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Delete(int id)
        {
            try
            {
                bool isDeleteSuccess = bll.Delete(id);
                return Ok();

            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("MetaDataParamController[Delete]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }
    }
}