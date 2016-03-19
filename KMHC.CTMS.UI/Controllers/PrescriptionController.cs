using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KMHC.CTMS.BLL;
using KMHC.CTMS.BLL.PrecisionMedicine;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.UI.Dtos;

namespace KMHC.CTMS.UI.Controllers
{
    public class PrescriptionController : Controller
    {
        private PrescriptionBLL pbll = new PrescriptionBLL();

        public JsonResult GetDrugInteration(string diseaseId, string drugId,string userId)
        {
            //var cookie =Request.Cookies["Token"];
            Response<AddDrugEffect> response = new Response<AddDrugEffect>();
            try
            {
                var model = pbll.CheckDrugUse(diseaseId, drugId, userId);
                response.Data = model;
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("PrescriptionController[Get]", ex.ToString());
            }
            return Json(response);
        }


        //获得单个开药信息
        public JsonResult GetInfo(string disId,int id)
        {
            Response<PrescritionModel> response = new Response<PrescritionModel>();
            try
            {
                var model = pbll.GetDrugUseList(disId,id);

                response.Data = model;

            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("PrescriptionController[Get]", ex.ToString());
            }
            return Json(response);
        }


        /// <summary>
        /// 获取用户基因突变信息
        /// </summary>
        /// <returns></returns>
        public JsonResult GetUserGeneDesc(string id)
        {
            Response<IEnumerable<string>> response = new Response<IEnumerable<string>>();
            try
            {
                var list = new GeneAlleleService().GetUserGeneDesc(id);
                response.Data = list;
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("PrescriptionController[GetUserGeneDesc]", ex.ToString());
            }
            return Json(response);
        }

	}
}