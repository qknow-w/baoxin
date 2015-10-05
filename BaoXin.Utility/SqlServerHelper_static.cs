/*
  copyright wuxiu, welcome download the last version of sqlhelper:http://www.wuxiu.org/
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;

namespace BaoXin.Utility
{
    public partial class SqlServerHelper
    {
        public static string default_connection_str = load_defaultConnectionstring();

        static string load_defaultConnectionstring()
        {
            System.Configuration.ConnectionStringSettings setting=ConfigurationManager.ConnectionStrings["SqlServerHelper"];
            if(setting==null)return null;
            return setting.ConnectionString;
        }

        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        public static int ExecuteNonQuery(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteNonQuery(default_connection_str, cmdType, cmdText, commandParameters);
        }

        public static int ExecuteNonQuery(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteNonQuery(default_connection_str, CommandType.Text, cmdText, commandParameters);
        }

        public static int ExecuteNonQueryProc(string StoredProcedureName, params SqlParameter[] commandParameters)
        {
            return ExecuteNonQuery(default_connection_str, CommandType.StoredProcedure, StoredProcedureName, commandParameters);
        }

        public static int ExecuteNonQuery(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        public static SqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch(Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }

        public static SqlDataReader ExecuteReader(SqlConnection conn,string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteReader(default_connection_str, CommandType.Text, cmdText, commandParameters);
        }

        public static SqlDataReader ExecuteReader(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.Default);
            cmd.Parameters.Clear();
            return rdr;
        }

        public static SqlDataReader ExecuteReader(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteReader(default_connection_str, CommandType.Text, cmdText, commandParameters);
        }

        public static SqlDataReader ExecuteReader(string cmdText, int pageSize, int currPage, string keyColumn, string OrderType, out int recordCount, string identity = "", params SqlParameter[] commandParameters)
        {
            string countSql = string.Format(";with t as({0})Select count(1) from t", cmdText);
            recordCount = Convert.ToInt32(ExecuteScalar(countSql, commandParameters));


            cmdText = CreatePagerSql(cmdText, pageSize, currPage, recordCount, keyColumn, OrderType, identity);

            return ExecuteReader(default_connection_str, CommandType.Text, cmdText, commandParameters);
        }

        public static SqlDataReader ExecuteReaderProc(string StoredProcedureName, params SqlParameter[] commandParameters)
        {
            return ExecuteReader(default_connection_str, CommandType.StoredProcedure, StoredProcedureName, commandParameters);
        }

        public static SqlDataReader ExecuteReader(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteReader(default_connection_str, cmdType, cmdText, commandParameters);
        }
        
        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        public static object ExecuteScalar(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteScalar(default_connection_str, CommandType.Text, cmdText, commandParameters);
        }

        public static object ExecuteScalarProc(string StoredProcedureName, params SqlParameter[] commandParameters)
        {
            return ExecuteScalar(default_connection_str, CommandType.StoredProcedure, StoredProcedureName, commandParameters);
        }

        public static object ExecuteScalar(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteScalar(default_connection_str, cmdType, cmdText, commandParameters);
        }

        public static object ExecuteScalar(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        public static object ExecuteScalar(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }
        
        public static void CacheParameters(string cacheKey, params SqlParameter[] commandParameters)
        {
            parmCache[cacheKey] = commandParameters;
        }

        public static SqlParameter[] GetCachedParameters(string cacheKey)
        {
            SqlParameter[] cachedParms = (SqlParameter[])parmCache[cacheKey];
            if (cachedParms == null)
                return null;
            SqlParameter[] clonedParms = new SqlParameter[cachedParms.Length];
            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (SqlParameter)((ICloneable)cachedParms[i]).Clone();
            return clonedParms;
        }

        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        public static DataTable ReadTable(SqlTransaction transaction, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, transaction.Connection, transaction, cmdType, cmdText, commandParameters);
            DataTable dt = HelperBase.ReadTable(cmd);
            cmd.Parameters.Clear();
            return dt;
        }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(default_connection_str);
        }

        public static DataTable ReadTable(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return ReadTable(connection, cmdType, cmdText, commandParameters);
            }
        }
        public static DataTable ReadTable(string cmdText, params SqlParameter[] commandParameters)
        {
            return ReadTable(CommandType.Text, cmdText, commandParameters);
        }
        public static DataTable ReadTable(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
             return ReadTable(default_connection_str, cmdType, cmdText, commandParameters);
        }

        public static DataTable ReadTable(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            DataTable dt = HelperBase.ReadTable(cmd);
            cmd.Parameters.Clear();
            return dt;
        }

        public static SqlParameter CreateInputParameter(string paramName, SqlDbType dbtype, object value)
        {
            return CreateParameter(ParameterDirection.Input, paramName, dbtype, 0, value);
        }
        public static SqlParameter CreateInputParameter(string paramName, SqlDbType dbtype,int size, object value)
        {
            return CreateParameter(ParameterDirection.Input, paramName, dbtype, size, value);
        }

        public static SqlParameter CreateOutputParameter(string paramName, SqlDbType dbtype)
        {
            return CreateParameter(ParameterDirection.Output, paramName, dbtype, 0, DBNull.Value);
        }

        public static SqlParameter CreateOutputParameter(string paramName, SqlDbType dbtype,int size)
        {
            return CreateParameter(ParameterDirection.Output, paramName, dbtype, size, DBNull.Value);
        }

        public static SqlParameter CreateParameter(ParameterDirection direction, string paramName, SqlDbType dbtype, int size,object value)
        {
            SqlParameter param = new SqlParameter(paramName, dbtype, size);
            if (value == null)
            {
                param.Value = DBNull.Value;
            }
            else
            {
                param.Value = value;
            }

            if (dbtype == SqlDbType.DateTime)
            {
                if (Convert.ToDateTime( value) < (new DateTime(1900,1,1)))
                {
                    param.Value = DBNull.Value;
                }
            }
            param.Direction = direction;
            return param;
        }

        /// <summary>
        /// 生成分页SQL语句(不支持参数sql里含CTE表达式的sql语句)
        /// </summary>
        /// <param name="sql">任意查询语句</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="currPage">当前页，即需要第几页的数据（从1开始）</param>
        /// <param name="keyColumn">排序字段</param>
        /// <param name="OrderType">排序方法，ASC或DESC</param>
        /// <param name="identity">标识字段，（此字段用于当排序字段出现大量重复相等数据时，增加排序的准确性）</param>
        /// <returns></returns>
        public static string CreatePagerSql(string sql, int pageSize, int currPage, int recordCount, string keyColumn, string OrderType, string identity = "")
        {
            int pageCount = (int)Math.Ceiling((double)recordCount / (double)pageSize);

            string strSql = string.Empty;
            string NOrderType;
            if (OrderType.ToLower().Equals("asc"))
                NOrderType = "desc";
            else if (OrderType.ToLower().Equals("desc"))
                NOrderType = "asc";
            else
                return string.Empty;
            if (currPage == 1)
            {
                strSql = String.Format("Select Top {0} * From ({1}) a order by {2} {3}", pageSize, sql, keyColumn, OrderType);
                if (!string.IsNullOrWhiteSpace(identity))
                {
                    strSql += string.Format(", {0} {1}", identity, OrderType);
                }
            }
            if (currPage > 1)
            {
                int outerTopCount = pageSize;
                if (currPage < pageCount)
                {
                }
                else // 最后一页
                {
                    outerTopCount = recordCount - (currPage - 1) * pageSize;
                }

                if (string.IsNullOrWhiteSpace(identity))
                {
                    strSql = String.Format("select * from (select top {0} * from (select top {1} * from ({2}) as a order by {3} {4}) as t order by {3} {5}) as a order by {3} {4}",
                    outerTopCount,
                    pageSize * currPage,
                    sql,
                    keyColumn, OrderType, NOrderType);
                }
                else
                {
                    strSql = String.Format("select * from (select top {0} * from (select top {1} * from ({2}) as a order by {3} {4},{6} {4}) as t order by {3} {5},{6} {5}) as a order by {3} {4},{6} {4}",
                    outerTopCount,
                    pageSize * currPage,
                    sql,
                    keyColumn,
                    OrderType,
                    NOrderType,
                    identity);
                }
            }
            return strSql;
        }

    }

}
