using System;
using System.Collections.Generic;
using System.Data.Entity;
using KMHC.CTMS.Model.Repository.Interface;
using System.Linq;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.DAL.Database;

namespace KMHC.CTMS.Model.Repository.CancerRecord
{
    public class NrsTemplate : Template
    {
        public override string getTemplateName()
        {
            return "Nrs";
        }

        public override double calculateResult(HPN_TESTRESULT testResult, List<HPN_TESTRESULTDETAILS> testDetails)
        {
            double score = 0;
            var item = testDetails.Find(p => p.TEMPLATEITEMNAME == "nrs_4");
            score += item == null ? 0 : item.ITEMRESULT.ToDouble(0);
            item = testDetails.Find(p => p.TEMPLATEITEMNAME == "nrs_5");
            score += item == null ? 0 : item.ITEMRESULT.ToDouble(0);
            item = testDetails.Find(p => p.TEMPLATEITEMNAME == "age");
            string testNo = testDetails.First().TESTNO;
            if (item != null)
            {
                if (item.ITEMRESULT.ToDouble(0) >= 70)
                    score += 1;
            }
            testResult.RESULTDETAIL = string.Format("测试结果为：{0}分", score);
            return score;
        }
    }
}
