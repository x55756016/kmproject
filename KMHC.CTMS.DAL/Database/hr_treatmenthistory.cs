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
    
    public partial class HR_TREATMENTHISTORY
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
