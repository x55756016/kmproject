using KMHC.CTMS.DAL.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.Repository.Implement.CancerRecord
{
    public class UserGeneRepository : BaseRepository<HR_USERGENE>
    {
        public UserGeneRepository()
            : base(new CRDatabase())
        {

        }
    }
}
