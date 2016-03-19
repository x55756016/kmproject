using System;
using System.Collections.Generic;
using System.IO;
using log4net;
using log4net.Config;

namespace KMHC.CTMS.Common.Helper
{
    public class LogHelper
    {
        static Logger log = new Logger("KMLOG");
        /// <summary>
        /// 记录错误信息
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteError(string msg)
        {
            log.ErrorFormat(msg);
        }
        /// <summary>
        /// 记录Debug信息
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteDebug(string msg)
        {
            log.DebugFormat(msg);
        }
        /// <summary>
        /// 记录警告信息
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteWarn(string msg)
        {
            log.WarnFormat(msg);
        }
        /// <summary>
        /// 记录普通信息
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteInfo(string msg)
        {
            log.InfoFormat(msg);
        }
        /// <summary>
        /// 记录严重错误信息
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteFatal(string msg)
        {
            log.FatalFormat(msg);
        }
    }
    internal class Logger
    {
        private ILog Log = null;

        /// <summary>
        /// Holds the type of formatting for Log4Net
        /// </summary>
        private enum formatType
        {
            Debug, Warn, Error, Fatal, Info
        }
        public Logger(string name, FileInfo configFile)
        {
            Log = LogManager.GetLogger(name);
            if (configFile != null && configFile.Exists)
                XmlConfigurator.Configure(configFile);
            else
                XmlConfigurator.Configure();
        }

        public Logger(string name)
        {
            Log = LogManager.GetLogger(name);
            XmlConfigurator.Configure();
        }

        #region DebugFormat
        /// <summary>
        /// Writes to both console and log4net. Log4net uses Log.Debug
        /// </summary>
        /// <param name="text"></param>
        public void DebugFormat(string text)
        {
            write(text, formatType.Debug);
        }
        #endregion

        #region WarnFormat
        /// <summary>
        /// Writes to both console and log4net. Log4net uses Log.Error.
        /// </summary>
        /// <param name="text"></param>
        public void WarnFormat(string text)
        {
            write(text, formatType.Warn);
        }
        #endregion

        #region ErrorFormat
        /// <summary>
        /// Writes to both console and log4net. Log4net uses Log.Error.
        /// </summary>
        /// <param name="text"></param>
        public void ErrorFormat(string text)
        {
            write(text, formatType.Error);
        }
        #endregion

        #region FatalFormat
        /// <summary>
        /// Writes to both console and log4net. Log4net uses Log.Fatal.
        /// </summary>
        /// <param name="text"></param>
        public void FatalFormat(string text)
        {
            write(text, formatType.Fatal);
        }
        #endregion

        #region InfoFormat
        /// <summary>
        /// Writes to both console and log4net. Log4net uses Log.Info
        /// </summary>
        /// <param name="text"></param>
        public void InfoFormat(string text)
        {
            write(text, formatType.Info);
        }

        public void InfoFormat(string text, object arg0)
        {
            string formattedString = string.Format(text, arg0);
            write(formattedString, formatType.Info);
        }

        public void InfoFormat(string text, object arg0, object arg1)
        {
            string formattedString = string.Format(text, arg0, arg1);
            write(formattedString, formatType.Info);
        }

        public void InfoFormat(string text, object arg0, object arg1, object arg2)
        {
            string formattedString = string.Format(text, arg0, arg1, arg2);
            write(formattedString, formatType.Info);
        }

        public void InfoFormat(string text, List<object> args)
        {
            string formattedString = string.Format(text, args);
            write(formattedString, formatType.Info);
        }
        #endregion

        #region Write
        /// <summary>
        /// Writes to both console and log4net. Uses type to determine which type of logging to use.
        /// </summary>
        /// <param name="text">Text to write to console and log4net.</param>
        /// <param name="incomingType">Type to use for log4net.</param>
        private void write(string text, formatType incomingType)
        {
            string now = DateTime.Now.ToString();
            //Add time stamp to the console.
            Console.WriteLine(String.Format("{0} [{1}] {2}", now, incomingType.ToString(), text));

            //Log out to log4net if configuration was present
            switch (incomingType)
            {
                case formatType.Debug:
                    System.Diagnostics.Trace.WriteLine(String.Format("[{0}]{1}", now, text));
                    Log.Debug(text);
                    break;

                case formatType.Warn:
                    System.Diagnostics.Trace.TraceWarning(String.Format("[{0}]{1}", now, text));
                    Log.Warn(text);
                    break;

                case formatType.Error:
                    System.Diagnostics.Trace.TraceError(String.Format("[{0}]{1}", now, text));
                    Log.Error(text);
                    break;

                case formatType.Fatal:
                    System.Diagnostics.Trace.TraceError(String.Format("[{0}]{1}", now, text));
                    Log.Fatal(text);
                    break;

                case formatType.Info:
                    System.Diagnostics.Trace.TraceInformation(String.Format("[{0}]{1}", now, text));
                    Log.Info(text);
                    break;
            }
        }
        #endregion
    }
}