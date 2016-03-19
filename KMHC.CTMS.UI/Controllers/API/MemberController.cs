using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KMHC.CTMS.BLL;
using KMHC.CTMS.BLL.Product;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.Model.Product;
using KMHC.CTMS.UI.Dtos;

namespace KMHC.CTMS.UI.Controllers.API
{
    public class MemberController : ApiController
    {

        private MemberBLL mBll = new MemberBLL();

        public IHttpActionResult Get([FromUri]Request<MemberModel> request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.ID))
                {
                    Response<IEnumerable<MemberModel>> response = new Response<IEnumerable<MemberModel>>();
                    PageInfo pageInfo = new PageInfo()
                    {
                        PageIndex = request.CurrentPage,
                        PageSize = request.PageSize,
                        Order = OrderEnum.desc,
                        OrderField = "MEMBERLEVEL"
                    };
                    var list = mBll.GetMemberList(pageInfo,request.Type);
                    response.Data = list;
                    response.PagesCount = pageInfo.PagesCount;
                    return Ok(response);
                }
                else
                {
                    Response<MemberModel> response = new Response<MemberModel>();
                    var list = mBll.GetMember(request.ID);
                    response.Data = list;
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Get([FromUri]string keyWord)
        {
            try
            {
                Response<IEnumerable<MemberModel>> response = new Response<IEnumerable<MemberModel>>();
                response.Data = mBll.GetMemberList();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Post([FromBody]Request<MemberModel> request)
        {
            Response<bool> response = new Response<bool>();
            var flag = true;
            try
            {
                if (string.IsNullOrEmpty(request.Data.MEMBERID))
                {
                    //添加
                    flag =mBll.AddMemberList(request.Data);
                }
                else
                {
                    //编辑
                    flag = mBll.UpdateMemberList(request.Data);
                }
                response.Data = flag;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }


        public IHttpActionResult Delete(string memberId)
        {
            try
            {
                bool isSuccess = mBll.DeleteMember(memberId);
                return Ok(isSuccess);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("MemberController[Delete]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }
    }
}
