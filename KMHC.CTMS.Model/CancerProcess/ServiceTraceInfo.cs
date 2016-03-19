using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.Common;

namespace KMHC.CTMS.Model.CancerProcess
{
    public class ServiceTraceInfo
    {
        public string Id { get; set; }

        public TraceType TraceType { get; set; }

        public string TraceTypeName
        {
            get
            {
                return TraceType.ToString();
            }
        }

        public string TraceDetail { get; set; }

        public DateTime CreateDateTime { get; set; }

        public string CreateUserId { get; set; }

        public string CreateUserName { get; set; }

        public string ForEventId { get; set; }

        public int IsValid { get; set; }
    }
}
