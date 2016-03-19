using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using KMHC.CTMS.Model.Repository.Interface;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.Model.Repository.Implement.CancerRecord;
using System.Text;

namespace KMHC.CTMS.Model.Repository.CancerRecord
{
    public class SclTemplate : Template
    {
        IHPNRepository templateService = new HPNRepository();

        public override string getTemplateName()
        {
            return "Scl";
        }

        public override double calculateResult(HPN_TESTRESULT testResult, List<HPN_TESTRESULTDETAILS> testDetails)
        {
            double score = 0, subscore = 0;
            List<int> positiveList = new List<int>();
            StringBuilder sb = new StringBuilder("<div class='panel-group' id='Scl' style='margin:2rem 0;'>");

            #region 躯体化因子
            HPN_TESTRESULTDETAILS testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_1");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_4");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_12");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_27");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_40");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_42");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_48");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_49");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_52");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_53");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_56");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_58");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            score += subscore;
            HPN_RESULTDESC subResult = templateService.getResultDescByScore("Scl_1", subscore).FirstOrDefault();
            if (subResult != null)
            {
                if (string.IsNullOrEmpty(subResult.RESULT))
                {
                    //有详情
                    sb.Append("<div class='panel panel-default'>");
                    sb.Append("<div class='panel-heading'>");
                    sb.Append("<h4 class='panel-title'>");
                    sb.Append("<a data-toggle='collapse' data-parent='#Scl' href='#Scl_1' style='text-decoration:none;width:100%;display:block;' onclick='$(this).parent().parent().parent().siblings(\"div\").children(\"div\").children(\"h4\").children(\"a\").children(\"span\").removeClass(\"glyphicon-minus\");$(this).children(\"span\").toggleClass(\"glyphicon-minus\");'>");
                    sb.AppendFormat("{0}<span class='pull-right glyphicon glyphicon-plus'></span>", subResult.SUBRESULTNAME);
                    sb.Append("</a>");
                    sb.Append("</h4>");
                    sb.Append("</div>");
                    sb.Append("<div id='Scl_1' class='panel-collapse collapse'>");
                    sb.Append("<div class='panel-body'>");
                    sb.AppendFormat("{0}", subResult.RESULTDETAIL);
                    sb.Append("</div>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                }
                else
                {
                    sb.Append("<div class='panel panel-default'>");
                    sb.Append("<div class='panel-heading'>");
                    sb.Append("<h4 class='panel-title'>");
                    sb.Append("<a data-toggle='collapse' style='text-decoration:none; width: 100 %; display: block; '>");
                    sb.AppendFormat("{0}<span class='pull-right'>{1}</span>", subResult.SUBRESULTNAME, subResult.RESULT);
                    sb.Append("</a>");
                    sb.Append("</h4>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                }
            }
            subscore = 0;
            #endregion

            #region 强迫症状
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_3");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_9");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_10");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_28");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_38");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_45");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_46");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_51");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_55");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_65");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            score += subscore;
            subResult = templateService.getResultDescByScore("Scl_2", subscore).FirstOrDefault();
            if (subResult != null)
            {
                if (string.IsNullOrEmpty(subResult.RESULT))
                {
                    //有详情
                    sb.Append("<div class='panel panel-default'>");
                    sb.Append("<div class='panel-heading'>");
                    sb.Append("<h4 class='panel-title'>");
                    sb.Append("<a data-toggle='collapse' data-parent='#Scl' href='#Scl_2' style='text-decoration:none;width:100%;display:block;' onclick='$(this).parent().parent().parent().siblings(\"div\").children(\"div\").children(\"h4\").children(\"a\").children(\"span\").removeClass(\"glyphicon-minus\");$(this).children(\"span\").toggleClass(\"glyphicon-minus\");'>");
                    sb.AppendFormat("{0}<span class='pull-right glyphicon glyphicon-plus'></span>", subResult.SUBRESULTNAME);
                    sb.Append("</a>");
                    sb.Append("</h4>");
                    sb.Append("</div>");
                    sb.Append("<div id='Scl_2' class='panel-collapse collapse'>");
                    sb.Append("<div class='panel-body'>");
                    sb.AppendFormat("{0}", subResult.RESULTDETAIL);
                    sb.Append("</div>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                }
                else
                {
                    sb.Append("<div class='panel panel-default'>");
                    sb.Append("<div class='panel-heading'>");
                    sb.Append("<h4 class='panel-title'>");
                    sb.Append("<a data-toggle='collapse' style='text-decoration:none; width: 100 %; display: block; '>");
                    sb.AppendFormat("{0}<span class='pull-right'>{1}</span>", subResult.SUBRESULTNAME, subResult.RESULT);
                    sb.Append("</a>");
                    sb.Append("</h4>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                }
            }
            subscore = 0;
            #endregion

            #region 人际关系敏感
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_6");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_21");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_34");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_36");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_37");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_41");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_61");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_69");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_73");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            score += subscore;
            subResult = templateService.getResultDescByScore("Scl_3", subscore).FirstOrDefault();
            if (subResult != null)
            {
                if (string.IsNullOrEmpty(subResult.RESULT))
                {
                    //有详情
                    sb.Append("<div class='panel panel-default'>");
                    sb.Append("<div class='panel-heading'>");
                    sb.Append("<h4 class='panel-title'>");
                    sb.Append("<a data-toggle='collapse' data-parent='#Scl' href='#Scl_3' style='text-decoration:none;width:100%;display:block;' onclick='$(this).parent().parent().parent().siblings(\"div\").children(\"div\").children(\"h4\").children(\"a\").children(\"span\").removeClass(\"glyphicon-minus\");$(this).children(\"span\").toggleClass(\"glyphicon-minus\");'>");
                    sb.AppendFormat("{0}<span class='pull-right glyphicon glyphicon-plus'></span>", subResult.SUBRESULTNAME);
                    sb.Append("</a>");
                    sb.Append("</h4>");
                    sb.Append("</div>");
                    sb.Append("<div id='Scl_3' class='panel-collapse collapse'>");
                    sb.Append("<div class='panel-body'>");
                    sb.AppendFormat("{0}", subResult.RESULTDETAIL);
                    sb.Append("</div>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                }
                else
                {
                    sb.Append("<div class='panel panel-default'>");
                    sb.Append("<div class='panel-heading'>");
                    sb.Append("<h4 class='panel-title'>");
                    sb.Append("<a data-toggle='collapse' style='text-decoration:none; width: 100 %; display: block; '>");
                    sb.AppendFormat("{0}<span class='pull-right'>{1}</span>", subResult.SUBRESULTNAME, subResult.RESULT);
                    sb.Append("</a>");
                    sb.Append("</h4>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                }
            }
            subscore = 0;
            #endregion

            #region 抑郁
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_5");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_14");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_15");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_20");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_22");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_26");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_29");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_30");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_31");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_32");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_54");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_71");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_79");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            score += subscore;
            subResult = templateService.getResultDescByScore("Scl_4", subscore).FirstOrDefault();
            if (subResult != null)
            {
                if (string.IsNullOrEmpty(subResult.RESULT))
                {
                    //有详情
                    sb.Append("<div class='panel panel-default'>");
                    sb.Append("<div class='panel-heading'>");
                    sb.Append("<h4 class='panel-title'>");
                    sb.Append("<a data-toggle='collapse' data-parent='#Scl' href='#Scl_4' style='text-decoration:none;width:100%;display:block;' onclick='$(this).parent().parent().parent().siblings(\"div\").children(\"div\").children(\"h4\").children(\"a\").children(\"span\").removeClass(\"glyphicon-minus\");$(this).children(\"span\").toggleClass(\"glyphicon-minus\");'>");
                    sb.AppendFormat("{0}<span class='pull-right glyphicon glyphicon-plus'></span>", subResult.SUBRESULTNAME);
                    sb.Append("</a>");
                    sb.Append("</h4>");
                    sb.Append("</div>");
                    sb.Append("<div id='Scl_4' class='panel-collapse collapse'>");
                    sb.Append("<div class='panel-body'>");
                    sb.AppendFormat("{0}", subResult.RESULTDETAIL);
                    sb.Append("</div>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                }
                else
                {
                    sb.Append("<div class='panel panel-default'>");
                    sb.Append("<div class='panel-heading'>");
                    sb.Append("<h4 class='panel-title'>");
                    sb.Append("<a data-toggle='collapse' style='text-decoration:none; width: 100 %; display: block; '>");
                    sb.AppendFormat("{0}<span class='pull-right'>{1}</span>", subResult.SUBRESULTNAME, subResult.RESULT);
                    sb.Append("</a>");
                    sb.Append("</h4>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                }
            }
            subscore = 0;
            #endregion

            #region 焦虑
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_2");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_17");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_23");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_33");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_39");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_57");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_72");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_78");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_80");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_86");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            score += subscore;
            subResult = templateService.getResultDescByScore("Scl_5", subscore).FirstOrDefault();
            if (subResult != null)
            {
                if (string.IsNullOrEmpty(subResult.RESULT))
                {
                    //有详情
                    sb.Append("<div class='panel panel-default'>");
                    sb.Append("<div class='panel-heading'>");
                    sb.Append("<h4 class='panel-title'>");
                    sb.Append("<a data-toggle='collapse' data-parent='#Scl' href='#Scl_5' style='text-decoration:none;width:100%;display:block;' onclick='$(this).parent().parent().parent().siblings(\"div\").children(\"div\").children(\"h4\").children(\"a\").children(\"span\").removeClass(\"glyphicon-minus\");$(this).children(\"span\").toggleClass(\"glyphicon-minus\");'>");
                    sb.AppendFormat("{0}<span class='pull-right glyphicon glyphicon-plus'></span>", subResult.SUBRESULTNAME);
                    sb.Append("</a>");
                    sb.Append("</h4>");
                    sb.Append("</div>");
                    sb.Append("<div id='Scl_5' class='panel-collapse collapse'>");
                    sb.Append("<div class='panel-body'>");
                    sb.AppendFormat("{0}", subResult.RESULTDETAIL);
                    sb.Append("</div>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                }
                else
                {
                    sb.Append("<div class='panel panel-default'>");
                    sb.Append("<div class='panel-heading'>");
                    sb.Append("<h4 class='panel-title'>");
                    sb.Append("<a data-toggle='collapse' style='text-decoration:none; width: 100 %; display: block; '>");
                    sb.AppendFormat("{0}<span class='pull-right'>{1}</span>", subResult.SUBRESULTNAME, subResult.RESULT);
                    sb.Append("</a>");
                    sb.Append("</h4>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                }
            }
            subscore = 0;
            #endregion

            #region 敌对
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_11");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_24");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_63");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_67");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_74");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_81");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            score += subscore;
            subResult = templateService.getResultDescByScore("Scl_6", subscore).FirstOrDefault();
            if (subResult != null)
            {
                if (string.IsNullOrEmpty(subResult.RESULT))
                {
                    //有详情
                    sb.Append("<div class='panel panel-default'>");
                    sb.Append("<div class='panel-heading'>");
                    sb.Append("<h4 class='panel-title'>");
                    sb.Append("<a data-toggle='collapse' data-parent='#Scl' href='#Scl_6' style='text-decoration:none;width:100%;display:block;' onclick='$(this).parent().parent().parent().siblings(\"div\").children(\"div\").children(\"h4\").children(\"a\").children(\"span\").removeClass(\"glyphicon-minus\");$(this).children(\"span\").toggleClass(\"glyphicon-minus\");'>");
                    sb.AppendFormat("{0}<span class='pull-right glyphicon glyphicon-plus'></span>", subResult.SUBRESULTNAME);
                    sb.Append("</a>");
                    sb.Append("</h4>");
                    sb.Append("</div>");
                    sb.Append("<div id='Scl_6' class='panel-collapse collapse'>");
                    sb.Append("<div class='panel-body'>");
                    sb.AppendFormat("{0}", subResult.RESULTDETAIL);
                    sb.Append("</div>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                }
                else
                {
                    sb.Append("<div class='panel panel-default'>");
                    sb.Append("<div class='panel-heading'>");
                    sb.Append("<h4 class='panel-title'>");
                    sb.Append("<a data-toggle='collapse' style='text-decoration:none; width: 100 %; display: block; '>");
                    sb.AppendFormat("{0}<span class='pull-right'>{1}</span>", subResult.SUBRESULTNAME, subResult.RESULT);
                    sb.Append("</a>");
                    sb.Append("</h4>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                }
            }
            subscore = 0;
            #endregion

            #region 恐怖
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_13");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_25");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_47");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_50");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_70");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_75");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_82");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            score += subscore;
            subResult = templateService.getResultDescByScore("Scl_7", subscore).FirstOrDefault();
            if (subResult != null)
            {
                if (string.IsNullOrEmpty(subResult.RESULT))
                {
                    //有详情
                    sb.Append("<div class='panel panel-default'>");
                    sb.Append("<div class='panel-heading'>");
                    sb.Append("<h4 class='panel-title'>");
                    sb.Append("<a data-toggle='collapse' data-parent='#Scl' href='#Scl_7' style='text-decoration:none;width:100%;display:block;' onclick='$(this).parent().parent().parent().siblings(\"div\").children(\"div\").children(\"h4\").children(\"a\").children(\"span\").removeClass(\"glyphicon-minus\");$(this).children(\"span\").toggleClass(\"glyphicon-minus\");'>");
                    sb.AppendFormat("{0}<span class='pull-right glyphicon glyphicon-plus'></span>", subResult.SUBRESULTNAME);
                    sb.Append("</a>");
                    sb.Append("</h4>");
                    sb.Append("</div>");
                    sb.Append("<div id='Scl_7' class='panel-collapse collapse'>");
                    sb.Append("<div class='panel-body'>");
                    sb.AppendFormat("{0}", subResult.RESULTDETAIL);
                    sb.Append("</div>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                }
                else
                {
                    sb.Append("<div class='panel panel-default'>");
                    sb.Append("<div class='panel-heading'>");
                    sb.Append("<h4 class='panel-title'>");
                    sb.Append("<a data-toggle='collapse' style='text-decoration:none; width: 100 %; display: block; '>");
                    sb.AppendFormat("{0}<span class='pull-right'>{1}</span>", subResult.SUBRESULTNAME, subResult.RESULT);
                    sb.Append("</a>");
                    sb.Append("</h4>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                }
            }
            subscore = 0;
            #endregion

            #region 偏执
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_8");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_18");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_43");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_68");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_76");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_83");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            score += subscore;
            subResult = templateService.getResultDescByScore("Scl_8", subscore).FirstOrDefault();
            if (subResult != null)
            {
                if (string.IsNullOrEmpty(subResult.RESULT))
                {
                    //有详情
                    sb.Append("<div class='panel panel-default'>");
                    sb.Append("<div class='panel-heading'>");
                    sb.Append("<h4 class='panel-title'>");
                    sb.Append("<a data-toggle='collapse' data-parent='#Scl' href='#Scl_8' style='text-decoration:none;width:100%;display:block;' onclick='$(this).parent().parent().parent().siblings(\"div\").children(\"div\").children(\"h4\").children(\"a\").children(\"span\").removeClass(\"glyphicon-minus\");$(this).children(\"span\").toggleClass(\"glyphicon-minus\");'>");
                    sb.AppendFormat("{0}<span class='pull-right glyphicon glyphicon-plus'></span>", subResult.SUBRESULTNAME);
                    sb.Append("</a>");
                    sb.Append("</h4>");
                    sb.Append("</div>");
                    sb.Append("<div id='Scl_8' class='panel-collapse collapse'>");
                    sb.Append("<div class='panel-body'>");
                    sb.AppendFormat("{0}", subResult.RESULTDETAIL);
                    sb.Append("</div>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                }
                else
                {
                    sb.Append("<div class='panel panel-default'>");
                    sb.Append("<div class='panel-heading'>");
                    sb.Append("<h4 class='panel-title'>");
                    sb.Append("<a data-toggle='collapse' style='text-decoration:none; width: 100 %; display: block; '>");
                    sb.AppendFormat("{0}<span class='pull-right'>{1}</span>", subResult.SUBRESULTNAME, subResult.RESULT);
                    sb.Append("</a>");
                    sb.Append("</h4>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                }
            }
            subscore = 0;
            #endregion

            #region 精神病性
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_7");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_16");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_35");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_62");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_77");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_84");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_85");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_87");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_88");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_90");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            score += subscore;
            subResult = templateService.getResultDescByScore("Scl_9", subscore).FirstOrDefault();
            if (subResult != null)
            {
                if (string.IsNullOrEmpty(subResult.RESULT))
                {
                    //有详情
                    sb.Append("<div class='panel panel-default'>");
                    sb.Append("<div class='panel-heading'>");
                    sb.Append("<h4 class='panel-title'>");
                    sb.Append("<a data-toggle='collapse' data-parent='#Scl' href='#Scl_9' style='text-decoration:none;width:100%;display:block;' onclick='$(this).parent().parent().parent().siblings(\"div\").children(\"div\").children(\"h4\").children(\"a\").children(\"span\").removeClass(\"glyphicon-minus\");$(this).children(\"span\").toggleClass(\"glyphicon-minus\");'>");
                    sb.AppendFormat("{0}<span class='pull-right glyphicon glyphicon-plus'></span>", subResult.SUBRESULTNAME);
                    sb.Append("</a>");
                    sb.Append("</h4>");
                    sb.Append("</div>");
                    sb.Append("<div id='Scl_9' class='panel-collapse collapse'>");
                    sb.Append("<div class='panel-body'>");
                    sb.AppendFormat("{0}", subResult.RESULTDETAIL);
                    sb.Append("</div>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                }
                else
                {
                    sb.Append("<div class='panel panel-default'>");
                    sb.Append("<div class='panel-heading'>");
                    sb.Append("<h4 class='panel-title'>");
                    sb.Append("<a data-toggle='collapse' style='text-decoration:none; width: 100 %; display: block; '>");
                    sb.AppendFormat("{0}<span class='pull-right'>{1}</span>", subResult.SUBRESULTNAME, subResult.RESULT);
                    sb.Append("</a>");
                    sb.Append("</h4>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                }
            }
            subscore = 0;
            #endregion

            #region 其他
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_19");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_44");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_59");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_60");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_64");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_66");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "Scl_89");
            subscore += testResultDetail.ITEMRESULT.ToInt(0);
            if (testResultDetail.ITEMRESULT.ToInt(0) >= 2)
                positiveList.Add(testResultDetail.ITEMRESULT.ToInt(0));
            score += subscore;
            subResult = templateService.getResultDescByScore("Scl_10", subscore).FirstOrDefault();
            if (subResult != null)
            {
                if (string.IsNullOrEmpty(subResult.RESULT))
                {
                    //有详情
                    sb.Append("<div class='panel panel-default'>");
                    sb.Append("<div class='panel-heading'>");
                    sb.Append("<h4 class='panel-title'>");
                    sb.Append("<a data-toggle='collapse' data-parent='#Scl' href='#Scl_10' style='text-decoration:none;width:100%;display:block;' onclick='$(this).parent().parent().parent().siblings(\"div\").children(\"div\").children(\"h4\").children(\"a\").children(\"span\").removeClass(\"glyphicon-minus\");$(this).children(\"span\").toggleClass(\"glyphicon-minus\");'>");
                    sb.AppendFormat("{0}<span class='pull-right glyphicon glyphicon-plus'></span>", subResult.SUBRESULTNAME);
                    sb.Append("</a>");
                    sb.Append("</h4>");
                    sb.Append("</div>");
                    sb.Append("<div id='Scl_10' class='panel-collapse collapse'>");
                    sb.Append("<div class='panel-body'>");
                    sb.AppendFormat("{0}", subResult.RESULTDETAIL);
                    sb.Append("</div>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                }
                else
                {
                    sb.Append("<div class='panel panel-default'>");
                    sb.Append("<div class='panel-heading'>");
                    sb.Append("<h4 class='panel-title'>");
                    sb.Append("<a data-toggle='collapse' style='text-decoration:none; width: 100 %; display: block; '>");
                    sb.AppendFormat("{0}<span class='pull-right'>{1}</span>", subResult.SUBRESULTNAME, subResult.RESULT);
                    sb.Append("</a>");
                    sb.Append("</h4>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                }
            }
            subscore = 0;
            #endregion

            sb.Append("</div>");

            string remark2 = string.Empty;
            remark2 = string.Format("阳性项目数<span class='pull-right' style='margin-right:4px'>{0}</span><br/>项目总得分<span class='pull-right' style='margin-right:4px'>{1}分</span><br/>总平均分<span class='pull-right' style='margin-right:4px'>{2:N2}分</span><br/><br/>", positiveList.Count, score, score / 90);
            testResult.RESULTDETAIL = remark2;
            testResult.TESTRESULT = sb.ToString();

            return score / 90;
        }
    }
}
