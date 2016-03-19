using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;

using Microsoft.Ajax.Utilities;

namespace KMHC.CTMS.UI.Controllers
{
    public class HealthManageController : BaseViewController
    {
        //
        // GET: /HealthManage/
        //public ActionResult HighBlood()
        //{
        //    return View();
        //}

        //public ActionResult ElderlyManageCardCreate()
        //{
        //    return View();
        //}

        //public ActionResult ReportShow(string IDNumber)
        //{
        //    if (!string.IsNullOrEmpty(IDNumber))
        //    {
        //        CreatePartialHtml(IDNumber);
        //        ViewBag.RenderData = Dicionary;
        //        return View();
        //    }
        //    else
        //    {
        //        return HttpNotFound();
        //    }
        //}

        //private Dictionary<string,string>_dicionary=new Dictionary<string, string>(); 
        //public Dictionary<string, string> Dicionary {
        //    get { return _dicionary; }
        //    set { _dicionary = value; }
        //}


        /// <summary>
        /// 将数据库中的每个数据，加工处理后转换成 字典 对应html内容 形式，方便前端绑定
        /// </summary>
        /// <param name="IDNumber"></param>
        ///[NonAction]
        //public void CreatePartialHtml(string IDNumber)
        //{

        //    //字典表
        //    var _dictionary = from it in _context.Dictionay select it;

        //    //个人信息
        //    var _userInfo = (from it in _context.HealthRecords where it.IDNumber == IDNumber select it).FirstOrDefault();

        //    //家族病史，疾病，手术，外伤，输血史
        //    var _history = (from it in _context.HealthHistoris where it.RecordId == _userInfo.ID select new { it.Code, it.Name, it.Type, it.StartDate, it.EndDate }).Distinct();
        //    var dic = new Dictionary<string, string>();
        //    var tempSb = new StringBuilder();
        //    if (_userInfo!=null)
        //    {
        //        foreach (var model in _dictionary)
        //        {
        //            switch (model.CodeId)
        //            {
        //                case "GB3304-1991":
        //                    dic.Add("Nationality", "<span>" + (from it in model.Options.Where(p => p.Value == _userInfo.Nationality) select it.Name).FirstOrDefault() + "</span>");
        //                    break;
        //                case "GBT2261.2-2003":
        //                    tempSb.Clear();
        //                    foreach (var item in model.Options)
        //                    {
        //                        if (item.Value == _userInfo.MarriageStatus)
        //                        {
        //                            tempSb.AppendFormat(
        //                            "<input type=\"checkbox\" name=\"{0}\" checked=\"checked\" disabled=\"disabled\" value=\"{1}\"/><span>{2}</span>",
        //                            "MarriageStatus", item.Value, item.Name);
        //                        }
        //                        else
        //                        {
        //                            tempSb.AppendFormat(
        //                            "<input type=\"checkbox\" name=\"{0}\" disabled=\"disabled\" value=\"{1}\"/><span>{2}</span>",
        //                            "MarriageStatus", item.Value, item.Name);
        //                        }

        //                    }
        //                    dic.Add("MarriageStatus", tempSb.ToString());
        //                    break;
        //                case "GBT2261.1-2003":
        //                    tempSb.Clear();
        //                    foreach (var item in model.Options)
        //                    {
        //                        if (item.Value == _userInfo.Gender)
        //                        {
        //                            tempSb.AppendFormat(
        //                            "<input type=\"checkbox\" name=\"{0}\" checked=\"checked\" disabled=\"disabled\" value=\"{1}\"/><span>{2}</span>",
        //                            "Gender", item.Value, item.Name);
        //                        }
        //                        else
        //                        {
        //                            tempSb.AppendFormat(
        //                            "<input type=\"checkbox\" name=\"{0}\" disabled=\"disabled\" value=\"{1}\"/><span>{2}</span>",
        //                            "Gender", item.Value, item.Name);
        //                        }

        //                    }
        //                    dic.Add("Gender", tempSb.ToString());
        //                    break;
        //                case "GBT4658-1984":
        //                    tempSb.Clear();

        //                    foreach (var item in model.Options)
        //                    {
        //                        if (item.Value == _userInfo.EducationLevel)
        //                        {
        //                            tempSb.AppendFormat(
        //                            "<input type=\"checkbox\" name=\"{0}\" checked=\"checked\" disabled=\"disabled\" value=\"{1}\"/><span>{2}</span>",
        //                            "EducationLevel", item.Value, item.Name);
        //                        }
        //                        else
        //                        {
        //                            tempSb.AppendFormat(
        //                            "<input type=\"checkbox\" name=\"{0}\" disabled=\"disabled\" value=\"{1}\"/><span>{2}</span>",
        //                            "EducationLevel", item.Value, item.Name);
        //                        }

