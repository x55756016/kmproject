//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace KMHC.CTMS.DAL.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class HR_PERSONDRUGHISTORY
    {
        public decimal HISTORYID { get; set; }
        public string DRUGNAME { get; set; }
        public Nullable<System.DateTime> STARTDATE { get; set; }
        public Nullable<System.DateTime> ENDDATE { get; set; }
        public string HISUSAGE { get; set; }
        public string DOSAGE { get; set; }
        public string DESCRIPTION { get; set; }
        public Nullable<decimal> PERSONID { get; set; }
    }
}
