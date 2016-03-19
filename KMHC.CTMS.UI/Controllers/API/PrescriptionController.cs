using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using KMHC.CTMS.BLL;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.UI.Dtos;

namespace KMHC.CTMS.UI.Controllers.API
{
    public class PrescriptionController : ApiController
    {

        private PrescriptionBLL pbll = new PrescriptionBLL();

        public IHttpActionResult Get([FromUri]Request<DrugUse> request )
        {

            Response<PrescritionModel> response = new Response<PrescritionModel>();
            try
            {
                if (!string.IsNullOrEmpty(request.ID))
                {
                    var model = pbll.GetDrugUseList(request.ID);
                    response.Data = model;
                    
                }
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("PrescriptionController[Get]", ex.ToString());
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }

        public IHttpActionResult Post([FromBody]Request<DrugUse> request)
        {
            Response<string> response = new Response<string>();
            try
            {
                DrugUse model = request.Data as DrugUse;
                if (model == null) return NotFound();
                model.DISEASEID = model.DISEASEID == "" ? Guid.NewGuid().ToString() : model.DISEASEID;
                var cookie = HttpContext.Current.Request.Cookies["Token"].Value;
                if (model.Action == 1) //添加
                {
                    

                    model.CREATEUSERID = new UserInfoService().GetLoginInfo(cookie).UserId;
                    model.CREATEDATETIME = System.DateTime.Now;
                    model.EDITDATETIME = System.DateTime.Now;
                    model.EDITUSERID = new UserInfoService().GetLoginInfo(cookie).UserId;
                    model.OWNERID= new UserInfoService().GetLoginInfo(cookie).UserId;
                    bool flag = pbll.AddDrugUse(model);
                }
                else //更新
                {



                    model.EDITDATETIME = System.DateTime.Now;
                    model.EDITUSERID = new UserInfoService().GetLoginInfo(cookie).UserId;


                    bool flag = pbll.UpdateDrugUse(model);



                }
                response.Data = model.DISEASEID;
                return Ok(response);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("PrescriptionController[Post]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Delete(int id)
        {
            try
            {
                //删除操作
                pbll.DeleteDrugUse(id);
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}
