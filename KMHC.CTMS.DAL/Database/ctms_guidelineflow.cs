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
    
    public partial class CTMS_GUIDELINEFLOW
    {
        public string ID { get; set; }
        public string GUIDELINENAME { get; set; }
        public string GUIDELINEINFO { get; set; }
        public string CREATEUSERID { get; set; }
        public Nullable<System.DateTime> CREATEDATETIME { get; set; }
        public string EDITUSERID { get; set; }
        public Nullable<System.DateTime> EDITDATETIME { get; set; }
        public decimal ISDELETED { get; set; }
        public string OWNERID { get; set; }
    }
}