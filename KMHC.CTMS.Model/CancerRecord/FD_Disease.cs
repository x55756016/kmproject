using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.CancerRecord
{
    public class FD_Disease
    {
        public string ID { get; set; }
        public string MemberID { get; set; }
        public string DiseaseCode { get; set; }
        public string DiseaseName { get; set; }
        public DateTime? AttackDate { get; set; }
        public int DiagnosisAge { get; set; }
        public bool IsInfectious { get; set; }
        public string TreatmentHospital { get; set; }
        public string Treatment { get; set; }
        public string TreatmentResult { get; set; }
    }
}