        //                    }
        //                    dic.Add("EducationLevel", tempSb.ToString());
        //                    break;
        //                case "GBT6565-1999":
        //                    tempSb.Clear();
        //                    foreach (var item in model.Options)
        //                    {
        //                        if (item.Value == _userInfo.OccupationClass)
        //                        {
        //                            tempSb.AppendFormat(
        //                            "<input type=\"checkbox\" name=\"{0}\" checked=\"checked\" disabled=\"disabled\" value=\"{1}\"/><span>{2}</span>",
        //                            "OccupationClass", item.Value, item.Name);
        //                        }
        //                        else
        //                        {
        //                            tempSb.AppendFormat(
        //                            "<input type=\"checkbox\" name=\"{0}\" disabled=\"disabled\" value=\"{1}\"/><span>{2}</span>",
        //                            "OccupationClass", item.Value, item.Name);
        //                        }

        //                    }
        //                    dic.Add("OccupationClass", tempSb.ToString());
        //                    break;
        //                case "CV04.50.005":
        //                    tempSb.Clear();
        //                    foreach (var item in model.Options)
        //                    {
        //                        if (item.Value == _userInfo.ABOType)
        //                        {
        //                            tempSb.AppendFormat(
        //                            "<input type=\"checkbox\" name=\"{0}\" checked=\"checked\" disabled=\"disabled\" value=\"{1}\"/><span>{2}</span>",
        //                            "ABOType", item.Value, item.Name);
        //                        }
        //                        else
        //                        {
        //                            tempSb.AppendFormat(
        //                            "<input type=\"checkbox\" name=\"{0}\" disabled=\"disabled\" value=\"{1}\"/><span>{2}</span>",
        //                            "ABOType", item.Value, item.Name);
        //                        }

        //                    }
        //                    dic.Add("ABOType", tempSb.ToString());
        //                    break;
        //                case "CV05.01.038":
        //                    tempSb.Clear();
        //                    foreach (var item in model.Options)
        //                    {
        //                        if (item.Value == _userInfo.AllergyHistory)
        //                        {
        //                            tempSb.Append("<span>" + item.Name + "</span>    ");
        //                        }
        //                    }
        //                    if (tempSb.Length == 0)
        //                    {
        //                        dic.Add("AllergyHistory", "无过敏史");
        //                    }
        //                    else
        //                    {
        //                        dic.Add("AllergyHistory", tempSb.ToString());
        //                    }
        //                    break;
        //                case "CV03.00.301":
        //                    tempSb.Clear();
        //                    foreach (var item in model.Options)
        //                    {
        //                        if (item.Value == _userInfo.RiskFactors)
        //                        {
        //                            tempSb.Append("<span>" + item.Name + "</span>    ");
        //                        }
        //                    }
        //                    if (tempSb.Length == 0)
        //                    {
        //                        dic.Add("RiskFactors", "无暴露史");
        //                    }
        //                    else
        //                    {
        //                        dic.Add("RiskFactors", tempSb.ToString());
        //                    }
        //                    break;
        //                case "CV05.10.001":
        //                    tempSb.Clear();
        //                    foreach (var item in model.Options)
        //                    {
        //                        if (item.Value == _userInfo.DisabilityStatus)
        //                        {
        //                            tempSb.AppendFormat(
        //                            "<input type=\"checkbox\" name=\"{0}\" checked=\"checked\" disabled=\"disabled\" value=\"{1}\"/><span>{2}</span>",
        //                            "DisabilityStatus", item.Value, item.Name);
        //                        }
        //                        else
        //                        {
        //                            tempSb.AppendFormat(
        //                            "<input type=\"checkbox\" name=\"{0}\" disabled=\"disabled\" value=\"{1}\"/><span>{2}</span>",
        //                            "DisabilityStatus", item.Value, item.Name);
        //                        }
        //                    }
        //                    dic.Add("DisabilityStatus", tempSb.ToString());

        //                    break;
        //                case "DE04.50.010.00":
        //                    tempSb.Clear();
        //                    foreach (var item in model.Options)
        //                    {
        //                        if (item.Value == _userInfo.RHType)
        //                        {
        //                            tempSb.AppendFormat(
        //                            "<input type=\"checkbox\" name=\"{0}\" checked=\"checked\" disabled=\"disabled\" value=\"{1}\"/><span>{2}</span>",
        //                            "RHType", item.Value, item.Name);
        //                        }
        //                        else
        //                        {
        //                            tempSb.AppendFormat(
        //                            "<input type=\"checkbox\" name=\"{0}\" disabled=\"disabled\" value=\"{1}\"/><span>{2}</span>",
        //                            "RHType", item.Value, item.Name);
        //                        }

