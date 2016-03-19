using System;
using System.Collections.Generic;
using KMHC.CTMS.Model.Repository.Interface;
using System.Linq;
using KMHC.CTMS.Common.Helper;
using System.Data.Entity;
using KMHC.CTMS.DAL.Database;

namespace KMHC.CTMS.Model.Repository.CancerRecord
{
    public class MnaTemplate : Template
    {
        public override string getTemplateName()
        {
            return "Mna";
        }

        public override double calculateResult(HPN_TESTRESULT testResult, List<HPN_TESTRESULTDETAILS> testDetails)
        {
            double weight = testDetails.Find(p => p.TEMPLATEITEMNAME == "weight").ITEMRESULT.ToDouble(0);
            double height = testDetails.Find(p => p.TEMPLATEITEMNAME == "height").ITEMRESULT.ToDouble(0) / 100;
            string testNo = testDetails.First().TESTNO;
            double bmi = weight / height / height;
            double score = 0;
            if (bmi >= 19 && bmi < 21)
                score += 1;
            else if (bmi >= 21 && bmi < 23)
                score += 2;
            else if (bmi >= 23)
                score += 3;
            testDetails = testDetails.FindAll(p => p.TEMPLATEITEMNAME.Contains("_"));
            foreach (HPN_TESTRESULTDETAILS item in testDetails)
            {
                score += item.ITEMRESULT.ToDouble(0);
            }
            testResult.RESULTDETAIL = string.Format("测试结果为：{0}分", score);
            return score;
        }
    }
}
