/*
 * 描述:返回的基类
 *  
 * 修订历史: 
 * 日期                 修改人              Email                  内容
 * 20151215   		  林德力         	takalin@qq.com   		 创建 
 *  
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.Common
{
   public class BaseResult
    {
       /// <summary>
       /// 执行是否成功
       /// </summary>
        public bool Succeeded { get; set; }

       /// <summary>
       /// 错误信息
       /// </summary>
        public string Error { get; set; }

       /// <summary>
       /// 错误信息列表
       /// </summary>
        public List<string> Errors { get; set; }
    }
}
