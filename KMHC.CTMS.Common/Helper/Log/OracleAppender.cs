/*
 * 描述:定义xxx作为Aggerate的根对外提供访问的接口
 *  
 * 修订历史: 
 * 日期          修改人              Email              内容
 * 20151102      张志明              			创建 
 *  
 */

using log4net.Appender;
using log4net.Core;
using log4net.Util;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Common.Helper.Log
{
    public class OracleAppender : BufferingAppenderSkeleton
    {
        // Fields
        private static readonly Type declaringType = typeof(AdoNetAppender);
        private string m_commandText;
        private CommandType m_commandType = CommandType.Text;
        private string m_connectionString;
        private string m_connectionType;
        private OracleCommand m_dbCommand;
        private OracleConnection m_dbConnection;
        protected ArrayList m_parameters = new ArrayList();
        private bool m_reconnectOnError = false;
        private SecurityContext m_securityContext;
        protected bool m_usePreparedCommand;
        private bool m_useTransactions = true;

        // Methods
        public override void ActivateOptions()
        {
            base.ActivateOptions();
            this.m_usePreparedCommand = (this.m_commandText != null) && (this.m_commandText.Length > 0);
            if (this.m_securityContext == null)
            {
                this.m_securityContext = SecurityContextProvider.DefaultProvider.CreateSecurityContext(this);
            }
            this.InitializeDatabaseConnection();
            this.InitializeDatabaseCommand();
        }

        public void AddParameter(OracleAppenderParameter parameter)
        {
            this.m_parameters.Add(parameter);
        }

        protected virtual string GetLogStatement(LoggingEvent logEvent)
        {
            if (this.Layout == null)
            {
                this.ErrorHandler.Error("ADOAppender: No Layout specified.");
                return "";
            }
            StringWriter writer = new StringWriter(CultureInfo.InvariantCulture);
            this.Layout.Format(writer, logEvent);
            return writer.ToString();
        }

        private void InitializeDatabaseCommand()
        {
            if ((this.m_dbConnection != null) && this.m_usePreparedCommand)
            {
                try
                {
                    this.m_dbCommand = this.m_dbConnection.CreateCommand();
                    this.m_dbCommand.CommandText = this.m_commandText;
                    this.m_dbCommand.CommandType = this.m_commandType;
                }
                catch (Exception exception)
                {
                    this.ErrorHandler.Error("Could not create database command [" + this.m_commandText + "]", exception);
                    if (this.m_dbCommand != null)
                    {
                        try
                        {
                            this.m_dbCommand.Dispose();
                        }
                        catch
                        {
                        }
                        this.m_dbCommand = null;
                    }
                }
                if (this.m_dbCommand != null)
                {
                    try
                    {
                        foreach (OracleAppenderParameter parameter in this.m_parameters)
                        {
                            try
                            {
                                parameter.Prepare(this.m_dbCommand);
                            }
                            catch (Exception exception2)
                            {
                                this.ErrorHandler.Error("Could not add database command parameter [" + parameter.ParameterName + "]", exception2);
                                throw;
                            }
                        }
                    }
                    catch
                    {
                        try
                        {
                            this.m_dbCommand.Dispose();
                        }
                        catch
                        {
                        }
                        this.m_dbCommand = null;
                    }
                }
                if (this.m_dbCommand != null)
                {
                    try
                    {
                        this.m_dbCommand.Prepare();
                    }
                    catch (Exception exception3)
                    {
                        this.ErrorHandler.Error("Could not prepare database command [" + this.m_commandText + "]", exception3);
                        try
                        {
                            this.m_dbCommand.Dispose();
                        }
                        catch
                        {
                        }
                        this.m_dbCommand = null;
                    }
                }
            }
        }

        private void InitializeDatabaseConnection()
        {
            try
            {
                this.m_dbConnection = new OracleConnection();
                this.m_dbConnection.ConnectionString = this.m_connectionString;
                using (this.SecurityContext.Impersonate(this))
                {
                    this.m_dbConnection.Open();
                }
            }
            catch (Exception exception)
            {
                this.ErrorHandler.Error("Could not open database connection [" + this.m_connectionString + "]", exception);
                this.m_dbConnection = null;
            }
        }

        protected override void OnClose()
        {
            base.OnClose();
            if (this.m_dbCommand != null)
            {
                this.m_dbCommand.Dispose();
                this.m_dbCommand = null;
            }
            if (this.m_dbConnection != null)
            {
                this.m_dbConnection.Close();
                this.m_dbConnection = null;
            }
        }

        protected virtual Type ResolveConnectionType()
        {
            Type type;
            try
            {
                type = SystemInfo.GetTypeFromString(this.m_connectionType, true, false);
            }
            catch (Exception exception)
            {
                this.ErrorHandler.Error("Failed to load connection type [" + this.m_connectionType + "]", exception);
                throw;
            }
            return type;
        }

        protected override void SendBuffer(LoggingEvent[] events)
        {
            if (this.m_reconnectOnError && ((this.m_dbConnection == null) || (this.m_dbConnection.State != ConnectionState.Open)))
            {
                LogLog.Debug(declaringType, "OracleAppender: Attempting to reconnect to database. Current Connection State: " + ((this.m_dbConnection == null) ? "<null>" : this.m_dbConnection.State.ToString()));
                this.InitializeDatabaseConnection();
                this.InitializeDatabaseCommand();
            }
            if ((this.m_dbConnection != null) && (this.m_dbConnection.State == ConnectionState.Open))
            {
                if (this.m_useTransactions)
                {
                    OracleTransaction dbTran = null;
                    try
                    {
                        dbTran = this.m_dbConnection.BeginTransaction();
                        this.SendBuffer(dbTran, events);
                        dbTran.Commit();
                    }
                    catch (Exception exception)
                    {
                        if (dbTran != null)
                        {
                            try
                            {
                                dbTran.Rollback();
                            }
                            catch (Exception)
                            {
                            }
                        }
                        this.ErrorHandler.Error("Exception while writing to database", exception);
                    }
                }
                else
                {
                    this.SendBuffer(null, events);
                }
            }
        }

        protected virtual void SendBuffer(OracleTransaction dbTran, LoggingEvent[] events)
        {
            if (!this.m_usePreparedCommand)
            {
                throw new NotImplementedException();
            }
            if (this.m_dbCommand != null)
            {
                ArrayList list = null;
                foreach (OracleAppenderParameter parameter in this.m_parameters)
                {
                    list = new ArrayList();
                    OracleParameter parameter2 = this.m_dbCommand.Parameters[parameter.ParameterName];
                    foreach (LoggingEvent event2 in events)
                    {
                        object obj2 = parameter.Layout.Format(event2);
                        if (obj2.ToString() == "(null)")
                        {
                            obj2 = DBNull.Value;
                        }
                        list.Add(obj2);
                    }
                    parameter2.Value = list.ToArray();
                }
                this.m_dbCommand.ArrayBindCount = events.Length;
                this.m_dbCommand.ExecuteNonQuery();
            }
        }

        // Properties
        public string CommandText
        {
            get
            {
                return this.m_commandText;
            }
            set
            {
                this.m_commandText = value;
            }
        }

        public CommandType CommandType
        {
            get
            {
                return this.m_commandType;
            }
            set
            {
                this.m_commandType = value;
            }
        }

        protected OracleConnection Connection
        {
            get
            {
                return this.m_dbConnection;
            }
            set
            {
                this.m_dbConnection = value;
            }
        }

        public string ConnectionString
        {
            get
            {
                return this.m_connectionString;
            }
            set
            {
                this.m_connectionString = value;
            }
        }

        public bool ReconnectOnError
        {
            get
            {
                return this.m_reconnectOnError;
            }
            set
            {
                this.m_reconnectOnError = value;
            }
        }

        public SecurityContext SecurityContext
        {
            get
            {
                return this.m_securityContext;
            }
            set
            {
                this.m_securityContext = value;
            }
        }

        public bool UseTransactions
        {
            get
            {
                return this.m_useTransactions;
            }
            set
            {
                this.m_useTransactions = value;
            }
        }
    }
}
