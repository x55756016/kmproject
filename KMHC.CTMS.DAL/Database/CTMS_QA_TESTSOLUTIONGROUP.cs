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
    
    public partial class CTMS_QA_TESTSOLUTIONGROUP
    {
        public string TESTSOLUTIONGROUPID { get; set; }
        public string SOLUTIONID { get; set; }
        public string TESTPAPERGROUPID { get; set; }
        public string TESTPAPERGROUPNAME { get; set; }
        public decimal ORDERNUMBER { get; set; }
        public string STEPNAME { get; set; }
        public string STEPDESC { get; set; }
        public Nullable<float> AUTOSENDDAY { get; set; }
    }
}
