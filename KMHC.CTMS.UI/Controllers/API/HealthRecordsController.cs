using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using System.Web.UI.WebControls;
using KMHC.CTMS.UI.Dtos;
using KMHC.CTMS.Model.CancerRecord;
using Newtonsoft.Json;
using KMHC.CTMS.Model.Repository.Implement;
using KMHC.CTMS.Model.Repository.Interface;
using KMHC.CTMS.BLL;
using System.Data.Entity.Validation;
using KMHC.CTMS.Model.Repository.Implement.CancerRecord;
using KMHC.CTMS.DAL.Database;


namespace KMHC.CTMS.UI.Controllers.API
{
    [RoutePrefix("api/healthrecords")]
    public class HealthRecordsController : ApiController
    {
        public IHttpActionResult Get(int ID)
        {
            try
            {
                using(EFPersonInfoRepository repository = new EFPersonInfoRepository())
                { 
                    PersonInfo personInfo = repository.Get(ID);
                    if (personInfo == null) return NotFound();
                    return Ok(personInfo);
                }
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("GeneDicGeneController[Get]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Post([FromBody]Request<PersonInfo> request)
        {
            Response<PersonInfo> response = new Response<PersonInfo>();
            try
            {

                using (EFPersonInfoRepository repository = new EFPersonInfoRepository())
                {

                    PersonInfo model = request.Data as PersonInfo;
                    if (model == null) return NotFound();
                    if (string.IsNullOrEmpty(model.PersonNo))
                    {
                        model.PersonNo = model.IDNumber;
                    }
                    if (model.PersonId <= 0)
                    {
                        int ID=repository.Add(model);
                        model.PersonId = ID;
                        if (!string.IsNullOrEmpty(request.Keyword))
                        {
                            CancerUserInfoRepository cancerUserRepository=new CancerUserInfoRepository();
                            HR_CNR_USER cancerUser = cancerUserRepository.Find(request.Keyword);
                            if (cancerUser != null)
                            {
                                cancerUser.PERSONID = ID.ToString();
                                cancerUserRepository.Update(cancerUser);
                            }
                        }
                    }
                    else
                    {
                        bool isEditSuccess = repository.Edit(model);
                    }
                    response.Data = model;
                    return Ok(response);
                }
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder errors = new StringBuilder();
                IEnumerable<DbEntityValidationResult> validationResult = ex.EntityValidationErrors;
                foreach (DbEntityValidationResult result in validationResult)
                {
                    ICollection<DbValidationError> validationError = result.ValidationErrors;
                    foreach (DbValidationError err in validationError)
                    {
                        errors.Append(err.PropertyName + ":" + err.ErrorMessage + "\r\n");
                    }
                }
                string errMsg = errors.ToString();
                LogService.WriteErrorLog("GeneDicGeneController[Post]", errMsg);
                return BadRequest(errMsg);
                //简写
                //var validerr = ex.EntityValidationErrors.First().ValidationErrors.First();
                //Console.WriteLine(validerr.PropertyName + ":" + validerr.ErrorMessage);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("GeneDicGeneController[Post]", ex.ToString());
                return BadRequest(ex.Message);
            }
        }

    }
}
