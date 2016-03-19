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
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Common.Helper.Log
{
    public class OracleAppenderParameter
    {
        // Fields
        private DbType m_dbType;
        private bool m_inferType = true;
        private IRawLayout m_layout;
        private string m_parameterName;
        private byte m_precision = 0;
        private byte m_scale = 0;
        private int m_size = 0;

        // Methods
        public virtual void FormatValue(OracleCommand command, LoggingEvent loggingEvent)
        {
            OracleParameter parameter = command.Parameters[this.m_parameterName];
            parameter.Value = this.Layout.Format(loggingEvent);
        }

        public virtual void Prepare(OracleCommand command)
        {
            OracleParameter param = command.CreateParameter();
            param.ParameterName = this.m_parameterName;
            if (!this.m_inferType)
            {
                param.DbType = this.m_dbType;
            }
            if (this.m_precision != 0)
            {
                param.Precision = this.m_precision;
            }
            if (this.m_scale != 0)
            {
                param.Scale = this.m_scale;
            }
            if (this.m_size != 0)
            {
                param.Size = this.m_size;
            }
            command.Parameters.Add(param);
        }

        // Properties
        public DbType DbType
        {
            get
            {
                return this.m_dbType;
            }
            set
            {
                this.m_dbType = value;
                this.m_inferType = false;
            }
        }

        public IRawLayout Layout
        {
            get
            {
                return this.m_layout;
            }
            set
            {
                this.m_layout = value;
            }
        }

        public string ParameterName
        {
            get
            {
                return this.m_parameterName;
            }
            set
            {
                this.m_parameterName = value;
            }
        }

        public byte Precision
        {
            get
            {
                return this.m_precision;
            }
            set
            {
                this.m_precision = value;
            }
        }

        public byte Scale
        {
            get
            {
                return this.m_scale;
            }
            set
            {
                this.m_scale = value;
            }
        }

        public int Size
        {
            get
            {
                return this.m_size;
            }
            set
            {
                this.m_size = value;
            }
        }
    }
}
