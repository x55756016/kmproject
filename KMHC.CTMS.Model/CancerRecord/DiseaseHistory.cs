using System;

namespace KMHC.CTMS.Model.CancerRecord
{
    public class DiseaseHistory
    {
        public string DISEASEHISTORYID { get; set; }
        public string PERSONID { get; set; }
        public string DISEASENAME { get; set; }
        public string ANNUALINCOME { get; set; }
        public string TREATPROCESS { get; set; }
        public Nullable<System.DateTime> CREATTIME { get; set; }
        public string HOSPITAL { get; set; }
        public string CREATUSER { get; set; }
        public string EDITUSER { get; set; }
        public Nullable<System.DateTime> EDITTIME { get; set; }
        public Nullable<System.DateTime> STARTTIME { get; set; }
        public Nullable<System.DateTime> RECOVERTIME { get; set; }
        public Nullable<System.DateTime> CONFIRMEDTIME { get; set; }
        public string CONFIRMEDAGE { get; set; }
        public string CONFIRMEDADDRESS { get; set; }
        public string CONFIRMEDMODE { get; set; }
        public string CONFIRMEDRELUST { get; set; }
        public string SYMPTOMDESCRIPTION { get; set; }
        public string ISCANCER { get; set; }
        public string CANCERCODE { get; set; }
        public string CANCERNAME { get; set; }
        public string CANCERPOSITION { get; set; }
        public string CELLULATYPE { get; set; }
        public string CLINICALSTAGES { get; set; }
        public string TUMOR { get; set; }
        public string N { get; set; }
        public string M { get; set; }
        public string GENE { get; set; }
        public string ISREAPPEAR { get; set; }
    }
}
