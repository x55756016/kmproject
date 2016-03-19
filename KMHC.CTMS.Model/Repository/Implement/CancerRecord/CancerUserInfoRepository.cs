using KMHC.CTMS.DAL.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.Repository.Implement.CancerRecord
{
    public class CancerUserInfoRepository : BaseRepository<HR_CNR_USER>
    {
        public CancerUserInfoRepository():base(new CRDatabase())
        {

        }
    }
}
