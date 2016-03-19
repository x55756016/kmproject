using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.CancerRecord
{
    public class TreatmentHistory
    {

        public string TREATMENTHISTORYID { get; set; }

        public string DISEASEHISTORYID { get; set; }

        public Nullable<System.DateTime> TREATMENTTIME { get; set; }
        public string TREATMENTHOSPITAL { get; set; }
        public Nullable<decimal> TREATMENTTYPE { get; set; }
        public string OPERATIONTYPE { get; set; }
        public string OPERATIONRESLUT { get; set; }
        public string RADIOTHERAPYDOSE { get; set; }
        public string RADIOTHERAPYRESLUT { get; set; }
        public string CHEMOTHERAPYPROJECT { get; set; }
        public string CHEMOTHERAPYDRUG { get; set; }
        public string CHEMOTHERAPYRESLUT { get; set; }
    }
}
