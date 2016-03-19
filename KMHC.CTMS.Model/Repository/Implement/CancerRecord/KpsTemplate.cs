using KMHC.CTMS.Model.Repository.Interface;
using KMHC.CTMS.DAL.Database;
using System.Collections.Generic;

namespace KMHC.CTMS.Model.Repository.CancerRecord
{
    public class KpsTemplate : Template
    {
        public override string getTemplateName()
        {
            return "Kps";
        }

        public override double calculateResult(HPN_TESTRESULT testResult, List<HPN_TESTRESULTDETAILS> testDetails)
        {
            double score = base.calculateResult(testResult, testDetails);
            testResult.RESULTDETAIL = string.Format("测试结果为：{0}分", score);
            return score;
        }
    }
}
