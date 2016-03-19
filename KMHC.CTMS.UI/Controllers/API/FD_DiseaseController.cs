using KMHC.CTMS.UI.Dtos;
using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.Model.Repository.Implement.CancerRecord;
using KMHC.CTMS.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace KMHC.CTMS.UI.Controllers.API
{

    [System.Web.Http.RoutePrefix("api/familyDisease")]
    public class familyDiseaseController : ApiController
    {
        EFFD_MemberRepository memberRepository = new EFFD_MemberRepository();
        
        public IHttpActionResult Post([FromBody]Request<FD_Member> request)
        {
            Response<IEnumerable<object>> response = new Response<IEnumerable<object>>();
            try
            {
                string Uid = request.Keyword;
                FD_Member model = request.Data as FD_Member;
                if (string.IsNullOrEmpty(model.ID))
                {
                    model.UserID = Uid;
                    memberRepository.Add(model);
                }
                else
                { 
                    memberRepository.Edit(model);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
                return BadRequest(ex.Message);
            }
        }
	}
}