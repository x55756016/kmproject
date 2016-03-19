/*
 * 描述:定义xxx作为Aggerate的根对外提供访问的接口
 *  
 * 修订历史: 
 * 日期          修改人              内容
 * 20151102      张志明              创建 
 *  
 */

using KMHC.CTMS.Common;
using KMHC.CTMS.Model.PrecisionMedicine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.Product
{
    public class AccountRecord : BaseModel
    {
        public string ID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID { get; set; }
        
        /// <summary>
        /// 登录账号
        /// </summary>
        public string LoginName { get; set; }
        
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderID { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        public string ProductID { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 账单描述
        /// </summary>
        public string AccountDescription { get; set; }

        private int _balance = 1;
        /// <summary>
        /// 收支
        /// </summary>
        public int Balance 
        {
            get { return _balance; }
            set { _balance=value;}
        }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Account { get; set; }

        /// <summary>
        /// 类型 0：充值，1：购买 2:消费使用
        /// </summary>
        public SpendType SpendType { get; set; }


        /// <summary>
        /// 类型
        /// </summary>
        public string SpendTypeText { get { return EnumHelper.GetDescription(SpendType); } }
    }
}
