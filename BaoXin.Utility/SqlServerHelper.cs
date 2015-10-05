using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Reflection;

namespace BaoXin.Utility
{
    /// <summary>
    /// SqlServer database operations class.
    /// </summary>
    public partial class SqlServerHelper : HelperBase
    {
        bool isclose = true;
        public new SqlConnection Connection
        {
            get { return (SqlConnection)base.Connection; }
            set { base.Connection = value; }
        }
        public new SqlCommand Command
        {
            get { return (SqlCommand)base.Command; }
            set { base.Command = value; }
        }

        public SqlServerHelper(SqlConnection conn)
        {
            base.Connection = conn;
            isclose = false;
            base.Command = conn.CreateCommand();
        }

        public SqlServerHelper()
        {
            base.ConnectionString = SqlServerHelper.default_connection_str;
            Connection = new SqlConnection();
            base.Command = Connection.CreateCommand();
        }

        public SqlServerHelper(int ConnectionStringsIndex)
        {
            ConnectionString = ConfigurationManager.ConnectionStrings[ConnectionStringsIndex].ConnectionString;
            Connection = new SqlConnection();
            base.Command = Connection.CreateCommand();
        }

        public SqlServerHelper(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
            Connection = new SqlConnection();
            base.Command = Connection.CreateCommand();
        }

        public SqlParameter AddParameter(string ParameterName, SqlDbType type, object value)
        {
            return AddParameter(ParameterName, type, value, ParameterDirection.Input);
        }
        public SqlParameter AddParameter(string ParameterName, object value)
        {
            if (value == null) value = DBNull.Value;
            SqlParameter param = new SqlParameter(ParameterName, value);
            Command.Parameters.Add(param);
            return param;
        }

        public SqlParameter AddParameter(string ParameterName, SqlDbType type, object value, ParameterDirection direction)
        {
            if (value == null) value = DBNull.Value;
            SqlParameter param = new SqlParameter(ParameterName, type);
            param.Value = value;
            param.Direction = direction;
            Command.Parameters.Add(param);
            return param;
        }

        public SqlParameter AddParameter(string ParameterName, SqlDbType type,int size, ParameterDirection direction)
        {
            SqlParameter param = new SqlParameter(ParameterName, type,size);
            param.Direction = direction;
            Command.Parameters.Add(param);
            return param;
        }

        public SqlParameter AddParameter(string ParameterName, SqlDbType type, int size, object value)
        {
            return AddParameter(ParameterName, type, size, value, ParameterDirection.Input);
        }

        public SqlParameter AddParameter(string ParameterName, SqlDbType type, int size, object value, ParameterDirection direction)
        {
            if (value == null) value = DBNull.Value;
            SqlParameter param = new SqlParameter(ParameterName, type, size);
            param.Direction = direction;
            param.Value = value;
            Command.Parameters.Add(param);
            return param;
        }

        public void AddRangeParameters(SqlParameter[] parameters)
        {
            Command.Parameters.AddRange(parameters);
        }

