/*
 * 描述:用药情况model
 *  
 * 修订历史: 
 * 日期               修改人         Email                  内容
 * 20151116   		  林德力        takalin@qq.com   		创建 
 *  
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.PrecisionMedicine
{
    public partial class DrugUse
    {
        public decimal ID { get; set; }
        public string DISEASEID { get; set; }
        public string USERID { get; set; }
        public string DRUGTYPE { get; set; }
        public string DRUGNAME { get; set; }
        public string DRUGID { get; set; }
        public string TREATMENTDISEASES { get; set; }
        public Nullable<System.DateTime> STARTTIME { get; set; }
        public Nullable<System.DateTime> ENDTIME { get; set; }
        public string TIMESADAY { get; set; }
        public string DOSE { get; set; }
        public string UNITS { get; set; }
        public Nullable<decimal> USEDAY { get; set; }
        public string ISNEWDRUG { get; set; }
        public string CREATEUSERID { get; set; }
        public Nullable<System.DateTime> CREATEDATETIME { get; set; }
        public string EDITUSERID { get; set; }
        public Nullable<System.DateTime> EDITDATETIME { get; set; }
        public string ISDELETED { get; set; }
        public string OWNERID { get; set; }

        public int Action { get; set; }

        public List<DrugEffect2> InterEffect { get; set; }
        public List<GeneEffect> GeneEffect { get; set; } 

        //public string InterEffect { get; set; }
        //public string GeneEffect { get; set; }
    }


    public class AddDrugEffect
    {
        public List<DrugEffect2> InterEffect { get; set; }
        public List<GeneEffect> GeneEffect { get; set; }
    }


    public class DrugEffect2
    {
        public decimal ID { get; set; }
        public string DISEASEID { get; set; }
        public string NAME { get; set; }
        public string DRUGID { get; set; }
        public string INTERDRUGID { get; set; }
        public string INTERNAME { get; set; }
        public string DRUGINTERACTION { get; set; }
    }


    public class GeneEffect
    {
        public decimal ID { get; set; }
        public string DISEASEID { get; set; }
        public string DRUGID { get; set; }
        public string DRUGNAME { get; set; }
        public string GENENAME { get; set; }
        public string GENEDESC { get; set; }
    }





    public class PrescritionModel
    {
        public List<DrugUse> OldDrug { get; set; }
        public List<DrugUse> NewDrug { get; set; }
        public List<DrugEffect2> InterEffect { get; set; }
        public List<GeneEffect> GeneEffect { get; set; } 
    }
}
