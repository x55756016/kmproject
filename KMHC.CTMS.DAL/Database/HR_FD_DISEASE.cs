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
    
    public partial class HR_FD_DISEASE
    {
        public string ID { get; set; }
        public string MEMBERID { get; set; }
        public string DISEASECODE { get; set; }
        public string DISEASENAME { get; set; }
        public Nullable<System.DateTime> ATTACKDATA { get; set; }
        public Nullable<decimal> DIAGNOSISAGE { get; set; }
        public Nullable<decimal> ISINFECTIOUS { get; set; }
        public string TREATMENTHOSPITAL { get; set; }
        public string TREATMENT { get; set; }
        public string TREATMENTRESULT { get; set; }
    }
}
