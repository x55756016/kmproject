using KMHC.CTMS.BLL;
using KMHC.CTMS.BLL.CancerProcess;
using KMHC.CTMS.Model.CancerProcess;
using KMHC.CTMS.UI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace KMHC.CTMS.UI.Controllers.API
{
    public class MetaDataPickerController : ApiController
    {
        MetaDataBLL bll = new MetaDataBLL();
        public IHttpActionResult Get([FromUri]Request<MetaData> request)
        {
            try
            {
                Response<List<TreeItem>> response = new Response<List<TreeItem>>();
                List<TreeItem> treeList = bll.GetTreeList(request.Keyword);
                response.Data = treeList;
                 return Ok(response);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("MetaDataPickerController[Get]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }
    }
}