/*
 * 描述:会员model
 *  
 * 修订历史: 
 * 日期               修改人              Email                  内容
 * 20160129   		  林德力         	takalin@qq.com   		 创建 
 *  
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.Product
{
    public class MemberModel
    {
        /// <summary>
        /// 会员id
        /// </summary>
        public string MEMBERID { get; set; }

        /// <summary>
        /// 会员名称 
        /// </summary>
        public string MEMBERNAME { get; set; }

        /// <summary>
        /// 会员级别
        /// </summary>
        public Nullable<decimal> MEMBERLEVEL { get; set; }

        /// <summary>
        /// 出现这个是因为dictionary中的值是string，这边需要转换过来才能正常选项
        /// </summary>
        public string LevelString { get; set; }

        /// <summary>
        /// 会员级别名称
        /// </summary>
        public string MEMBERLEVELName { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public Nullable<decimal> MEMBERPRICE { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string MEMBERUNIT { get; set; }

        /// <summary>
        /// 描述信息
        /// </summary>
        public string MEMBERDESCRIPT { get; set; }

        public List<MemberProducts> menberProductList { get; set; }
    }
}
