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
    
    public partial class CTMS_GUIDELINE
    {
        public string ID { get; set; }
        public string CODE { get; set; }
        public string NAME { get; set; }
        public string PARENTID { get; set; }
        public decimal ISINHERIT { get; set; }
        public string CLINICALBASIS { get; set; }
        public decimal ENTERLOGICALOPERATOR { get; set; }
        public string ENTERCONDITEMIDS { get; set; }
        public decimal OUTLOGICALOPERATOR { get; set; }
        public string OUTCONDITEMIDS { get; set; }
        public string CREATEUSERID { get; set; }
        public string CREATEUSERNAME { get; set; }
        public Nullable<System.DateTime> CREATEDATETIME { get; set; }
        public string EDITUSERID { get; set; }
        public string EDITUSERNAME { get; set; }
        public Nullable<System.DateTime> EDITDATETIME { get; set; }
        public string OWNERID { get; set; }
        public string OWNERNAME { get; set; }
        public decimal ISDELETED { get; set; }
        public string RECOMMENDPROCESS { get; set; }
    }
}
