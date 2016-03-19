using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Net.Http;
using KMHC.CTMS.UI.Dtos;
using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.Model.Repository.Implement;
using KMHC.CTMS.Model.Repository.Interface;

namespace KMHC.CTMS.UI.Controllers.API
{
    [System.Web.Http.RoutePrefix("api/scoreTemplate")]
    public class ScoreTemplateController : ApiController
    {
        //
        protected IScoreTemplateRepository _repository = new EFScoreTemplateRepository();
        public IHttpActionResult Post([FromBody]Request<ScoreTemplate> request)
        {
            try
            {
                if (request.Data != null)
                {
                    ScoreTemplate scoreTemplate = request.Data;
                    if (string.IsNullOrEmpty(scoreTemplate.TemplateID))
                    {
                        _repository.Add(scoreTemplate);
                    }
                    else
                    {
                        _repository.Edit(scoreTemplate);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        public IHttpActionResult Get([FromUri]Request<ScoreTemplate> request)
        {
            Response<IEnumerable<object>> response = new Response<IEnumerable<object>>();
            try
            {
                var model = _repository.GetAll();
                if (model == null || model.Count==0)
                {
                    return NotFound();
                }
                response.Data = model;
                return Ok(response);
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Get(string id)
        {
            Response<object> response = new Response<object>();
            try
            {
                var model=_repository.Get(id);
                if (!string.IsNullOrEmpty(model.QuestionsJson))
                {
                    model.Questions = JsonHelper.JsonDeserialize<List<ScoreTemplateQuestion>>(model.QuestionsJson);
                }
                if (!string.IsNullOrEmpty(model.AnswersJson))
                {
                    model.Grades = JsonHelper.JsonDeserialize<List<ScoreTemplateGrade>>(model.AnswersJson);
                }
                response.Data = model;
                return Ok(response);
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
                return BadRequest(ex.Message);
            }
            
        }
	}
}