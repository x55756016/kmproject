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
    
    public partial class CTMS_MYQUESTION
    {
        public string ID { get; set; }
        public string USERID { get; set; }
        public string OBJECTTYPE { get; set; }
        public string OBJECTUSERID { get; set; }
        public string QUESTION { get; set; }
        public string ANSWER { get; set; }
        public string CREATEUSERID { get; set; }
        public string CREATEUSERNAME { get; set; }
        public Nullable<System.DateTime> CREATEDATETIME { get; set; }
        public string EDITUSERID { get; set; }
        public string EDITUSERNAME { get; set; }
        public Nullable<System.DateTime> EDITDATETIME { get; set; }
        public string OWNERID { get; set; }
        public string OWNERNAME { get; set; }
        public decimal ISDELETED { get; set; }
        public string LOGINNAME { get; set; }
        public string OBJECTLOGINNAME { get; set; }
    }
}
