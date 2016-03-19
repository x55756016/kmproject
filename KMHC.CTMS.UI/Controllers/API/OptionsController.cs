using System.Net.Mime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web.Http;
using System.Xml.Linq;
using System.Xml.Serialization;
using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.Model.Repository.Implement;
using KMHC.CTMS.Model.Repository.Interface;

namespace KMHC.CTMS.UI.Controllers.API
{
    [RoutePrefix("api/options")]
    public class OptionsController : ApiController
    {
        private readonly ICodeRepository repo = new EFCodeRepository();
        //private IAreaRepository repository = new EFAreaRepository();
        [Route("{id}")]
        public IHttpActionResult Get(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }
            var codes = repo.GetList(id.Split(',')).ToList();
            Dictionary<string, List<Option>> dic = codes.ToDictionary(c => c.CodeNo, c => c.Options);
            //if (id.Contains("GBT2260-2007"))
            //{
            //    var list = repository.GetAllAreas().OrderBy(o => o.ParentId).Select(o => new Option() { Name = o.AreaName, Value = o.AreaId.ToString(), Parent = o.ParentId.ToString() }).ToList();
            //    //var areaList = CacheManager.Get(CacheManager.AreaKey);
            //    dic.Add("GBT2260-2007", list);
            //}
            return Ok(dic);
        }

       
    }
}
