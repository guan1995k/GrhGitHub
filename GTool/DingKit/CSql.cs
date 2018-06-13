using System;
using System.Collections;
using System.Configuration;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;
namespace DingKit
	
{
/// <summary>
/// ������Ҫִ�����ݿ�ײ����(��Ҫweb.Config�ж���sqlConnString)
/// </summary>
	public class CSql
	{
		
        /// <summary>
        /// ���ݿ������ַ���
        /// </summary>
        public static string ConnString = CConfig.GetValueByKey("sqlConnString");

        /// <summary>
        /// ���ݿ����ӷ���
        /// </summary>
        public SqlConnection Connection;

        /// <summary>
        /// �����ݿ�����
        /// </summary>
        public void DBOpen()
        {
            Connection = new SqlConnection(ConnString);
            Connection.Open();

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
                SqlDataAdapter objCmd = new SqlDataAdapter(strSQL, ConnString);
                objCmd.Fill(ds);
                objCmd.Dispose();
            }
            catch 
            {
                return null;
            }

            return ds;
        }
        public static DataTable CreateDataTable(String strSQL)
        {

            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter objCmd = new SqlDataAdapter(strSQL, ConnString);
                objCmd.Fill(dt);
                objCmd.Dispose();
            }
            catch
            {
                return null;
            }

            return dt;
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
                SqlDataAdapter objCmd = new SqlDataAdapter(strSQL, ConnString);
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
        public static SqlDataReader CreateReader(string strsql)
        {
            SqlDataReader da;
            SqlConnection sqlconn = new SqlConnection(ConnString);
            try
            {
                SqlCommand cmdTable = new SqlCommand(strsql, sqlconn);
                cmdTable.CommandType = CommandType.Text;
                sqlconn.Open();
                da = cmdTable.ExecuteReader();
                cmdTable.Dispose();
                sqlconn.Close();
                sqlconn.Dispose();
                return da;
            }
            catch
            {
                return null;
            }
            finally
            {
                sqlconn.Close();
                sqlconn.Dispose();
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
                SqlConnection sqlconn = new SqlConnection(ConnString);
                SqlCommand cmdTable = new SqlCommand(strsql, sqlconn);
                cmdTable.CommandType = CommandType.Text;
                sqlconn.Open();
                cmdTable.ExecuteNonQuery();
                cmdTable.Dispose();
                sqlconn.Close();
                sqlconn.Dispose();
            }
            catch
            {
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
                SqlConnection sqlconn = new SqlConnection(ConnString);
                SqlCommand cmdTable = new SqlCommand(strsql, sqlconn);
                cmdTable.CommandType = CommandType.Text;
                sqlconn.Open();
                i = cmdTable.ExecuteNonQuery();
                cmdTable.Dispose();
                sqlconn.Close();
                sqlconn.Dispose();
            }
            catch 
            {
                return -1;
            }

            return i;
        }



