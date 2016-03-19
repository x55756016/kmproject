using System;
using System.Collections.Generic;
using System.Data.Entity;
using KMHC.CTMS.Model.Repository.Interface;
using KMHC.CTMS.DAL.Database;
using System.Linq;
using KMHC.CTMS.Common.Helper;

namespace KMHC.CTMS.Model.Repository.CancerRecord
{
    public class MustTemplate : Template
    {
        public override string getTemplateName()
        {
            return "Must";
        }

        public override double calculateResult(HPN_TESTRESULT testResult, List<HPN_TESTRESULTDETAILS> testDetails)
        {
            double height = testDetails.Find(p => p.TEMPLATEITEMNAME == "height").ITEMRESULT.ToDouble(0) / 100;
            double weight = testDetails.Find(p => p.TEMPLATEITEMNAME == "weight").ITEMRESULT.ToDouble(0);
            double loseweight = testDetails.Find(p => p.TEMPLATEITEMNAME == "loseweight").ITEMRESULT.ToDouble(0);
            double disease = testDetails.Find(p => p.TEMPLATEITEMNAME == "disease").ITEMRESULT.ToDouble(0);
            string testNo = testDetails.First().TESTNO;
            double bmi = weight / height / height;
            double weightlosepersent = loseweight / (weight + loseweight);
            double score = 0;
            if (bmi < 18.5)
                score += 2;
            else if (bmi < 20)
                score += 1;
            if (weightlosepersent >= 0.05 && weightlosepersent <= 0.1)
                score += 1;
            else if (weightlosepersent > 0.1)
                score += 2;
            score += disease;
            testResult.RESULTDETAIL = string.Format("测试结果为：{0}分", score);
            return score;
        }
    }
}
