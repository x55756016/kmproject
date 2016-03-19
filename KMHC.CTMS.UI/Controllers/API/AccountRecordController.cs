using KMHC.CTMS.BLL;
using KMHC.CTMS.BLL.Product;
using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.Model.Product;
using KMHC.CTMS.UI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace KMHC.CTMS.UI.Controllers.API
{
    public class AccountRecordController : ApiController
    {
        AccountRecordBLL bll = new AccountRecordBLL();
        public IHttpActionResult Get(string startDate, string endDate)
        {
            try
            {
                Response<IEnumerable<AccountRecord>> response = new Response<IEnumerable<AccountRecord>>();
                UserInfo user = new UserInfoService().GetCurrentUser();
                if (user == null || string.IsNullOrEmpty(user.UserId)) return BadRequest("查询不到当前用户");
                List<AccountRecord> list = bll.GetList(user.UserId, Convert.ToDateTime(startDate), Convert.ToDateTime(endDate));
                response.Data = list;
                return Ok(response);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("AccountRecordController[Get]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Post([FromBody]Request<AccountRecord> request)
        {
            try
            {
                Response<AccountRecord> response = new Response<AccountRecord>();
                AccountRecord model = request.Data as AccountRecord;
                if (model == null) return NotFound();
                UserInfo user = new UserInfoService().GetCurrentUser();
                if (user == null || string.IsNullOrEmpty(user.UserId)) return BadRequest("查询不到当前用户");
                model.UserID=user.UserId;
                model.LoginName = user.LoginName;
                model.CreateDateTime = DateTime.Now;
                if (string.IsNullOrEmpty(model.ID))
                {
                    string ID = bll.Add(model);
                    model.ID = ID;
                }
                else
                {
                    //bool isEditSuccess = bll.Edit(model);
                }
                response.Data = model;
                return Ok(response);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("AccountRecordController[Post]", ex.ToString());
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
                LogService.WriteErrorLog("AccountRecordController[Delete]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }
    }
}