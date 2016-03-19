using KMHC.CTMS.BLL.PrecisionMedicine;
using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.UI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace KMHC.CTMS.UI.Controllers.API
{
    [System.Web.Http.RoutePrefix("api/DrugControl")]

    public class DrugControlController : ApiController
    {
        private CnDrugBLL service = new CnDrugBLL();

        public IHttpActionResult Get(int CurrentPage, string Name = "", string PinYin = "", string Indication = "", string IsPrescription = "", string IsMedicalInsurance = "", string TypeName = "", string KindName = "")
        {
            //申明参数
            int _pageSize = 10;

            try
            {
                PageInfo pageInfo = new PageInfo()
                {
                    PageIndex = CurrentPage,
                    PageSize = _pageSize,
                    OrderField = "ID",
                    Order = OrderEnum.asc
                };
                var list = service.GetList(pageInfo, Name, PinYin, Indication,IsPrescription,IsMedicalInsurance,TypeName,KindName);
                Response<IEnumerable<CnDrug>> response = new Response<IEnumerable<CnDrug>>
                {
                    Data = list,
                    PagesCount = pageInfo.Total / _pageSize
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                LogHelper.WriteInfo(ex.ToString());
                return BadRequest("异常");
            }
        }
    }
}
