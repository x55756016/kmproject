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
    
    public partial class HPN_TEMPLATE
    {
        public string ID { get; set; }
        public string TITLE { get; set; }
        public string TEMPLATEFROM { get; set; }
        public string WELCOMELABEL { get; set; }
        public string TEMPLATENAME { get; set; }
        public string ICON { get; set; }
        public Nullable<decimal> TEMPLATEINDEX { get; set; }
        public Nullable<decimal> TEMPLATESTATUS { get; set; }
        public Nullable<decimal> TEMPLATETYPE { get; set; }
        public string DESCRIPTION { get; set; }
        public string TEMPLATETIME { get; set; }
        public string INSTRUCTION { get; set; }
        public Nullable<System.DateTime> CREATETIME { get; set; }
        public string CREATEUSER { get; set; }
        public Nullable<System.DateTime> LASTMODIFYTIME { get; set; }
        public string LASTMODIFYUSER { get; set; }
    }
}