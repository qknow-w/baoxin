using System;
using System.Data.OleDb;
using System.Reflection;
using System.IO;
using System.Data;

namespace BaoXin.Utility
{
    /// <summary>
    /// Microsoft Access database operations class.
    /// Copyright www.jishu.me
    /// </summary>
    public partial class AccessHelper:HelperBase
    {

        public new OleDbConnection Connection
        {
            get { return (OleDbConnection)base.Connection; }
            set { base.Connection = value; }
        }
        public new OleDbCommand Command
        {
            get { return (OleDbCommand)base.Command; }
            set { base.Command = value; }
        }

        string _accessFPath = get_defualt_dbpath();

        public virtual string AccessFPath
        {
            get
            {
                return _accessFPath;
            }
            set
            {
                _accessFPath = value;
            }
        }
        
        public AccessHelper()
        {
            this.Connection = new OleDbConnection();
            Command = Connection.CreateCommand();
        }

        public override void Open()
        {
            this.Connection = new OleDbConnection();
            Command = Connection.CreateCommand();
            base.ConnectionString= "Provider=Microsoft.Jet.Oledb.4.0;data source=" + AccessFPath;
            base.Open();
        }

        public AccessHelper(string accessfpath)
        {
            this.Connection = new OleDbConnection();
            Command = Connection.CreateCommand();
            this.AccessFPath = accessfpath;
            Open();
        }

        public OleDbParameter AddParameter(string ParameterName, OleDbType type, object value)
        {
            return AddParameter(ParameterName, type, value,ParameterDirection.Input);
        }

        public OleDbParameter AddParameter(string ParameterName, OleDbType type, object value, ParameterDirection direction)
        {
            OleDbParameter param = new OleDbParameter(ParameterName, type);
            param.Value = value;
            param.Direction = direction;
            Command.Parameters.Add(param);
            return param;
        }

        public OleDbParameter AddParameter(string ParameterName, OleDbType type, int size, object value)
        {
            return AddParameter(ParameterName, type, size, value, ParameterDirection.Input);
        }

        public OleDbParameter AddParameter(string ParameterName, OleDbType type, int size, object value, ParameterDirection direction)
        {
            OleDbParameter param = new OleDbParameter(ParameterName, type, size);
            param.Direction = direction;
            param.Value = value;
            Command.Parameters.Add(param);
            return param;
        }

        public void AddRangeParameters(OleDbParameter[] parameters)
        {
            Command.Parameters.AddRange(parameters);
        }
        public void InsertTable(TableFramework FrameWork)
        {
            if (FrameWork.TableName == null)
                FrameWork.TableName = TableName;
            if (FrameWork.TableName == null)
                throw new Exception("No Found TableName Attribute!");
            string sql = "insert into [" + FrameWork.TableName + "](";
            string Names = "";
            string Values = "";
            foreach (TableFramework.Column col in FrameWork)
            {
                if (Names != "")
                {
                    Names += ",";
                    Values += ",";
                }
                Names += "[" + col.Name + "]";
                if (col.CType == typeof(int) || col.CType == typeof(double) || col.CType == typeof(long) || col.CType == typeof(short) || col.CType == typeof(float) || col.CType == typeof(decimal))
                {
                    Values += col.Value;
                }
                else if (col.CType == typeof(string))
                {
                    Values += "'" + SafeText(col.Value.ToString()) + "'";
                }
                else if (col.CType == typeof(DateTime))
                {
                    Values += "#" + col.Value + "#";
                }
                else if (col.CType == typeof(bool))
                {
                    Values += col.Value.ToString();
                }
                else
                {
                    throw new Exception("I dont' know" + col.CType.Name + " is Nothign!");
                }
            }
            if (Names == "")
                throw new Exception("Table's FrameWork is not Empty！");
            sql += Names + ")values(" + Values + ")";
            Command.CommandText = sql;
            Command.ExecuteNonQuery();
        }
        public void UpdateTable(TableFramework FrameWork, string whereText)
        {
            if (FrameWork.TableName == null)
                FrameWork.TableName = TableName;
            if (FrameWork.TableName == null)
                throw new Exception("No Found TableName Attribute!");
            string sql = "update [" + FrameWork.TableName + "] set";
            string setText = "";
            foreach (TableFramework.Column col in FrameWork)
            {
                if (setText != "")
                {
                    setText += ",";
                }
                setText += "[" + col.Name + "]=";
                if (col.CType == typeof(int) || col.CType == typeof(double) || col.CType == typeof(long) || col.CType == typeof(short) || col.CType == typeof(float) || col.CType == typeof(decimal) || col.CType == typeof(long))
                {
                    setText += col.Value;
                }
                else if (col.CType == typeof(string))
                {
                    setText += "'" + SafeText(col.Value.ToString()) + "'";
                }
                else if (col.CType == typeof(DateTime))
                {
                    setText += "#" + col.Value + "#";
                }
                else if (col.CType == typeof(bool))
                {
                    setText += col.Value.ToString();
                }
                else
                {
                    throw new Exception("I dont' no " + col.CType.Name + " is Nothign!");
                }
            }
            if (setText == "")
                throw new Exception("Table's FrameWork is not Empty！");
            sql += setText + " " + whereText;
            Command.CommandText = sql;
            Command.ExecuteNonQuery();
        }

    
    }
}
