using System;
using System.Collections.Generic;
using System.Data.Entity;
using KMHC.CTMS.Model.Repository.Interface;
using System.Linq;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.DAL.Database;

namespace KMHC.CTMS.Model.Repository.CancerRecord
{
    public class SgaTemplate : Template
    {
        public override string getTemplateName()
        {
            return "Sga";
        }

        public override double calculateResult(HPN_TESTRESULT testResult, List<HPN_TESTRESULTDETAILS> testDetails)
        {
            int score = 0, sum1 = 0, sum2 = 0, sum3 = 0;
            sum1 = testDetails.FindAll(p => p.ITEMRESULT == "0").Count;
            sum2 = testDetails.FindAll(p => p.ITEMRESULT == "1").Count;
            sum3 = testDetails.FindAll(p => p.ITEMRESULT == "2").Count;
            string testNo = testDetails.First().TESTNO;

            if (sum1 > sum2 && sum1 > sum3)
                score = 0;
            else if (sum2 > sum1 && sum2 > sum3)
                score = 1;
            else if (sum3 > sum1 && sum3 > sum2)
                score = 2;

            testResult.RESULTDETAIL = string.Format("测试结果为：{0}分", score);
            return score;
        }
    }
}
