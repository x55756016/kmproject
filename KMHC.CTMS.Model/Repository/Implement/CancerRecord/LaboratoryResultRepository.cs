using KMHC.CTMS.DAL.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.Repository.Implement.CancerRecord
{
    public class LaboratoryResultRepository : BaseRepository<HR_LABORATORYRESULT>
    {
        private static CRDatabase dbcontext = new CRDatabase();

        public LaboratoryResultRepository()
            : base(dbcontext)
        {

        }

        //public bool addHR_LaboratoryResult(HR_SEEDOCTORHISTORY MasterEntity, HR_LABORATORYRESULT entity)
        //{
        //    var q = dbcontext.Set<HR_SEEDOCTORHISTORY>().Find(MasterEntity.HISTORYID);
        //    entity.HR_SEEDOCTORHISTORY = q;
        //    dbcontext.Set<HR_LABORATORYRESULT>().Add(entity);

        //    return dbcontext.SaveChanges() > 0 ? true : false;
        //}
    }
}
