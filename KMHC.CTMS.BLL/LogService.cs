/*
 * 描述:定义日志操作类
 *  
 * 修订历史: 
 * 日期          修改人              内容
 * 20151113      张志明              创建 
 *  
 */

using KMHC.CTMS.Common.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace KMHC.CTMS.BLL
{
    public static class LogService
    {
        /// <summary>
        /// 日志通用方法
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">内容</param>
        /// <param name="logLevel">日志级别</param>
        /// <param name="logPriority">优先等级</param>
        public static  void WriteLog(string title,string message,LogLevel logLevel,LogPriority logPriority)
        {
            //Todo 暂时以LogHelper写入数据库，后续增加自定义字段
            switch (logLevel)
            { 
                case LogLevel.Debug:
                    LogHelper.WriteDebug(message);
                    break;
                case LogLevel.Info:
                    LogHelper.WriteInfo(message);
                    break;
                case LogLevel.Warn:
                    LogHelper.WriteWarn(message);
                    break;
                case LogLevel.Error:
                    LogHelper.WriteError(message);
                    break;
                case LogLevel.Fatal:
                    LogHelper.WriteFatal(message);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 写Debug日志
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">内容</param>
        public static void WriteDebugLog(string title, string message)
        {
            WriteLog(title, message, LogLevel.Debug, LogPriority.Normal);
        }

        /// <summary>
        /// 写Debug日志
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">内容</param>
        /// <param name="logPriority">优先级</param>
        public static void WriteDebugLog(string title, string message,LogPriority logPriority)
        {
            WriteLog(title, message, LogLevel.Debug, LogPriority.Normal);
        }

        /// <summary>
        /// 写消息日志
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">内容</param>
        public static void WriteInfoLog(string title, string message)
        {
            WriteLog(title, message, LogLevel.Info, LogPriority.Normal);
        }

        /// <summary>
        /// 写消息日志
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">内容</param>
        public static void WriteInfoLog(string title, string message, LogPriority logPriority)
        {
            WriteLog(title, message, LogLevel.Info, logPriority);
        }

        /// <summary>
        /// 写警告日志
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">内容</param>
        public static void WriteWarnLog(string title, string message)
        {
            WriteLog(title, message, LogLevel.Warn, LogPriority.Normal);
        }

        /// <summary>
        /// 写警告日志
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">内容</param>
        public static void WriteWarnLog(string title, string message, LogPriority logPriority)
        {
            WriteLog(title, message, LogLevel.Warn, logPriority);
        }

        /// <summary>
        /// 写错误日志
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">内容</param>
        public static void WriteErrorLog(string title, string message)
        {
            WriteLog(title, message, LogLevel.Error, LogPriority.Normal);
        }

        /// <summary>
        /// 写错误日志
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">内容</param>
        public static void WriteErrorLog(string title, string message, LogPriority logPriority)
        {
            WriteLog(title, message, LogLevel.Error, logPriority);
        }

        /// <summary>
        /// 写Fatal日志
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">内容</param>
        public static void WriteFatalLog(string title, string message)
        {
            WriteLog(title, message, LogLevel.Fatal, LogPriority.Normal);
        }

        /// <summary>
        /// 写Fatal日志
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">内容</param>
        public static void WriteFatalLog(string title, string message, LogPriority logPriority)
        {
            WriteLog(title, message, LogLevel.Fatal, logPriority);
        }
      

    }

    /// <summary>
    /// 优先等级
    /// </summary>
    public enum LogPriority
    {
        /// <summary>
        /// 较低
        /// </summary>
        Lower =0,
 
        /// <summary>
        /// 一般
        /// </summary>
        Normal=1,

        /// <summary>
        /// 较高
        /// </summary>
        Higher=2,

        /// <summary>
        /// 紧急
        /// </summary>
        Urgent=3
    }

    /// <summary>
    /// 日志级别 Fatal>Error>Warn>Info>Debug
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// Debug
        /// </summary>
        Debug=0,

        /// <summary>
        /// 系统，消息日志
        /// </summary>
        Info=1,

        /// <summary>
        /// 警告
        /// </summary>
        Warn=2,

        /// <summary>
        /// 一般错误日志
        /// </summary>
        Error=3,

        /// <summary>
        /// 致命错误，如服务器宕机,关键服务不可用等
        /// </summary>
        Fatal=4
    }
}
