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
    
    public partial class HR_EXAMINERECORD
    {
        public decimal EXAMID { get; set; }
        public string EXAMNO { get; set; }
        public string EXAMTYPE { get; set; }
        public string VISITWAY { get; set; }
        public string DOCTORNAME { get; set; }
        public Nullable<System.DateTime> CREATEDATE { get; set; }
        public string CREATEBY { get; set; }
        public Nullable<System.DateTime> UPDATEDATE { get; set; }
        public string UPDATEBY { get; set; }
        public decimal STATUS { get; set; }
        public Nullable<System.DateTime> EXAMDATE { get; set; }
        public Nullable<decimal> PERSONID { get; set; }
    }
}
