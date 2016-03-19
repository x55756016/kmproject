using KMHC.HR.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.HR.Domain.Repository.Implement.CancerRecord
{
    public class PersonInfoRepository : BaseRepository<HR_PERSONINFO>
    {
        public PersonInfoRepository() : base(new HRDatabase()) { 
            
        }
    }
}
