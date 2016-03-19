using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.Common.Helper;


namespace KMHC.CTMS.Model.Repository.Interface
{
    public interface IDiseaseRepository
    {
        IEnumerable<DiseaseType> GetDiseaseTypes();


        IEnumerable<Disease> GetDiseases();
        IEnumerable<Disease> GetDiseaseList(PageInfo page,string[] types, string key);
    }
}