        /// <summary>
        /// ִ�ж���SQL��䣬ʵ�����ݿ�����
        /// </summary>
        /// <param name="SQLStringList">����SQL���</param>		
        /// <returns>�ɹ����</returns>
        public static bool ExecuteSqlTran(ArrayList SQLStringList)
        {
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                SqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n].ToString();
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    tx.Rollback();
                    CFun.JsAlerT(e.Message.ToString());
                    return false;
                }
            }
            return true;
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


        /// <summary>
        /// ��ҳ��ʽ��ȡDataSet(ͨ���洢����P_GridPage)
        /// </summary>
        /// <param name="SQL">SQL ���</param>
        /// <param name="Page">��ǰҳ��</param>
        /// <param name="RecsPerPage">ÿҳ��¼��</param>
        /// <param name="ID">�����ֶ�</param>
        /// <param name="Sort">�����ֶ�</param>
        /// <returns>��ǰҳ�ļ�¼����</returns>
        public static DataSet GetPageDataSetByProc(string SQL, int Page, int RecsPerPage, string ID, string Sort)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection Connection = new SqlConnection(ConnString);
                SqlDataAdapter objCmd = new SqlDataAdapter("P_GridPage", Connection);
                objCmd.SelectCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter pSQL = new SqlParameter("@SQL", SqlDbType.VarChar, 4000);
                pSQL.Value = SQL;
                objCmd.SelectCommand.Parameters.Add(pSQL);

                SqlParameter pPage = new SqlParameter("@Page", SqlDbType.Int);
                pPage.Value = Page;
                objCmd.SelectCommand.Parameters.Add(pPage);

                SqlParameter pRecsPerPage = new SqlParameter("@RecsPerPage", SqlDbType.Int);
                pRecsPerPage.Value = RecsPerPage;
                objCmd.SelectCommand.Parameters.Add(pRecsPerPage);

                SqlParameter pID = new SqlParameter("@ID", SqlDbType.VarChar, 255);
                pID.Value = ID;
                objCmd.SelectCommand.Parameters.Add(pID);

                SqlParameter pSort = new SqlParameter("@Sort", SqlDbType.VarChar, 255);
                pSort.Value = Sort;
                objCmd.SelectCommand.Parameters.Add(pSort);

                objCmd.Fill(ds);
                objCmd.Dispose();
            }
            catch (Exception e)
            {
                CFun.JsAlerT(e.Message.ToString());
            }

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
            SqlConnection Connection = new SqlConnection(ConnString);
            string sReturn;
            try
            {
                Connection.Open();
                SqlCommand command = new SqlCommand(strSQL, Connection);
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
               
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
            catch
            {
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
            using (SqlConnection connection = new SqlConnection(ConnString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
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
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        CFun.JsAlerT(e.Message.ToString());
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

            SqlConnection Connection = new SqlConnection(ConnString);
            Connection.Open();
            SqlCommand command = new SqlCommand(sSQL, Connection);
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
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

            SqlCommand command = new SqlCommand(sSQL, Connection);
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
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

        #region �洢���̲���

        /// <summary>
        /// ִ�д洢����
        /// </summary>
        /// <param name="storedProcName">�洢������</param>
        /// <param name="parameters">�洢���̲���</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader RunProcedure(string storedProcName, IDataParameter[] parameters)
        {
            SqlConnection connection = new SqlConnection(ConnString);
            SqlDataReader returnReader;
            connection.Open();
            SqlCommand command = BuildCreateDataSetCommand(connection, storedProcName, parameters);
            command.CommandType = CommandType.StoredProcedure;
            returnReader = command.ExecuteReader();
            return returnReader;
        }


        /// <summary>
        /// ִ�д洢����
        /// </summary>
        /// <param name="storedProcName">�洢������</param>
        /// <param name="parameters">�洢���̲���</param>
        /// <param name="tableName">DataSet����еı���</param>
        /// <returns>DataSet</returns>
        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            using (SqlConnection connection = new SqlConnection(ConnString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildCreateDataSetCommand(connection, storedProcName, parameters);
                sqlDA.Fill(dataSet, tableName);
                connection.Close();
                return dataSet;
            }
        }


        /// <summary>
        /// ���� SqlCommand ����(��������һ���������������һ������ֵ)
        /// </summary>
        /// <param name="connection">���ݿ�����</param>
        /// <param name="storedProcName">�洢������</param>
        /// <param name="parameters">�洢���̲���</param>
        /// <returns>SqlCommand</returns>
        private static SqlCommand BuildCreateDataSetCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }
            return command;
        }

        /// <summary>
        /// ִ�д洢���̣�����Ӱ�������		
        /// </summary>
        /// <param name="storedProcName">�洢������</param>
        /// <param name="parameters">�洢���̲���</param>
        /// <param name="rowsAffected">Ӱ�������</param>
        /// <returns></returns>
        public static int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected)
        {
            using (SqlConnection connection = new SqlConnection(ConnString))
            {
                int result;
                connection.Open();
                SqlCommand command = BuildIntCommand(storedProcName, parameters);
                rowsAffected = command.ExecuteNonQuery();
                result = (int)command.Parameters["ReturnValue"].Value;
                //Connection.Close();
                return result;
            }
        }

        /// <summary>
        /// ���� SqlCommand ����ʵ��(��������һ������ֵ)	
        /// </summary>
        /// <param name="storedProcName">�洢������</param>
        /// <param name="parameters">�洢���̲���</param>
        /// <returns>SqlCommand ����ʵ��</returns>
        private static SqlCommand BuildIntCommand(string storedProcName, IDataParameter[] parameters)
        {
            SqlConnection connection = new SqlConnection(ConnString);
            SqlCommand command = BuildCreateDataSetCommand(connection, storedProcName, parameters);
            command.Parameters.Add(new SqlParameter("ReturnValue",
                SqlDbType.Int, 4, ParameterDirection.ReturnValue,
                false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return command;
        }
        #endregion

      

	}	



}

