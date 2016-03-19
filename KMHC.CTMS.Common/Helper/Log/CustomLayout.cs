/*
 * 描述:定义xxx作为Aggerate的根对外提供访问的接口
 *  
 * 修订历史: 
 * 日期          修改人              Email              内容
 * 20151102      张志明              			创建 
 *  
 */

using log4net.Core;
using log4net.Layout;
using log4net.Layout.Pattern;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Common.Helper.Log
{
    public class CustomLayout : PatternLayout
    {
        // Methods  
        public CustomLayout()
        {
            this.AddConverter("property", typeof(MyMessagePatternConverter));
        }
    }


    /// <summary>
    /// LogContent 的摘要说明
    /// </summary>
    public class LogContent
    {
        public LogContent(string title)
        {
            _title = title;
        }


        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

    }


    public class MyMessagePatternConverter : PatternLayoutConverter
    {
        protected override void Convert(System.IO.TextWriter writer, log4net.Core.LoggingEvent loggingEvent)
        {
            if (Option != null)
            {
                // Write the value for the specified key
                WriteObject(writer, loggingEvent.Repository, LookupProperty(Option, loggingEvent));
            }
            else
            {
                // Write all the key value pairs
                WriteDictionary(writer, loggingEvent.Repository, loggingEvent.GetProperties());
            }
           
        }
        /// <summary>
        /// 通过反射获取传入的日志对象的某个属性的值
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        private object LookupProperty(string property, log4net.Core.LoggingEvent loggingEvent)
        {
            object propertyValue = string.Empty;
            PropertyInfo propertyInfo = loggingEvent.MessageObject.GetType().GetProperty(property);
            if (propertyInfo != null)
                propertyValue = propertyInfo.GetValue(loggingEvent.MessageObject, null);
            return propertyValue;
        }
    }

}
