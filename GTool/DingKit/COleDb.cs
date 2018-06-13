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
/// ������Ҫִ�����ݿ�ײ����(��Ҫweb.Config�ж���OleDbConnString)
/// </summary>
    public class COleDb
	{
		
        /// <summary>
        /// ���ݿ������ַ���
        /// </summary>
        public static string ConnString = CConfig.GetValueByKey("oledbConnString");// @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source="+System.Web.HttpContext.Current.Server.MapPath("..\\..\\Uploads\\KSTDD")+";Jet OLEDB:Engine Type=82;";

//CConfig.GetValueByKey("OleDbConnString");

        /// <summary>
        /// ���ݿ����ӷ���
        /// </summary>
        public OleDbConnection Connection;

        /// <summary>
        /// �����ݿ�����
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
        /// �ر����ݿ�����
        /// </summary>
        public void DBClose()
        {
            Connection.Close();
        }

        #region ���÷���

        /// <summary>
        /// ��ȡ�µ�����ţ��û����ݱ�ţ�
        /// </summary>
        /// <param name="FieldName">�ֶ�</param>
        /// <param name="TableName">����</param>
        /// <returns>�µ������</returns>
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
        /// �ж���ֵ�Ƿ����
        /// </summary>
        /// <param name="strSql">������ֵֵSQL���</param>
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


        #region ���ݿ���ʲ���
        /// <summary>
        /// ���ݴ����SQL����ȡ��ӦDataSet
        /// </summary>
        /// <param name="strSQL">strSQL���</param>
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
        /// ���ݴ����SQL����ȡ��ӦDataSet������������������
        /// </summary>
        /// <param name="strSQL">strSQL���</param>
        /// <param name="TableName">��������</param>
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
        /// ���ݴ����SQL����ȡ��ӦDataReader
        /// </summary>
        /// <param name="strsql">strSQL���</param>
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
        /// ��ȡ�ֶ�����
        /// </summary>
        /// <param name="strsql">strSQL���</param>
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
        /// ִ��insert SQL ���,�����²����¼�����ֶε�ֵ
        /// </summary>
        /// <param name="strsql">SQL���</param>
        /// <returns>ִ�гɹ����</returns>
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
        /// ִ��SQL ���
        /// </summary>
        /// <param name="strsql">SQL���</param>
        /// <returns>ִ�гɹ����</returns>
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
        /// ִ��SQL ���,����Ӱ������
        /// </summary>
        /// <param name="strsql">SQL���</param>
        /// <returns>Ӱ�������</returns>
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
        /// ��ҳ��ʽ��ȡDataSet
        /// </summary>
        /// <param name="SQL">SQL ���</param>
        /// <param name="Page">��ǰҳ��</param>
        /// <param name="RecsPerPage">ÿҳ��¼��</param>
        /// <param name="ID">�����ֶ�</param>
        /// <param name="Sort">�����ֶ�</param>
        /// <returns>��ǰҳ�ļ�¼����</returns>
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


        #region ���ݲ��Ҳ���
        /// <summary>
        /// ���ݵ�ֵ��strSQL��ȡstring�͵�ֵ
        /// </summary>
        /// <param name="strSQL">��ֵ��strSQL���</param>
        /// <returns>��ֵ</returns>
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
        /// ���ݵ�ֵ��strSQL��ȡobject�͵�ֵ
        /// </summary>
        /// <param name="SQLString">��ֵ��strSQL���</param>
        /// <returns>��ѯ�����object��</returns>
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
        /// ���ҵ���ֵstring�ͷ���ֵ
        /// </summary>
        /// <param name="table">����</param>
        /// <param name="field">�ֶ�</param>
        /// <param name="sWhere">����</param>
        /// <returns>���ҽ��</returns>
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
        /// ���ҵ���ֵint�ͷ���ֵ
        /// </summary>
        /// <param name="table">����</param>
        /// <param name="field">�ֶ�</param>
        /// <param name="sWhere">����</param>
        /// <returns>���ҽ��</returns>
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
        /// ��ȡDataRow����Ӧ�ֶε�ֵ
        /// </summary>
        /// <param name="row">��</param>
        /// <param name="field">�ֶ�</param>
        /// <returns>ֵ</returns>
        public static string GetRowValue(DataRow row, string field)
        {
            if (row[field].ToString() == null)
                return "";
            else
                return row[field].ToString();
        }


        /// <summary>
        /// ��ѯָ��ֵ��Ӧ�����ƣ����ݱ�����Ҷ�Ӧ���ƣ�
        /// </summary>
        /// <param name="table">����</param>
        /// <param name="Filedname">��ѯ�ֶ���</param>
        /// <param name="Filedid">�����ֶ�</param>
        /// <param name="value">����ֵ</param>
        /// <returns>��ѯ�ֶ�ֵ</returns>
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



        #region DataGride�������

        /// <summary>
        /// ���ø�����SQL���󶨸�����DataGrid
        /// </summary>
        /// <param name="strSql">������SQL���</param>
        /// <param name="DG">������DataGrid</param>
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
        /// ��DataGrid�ж�ҳ������ɾ����ڵ�N��N>1��ҳ�Ҹ�ҳ������ȫ��ɾ��ʱ��ɾ����
        /// �ᵼ��ҳ����1���ٴΰ�ʱDataGrid��Ȼ��λ����ҳ����������
        /// �ʶ���˺�����ֹ����������֡�
        /// ��ִ��ɾ�������ٴΰ�ǰ�����ô˺������ɡ�
        /// </summary>
        /// <param name="DG">Ҫ������DataGrid����</param>
        /// <param name="DelCount">ɾ����¼����</param>
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
        /// ��DataGrid�ж�ҳ������ɾ����ڵ�N��N>1��ҳ�Ҹ�ҳֻ��1������ʱ��ɾ����
        /// �ᵼ��ҳ����1���ٴΰ�ʱDataGrid��Ȼ��λ����ҳ����������
        /// �ʶ���˺�����ֹ����������֡�
        /// ��ִ��ɾ�������ٴΰ�ǰ�����ô˺������ɡ�
        /// </summary>
        /// <param name="DG">Ҫ������DataGrid����</param>
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
        /// ���ø�����SQL���󶨸�����DataGrid
        /// ��������һ�н���UrlEncode����Ϊ�����Ĳ���
        /// ʹ�øú���ʱ����DataGrid�����е�URL�ֶδ��KeyField������
        /// </summary>
        /// <param name="strSql">������SQL���</param>
        /// <param name="DG">������DataGrid</param>
        /// <param name="ColumnName">��ҪUrlEncode������</param>
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

