using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KMHC.CTMS.BLL;
using KMHC.CTMS.BLL.CancerProcess;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.Model.CancerProcess;
using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.UI.Dtos;

namespace KMHC.CTMS.UI.Controllers.API
{
    public class GuideLineFlowController : ApiController
    {
        private GuideLineFlowBLL glBll = new GuideLineFlowBLL();

        public IHttpActionResult Get([FromUri] Request<GuideLine> request)
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
                        OrderField = "CREATEDATETIME"
                    };
                    var list = glBll.GetGuideLineFlowList(pageInfo, request.Keyword);
                    response.Data = list;
                    response.PagesCount = pageInfo.PagesCount;
                    return Ok(response);
                }
                else
                {
                    Response<string> response = new Response<string>();
                    //request.ID = "adad44ae-8317-47a0-b434-a2a53e1a610d";
                    var list = glBll.GetGuideLineFlow(request.ID);
                    response.Data = list;
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("GuideLineFlowController[Get]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Post([FromBody]Request<GuideLineFlow> request)
        {
            try
            {
                UserInfo currentUser = new UserInfoService().GetCurrentUser();
                request.Data.CreateUserID = "234";
                request.Data.CreateDateTime = DateTime.Now;
                request.Data.EditTime = DateTime.Now;
                request.Data.EditUserID = currentUser.UserId;
                request.Data.IsDeleted = false;
                request.Data.OwnerID = currentUser.UserId;
                if (string.IsNullOrEmpty(request.Data.ID))
                {
                    request.Data.ID = Guid.NewGuid().ToString();
                    //添加
                    glBll.AddGuideLineFlow(request.Data);

                }
                else
                {
                    //编辑
                    glBll.EditGuideLineFlow(request.Data);
                }
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("GuideLineFlowController[Post]", ex.ToString());
                return BadRequest(ex.Message);
            }
            return Ok();
        }


        public IHttpActionResult Delete(string id)
        {
            try
            {
                bool isSuccess = glBll.DeleteGuideLineFlow(id);
                return Ok();
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("GuideLineFlowController[Delete]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }
    }
}
