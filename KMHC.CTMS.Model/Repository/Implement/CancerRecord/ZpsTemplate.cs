using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.Model.Repository.Interface;
using KMHC.CTMS.DAL.Database;
using System.Data.Entity;
using KMHC.CTMS.Common.Helper;

namespace KMHC.CTMS.Model.Repository.CancerRecord
{
    public class ZpsTemplate : Template
    {
        public override string getTemplateName()
        {
            return "Zps";
        }

        public override double calculateResult(HPN_TESTRESULT testResult, List<HPN_TESTRESULTDETAILS> testDetails)
        {
            double score = base.calculateResult(testResult, testDetails);
            testResult.RESULTDETAIL = string.Format("测试结果为：{0}分", score);
            return score;
        }
    }
}
