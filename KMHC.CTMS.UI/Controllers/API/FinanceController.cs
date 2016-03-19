using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.Repository.Implement.CancerRecord;
using KMHC.CTMS.UI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace KMHC.CTMS.UI.Controllers.API
{
    [System.Web.Http.RoutePrefix("api/finance")]
    public class FinanceController : ApiController
    {
        AnnualIncomeRepository _annualRepository = new AnnualIncomeRepository();

        public IHttpActionResult Get(string Id)
        {
           

            HR_ANNUALINCOME _annualModel = new HR_ANNUALINCOME();
            try
            {
                _annualModel = _annualRepository.FindOne(p => p.PERSONID == Id);
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
                return BadRequest(ex.Message);
            }

            return Ok(_annualModel);
        }

        public IHttpActionResult Post([FromBody]Request<HR_ANNUALINCOME> request)
        {
            HR_ANNUALINCOME model = request.Data;
            if (string.IsNullOrEmpty(model.ANNUALINCOMEID))
            {
                model.ANNUALINCOMEID = Guid.NewGuid().ToString("N");
                _annualRepository.Insert(model);
            }
            else
            {
                _annualRepository.Update(model);
            }

            return Ok();
        }
    }
}
