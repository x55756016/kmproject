using KMHC.CTMS.BLL;
using KMHC.CTMS.BLL.CancerProcess;
using KMHC.CTMS.Model.CancerProcess;
using KMHC.CTMS.UI.Dtos;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Http;
using KMHC.CTMS.Common.Helper;

namespace KMHC.CTMS.UI.Controllers.API
{
    public class GuideLineController : ApiController
    {
        GuideLineBLL bll = new GuideLineBLL();
        public IHttpActionResult Get([FromUri]Request<GuideLine> request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.ID))
                {
                    Response<IEnumerable<GuideLine>> response = new Response<IEnumerable<GuideLine>>();

                    PageInfo pageInfo = new PageInfo()
                    {
                        PageIndex = request.CurrentPage,
                        PageSize = request.PageSize,
                        Order = OrderEnum.desc,
                        OrderField = "ID"
                    };
                    
                    List<GuideLine> list = bll.GetList(pageInfo,request.Keyword);

                    GuidelineProductBLL gpBLL = new GuidelineProductBLL();
                    foreach (GuideLine item in list)
                    {
                        item.Products = gpBLL.GetList(p => p.GUIDELINEID.Equals(item.ID)).ToList();
                    }
                    response.PagesCount = pageInfo.PagesCount;
                    response.Data = list;
                    return Ok(response);
                }
                else
                {
                    Response<GuideLine> response = new Response<GuideLine>();
                    response.Data = bll.GetSimpleModel(request.ID);

                    GuidelineProductBLL gpBLL = new GuidelineProductBLL();
                    response.Data.Products = gpBLL.GetList(p => p.GUIDELINEID.Equals(response.Data.ID)).ToList();
                    
                    return Ok(response);
                   
                }
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("GuideLineController[Get]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Post([FromBody]Request<GuideLine> request)
        {
            try
            {
                Response<GuideLine> response = new Response<GuideLine>();
                GuideLine model = request.Data as GuideLine;
                List<GuidelineProduct> products = model.Products;
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

                //todo：父类的guideline已经迁移到表CTMS_PARENTGUIDELINE
                bll.SaveParentGuideLine(model.ID, model.ParentList);

                GuidelineProductBLL gpBLL = new GuidelineProductBLL();
                foreach (GuidelineProduct item in products)
                {
                    if (string.IsNullOrEmpty(item.GuidelineProductId))
                    {
                        item.GuidelineId = model.ID;
                        gpBLL.Add(item);
                    }
                    else
                    {
                        gpBLL.Edit(item);
                    }
                }
                response.Data = model;
                return Ok(response);
            }
            catch (DbEntityValidationException ex)
            {
                LogService.WriteErrorLog("GuideLineController[Post]", ex.ToString());
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
                LogService.WriteErrorLog("GuideLineController[Delete]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Delete(string gid,string gpid)
        {
            try
            {
                GuidelineProductBLL gpBLL = new GuidelineProductBLL();
                bool isDeleteSuccess = gpBLL.Delete(gpid);
                return Ok("ok");
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("GuideLineController[Delete]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }
    }
}