        public bool InsertTable(TableFramework FrameWork)
        {
            return InsertTable(FrameWork, true);
        }
        public bool InsertTable(TableFramework FrameWork, bool newCommand)
        {
            SqlCommand cmd;
            if (newCommand)
            {
                cmd = (SqlCommand)Connection.CreateCommand();
                cmd.Transaction = this.Command.Transaction;
            }
            else cmd = Command;


            if (FrameWork.TableName == null)
                FrameWork.TableName = TableName;
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
                Values += "@" + col.Name;
                object v = col.Value == null ? DBNull.Value : col.Value;
                if (col.CType == typeof(string))
                {
                    cmd.Parameters.Add(new SqlParameter("@" + col.Name, v));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@" + col.Name, v));
                }
            }
            if (Names == "")
                throw new Exception("Table's FrameWork is not Empty！");
            sql += Names + ")values(" + Values + ")";
            cmd.CommandText = sql;
            return cmd.ExecuteNonQuery() > 0;
        }
        public int Insert<T>(T t, string tname)
        {
            string sql_fields = "";
            string sql_vals = "";
            bool isfirstfield = true;
            
            foreach (PropertyInfo pinfo in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (pinfo.GetCustomAttributes(typeof(BaoXin.Utility.Attributes.NoInsertField), false).Length > 0)
                {
                    continue;
                }
                object param_val = pinfo.GetValue(t, null);
                if (param_val == null) param_val = DBNull.Value;
                if (!isfirstfield)
                {
                    sql_fields += ",";
                    sql_vals += ",";
                }
                else
                {
                    isfirstfield = false;
                }
                sql_fields += "[" + pinfo.Name + "]";
                sql_vals += "@" + pinfo.Name + "";
                AddParameter(pinfo.Name, param_val);
            }
            Command.CommandText = "insert into [" + tname + "](" + sql_fields + ")values(" + sql_vals + ")";
            return Command.ExecuteNonQuery();
        }
        public bool UpdateTable(TableFramework FrameWork, string whereText, params SqlParameter[] ps)
        {
            return UpdateTable(FrameWork, whereText, true, ps);
        }
        public bool UpdateTable(TableFramework FrameWork, string whereText, bool newcommand)
        {
            return UpdateTable(FrameWork, whereText, newcommand,new SqlParameter[]{});
        }
        public bool UpdateTable(TableFramework FrameWork, string whereText, bool newcommand, params SqlParameter[] ps)
        {
            if (FrameWork.TableName == null)
                FrameWork.TableName = TableName;

            SqlCommand cmd;
            if (newcommand) cmd = (SqlCommand)Connection.CreateCommand();
            else cmd = Command;
            string sql = "update [" + FrameWork.TableName + "] set";
            string setText = "";
            foreach (TableFramework.Column col in FrameWork)
            {
                if (setText != "")
                {
                    setText += ",";
                }
                setText += "[" + col.Name + "]=@" + col.Name;
                object v = col.Value == null ? DBNull.Value : col.Value;
                cmd.Parameters.Add(new SqlParameter("@" + col.Name, v));
            }
            if (setText == "")
                throw new Exception("Table's FrameWork is not Empty！");
            sql += setText + " " + whereText;
            cmd.CommandText = sql;
            cmd.Parameters.AddRange(ps);
            int i = cmd.ExecuteNonQuery();
            return i > 0;
        }

        public T ReadObj<T>(SqlDataReader reader,out bool isread)
        {
            if (reader.Read())
            {
                T r = Activator.CreateInstance<T>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string columnName = reader.GetName(i);
#if RELEASE
                    try{
#endif
                    PropertyInfo attr = typeof(T).GetProperty(columnName, System.Reflection.BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    object v = reader[i];
                    if (v == DBNull.Value) v = null;
                    if (attr != null)
                    {
                        //if (attr.PropertyType == typeof(string)) {v=v.ToString(); }
                        attr.SetValue(r, v, null);
                    }
                    else
                    {
                        FieldInfo fi = typeof(T).GetField(columnName);
                        if (fi != null)
                        {
                            if (fi.FieldType == typeof(string)&&v!=null) { v = v.ToString(); }
                            fi.SetValue(r, v);
                        }
                    }
#if RELEASE
                        }catch{}
#endif
                }
                isread = true;
                return r;
            }
            isread = false;
            return default(T);
        }

        public void Fill<T>(List<T> list)
        {
            SqlDataReader reader = this.Command.ExecuteReader();
            try
            {
                T t;
                bool isread;
                while (true)
                {
                    t = ReadObj<T>(reader, out isread);
                    if (isread)
                    {
                        list.Add(t);
                    }
                    else break;
                }
            }
            finally
            {
                reader.Close();
            }
        }

        public T ReadObj<T>()
        {
            SqlDataReader reader = this.Command.ExecuteReader();
            try
            {
                bool isread ;
                return ReadObj<T>(reader, out isread);
            }
            finally
            {
                reader.Close();
            }
        }
    }
}
