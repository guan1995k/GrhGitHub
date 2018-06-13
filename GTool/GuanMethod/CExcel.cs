using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace GuanMethod
{
    public class CExcel
    {
        #region Excel转化成Datatable
        public static DataSet Import_Excel(string filePath)
        {
            string sqlconn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 12.0;HDR=NO;IMEX=1'";

            string sql = @"select * from [Sheet1$]";

            try
            {
                OleDbConnection conn = new OleDbConnection(sqlconn);
                OleDbDataAdapter adapter = new OleDbDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                if (ds.Tables[0].Rows[0][0].ToString() == "列名" && ds.Tables[0].Rows[0][1].ToString() == "数据类型" && ds.Tables[0].Rows[0][2].ToString() == "含义" && ds.Tables[0].Rows[0][3].ToString() == "备注")
                {
                    return ds;
                }
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }
}
