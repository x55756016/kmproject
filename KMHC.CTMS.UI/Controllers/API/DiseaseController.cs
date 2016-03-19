using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.Model.Repository;
using KMHC.CTMS.Model.Repository.Implement;
using KMHC.CTMS.Model.Repository.Interface;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.UI.Dtos;
using KMHC.CTMS.Common.Cached;

namespace HRWebUI.Controllers.API
{
     [RoutePrefix("api/disease")]
    public class DiseaseController : ApiController
    {
        IDiseaseRepository repository = new EFDiseaseRepository();


        public IHttpActionResult Get(int currentPage, int pageSize,string types, string key)
        {
            PageInfo pageInfo =new PageInfo()
            {
                PageIndex = currentPage,
                PageSize = pageSize,
                OrderField = "DiseaseCode",
                Order = OrderEnum.desc
            };
            var list=repository.GetDiseaseList(pageInfo, GetDiseaseType(types), key);
            Response<IEnumerable<Disease>> response = new Response<IEnumerable<Disease>>
            {
                Data = list,
                PagesCount = pageInfo.Total / pageSize
            };
            return Ok(response);
        }
  
        public IHttpActionResult Get()
        {
            var tree = LocalCachedProvider.Instance.Get("DiseaseTypeTree");
            if (tree != null) return Ok(tree);
            var diseaseType = repository.GetDiseaseTypes().ToList();
            tree = CreateTree(diseaseType,0).JsonSerialize();
            LocalCachedProvider.Instance.Set("DiseaseTypeTree", tree);
            return Ok(tree);
        }



        private List<DiseaseTypeTree> CreateTree(List<DiseaseType> diseaseType, int pId)
        {
            var list = diseaseType.Where(o => o.ParentId == pId).ToList();
            if (!list.Any())
            {
                return null;
            }
            return list.Select(type => new DiseaseTypeTree()
            {
                text = type.DiseaseName,
                value = type.CategoryCode,
                //href = type.DiseaseName,
                //tags = null,
                nodes = CreateTree(diseaseType,type.Id)
            }).ToList();
        }

         private string[] GetDiseaseType(string types)
         {
             if (string.IsNullOrWhiteSpace(types))
             {
                 return null;
             }
             var index = types.IndexOf('-');
             if (index==-1)
             {
                 return new[] { types };
             }
             var startChar = types.Substring(0, 1);
             var startIndex = types.Substring(1, index).ToInt(0);
             var endIndex = types.Substring(index + 2).ToInt(0);

             var typesArr = new string[endIndex - startIndex+1];
             for (int i = startIndex; i <= endIndex; i++)
             {
                 typesArr[i - startIndex] = string.Format("{0}{1:D2}", startChar,i);
             }
             return typesArr;

         }
    }
}
