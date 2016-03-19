using System;
using System.Collections.Generic;
using System.Data.Entity;
using KMHC.CTMS.Model.Repository.Interface;
using KMHC.CTMS.Common.Helper;
using System.Linq;
using KMHC.CTMS.DAL.Database;
using System.Text;

namespace KMHC.CTMS.Model.Repository.CancerRecord
{
    public class PsqiTemplate : Template
    {
        public override string getTemplateName()
        {
            return "Psqi";
        }

        public override double calculateResult(HPN_TESTRESULT testResult, List<HPN_TESTRESULTDETAILS> testDetails)
        {
            //计算分数，并且把结果保存到HPN_TestResult的Remark字段，本函数返回分数
            double score = 0, subscore = 0;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string testNo = testDetails.First().TESTNO;

            StringBuilder sb = new StringBuilder("<div class='panel-group' id='Scl' style='margin-bottom:2rem;'>");

            #region A睡眠质量
            HPN_TESTRESULTDETAILS testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "psqi_6");
            subscore = testResultDetail.ITEMRESULT.ToDouble(0);
            score += subscore;
            sb.Append("<div class='panel panel-default'>");
            sb.Append("<div class='panel-heading'>");
            sb.Append("<h4 class='panel-title'>");
            sb.Append("<a data-toggle='collapse' style='text-decoration:none; width: 100 %; display: block; '>");
            sb.AppendFormat("{0}<span class='pull-right'>{1}</span>", "睡眠质量", subscore + "分");
            sb.Append("</a>");
            sb.Append("</h4>");
            sb.Append("</div>");
            sb.Append("</div>");
            subscore = 0;
            #endregion

