using KMHC.CTMS.DAL.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.Repository.Implement.CancerRecord
{
    public class LaboratoryTestItemRepository : BaseRepository<HR_LABORATORYTESTITEM>
    {

        private static CRDatabase dbcontext = new CRDatabase();
        public LaboratoryTestItemRepository()
            : base(dbcontext)
        {

        }
    }
}
