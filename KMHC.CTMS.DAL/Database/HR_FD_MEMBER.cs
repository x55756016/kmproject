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
    
    public partial class HR_FD_MEMBER
    {
        public string ID { get; set; }
        public string USERID { get; set; }
        public string RELATIONSHIP { get; set; }
        public string MEMBERNAME { get; set; }
        public Nullable<decimal> SEX { get; set; }
        public Nullable<decimal> ISTWINS { get; set; }
        public Nullable<decimal> ISIDENTICAL { get; set; }
        public Nullable<decimal> ISALIVE { get; set; }
        public string DEADREASON { get; set; }
        public Nullable<System.DateTime> CREATEDTIME { get; set; }
        public string CREATEDBY { get; set; }
        public Nullable<System.DateTime> MODIFIEDTIME { get; set; }
        public string MODIFIEDBY { get; set; }
    }
}