            #region B入睡时间
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "psqi_2");
            double d = testResultDetail.ITEMRESULT.ToDouble(0);
            if (d >= 16 && d <= 30)
                subscore += 1;
            else if (d >= 31 && d < 60)
                subscore += 2;
            else if (d >= 60)
                subscore += 3;
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "psqi_5_a");
            subscore += testResultDetail.ITEMRESULT.ToDouble(0);
            if (subscore >= 1 && subscore <= 2)
                subscore = 1;
            else if (subscore >= 3 && subscore <= 4)
                subscore = 2;
            else if (subscore >= 5)
                subscore = 3;
            score += subscore;
            sb.Append("<div class='panel panel-default'>");
            sb.Append("<div class='panel-heading'>");
            sb.Append("<h4 class='panel-title'>");
            sb.Append("<a data-toggle='collapse' style='text-decoration:none; width: 100 %; display: block; '>");
            sb.AppendFormat("{0}<span class='pull-right'>{1}</span>", "入睡时间", subscore + "分");
            sb.Append("</a>");
            sb.Append("</h4>");
            sb.Append("</div>");
            sb.Append("</div>");
            subscore = 0;
            #endregion

            #region C睡眠时间
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "psqi_4");
            d = testResultDetail.ITEMRESULT.ToDouble(0);
            if (d < 5)
                subscore = 3;
            else if (d >= 5 && d < 6)
                subscore = 2;
            else if (d >= 6 && d < 7)
                subscore = 1;
            score += subscore;
            sb.Append("<div class='panel panel-default'>");
            sb.Append("<div class='panel-heading'>");
            sb.Append("<h4 class='panel-title'>");
            sb.Append("<a data-toggle='collapse' style='text-decoration:none; width: 100 %; display: block; '>");
            sb.AppendFormat("{0}<span class='pull-right'>{1}</span>", "睡眠时间", subscore + "分");
            sb.Append("</a>");
            sb.Append("</h4>");
            sb.Append("</div>");
            sb.Append("</div>");
            subscore = 0;
            #endregion

            #region D睡眠效率
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "psqi_3");
            HPN_TESTRESULTDETAILS tempResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "psqi_1");
            double time1 = testResultDetail.ITEMRESULT.ToDouble(0) + 24 - tempResultDetail.ITEMRESULT.ToDouble(0);
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "psqi_4");
            double persent = testResultDetail.ITEMRESULT.ToDouble(0) / time1 * 100;
            if (persent < 65)
                subscore = 3;
            else if (persent >= 65 && persent < 75)
                subscore = 2;
            else if (persent >= 75 && persent < 85)
                subscore = 1;
            score += subscore;
            sb.Append("<div class='panel panel-default'>");
            sb.Append("<div class='panel-heading'>");
            sb.Append("<h4 class='panel-title'>");
            sb.Append("<a data-toggle='collapse' style='text-decoration:none; width: 100 %; display: block; '>");
            sb.AppendFormat("{0}<span class='pull-right'>{1}</span>", "睡眠效率", subscore + "分");
            sb.Append("</a>");
            sb.Append("</h4>");
            sb.Append("</div>");
            sb.Append("</div>");
            subscore = 0;
            #endregion

            #region E睡眠障碍
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "psqi_5_b");
            subscore += testResultDetail.ITEMRESULT.ToDouble(0);
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "psqi_5_c");
            subscore += testResultDetail.ITEMRESULT.ToDouble(0);
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "psqi_5_d");
            subscore += testResultDetail.ITEMRESULT.ToDouble(0);
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "psqi_5_e");
            subscore += testResultDetail.ITEMRESULT.ToDouble(0);
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "psqi_5_f");
            subscore += testResultDetail.ITEMRESULT.ToDouble(0);
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "psqi_5_g");
            subscore += testResultDetail.ITEMRESULT.ToDouble(0);
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "psqi_5_h");
            subscore += testResultDetail.ITEMRESULT.ToDouble(0);
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "psqi_5_i");
            subscore += testResultDetail.ITEMRESULT.ToDouble(0);
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "psqi_5_j");
            subscore += testResultDetail.ITEMRESULT.ToDouble(0);
            if (subscore >= 19)
                subscore = 3;
            else if (subscore >= 10 && subscore < 19)
                subscore = 2;
            else if (subscore >= 1 && subscore < 10)
                subscore = 1;
            score += subscore;
            sb.Append("<div class='panel panel-default'>");
            sb.Append("<div class='panel-heading'>");
            sb.Append("<h4 class='panel-title'>");
            sb.Append("<a data-toggle='collapse' style='text-decoration:none; width: 100 %; display: block; '>");
            sb.AppendFormat("{0}<span class='pull-right'>{1}</span>", "睡眠障碍", subscore + "分");
            sb.Append("</a>");
            sb.Append("</h4>");
            sb.Append("</div>");
            sb.Append("</div>");
            subscore = 0;
            #endregion

            #region F催眠药物
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "psqi_7");
            subscore = testResultDetail.ITEMRESULT.ToDouble(0);
            score += subscore;
            sb.Append("<div class='panel panel-default'>");
            sb.Append("<div class='panel-heading'>");
            sb.Append("<h4 class='panel-title'>");
            sb.Append("<a data-toggle='collapse' style='text-decoration:none; width: 100 %; display: block; '>");
            sb.AppendFormat("{0}<span class='pull-right'>{1}</span>", "催眠药物", subscore + "分");
            sb.Append("</a>");
            sb.Append("</h4>");
            sb.Append("</div>");
            sb.Append("</div>");
            subscore = 0;
            #endregion

            #region G日间功能障碍
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "psqi_8");
            subscore = testResultDetail.ITEMRESULT.ToDouble(0);
            testResultDetail = testDetails.Find(p => p.TEMPLATEITEMNAME == "psqi_9");
            subscore = testResultDetail.ITEMRESULT.ToDouble(0);
            if (subscore >= 5)
                subscore = 3;
            else if (subscore >= 3 && subscore <= 4)
                subscore = 2;
            else if (subscore >= 1 && subscore <= 2)
                subscore = 1;
            score += subscore;
            sb.Append("<div class='panel panel-default'>");
            sb.Append("<div class='panel-heading'>");
            sb.Append("<h4 class='panel-title'>");
            sb.Append("<a data-toggle='collapse' style='text-decoration:none; width: 100 %; display: block; '>");
            sb.AppendFormat("{0}<span class='pull-right'>{1}</span>", "日间功能障碍", subscore + "分");
            sb.Append("</a>");
            sb.Append("</h4>");
            sb.Append("</div>");
            sb.Append("</div>");
            subscore = 0;
            #endregion

            sb.Append("</div>");

            testResult.RESULTDETAIL = string.Format("测试结果为：{0}分", score);
            testResult.TESTRESULT = sb.ToString();
            return score;

        }
    }
}
