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
    [System.Web.Http.RoutePrefix("api/familyDiseaseMember")]
    public class familyDiseaseMemberController : ApiController
    {
        EFFD_MemberRepository _repository = new EFFD_MemberRepository();
        public IHttpActionResult Get([FromUri]Request<FD_Relation> request)
        {
            Response<IEnumerable<object>> response = new Response<IEnumerable<object>>();
            try
            {
                if (!string.IsNullOrEmpty(request.Keyword))
                {
                    var list=_repository.GetByUserID(request.Keyword);
                    response.Data = list;
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Post([FromBody]Request<List<FD_Relation>> request)
        {
            Response<IEnumerable<object>> response = new Response<IEnumerable<object>>();
            try
            {
                var UID = request.Keyword;
                var list = request.Data;
                List<FD_Member> memberList=new List<FD_Member>();
                foreach (FD_Relation relation in list)
                {
                    memberList.Add(new FD_Member()
                    {
                       ID=Guid.NewGuid().ToString(),
                       UserID = UID,
                       RelationShip=relation,
                       RelationID=relation.ID,
                       Sex=relation.Sex,
                       IsAlive=true
                    });
                }
                _repository.AddList(memberList);
                response.Data = memberList;
                return Ok(response);
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
                return BadRequest(ex.Message);
            }
        }


        public IHttpActionResult Delete(string id)
        {
            try
            {
                bool isSuccess = _repository.Delete(id);
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