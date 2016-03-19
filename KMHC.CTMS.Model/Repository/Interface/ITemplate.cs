using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Common.Helper;

namespace KMHC.CTMS.Model.Repository.Interface
{
    public abstract class Template
    {
        protected DbContext _db = null;
        public virtual double calculateResult(HPN_TESTRESULT testResult, List<HPN_TESTRESULTDETAILS> testDetails)
        {
            double score = 0;
            foreach (HPN_TESTRESULTDETAILS item in testDetails)
            {
                if (!item.TEMPLATEITEMNAME.Contains('_'))
                    continue;
                score += item.ITEMRESULT.ToDouble(0);
            }
            var resultDesc = (from x in _db.Set<HPN_RESULTDESC>() where x.TEMPLATENAME == testResult.TEMPLATENAME && x.MINSCORES <= (decimal)score && x.MAXSCORES >= (decimal)score select x).FirstOrDefault();
            if (resultDesc != null)
            {
                testResult.TESTRESULT = resultDesc.RESULTDETAIL;
            }
            return score;
        }

        public abstract string getTemplateName();

        public void setContext(DbContext _db)
        {
            this._db = _db;
        }
    }
}
