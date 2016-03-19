using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace KMHC.CTMS.Model.Common
{
    public class Info
    {
        public ObjectId _id { get; set; }

        public string CategoryCode { get; set; }

        public string CategoryName { get; set; }

        [Description("题名")]
        public string Title { get; set; }

        [Description("文献来源")]
        public string Source { get; set; }

        [Description("摘要")]
        public string Digest { get; set; }

        [Description("关键词")]
        public string KeyWord { get; set; }

        public string Html { get; set; }

        [Description("作者")]
        public string Author { get; set; }

        public string FileId { get; set; }

        /// <summary>
        /// 随机文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 原文件名称
        /// </summary>
        public string OriginalFileName { get; set; }

        public DateTime PublishTime { get; set; }

        public string ArticleType { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime ModifyTime { get; set; }

        public string ModifyUser { get; set; }
    }
}
