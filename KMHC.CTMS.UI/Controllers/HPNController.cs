using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;
using KMHC.CTMS.Model.Repository.CancerRecord;
using KMHC.CTMS.Model.Repository.Implement.CancerRecord;
using KMHC.CTMS.Model.Repository.Interface;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.Model.PrecisionMedicine;
using System.Configuration;
using KMHC.CTMS.BLL;

namespace KMHC.CTMS.UI.Controllers
{
    public class HPNController : Controller
    {
        private readonly DbContext _db = new CRDatabase();
        IHPNRepository templateService = new HPNRepository();
        UserInfoService _user = new UserInfoService();
        private readonly DbContext _context = new CRDatabase();

        #region 属性
        /// <summary>
        /// 发送短信url
        /// </summary>
        public string VercodeUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["VercodeUrl"];
            }
        }

        /// <summary>
        /// 每天每个号码可发送的短信验证码条数
        /// </summary>
        public int MaxVercodeCount
        {
            get
            {
                return ConfigurationManager.AppSettings["MaxVercodeCount"].ToInt(5);
            }
        }
        #endregion

        public ActionResult getTestList(string UserId = null,decimal? TemplateType = null)
        {
            IQueryable<HPN_TEMPLATE> testList = _db.Set<HPN_TEMPLATE>().Where(p => p.STATUS == 1 && (TemplateType == null ? true : p.TYPE == TemplateType)).OrderBy(p => p.INDEX);
            List<object> list = new List<object>();
            HR_CNR_USER user = null;
            if (!string.IsNullOrEmpty(UserId))
            {
                user = _db.Set<HR_CNR_USER>().Where(p => p.USERID == UserId).FirstOrDefault();
                if (user != null)
                {
                    foreach (HPN_TEMPLATE item in testList)
                    {
                        HPN_TESTRESULT testResult = (from x in _db.Set<HPN_TESTRESULT>()
                                                     where x.TEMPLATENAME == item.NAME
                                                     select x).OrderByDescending(p => p.INPUTTIME).FirstOrDefault();
                        if (testResult == null)
                            continue;
                        if (!string.IsNullOrEmpty(testResult.RESULT))
                            item.TestScore = testResult.RESULT.ToDouble(0);
                        var result = templateService.getResultDescByScore(testResult.TEMPLATENAME, testResult.RESULT.ToDouble(0));
                        item.WELCOMELABEL = string.Empty;
                        if (result.Count > 0)
                        {
                            string desc = result.First().RESULT;
                            if (string.IsNullOrEmpty(desc))
                                desc = result.First().RESULTDETAIL;
                            if(!string.IsNullOrEmpty(desc))
                                item.TestResult = desc;
                        }
                        if(testResult.INPUTTIME != null)
                            item.TestTime = testResult.INPUTTIME;
                    }                    
                }
            }
            foreach (HPN_TEMPLATE item in testList)
            {
                list.Add(new { Title = item.TITLE, ActionName = item.NAME, TestScore = item.TestScore == 0 ? string.Empty : item.TestScore.ToString(), TestResult = item.TestResult, TestTime = item.TestTime.ToString() });
            }
            return Json(new {UserInfo = user,Templates = list,TemplateType = TemplateType == 1 ? "营养状况量表列表" : TemplateType == 2 ? "心理状况量表列表" : TemplateType == 3 ? "体力状况量表列表" : string.Empty }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult sendVerCode(string phone)
        {
            if (string.IsNullOrEmpty(phone))
                return Json(new { Msg = "请输入手机号码" }, JsonRequestBehavior.AllowGet);

            DateTime dtNow = DateTime.Now;
            DateTime dtToday = new DateTime(dtNow.Year,dtNow.Month,dtNow.Day,0,0,0);
            List<HPN_SENDVERCODELOG> vercodeList = _context.Set<HPN_SENDVERCODELOG>().Where(p => p.MOBILEPHONE == phone && p.STATUS == 0 && p.INPUTTIME >= dtToday).ToList();
            if (vercodeList.Count >= MaxVercodeCount)
                return Json(new { Msg = "今天发送验证码过多" }, JsonRequestBehavior.AllowGet);

            request req = new request();
            req.userId = "152000133";
            req.password = "5B04B6DE58D5FE0C4A94BDC0B129AB41";
            req.templateId = "152000133_001";
            req.port = "00133";
            req.signature = "";
            req.phone = new List<string>();
            req.phone.Add(phone);
            req.data = new XmlDocument().CreateNode(XmlNodeType.CDATA, "", "");
            string guid = Guid.NewGuid().GetHashCode().ToString();
            string verCode = guid.Substring(guid.Length - 6, 6);
            if (verCode[0] == '0')
                verCode = '1' + verCode.Substring(1);
            req.data.Value = verCode;

            try
            {
                string result = SendVercode(req);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(result);
                result = doc.LastChild.FirstChild.InnerText;
                //result = XmlToJson.XmlToJSON(result);
                return Json(new { retCode = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message,JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult getResult(string testno)
        {
            HPN_TESTRESULT testResult = (from x in _db.Set<HPN_TESTRESULT>() where x.TESTNO == testno select x).FirstOrDefault();
            List<subresult> list = new List<subresult>();
            HPN_TEMPLATE t = (from x in _db.Set<HPN_TEMPLATE>() where x.NAME == testResult.TEMPLATENAME select x).FirstOrDefault();
            int templateType = 1;
            if (t != null)
                templateType = (int)t.TYPE;
            if (!string.IsNullOrEmpty(testResult.RESULT))
                list = testResult.RESULT.JsonDeserialize() as List<subresult>;
            else
            {
                List<HPN_RESULTDESC> resultList = templateService.getResultDescByScore(testResult.TEMPLATENAME, testResult.RESULT.ToDouble(0));
                if (resultList.Count <= 0)
                {
                    subresult sr = new subresult();
                    sr.Score = testResult.RESULT.ToDouble(0);
                    sr.RESULT = string.Empty;
                    sr.Remark = string.Empty;
                    list.Add(sr);
                }
                else
                {
                    foreach (HPN_RESULTDESC result in resultList)
                    {
                        subresult sr = new subresult();
                        sr.Score = testResult.RESULT.ToDouble(0);
                        sr.RESULT = result.RESULT;
                        sr.Remark = result.RESULTDETAIL;
                        list.Add(sr);
                    }
                }
            }
            return Json(new { Score = testResult.RESULT, Result = list, IsFinish = testResult.USERID == "0" ? true : false, TemplateType = templateType,UserId = testResult.USERID }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getTemplate(string templateName,string userId = null)
        {
            if (string.IsNullOrEmpty(templateName))
                return null;

            HPN_TEMPLATE template = templateService.getTemplateByName(templateName);
            if (template == null)
                return null;

            return Json(template, JsonRequestBehavior.AllowGet);
        }

        public ActionResult submitTest()
        {
            try
            {
                #region 验证参数
                string templateName = Request.Form["templateName"];

                if (string.IsNullOrEmpty(templateName))
                    return Json(new { Status = -9, Data = string.Empty, Msg = "参数不正确" });
                #endregion

                #region 生成订单
                DateTime dtNow = DateTime.Now;
                string testNo = dtNow.Year.ToString().PadLeft(4, '0') + dtNow.Month.ToString().PadLeft(2, '0') + dtNow.Day.ToString().PadLeft(2, '0') + dtNow.Hour.ToString().PadLeft(2, '0') + dtNow.Minute.ToString().PadLeft(2, '0') + dtNow.Second.ToString().PadLeft(2, '0');
                HPN_TESTRESULT test = new HPN_TESTRESULT();
                test.INPUTTIME = dtNow;
                test.TESTNO = testNo;
                test.OTHERTESTNO = string.Empty;
                UserInfo user = _user.GetCurrentUser();
                test.USERID = user == null ? "0" : user.UserId;
                test.TEMPLATENAME = templateName;
                test.STATUS = 1;
                test.IP = StringExtension.getIPAddress();
                test.RESULT = string.Empty;
                test.RESULT = string.Empty;
                test.RESULTDETAIL = string.Empty;
                test.ID = Guid.NewGuid().ToString();
                #endregion

                #region 保存明细
                List<HPN_TEMPLATEITEM> questions = templateService.getTemplateItemList(templateName).ToList();
                if (questions.Count <= 0)
                    return Json(new { Status = -8, Data = string.Empty, Msg = "模版关键字不正确" });

                List<HPN_TEMPLATEITEMOPTIONS> options = templateService.getTemplateItemOptionsListByTemplateName(templateName);
                HPN_TESTRESULTDETAILS detail = new HPN_TESTRESULTDETAILS();
                List<HPN_TESTRESULTDETAILS> testDetails = new List<HPN_TESTRESULTDETAILS>();
                foreach (HPN_TEMPLATEITEM item in questions)
                {
                    if (item.TAGTYPE == 4)
                    {
                        detail = new HPN_TESTRESULTDETAILS();
                        detail.INPUTTIME = dtNow;
                        detail.TESTNO = testNo;
                        detail.TEMPLATEITEMNAME = item.KWD;
                        HPN_TEMPLATEITEMOPTIONS option = options.Find(p => p.TEMPLATEITEMNAME == item.KWD && p.VALUE.Trim() == Request.Form[item.KWD].Trim());
                        detail.ITEMRESULTID = option.IDATTR;
                        detail.ITEMRESULT = Request.Form[item.KWD];
                    }
                    else
                    {
                        //直接写
                        detail = new HPN_TESTRESULTDETAILS();
                        detail.INPUTTIME = dtNow;
                        detail.TESTNO = testNo;
                        detail.TEMPLATEITEMNAME = item.KWD;
                        detail.ITEMRESULTID = item.KWD;
                        if ((templateName == "Psqi" && item.KWD == "psqi_1") || (templateName == "Psqi" && item.KWD == "psqi_3"))
                        {
                            string val = Request.Form[item.KWD];
                            string[] arr = val.Split(':');
                            if (arr.Length == 2)
                                val = (arr[0].ToDouble(0) + arr[1].ToDouble(0) / 60).ToString();
                            detail.ITEMRESULT = val;
                        }
                        else
                            detail.ITEMRESULT = Request.Form[item.KWD];
                    }
                    detail.ID = Guid.NewGuid().ToString();
                    testDetails.Add(detail);
                }
                #endregion

                #region 计算分数
                TemplateService _tservice = new TemplateService(_db);
                test = _tservice.handleResult(test, testDetails);
                #endregion

                #region 保存结果
                _db.Set<HPN_TESTRESULT>().Add(test);
                _db.Set<HPN_TESTRESULTDETAILS>().AddRange(testDetails);
                _db.SaveChanges();
                #endregion

                return Json(new { Status = 1, Data = string.Empty, Msg = "操作成功", Url = "/result/" + testNo });
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog("HPNController[Post]", ex.ToString());
                return Json(new { Status = -99, Data = string.Empty, Msg = ex.Message });
            }
        }

        private string SendVercode(request req)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(request));
            StringWriter sw = new StringWriter();
            xmlSerializer.Serialize(sw, req);

            string formData = sw.ToString().Replace("utf-16", "utf-8");
            string formUrl = VercodeUrl;

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("Cmd", "mt");
            int timestamp = StringExtension.getTimeStamp(DateTime.Now);
            dic.Add("TS", timestamp.ToString());
            string md5String = (formData + timestamp + "5B04B6DE58D5FE0C4A94BDC0B129AB41").ToMd5().ToUpper();
            dic.Add("Authorization", md5String);

            try
            {
                string result = HttpHelper.Post(formUrl, dic, formData);
                HPN_SENDVERCODELOG sendVercodeLog = new HPN_SENDVERCODELOG();
                sendVercodeLog.INPUTTIME = DateTime.Now;
                StringBuilder sb = new StringBuilder();
                foreach (string phone in req.phone)
                {
                    sb.AppendFormat(",{0}", phone);
                }
                sendVercodeLog.MOBILEPHONE = sb.Length > 0 ? sb.ToString().Substring(1) : sb.ToString();
                sendVercodeLog.FORMURL = formUrl;
                sendVercodeLog.STATUS = 0;
                sendVercodeLog.VERCODE = req.data.Value;
                sendVercodeLog.ID = Guid.NewGuid().ToString();
                _db.Set<HPN_SENDVERCODELOG>().Add(sendVercodeLog);
                _db.SaveChanges();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int GetMaxId(string table, string keyId = "ID")
        {
            return _db.Database.SqlQuery<int>(string.Format("select {0}_{1}.nextval from dual ", table, keyId)).FirstOrDefault();
        }

        public ActionResult getHistory(string userId,string templateName)
        {
            try
            {
                if(string.IsNullOrEmpty(templateName) || string.IsNullOrEmpty(userId))
                    return Json(new { Status = -1, Data = string.Empty, Msg = "参数错误1" }, JsonRequestBehavior.AllowGet);

                HR_CNR_USER user = _db.Set<HR_CNR_USER>().Where(p => p.USERID == userId).FirstOrDefault();
                if(user == null)
                    return Json(new { Status = -2, Data = string.Empty, Msg = "参数错误2" }, JsonRequestBehavior.AllowGet);

                /*if (string.IsNullOrEmpty(user.IDCARD))
                    return Json(new { Status = -3, Data = string.Empty, Msg = "参数错误3" }, JsonRequestBehavior.AllowGet);

                HPN_Users hpn_user = (from x in _db.Set<HPN_Users>() where x.IDCard == user.IDCARD select x).FirstOrDefault();
                if(hpn_user == null)
                    return Json(new { Status = -4, Data = string.Empty, Msg = "参数错误4" }, JsonRequestBehavior.AllowGet);*/

                HPN_TEMPLATE temp = templateService.getTemplateByName(templateName);
                if(temp == null)
                    return Json(new { Status = -5, Data = string.Empty, Msg = "参数错误5" }, JsonRequestBehavior.AllowGet);

                List<HPN_TESTRESULT> testResultList = (from x in _db.Set<HPN_TESTRESULT>()
                                                       where x.TEMPLATENAME == templateName && x.USERID.Equals(user.USERID)
                                                       select x).OrderBy(p => p.INPUTTIME).ToList();

                Chart chart = new Chart();
                chart.title = temp.TITLE;

                ChartData chartData = new ChartData();
                chartData.label = temp.TITLE;
                chartData.fillColor = "rgba(151,187,205,0.2)";
                chartData.strokeColor = "rgba(151,187,205,1)";
                chartData.pointColor = "rgba(151,187,205,1)";
                chartData.pointStrokeColor = "#fff";
                chartData.pointHighlightFill = "#fff";
                chartData.pointHighlightStroke = "rgba(151,187,205,1)";

                foreach (HPN_TESTRESULT item in testResultList)
                {
                    chart.labels.Add(item.INPUTTIME.ToString());
                    chartData.data.Add(item.RESULT.ToDouble(0));
                }
                chart.datasets.Add(chartData);

                return Json(new { Status = 1, Data = chart, Msg = "操作成功" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Status = -99, Data = string.Empty, Msg = ex.Message },JsonRequestBehavior.AllowGet);
            }
        }
	}
}