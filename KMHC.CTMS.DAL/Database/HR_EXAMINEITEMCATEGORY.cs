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
    
    public partial class HR_EXAMINEITEMCATEGORY
    {
        public decimal CATEGORYID { get; set; }
        public decimal ITEMID { get; set; }
        public decimal ID { get; set; }
        public string CATEGORYCODE { get; set; }
        public string ITEMCODE { get; set; }
        public decimal SORTNUMBER { get; set; }
        public Nullable<System.DateTime> CREATEDATETIME { get; set; }
        public Nullable<decimal> CREATEUSERID { get; set; }
        public Nullable<System.DateTime> EDITDATETIME { get; set; }
        public Nullable<decimal> EDITUSERID { get; set; }
        public Nullable<decimal> DELETERUSERID { get; set; }
        public Nullable<System.DateTime> DELETIONTIME { get; set; }
        public decimal ISDELETED { get; set; }
    }
}
