/*
 * 描述:
 *  
 * 修订历史: 
 * 日期           修改人              Email                   内容
 * 2015/11/9      邓柏生                                      创建 
 *  
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.CancerRecord
{
    public partial class FileUpload
    {
        /// <summary>
        /// 上传记录id
        /// </summary>
        public string FileUploadid { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        /// 模块代码
        /// </summary>
        public string ModelCode { get; set; }

        /// <summary>
        /// 表单记录id
        /// </summary>
        public string FormId { get; set; }

        /// <summary>
        /// 文件地址
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 创建时间id
        /// </summary>
        public Nullable<DateTime> CreatTime { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string CreatBy { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public Nullable<DateTime> ModifyTime { get; set; }

        /// <summary>
        /// 修改者
        /// </summary>
        public string ModifyBy { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }

    public class FileUserArrange 
    {
        public string FileId { get; set; }
        public DateTime? CreatTime { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string Remark { get; set; }
        public string Type { get; set; }
        public string Date { get; set; }
        public string Hosiptal { get; set; }
        public string FormId { get; set; }
    }
}
