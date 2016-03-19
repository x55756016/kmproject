using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.Model.CancerRecord;

namespace KMHC.CTMS.Model.Repository.Interface
{
    public interface IAreaRepository
    {
        IEnumerable<Area> GetAreas(int pid);

        IEnumerable<Area> GetAllAreas();
    }
}
