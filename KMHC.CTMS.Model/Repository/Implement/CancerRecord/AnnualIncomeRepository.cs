using KMHC.CTMS.DAL.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.Repository.Implement.CancerRecord
{
    public class AnnualIncomeRepository : BaseRepository<HR_ANNUALINCOME>
    {
        public AnnualIncomeRepository()
            : base(new CRDatabase())
        {

        }
    }
}
