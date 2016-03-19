using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KMHC.CTMS.BLL;
using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.UI.Dtos;

namespace KMHC.CTMS.UI.Controllers.API
{

    [RoutePrefix("api/DrugGeneSearch")]
    public class DrugGeneSearchController : ApiController
    {
        private DrugBankBLL bll = new DrugBankBLL();

        /// <summary>
        /// 返回根据药典的查询结果
        /// </summary>
        /// <param name="dbId"></param>
        /// <param name="drugName"></param>
        /// <returns></returns>
        public IHttpActionResult Get(string dbId )
        {
            Response<IEnumerable<DrugBank>> response = new Response<IEnumerable<DrugBank>>();
            try
            {
                var model = bll.GetDrugGeneInfo(dbId);
                response.Data = model;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }
    }



    public class DrugInterSearchController : ApiController
    {

        private DrugBankBLL bll = new DrugBankBLL();

        /// <summary>
        /// 返回根据药典的查询结果
        /// </summary>
        /// <param name="dbId"></param>
        /// <param name="drugName"></param>
        /// <returns></returns>
        public IHttpActionResult Get(string dbId )
        {
            Response<IEnumerable<DrugBank>> response = new Response<IEnumerable<DrugBank>>();
            try
            {
                var model = bll.GetDrugInteration(dbId);
                response.Data = model;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }

    }
}
