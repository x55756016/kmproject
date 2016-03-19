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
    public class ConditionItemController:ApiController
    {
        ConditionItemBLL bll = new ConditionItemBLL();
        public IHttpActionResult Get([FromUri]Request<ConditionItem> request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.ID))
                {
                    Response<IEnumerable<ConditionItem>> response = new Response<IEnumerable<ConditionItem>>();
                    List<ConditionItem> list = bll.GetList(request.Keyword);
                    response.Data = list;
                    return Ok(response);
                }
                else
                {
                    Response<ConditionItem> response = new Response<ConditionItem>();
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
                LogService.WriteErrorLog("ConditionItemController[Get]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Post([FromBody]Request<ConditionItem> request)
        {
            try
            {
                Response<ConditionItem> response = new Response<ConditionItem>();
                ConditionItem model = request.Data as ConditionItem;
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
                LogService.WriteErrorLog("ConditionItemController[Post]", ex.ToString());
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
                LogService.WriteErrorLog("ConditionItemController[Delete]", ex.ToString());
                return BadRequest(ex.Message);
            }
        } 
    }
}