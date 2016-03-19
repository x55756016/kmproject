using KMHC.CTMS.BLL;
using KMHC.CTMS.BLL.CancerProcess;
using KMHC.CTMS.Model.CancerProcess;
using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.UI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KMHC.CTMS.UI.Controllers.API
{
    public class SymptomController : ApiController
    {
        SymptomBLL bll = new SymptomBLL();

        public IHttpActionResult Get()
        {
            IList<SymptomExt> response = new List<SymptomExt>();
            
            //获取登录信息
            UserInfo currentUser = new UserInfoService().GetCurrentUser();
            if(currentUser==null){
                return Ok(response);
            }
            string userId = currentUser.UserId;
            IEnumerable<Symptom> list = bll.GetList(p=>p.USERID==userId);
            foreach(Symptom item in list){
                SymptomExt ext = new SymptomExt();
                ext.UserId=item.UserId;
                ext.SymptomID=item.ID;
                ext.SymptomName=item.SymptomName;
                ext.DictsymptomId=item.DictsymptomId;

                response.Add(ext);
            }

            return Ok(response);
        }

        public IHttpActionResult Get(string id)
        {
            try{
                IList<SymptomExt> response = new List<SymptomExt>();
                //获取登录信息
                UserInfo currentUser = new UserInfoService().GetCurrentUser();
                if (currentUser == null)
                {
                    return Ok(response);
                }

                string userId = currentUser.UserId;
                IEnumerable<Symptom> list = bll.GetList(p => p.USERID == userId);

                SymptomRecordBLL strBLL = new SymptomRecordBLL();
                IEnumerable<SymptomRecord> symRecords = strBLL.GetList(p=>p.USERID == userId);
                foreach (SymptomRecord item in symRecords)
                {
                    SymptomExt ext = new SymptomExt();
                    ext.SymptomID = item.SymptomId;
                    ext.SymptomName = GetSymptomName(list,item.SymptomId);
                    ext.SymptomDate = item.SymptomDate;
                    ext.SymptomLevel = item.SymptomLevel == "" ? "0" : item.SymptomLevel;

                    response.Add(ext);
                }

                return Ok(response.OrderBy(p=>p.SymptomDate));
            }catch(Exception ex){
                return BadRequest(ex.ToString());
            }
        }

        public IHttpActionResult Post([FromBody]Request<IList<SymptomExt>> request)
        {
            try
            {
                //获取参数
                IList<SymptomExt> list = request.Data;
                DateTime dt = DateTime.Parse(request.ID);

                //获取登录信息
                UserInfo currentUser = new UserInfoService().GetCurrentUser();
                string userId = currentUser == null ? "" : currentUser.UserId;

                foreach (SymptomExt item in list)
                {
                    Symptom symtom = new Symptom();
                    symtom = bll.GetOne(p => p.USERID == userId && p.SYMPTOMNAME.Equals(item.SymptomName));

                    string _symtomId = symtom==null?"":symtom.ID;
                    if (string.IsNullOrEmpty(_symtomId))
                    {
                        symtom = new Symptom();
                        symtom.SymptomName = item.SymptomName;
                        symtom.DictsymptomId = item.DictsymptomId;
                        symtom.UserId = userId;
                        symtom.CreateDate = DateTime.Now;

                        string symtomId = bll.Add(symtom);
                        if (!string.IsNullOrEmpty(symtomId))
                        {
                            SymptomRecord symRecord = new SymptomRecord();
                            symRecord.SymptomId = symtomId;
                            symRecord.SymptomLevel = item.SymptomLevel;
                            symRecord.UserId = userId;
                            symRecord.SymptomDate = dt;

                            SymptomRecordBLL strBLL = new SymptomRecordBLL();
                            strBLL.Add(symRecord);
                        }

                    }
                    else
                    {
                        SymptomRecordBLL strBLL = new SymptomRecordBLL();
                        SymptomRecord symRecord = strBLL.GetOne(p => p.SYSMPTOMDATE == dt && p.USERID == userId && p.SYMPTOMID == _symtomId);
                        if (symRecord == null)
                        {
                            symRecord = new SymptomRecord();
                            symRecord.SymptomId = _symtomId;
                            symRecord.SymptomLevel = item.SymptomLevel;
                            symRecord.UserId = userId;
                            symRecord.SymptomDate = dt;
                            symRecord.CreateDate = DateTime.Now;

                            strBLL.Add(symRecord);
                        }
                        else
                        {
                            symRecord.SymptomLevel = item.SymptomLevel;
                            symRecord.EditDate = DateTime.Now;
                            strBLL.Edit(symRecord);
                        }
                    }
                }

                return Ok("ok");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }

        /// <summary>
        /// 根据ID获取症状名称
        /// </summary>
        /// <param name="symptoms"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private string GetSymptomName(IEnumerable<Symptom> symptoms, string id)
        {
            string name = string.Empty;
            foreach (Symptom item in symptoms)
            {
                if(item.ID.Equals(id)){
                    name = item.SymptomName;
                    break;
                }
            }

            return name;
        }

        /// <summary>
        /// 获取症状等级名称
        /// </summary>
        /// <param name="levelId"></param>
        /// <returns></returns>
        private string GetLevelName(string levelId)
        {
            string name = string.Empty;
            return name;
        }
    }
}