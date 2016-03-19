using KMHC.CTMS.UI.Dtos;
using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.Model.Repository.Implement;
using KMHC.CTMS.Model.Repository.Implement.CancerRecord;
using KMHC.CTMS.Model.Repository.Interface;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using KMHC.CTMS.BLL.CancerProcess;
using KMHC.CTMS.BLL.CancerRecord;
using KMHC.CTMS.Model.CancerProcess;

namespace KMHC.CTMS.UI.Controllers.API
{
    [System.Web.Http.RoutePrefix("api/doctor")]
    public class DoctorController : ApiController
    {
        SeeDoctorHistoryRepository _repository = new SeeDoctorHistoryRepository();

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IHttpActionResult Get(string uid, int currentPage, string keyword = "")
        {
            //数据异常判断处理
            if (string.IsNullOrEmpty(uid))
            {
                return BadRequest("数据异常");
            }

            //查询数据
            var list = new List<SeeDoctorHistory>();
            //int count = 1;
            try
            {
                Response<IEnumerable<SeeDoctorHistory>> response = new Response<IEnumerable<SeeDoctorHistory>>();
                SeeDoctorHistoryBLL sBll = new SeeDoctorHistoryBLL();

                PageInfo pageInfo = new PageInfo()
                {
                    PageIndex = currentPage,
                    PageSize =10,
                    OrderField = "DIAGNOSISTIME",
                    Order = OrderEnum.desc
                };

                list = sBll.GetSeeDoctorHistories(pageInfo, keyword, uid);
                response.Data = list;
                response.PagesCount = pageInfo.PagesCount;
                return Ok(response);
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 获取公共个人信息
        /// </summary>
        /// <param name="personid"></param>
        /// <returns></returns>
        public IHttpActionResult Get(string Uid)
        {
            IPersonInfoRepository _personRepository = new EFPersonInfoRepository();
            CancerUserInfoRepository _repository = new CancerUserInfoRepository();

            //申明返回
            HR_CNR_USER model = new HR_CNR_USER();
            try
            {
                model = _repository.FindOne(p => p.USERID == Uid);
                if (model != null)
                {
                    int personID = 0;
                    int.TryParse(model.PERSONID, out personID);
                    if (personID > 0)
                    {
                        var person = _personRepository.Get(personID);
                        if (person != null && person.BirthDate.Length > 4)
                        {
                            int birthDateYear = Convert.ToInt32(person.BirthDate.Substring(0, 4));
                            model.AGE = DateTime.Now.Year - birthDateYear + 1;
                            switch (person.Gender)
                            {
                                case "1":
                                    model.SEX = "男";
                                    break;
                                case "2":
                                    model.SEX = "女";
                                    break;
                                case "0":
                                    model.SEX = "未知";
                                    break;
                                default:
                                    model.SEX = "其他";
                                    break;
                            }
                        }                    
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
                return BadRequest(ex.Message);
            }

            //返回
            return Ok(model);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IHttpActionResult Post([FromBody]Request<HR_SEEDOCTORHISTORY> request)
        {
            //获取参数
            var model = request.Data;

            try
            {
                if (string.IsNullOrEmpty(model.HISTORYID))//新增插入
                {
                    model.HISTORYID = System.Guid.NewGuid().ToString("N");
                    _repository.Insert(model);
                }
                else
                {//修改更新 
                    _repository.Update(model);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        public IHttpActionResult Get(string id, int currentPage)
        {
            //数据异常判断处理
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("数据异常");
            }

            //查询数据
            IEnumerable<SeeDoctorHistory> _list = new List<SeeDoctorHistory>();
            int count = 1;
            try
            {
                UserEventBLL userBLL = new UserEventBLL();
                UserEvent userEvent = userBLL.Get(p => p.EVENTID.Equals(id));
                if (userEvent == null)
                {
                    return BadRequest("该记录不存在！");
                }

                _list = _repository.GetList(p => p.PERSONID == userEvent.FromUser);

                count = _list.Count();
                int _pageSize = 10;
                _list = _list.Skip((currentPage - 1) * _pageSize).Take(_pageSize);
                SeeDoctorHistoryBLL seeDoctorBLL = new SeeDoctorHistoryBLL();
                foreach (SeeDoctorHistory item in _list)
                {
                    item.ICD10 = seeDoctorBLL.GetICD10(item.HISTORYID);
                }

                count = count / _pageSize;
                if (count % _pageSize > 0)
                {
                    count += 1;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
                return BadRequest(ex.Message);
            }

            Response<IEnumerable<object>> response = new Response<IEnumerable<object>>();
            response.Data = _list;
            response.PagesCount = count;

            //返回
            return Ok(response);
        }

    }
}
