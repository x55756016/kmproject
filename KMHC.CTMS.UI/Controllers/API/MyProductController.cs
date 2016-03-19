using KMHC.CTMS.BLL;
using KMHC.CTMS.BLL.CancerRecord;
using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.Model.Product;
using KMHC.CTMS.UI.Dtos;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace KMHC.CTMS.UI.Controllers.API
{
    public class MyProductController : ApiController
    {
        MyProductBLL bll = new MyProductBLL();
        public IHttpActionResult Get([FromUri]Request<MyProduct> request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.ID))
                {
                    Response<IEnumerable<MyProduct>> response = new Response<IEnumerable<MyProduct>>();
                    UserInfo user = new UserInfoService().GetCurrentUser();
                    if (user == null || string.IsNullOrEmpty(user.UserId)) return BadRequest("查询不到当前用户");
                    List<MyProduct> list = bll.GetList(user.UserId);
                    response.Data = list;
                    return Ok(response);
                }
                else
                {
                    Response<MyProduct> response = new Response<MyProduct>();
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
                LogService.WriteErrorLog("MyProductController[Get]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Post([FromBody]Request<MemberModel> request)
        {
            try
            {
                Response<MyProduct> response = new Response<MyProduct>();
                UserInfo user = new UserInfoService().GetCurrentUser();
                if (user == null || string.IsNullOrEmpty(user.UserId)) return BadRequest("查询不到当前用户");
                if(request.Keyword.Equals("BuyMember"))
                { 
                    bll.BuyMember(user.UserId, request.ID);
                }
                return Ok();
            }
            catch (DbEntityValidationException dbEx)
            {
                string error = string.Empty;
                foreach (var eve in dbEx.EntityValidationErrors)
                {
                    foreach (var e in eve.ValidationErrors)
                    {
                        error += e.ErrorMessage+";";
                    }
                }
                LogService.WriteErrorLog("MyProductController[Post]", dbEx.ToString());
                return BadRequest(dbEx.Message);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("MyProductController[Post]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Post(string productID)
        {
            try
            {
                Response<MyProduct> response = new Response<MyProduct>();
                UserInfo user = new UserInfoService().GetCurrentUser();
                if (user == null || string.IsNullOrEmpty(user.UserId)) return BadRequest("查询不到当前用户");
                bll.BuyProduct(user.UserId, productID);
                return Ok();
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("MyProductController[Post]", ex.ToString());
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
                LogService.WriteErrorLog("MyProductController[Delete]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }
    }
}