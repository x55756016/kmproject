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
    
    public partial class CTMS_SYS_LOGINFO
    {
        public decimal LOGID { get; set; }
        public Nullable<System.DateTime> LOGTIME { get; set; }
        public string LOGLEVEL { get; set; }
        public string TITLE { get; set; }
        public string LOGEXCEPTION { get; set; }
        public string LOGGER { get; set; }
        public string LOGSOURCE { get; set; }
        public string IP { get; set; }
        public string MAC { get; set; }
        public string HOSTNAME { get; set; }
        public string LOGINNAME { get; set; }
        public string USERNAME { get; set; }
        public string LOGMESSAGE { get; set; }
    }
}