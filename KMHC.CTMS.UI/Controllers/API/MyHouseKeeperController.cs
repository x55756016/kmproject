using KMHC.CTMS.BLL;
using KMHC.CTMS.BLL.CancerRecord;
using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.UI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace KMHC.CTMS.UI.Controllers.API
{
    public class MyHouseKeeperController : ApiController
    {
        MyHouseKeeperBLL bll = new MyHouseKeeperBLL();
        public IHttpActionResult Get([FromUri]Request<MyHouseKeeper> request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.ID))
                {
                    Response<IEnumerable<MyHouseKeeper>> response = new Response<IEnumerable<MyHouseKeeper>>();
                    UserInfo user = new UserInfoService().GetCurrentUser();
                    if (user == null || string.IsNullOrEmpty(user.UserId)) return BadRequest("查询不到当前用户");
                    List<MyHouseKeeper> list = bll.GetList(user.UserId);
                    response.Data = list;
                    return Ok(response);
                }
                else
                {
                    Response<MyHouseKeeper> response = new Response<MyHouseKeeper>();
                    if (!string.IsNullOrEmpty(request.ID))
                    {
                        response.Data = bll.Get(request.ID);
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
                LogService.WriteErrorLog("MetaDataController[Get]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Post([FromBody]Request<MyHouseKeeper> request)
        {
            try
            {
                Response<MyHouseKeeper> response = new Response<MyHouseKeeper>();
                MyHouseKeeper model = request.Data as MyHouseKeeper;
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
                LogService.WriteErrorLog("MetaDataController[Post]", ex.ToString());
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
                LogService.WriteErrorLog("MetaDataController[Delete]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }
    }
}