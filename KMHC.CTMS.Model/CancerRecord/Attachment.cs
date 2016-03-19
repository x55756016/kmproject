/*
 * 描述:用户上传附件管理
 *  
 * 修订历史: 
 * 日期       修改人              Email                   内容
 * 20151109   郝元彦              haoyuanyan@gmail.com    创建 
 *  
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.CancerRecord
{
    public class Attachment
    {
        /// <summary>
        /// 文件路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 文件描述
        /// </summary>
        public string Desccription { get; set; }
    }
}
