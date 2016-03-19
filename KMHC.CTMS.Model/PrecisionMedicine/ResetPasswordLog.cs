using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.PrecisionMedicine
{
    public class ResetPasswordLog
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime InputTime { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 重置密码类型(0：邮箱；1、手机)
        /// </summary>
        public int ResetType { get; set; }

        /// <summary>
        /// 状态(0、未使用；1、已使用)
        /// </summary>
        public int Status { get; set; }
    }
}