        //                    }
        //                    dic.Add("RHType", tempSb.ToString());
        //                    break;
        //                case "CV07.10.003":
        //                    tempSb.Clear();
        //                    foreach (var item in model.Options)
        //                    {
        //                        if (item.Value == _userInfo.PayMethod)
        //                        {
        //                            tempSb.AppendFormat(
        //                            "<input type=\"checkbox\" name=\"{0}\" checked=\"checked\" disabled=\"disabled\" value=\"{1}\"/><span>{2}</span>",
        //                            "PayMethod", item.Value, item.Name);
        //                        }
        //                        else
        //                        {
        //                            tempSb.AppendFormat(
        //                            "<input type=\"checkbox\" name=\"{0}\" disabled=\"disabled\" value=\"{1}\"/><span>{2}</span>",
        //                            "PayMethod", item.Value, item.Name);
        //                        }

        //                    }
        //                    dic.Add("PayMethod", tempSb.ToString());
        //                    break;
        //            }
        //        }

        //        dic.Add("Name", "<span>" + _userInfo.Name + "</span>");
        //        dic.Add("RecordNo", "<span>" + _userInfo.RecordNo + "</span>");
        //        dic.Add("BirthDate", "<span>" + _userInfo.BirthDate + "</span>");
        //        dic.Add("IDNumber", "<span>" + _userInfo.IDNumber + "</span>");
        //        dic.Add("Company", "<span>" + _userInfo.Company + "</span>");
        //        dic.Add("Phone", "<span>" + _userInfo.Phone + "</span>");
        //        dic.Add("ContactName", "<span>" + _userInfo.ContactName + "</span>");
        //        dic.Add("ContactPhone", "<span>" + _userInfo.ContactPhone + "</span>");
        //        dic.Add("Disease","");
        //        dic.Add("Operation", "");
        //        dic.Add("OuterInjuery", "");
        //        dic.Add("BloodTrans", "");
        //        dic.Add("Father", "");
        //        dic.Add("Mother", "");
        //        dic.Add("Sibling", "");
        //        dic.Add("Children", "");
        //        var tempName = string.Empty;
        //        foreach (var item in _history)
        //        {
        //            switch (item.Type)
        //            {
        //                case "1":  tempName = "Disease";break;
        //                case "2": tempName = "Operation"; break;
        //                case "3": tempName = "OuterInjuery"; break;
        //                case "4": tempName = "BloodTrans"; break;
        //                case "11": tempName = "Father"; break;
        //                case "12": tempName = "Mother"; break;
        //                case "13": tempName = "Sibling"; break;
        //                case "14": tempName = "Children"; break;
        //            }
        //            dic[tempName] += ("<span>" + item.Name + "</span>    ");
        //        }
        //    }
        //    Dicionary=dic;
        //}


        /// <summary>
        ///生成PDF 
        /// </summary>
        /// <param name="IDNumber">身份证号</param>
        /// <returns></returns>
        
        //public FileStreamResult PdFileContentResult(string IDNumber)
        //{

        //    try
        //    {
        //        StringBuilder sb = new StringBuilder();
        //        Regex regex = new Regex("@Html.Raw\\(ViewBag.RenderData\\[\"(\\w+)\"\\]\\)");
        //        var fileName = DateTime.Now.ToString("yyyymmddhhmmss") + ".pdf";

        //        CreatePartialHtml(IDNumber);

        //        var memoryStream = new MemoryStream();

        //        //直接使用View中的cshtml视图模板，让该视图能够在html展示和pdf转换中都能使用
        //        using (StreamReader reader = new StreamReader(Server.MapPath("~/Views/HealthManage/Reportshow.cshtml")))
        //        {
        //            string strTemp = reader.ReadLine();
        //            while (!reader.EndOfStream)
        //            {   //正则表达式的长度刚好33,如果长度小于33表示改行根本没有正则匹配的必要
        //                if (strTemp.Length >= 33)
        //                {
        //                    sb.Append(regex.Replace(strTemp, Replace));
        //                }
        //                else
        //                {
        //                    sb.Append(strTemp);
        //                }

        //                strTemp = reader.ReadLine();
        //            }
        //        }

        //        PdfConvert.ConvertHtmlToPdf(new PdfDocument
        //        {
        //            Url = "-",
        //            Html = sb.ToString()
        //        }, new PdfOutput
        //        {
        //            OutputStream = memoryStream
        //        });
        //        memoryStream.Position = 0;
        //        return File(memoryStream, "application/octet-stream", Server.UrlEncode(fileName));
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        /// <summary>
        /// 正则匹配后，执行替换函数
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        //public string Replace(Match match)
        //{
        //    var group = match.Groups;
        //    return Dicionary[group[1].Value];
        //}
    }
}
