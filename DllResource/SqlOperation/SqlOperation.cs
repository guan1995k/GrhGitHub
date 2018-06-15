using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace SqlOperation
{
    /// <summary>
    /// SQL Server操作类
    /// </summary>
    public class SqlFun
    {
        #region Connection连接
        public SqlFun()
        {

        }
        private static string SpareConnString = "Server = .\\SQLEXPRESS; database = OHGIMS; Persist Security Info=True;User ID=sa;Password=sasa";
        private static string ConnString;
        public static string CONNSTRING
        {
            set { SqlFun.ConnString = value; }
            get { return SqlFun.ConnString; }
        }

        #endregion

        #region Execute执行
        /// <summary>
        /// SQL执行SQL语句
        /// </summary>
        /// <param name="SqlStr">SQL语句</param>
        /// <returns>是否执行成功</returns>
        public static bool SOExecuteSqlStr(string SqlStr)
        {
            try
            {
                if (string.IsNullOrEmpty(ConnString))
                    ConnString = SpareConnString;
                SqlConnection Connection = new SqlConnection(ConnString);
                Connection.Open();
                SqlCommand sc = new SqlCommand(SqlStr, Connection);
                sc.CommandType = CommandType.Text;
                sc.ExecuteNonQuery();
                sc.Dispose();
                Connection.Close();
                Connection.Dispose();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region INSERT插入

        /// <summary>
        /// SQL操作INSERT(表名+值)
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="ColumnValue">值</param>
        /// <returns></returns>
        public static bool SOInsert(string TableName, string[] ColumnValue)
        {
            string values = "";
            foreach (string value in ColumnValue)
            {
                if (value != ColumnValue[0])
                    values += ",";
                values += "'" + value + "'";
            }
            string sqlstr = "INSERT INTO [" + TableName + "] VALUES(" + values + ")";
            if (SOExecuteSqlStr(sqlstr))
                return true;
            else
                return false;
        }
        /// <summary>
        /// SQL操作INSERT(表名+列名+值)
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="ColumnName">列名</param>
        /// <param name="ColumnValue">值</param>
        /// <returns></returns>
        public static bool SOInsert(string TableName, string[] ColumnName, string[] ColumnValue)
        {
            string names = "";
            foreach (string name in ColumnName)
            {
                if (name != ColumnName[0])
                    names += ",";
                names += name;
            }
            string values = "";
            foreach (string value in ColumnValue)
            {
                if (value != ColumnValue[0])
                    values += ",";
                values += "'" + value + "'";
            }
            string sqlstr = "INSERT INTO [" + TableName + "](" + names + ") VALUES(" + values + ")";
            if (SOExecuteSqlStr(sqlstr))
                return true;
            else
                return false;
        }
        #endregion

        #region UPDATE更新
        public static bool SOUpdate(string TableName, string[] ColumnName, string[] ColumnValue, string Where)
        {
            string update = "";
            for (int i = 0; i < ColumnName.Length; i++)
            {
                if (i != 0)
                    update += ",";
                update += ColumnName[i] + "='" + ColumnValue[i] + "'";
            }
            string sqlstr = "UPDATE [" + TableName + "] SET " + update + " WHERE " + Where;
            if (SOExecuteSqlStr(sqlstr))
                return true;
            else
                return false;
        }
        #endregion

        #region DELETE删除
        public static bool SODelete(string TableName, string Where)
        {
            string sqlstr = "DELETE FROM [" + TableName + "] WHERE " + Where;
            if (SOExecuteSqlStr(sqlstr))
                return true;
            else
                return false;
        }
        #endregion

        #region SELECT查询
        /// <summary>
        /// 根据SQL查询语句获取DataSet
        /// </summary>
        /// <param name="SqlStr">SQL查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet SOSelectGetDataSet(string SqlStr)
        {
            DataSet ds = new DataSet();
            try
            {
                if (string.IsNullOrEmpty(ConnString))
                    ConnString = SpareConnString;
                SqlDataAdapter sda = new SqlDataAdapter(SqlStr, ConnString);
                sda.Fill(ds);
                sda.Dispose();
            }
            catch
            {
                return null;
            }
            return ds;
        }
        /// <summary>
        /// 根据SQL查询语句获取DataTable
        /// </summary>
        /// <param name="SqlStr">SQL查询语句</param>
        /// <returns>DataTable</returns>
        public static DataTable SOSelectGetDataTable(string SqlStr)
        {
            DataTable dt = new DataTable();
            try
            {
                if (string.IsNullOrEmpty(ConnString))
                    ConnString = SpareConnString;
                SqlDataAdapter sda = new SqlDataAdapter(SqlStr, ConnString);
                sda.Fill(dt);
                sda.Dispose();
            }
            catch
            {
                return null;
            }
            return dt;
        }

        public static string GetStringSingle(string StrSql)
        {
            string Str;
            SqlConnection Connection = null;
            try
            {
                if (string.IsNullOrEmpty(ConnString))
                    ConnString = SpareConnString;
                Connection = new SqlConnection(ConnString);
                Connection.Open();
                SqlCommand sc = new SqlCommand(StrSql, Connection);
                SqlDataReader sdr = sc.ExecuteReader(CommandBehavior.SingleRow);
                if (sdr.Read())
                {
                    Str = sdr[0].ToString();
                    if (string.IsNullOrEmpty(Str))
                    {
                        Str = "";
                    }
                }
                else
                {
                    Str = "";
                }
                sdr.Close();
                sc.Dispose();
            }
            catch { return ""; }
            finally 
            { 
                Connection.Close();
                Connection.Dispose();
            }
            return Str;
        }
        #endregion
    }
}
