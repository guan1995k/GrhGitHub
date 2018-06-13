using System;
using System.Collections;
using System.Configuration;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.UI.WebControls;
namespace DingKit
	
{
/// <summary>
/// 该类主要执行数据库底层访问(需要web.Config中定义OleDbConnString)
/// </summary>
    public class COleDb
	{
		
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string ConnString = CConfig.GetValueByKey("oledbConnString");// @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source="+System.Web.HttpContext.Current.Server.MapPath("..\\..\\Uploads\\KSTDD")+";Jet OLEDB:Engine Type=82;";

//CConfig.GetValueByKey("OleDbConnString");

        /// <summary>
        /// 数据库连接方法
        /// </summary>
        public OleDbConnection Connection;

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        public void DBOpen()
        {
            Connection = new OleDbConnection(ConnString);
            Connection.Open();

        }

        public static void ChangDataSourse(string type)
        {
            if (type == "KSTDD")
            {
                ConnString = CConfig.GetValueByKey("oledbConnStringMark");
            }
            else
            {
                ConnString = CConfig.GetValueByKey("oledbConnString");
            }

        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void DBClose()
        {
            Connection.Close();
        }

        #region 公用方法

        /// <summary>
        /// 获取新的最大编号（用户数据编号）
        /// </summary>
        /// <param name="FieldName">字段</param>
        /// <param name="TableName">表名</param>
        /// <returns>新的最大编号</returns>
        public static int GetMaxID(string FieldName, string TableName)
        {
            string strsql = "select max(" + FieldName + ")+1 from " + TableName;
            object obj = GetObjectSingle(strsql);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }

        /// <summary>
        /// 判断数值是否存在
        /// </summary>
        /// <param name="strSql">单返回值值SQL语句</param>
        /// <returns>True or false</returns>
        public static bool Exists(string strSql)
        {
            string result = GetStringSingle(strSql);
            if (result == "" || result == "0")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

      
        #endregion


        #region 数据库访问操作
        /// <summary>
        /// 根据传入的SQL语句获取相应DataSet
        /// </summary>
        /// <param name="strSQL">strSQL语句</param>
        /// <returns>DataSet</returns>
        public static DataSet CreateDataSet(String strSQL)
        {

            DataSet ds = new DataSet();
            try
            {
                OleDbDataAdapter objCmd = new OleDbDataAdapter(strSQL, ConnString);
                objCmd.Fill(ds);
                objCmd.Dispose();
            }
            catch(Exception e)
            {
                string str =e.Message;
                return null;
            }

            return ds;
        }


        /// <summary>
        /// 根据传入的SQL语句获取相应DataSet，并按给定表名命名
        /// </summary>
        /// <param name="strSQL">strSQL语句</param>
        /// <param name="TableName">给定表名</param>
        /// <returns>DataSet</returns>
        public static DataSet CreateDataSet(String strSQL, string TableName)
        {

            DataSet ds = new DataSet();
            try
            {
                OleDbDataAdapter objCmd = new OleDbDataAdapter(strSQL, ConnString);
                objCmd.Fill(ds, TableName);
                objCmd.Dispose();
            }
            catch
            {
                return null;
            }

            return ds;
        }


        /// <summary>
        /// 根据传入的SQL语句获取相应DataReader
        /// </summary>
        /// <param name="strsql">strSQL语句</param>
        /// <returns>Reader</returns>
        public static OleDbDataReader CreateReader(string strsql)
        {
            OleDbDataReader da;
            OleDbConnection OleDbConn = new OleDbConnection(ConnString);
            try
            {
                OleDbCommand cmdTable = new OleDbCommand(strsql, OleDbConn);
                cmdTable.CommandType = CommandType.Text;
                OleDbConn.Open();
                da = cmdTable.ExecuteReader();
                cmdTable.Dispose();
                OleDbConn.Close();
                OleDbConn.Dispose();
                return da;
            }
            catch
            {
                return null;
            }
            finally
            {
                OleDbConn.Close();
                OleDbConn.Dispose();
            }
        }

        /// <summary>
        /// 获取字段名称
        /// </summary>
        /// <param name="strsql">strSQL语句</param>
        /// <returns>Reader</returns>
        public static ArrayList GetFieldNames(string strsql)
        {
            ArrayList arr = new ArrayList();
            OleDbDataReader da;
            OleDbConnection OleDbConn = new OleDbConnection(ConnString);
            try
            {
                OleDbCommand cmdTable = new OleDbCommand(strsql, OleDbConn);
                cmdTable.CommandType = CommandType.Text;
                OleDbConn.Open();
                da = cmdTable.ExecuteReader();
                for (int i = 0; i < da.FieldCount; i++)
                {
                    arr.Add(da.GetName(i));
                }
                cmdTable.Dispose();
                OleDbConn.Close();
                OleDbConn.Dispose();
                return arr;
            }
            catch
            {
                return null;
            }
            finally
            {
                OleDbConn.Close();
                OleDbConn.Dispose();
            }
        }

        /// <summary>
        /// 执行insert SQL 语句,返回新插入记录自增字段的值
        /// </summary>
        /// <param name="strsql">SQL语句</param>
        /// <returns>执行成功与否</returns>
        public static int ExecuteInsertSql(string strsql)
        {
            strsql += ";select @@IDENTITY";
            object obj = CSql.GetObjectSingle(strsql);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }



        /// <summary>
        /// 执行SQL 语句
        /// </summary>
        /// <param name="strsql">SQL语句</param>
        /// <returns>执行成功与否</returns>
        public static bool ExecuteSql(string strsql)
        {
            try
            {
                OleDbConnection OleDbConn = new OleDbConnection(ConnString);
                OleDbCommand cmdTable = new OleDbCommand(strsql, OleDbConn);
                cmdTable.CommandType = CommandType.Text;
                OleDbConn.Open();
                cmdTable.ExecuteNonQuery();
                cmdTable.Dispose();
                OleDbConn.Close();
                OleDbConn.Dispose();
            }
            catch(Exception e)
            {
                string msg = e.Message;
                return false;
            }

            return true;
        }

        /// <summary>
        /// 执行SQL 语句,返回影响行数
        /// </summary>
        /// <param name="strsql">SQL语句</param>
        /// <returns>影响的行数</returns>
        public static int ExecuteSqlWithCount(string strsql)
        {
            int i = -1;
            try
            {
                OleDbConnection OleDbConn = new OleDbConnection(ConnString);
                OleDbCommand cmdTable = new OleDbCommand(strsql, OleDbConn);
                cmdTable.CommandType = CommandType.Text;
                OleDbConn.Open();
                i = cmdTable.ExecuteNonQuery();
                cmdTable.Dispose();
                OleDbConn.Close();
                OleDbConn.Dispose();
            }
            catch 
            {
                return -1;
            }

            return i;
        }



       

        /// <summary>
        /// 分页方式获取DataSet
        /// </summary>
        /// <param name="SQL">SQL 语句</param>
        /// <param name="Page">当前页号</param>
        /// <param name="RecsPerPage">每页记录数</param>
        /// <param name="ID">索引字段</param>
        /// <param name="Sort">排序字段</param>
        /// <returns>当前页的记录集合</returns>
        public static DataSet GetPageDataSet(string SQL, int Page, int RecsPerPage, string ID, string Sort)
        {
            int iPage = RecsPerPage * (Page - 1);
            string sql = "";
            if (iPage == 0)
            {
                sql = "SELECT  TOP " + RecsPerPage + " * FROM (" + SQL + ") T  ORDER BY " + Sort;
            }
            else
            {
                sql = "SELECT  TOP " + RecsPerPage + " * FROM (" + SQL + ") T WHERE T." + ID + " NOT IN (SELECT  TOP  " + iPage.ToString() + "  " + ID + "  FROM (" + SQL + ") T9 ORDER BY " + Sort + ") ORDER BY " + Sort;

            } DataSet ds = new DataSet();
            ds = CSql.CreateDataSet(sql);

            return ds;

        }


       
        #endregion


        #region 数据查找操作
        /// <summary>
        /// 根据单值型strSQL获取string型单值
        /// </summary>
        /// <param name="strSQL">单值型strSQL语句</param>
        /// <returns>数值</returns>
        public static string GetStringSingle(String strSQL)
        {
            OleDbConnection Connection = new OleDbConnection(ConnString);
            string sReturn;
            try
            {
                Connection.Open();
                OleDbCommand command = new OleDbCommand(strSQL, Connection);
                OleDbDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
               
                if (reader.Read())
                {
                    sReturn = reader[0].ToString();
                    if (sReturn == null)
                    {
                        sReturn = "";
                    }
                }
                else
                {
                    sReturn = "";
                }

                reader.Close();
                command.Dispose();
                Connection.Close();
                Connection.Dispose();
            }
            catch(Exception e)
            {
                string str = e.Message;
                return "";
            }
            finally
            {
                Connection.Close();
                Connection.Dispose();
            }
            return sReturn;
        }

        /// <summary>
        /// 根据单值型strSQL获取object型单值
        /// </summary>
        /// <param name="SQLString">单值型strSQL语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetObjectSingle(string SQLString)
        {
            using (OleDbConnection connection = new OleDbConnection(ConnString))
            {
                using (OleDbCommand cmd = new OleDbCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch 
                    {
                        connection.Close();
   
                    }
                }
            }
            return null;
        }

       

        /// <summary>
        /// 查找单表单值string型返回值
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="field">字段</param>
        /// <param name="sWhere">条件</param>
        /// <returns>查找结果</returns>
        public static string Dlookup(string table, string field, string sWhere)
        {
            string sSQL = "SELECT " + field + " FROM " + table + " WHERE " + sWhere;

            OleDbConnection Connection = new OleDbConnection(ConnString);
            Connection.Open();
            OleDbCommand command = new OleDbCommand(sSQL, Connection);
            OleDbDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            string sReturn;

            if (reader.Read())
            {
                sReturn = reader[0].ToString();
                if (sReturn == null)
                    sReturn = "";
            }
            else
            {
                sReturn = "";
            }

            reader.Close();
            command.Dispose();
            Connection.Close();
            Connection.Dispose();
            return sReturn;
        }


        /// <summary>
        /// 查找单表单值int型返回值
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="field">字段</param>
        /// <param name="sWhere">条件</param>
        /// <returns>查找结果</returns>
        public int DlookupInt(string table, string field, string sWhere)
        {
            string sSQL = "SELECT " + field + " FROM " + table + " WHERE " + sWhere;

            OleDbCommand command = new OleDbCommand(sSQL, Connection);
            OleDbDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            int iReturn = -1;

            if (reader.Read())
            {
                iReturn = reader.GetInt32(0);
            }

            reader.Close();
            command.Dispose();
            return iReturn;
        }

      

        /// <summary>
        /// 获取DataRow中相应字段的值
        /// </summary>
        /// <param name="row">行</param>
        /// <param name="field">字段</param>
        /// <returns>值</returns>
        public static string GetRowValue(DataRow row, string field)
        {
            if (row[field].ToString() == null)
                return "";
            else
                return row[field].ToString();
        }


        /// <summary>
        /// 查询指定值对应的名称（根据编码查找对应名称）
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="Filedname">查询字段名</param>
        /// <param name="Filedid">条件字段</param>
        /// <param name="value">条件值</param>
        /// <returns>查询字段值</returns>
        public static string GetNamebyID(string table, string Filedname, string Filedid, string value)
        {
            DataSet ds = new DataSet();
            string sqlTxt = "SELECT " + Filedname + " from " + table + " where " + Filedid + "='" + value + "'";
            ds = CSql.CreateDataSet(sqlTxt);
            if (ds.Tables[0].Rows.Count > 0 && !(ds.Tables[0].Rows[0][0] is System.DBNull))
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            return "";
        }
        #endregion



        #region DataGride操作相关

        /// <summary>
        /// 利用给定的SQL语句绑定给定的DataGrid
        /// </summary>
        /// <param name="strSql">给定的SQL语句</param>
        /// <param name="DG">给定的DataGrid</param>
        public static bool BindDataGrid(string strSql,ref DataGrid DG)
        {
            try
            {
                DataSet DS = new DataSet();
                DS = CreateDataSet(strSql);
                DG.DataSource = DS.Tables[0].DefaultView;
                if (DG.CurrentPageIndex > DG.PageCount - 1)
                {
                    if (DG.PageCount > 0)
                        DG.CurrentPageIndex = DG.PageCount - 1;
                    else
                        DG.CurrentPageIndex = 0;
                }
                DG.DataBind();
            }
            catch
            {
                return false;
        
            }
            return true;
        }

        /// <summary>
        /// 若DataGrid有多页，而被删除项处于第N（N>1）页且该页条数据全部删除时，删除后
        /// 会导致页数减1，再次绑定时DataGrid仍然定位到该页，引发错误。
        /// 故定义此函数防止此种情况出现。
        /// 于执行删除语句后，再次绑定前，调用此函数即可。
        /// </summary>
        /// <param name="DG">要操作的DataGrid对象</param>
        /// <param name="DelCount">删除记录条数</param>
        public static void DeleteDGNotice(ref DataGrid DG, int DelCount)
        {
            if ((DG.CurrentPageIndex == DG.PageCount - 1) && DG.Items.Count == DelCount)
            {
                if (DG.CurrentPageIndex - 1 > 1)
                {
                    DG.CurrentPageIndex = DG.CurrentPageIndex - 1;
                }
                else
                {
                    DG.CurrentPageIndex = 0;
                }
            }
        }

        /// <summary>
        /// 若DataGrid有多页，而被删除项处于第N（N>1）页且该页只有1条数据时，删除后
        /// 会导致页数减1，再次绑定时DataGrid仍然定位到该页，引发错误。
        /// 故定义此函数防止此种情况出现。
        /// 于执行删除语句后，再次绑定前，调用此函数即可。
        /// </summary>
        /// <param name="DG">要操作的DataGrid对象</param>
        public static void DeleteDGNotice(ref DataGrid DG)
        {
            if ((DG.Items.Count % DG.PageSize == 1) && (DG.PageCount > 1))
            {
                if (DG.PageCount > 0)
                    DG.CurrentPageIndex = DG.CurrentPageIndex - 1;
                else
                    DG.CurrentPageIndex = 0;
            }
        }
        /// <summary>
        /// 利用给定的SQL语句绑定给定的DataGrid
        /// 并将其中一列进行UrlEncode以作为超链的参数
        /// 使用该函数时，将DataGrid超链列的URL字段处填“KeyField”即可
        /// </summary>
        /// <param name="strSql">给定的SQL语句</param>
        /// <param name="DG">给定的DataGrid</param>
        /// <param name="ColumnName">需要UrlEncode的列名</param>
        public static void BindDataGrid(string strSql, DataGrid DG, string ColumnName)
        {
            try
            {
                DataSet DS = new DataSet();
                DS = CreateDataSet(strSql);
                DS.Tables[0].Columns.Add("KeyField");
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    DS.Tables[0].Rows[i]["KeyField"] = HttpUtility.UrlEncode(DS.Tables[0].Rows[i][ColumnName].ToString().Trim());
                DG.DataSource = DS.Tables[0].DefaultView;
                if (DG.CurrentPageIndex > DG.PageCount - 1)
                {
                    if (DG.PageCount > 0)
                        DG.CurrentPageIndex = DG.PageCount - 1;
                    else
                        DG.CurrentPageIndex = 0;
                }
                DG.DataBind();
            }
            catch (Exception e)
            {
                CFun.JsAlerT(e.Message.ToString());
            }
            finally
            {
                //Close();
                //Dispose();
            }
        }
        #endregion

 

      

	}	



}

