/*
 * 描述:
 *  
 * 修订历史: 
 * 日期       修改人              Email                   内容
 * 20151102   郝元彦              haoyuanyan@gmail.com    创建 
 * 20151109   邓柏生                                      补充Model字段
 *  
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.CancerRecord
{
    public class LabTestRecord
    {
        public int  RecordId { get; set; }
        public Nullable<System.DateTime>   RecordDate{ get; set; }
        public string  Hospital { get; set; }
        public string  LabDoctor{ get; set; }
        public Nullable<int>  ItemId{ get; set; }
        public string  Report{ get; set; }
        public string  Attachment { get; set; }
        public string  Itemname { get; set; }
        public string  ItemNo { get; set; }
        public string  RecordNo { get; set; }
        public Nullable<System.DateTime>  CreateDate{ get; set; }
        public string  CreateBy { get; set; }
        public Nullable<System.DateTime>  UpdateDate{ get; set; }
        public string  UpdateBy { get; set; }
        public Nullable<byte>  Status{ get; set; }

    }
}
