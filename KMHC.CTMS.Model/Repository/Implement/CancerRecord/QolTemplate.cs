using System;
using KMHC.CTMS.Model.Repository.Interface;
using KMHC.CTMS.Common.Helper;
using System.Linq;
using System.Data.Entity;
using KMHC.CTMS.DAL.Database;
using System.Collections.Generic;

namespace KMHC.CTMS.Model.Repository.CancerRecord
{
    public class QolTemplate : Template
    {
        public override string getTemplateName()
        {
            return "Qol";
        }

        public override double calculateResult(HPN_TESTRESULT testResult, List<HPN_TESTRESULTDETAILS> testDetails)
        {
            double score = base.calculateResult(testResult, testDetails);
            testResult.RESULTDETAIL = string.Format("测试结果为：{0}分", score);
            return score;
        }
    }
}
