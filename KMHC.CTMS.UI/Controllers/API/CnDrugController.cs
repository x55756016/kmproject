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
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;

namespace KMHC.CTMS.UI.Controllers.API
{
    [System.Web.Http.RoutePrefix("api/CnDrug")]
    public class CnDrugController : ApiController
    {
        private CnDrugBLL service = new CnDrugBLL();

        //
        // GET: /CnDrug/
        public IHttpActionResult Get(int CurrentPage, string Name = "", string PinYin = "", string Indication = "")
        {
            //申明参数
            int _pageSize = 15;

            try
            {
                PageInfo pageInfo = new PageInfo()
                {
                    PageIndex = CurrentPage,
                    PageSize = _pageSize,
                    OrderField = "ID",
                    Order = OrderEnum.asc
                };
                var list = service.GetList(pageInfo, Name, PinYin, Indication);
                Response<IEnumerable<CnDrug>> response = new Response<IEnumerable<CnDrug>>
                {
                    Data = list,
                    PagesCount = pageInfo.PagesCount
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                LogHelper.WriteInfo(ex.ToString());
                return BadRequest("异常");
            }
        }

        public IHttpActionResult Get(string ID)
        {
            CnDrug model = service.Get(ID);
            return Ok(model);
        }

        public IHttpActionResult Get(string Name, string token)
        {
            string _name = Name.ToLower();
            Expression<Func<DUG_CNDRUG, bool>> pridicate = p => !p.ISDELETED&&(p.NAME.ToLower().Contains(_name)||p.COMMONNAME.ToLower().Contains(_name)||p.ENNAME.ToLower().Contains(_name));
            IEnumerable<CnDrug> list = service.Get(pridicate).Take(15);

            return Ok(list);
        }

        public IHttpActionResult Post([FromBody]Request<CnDrug> request)
        {
            CnDrug model = request.Data as CnDrug;
            if (string.IsNullOrEmpty(model.ID))
            {
                service.Add(model);
            }
            else
            {
                service.Edit(model);
            }

            return Ok("ok");
        }

        public IHttpActionResult Delete(string ID)
        {
            CnDrug model = service.Get(ID);
            if (model != null)
            {
                model.IsDeleted = true;
                service.Edit(model);
            }

            return Ok("ok");
        }


        /// <summary>
        /// 获取单元格内容
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        private string GetCellValue(ICell cell, int maxLength = 0)
        {
            string str = "";
            if (cell != null)
            {
                if (maxLength == 0)
                    str = cell.ToString();
                else
                    str = cell.ToString().Length >= maxLength ? cell.ToString().Substring(0, maxLength) : cell.ToString();
            }

            return str;
        }
    }
}
