using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web;
//using System.Web.SessionState;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Web.UI.HtmlControls;
namespace DingKit
{
    /// <summary>
    /// 对于下拉框，单选框，复选框的处理
    /// </summary>

    public class CSelections
    {
        //public int count;
        //public int index;

        /// <summary>
        /// 构造函数
        /// </summary>
        public CSelections()
        {
            //count = 0;
            //index = 0;
        }


     
        /// <summary>
        /// 根据特定条件,下拉框数据加载
        /// </summary>
        /// <param name="DDL_Items">下拉框</param>
        /// <param name="tablename">表名</param>
        /// <param name="mtext">名称字段</param>
        /// <param name="mvalue">编号字段</param>
        /// <param name="RowName">条件字段名</param>
        /// <param name="oValue">条件字段值</param>
        public static void LoadSelection(ref System.Web.UI.WebControls.DropDownList DDL_Items, string tablename, string mtext, string mvalue, string RowName, string oValue)
        {
            DDL_Items.DataSource = GetSelections(tablename, RowName, oValue);
            DDL_Items.Items.Clear();
            DDL_Items.DataTextField = mtext;
            DDL_Items.DataValueField = mvalue;

            DDL_Items.DataBind();
        }

        /// <summary>
        /// 根据特定条件,下拉框数据加载
        /// </summary>
        /// <param name="DDL_Items">下拉框</param>
        /// <param name="tablename">表名</param>
        /// <param name="mtext">名称字段</param>
        /// <param name="mvalue">编号字段</param>
        /// <param name="RowName">条件字段名</param>
        /// <param name="oValue">条件字段值</param>
        public static void LoadSelectionBySql(ref System.Web.UI.WebControls.DropDownList DDL_Items, string sql, string mtext, string mvalue)
        {
            DDL_Items.DataSource = CSql.CreateDataSet(sql);
            DDL_Items.Items.Clear();
            DDL_Items.DataTextField = mtext;
            DDL_Items.DataValueField = mvalue;

            DDL_Items.DataBind();
        }



        /// <summary>
        /// 根据特定条件,下拉框数据加载
        /// </summary>
        /// <param name="DDL_Items">下拉框</param>
        /// <param name="tablename">表名</param>
        /// <param name="mtext">名称字段</param>
        /// <param name="mvalue">编号字段</param>
        /// <param name="ConStr">条件字段</param>
        public static void LoadSelection(ref System.Web.UI.WebControls.DropDownList DDL_Items, string tablename, string mtext, string mvalue, string ConStr)
        {

            string sqlTxt = "SELECT * FROM " + tablename + " where " + ConStr;
    
            DDL_Items.DataSource =  CSql.CreateDataSet(sqlTxt);
            DDL_Items.Items.Clear();
            DDL_Items.DataTextField = mtext;
            DDL_Items.DataValueField = mvalue;

            DDL_Items.DataBind();
        }

        /// <summary>
        /// 根据特定条件,下拉框数据加载
        /// </summary>
        /// <param name="DDL_Items">下拉框</param>
        /// <param name="tablename">表名</param>
        /// <param name="mtext">名称字段</param>
        /// <param name="mvalue">编号字段</param>
        /// <param name="ConStr">条件字段</param>
        /// <param name="defaultText">默认字段（插入第一行的字段）文本内容</param>
        /// <param name="defaultValue">默认字段（插入第一行的字段）值</param>
        public static void LoadSelection(ref System.Web.UI.WebControls.DropDownList DDL_Items, string tablename, string mtext, string mvalue, string ConStr, string defaultText, string defaultValue)
        {

            string sqlTxt = "SELECT * FROM " + tablename + " where " + ConStr;

            DataSet ds = new DataSet();
            ds = CSql.CreateDataSet(sqlTxt);
            //添加一行选择行
            DataRow dr = ds.Tables[0].NewRow();
            dr[mvalue] = defaultValue;
            dr[mtext] = defaultText;
            ds.Tables[0].Rows.InsertAt(dr, 0);

            DDL_Items.DataSource = ds.Tables[0];
            DDL_Items.Items.Clear();
            DDL_Items.DataTextField = mtext;
            DDL_Items.DataValueField = mvalue;

            DDL_Items.DataBind();
        }


        /// <summary>
        ///下拉框数据加载
        /// </summary>
        /// <param name="DDL_Items">下拉框</param>
        /// <param name="tablename">表名</param>
        /// <param name="mtext">名称字段</param>
        /// <param name="mvalue">编号字段</param>
        public static void LoadSelection(ref System.Web.UI.WebControls.DropDownList DDL_Items, string tablename, string mtext, string mvalue)
        {
            DDL_Items.DataSource = GetSelections(tablename);
            DDL_Items.Items.Clear();
            DDL_Items.DataTextField = mtext;
            DDL_Items.DataValueField = mvalue;

            DDL_Items.DataBind();
        }


        /// <summary>
        ///下拉框数据加载
        /// </summary>
        /// <param name="DDL_Items">下拉框</param>
        /// <param name="tablename">表名</param>
        /// <param name="mtext">名称字段</param>
        /// <param name="mvalue">编号字段</param>
        /// <param name="isTextShowCode">文本带编号显示</param>
        public static void LoadSelection(ref System.Web.UI.WebControls.DropDownList DDL_Items, string tablename, string mtext, string mvalue, bool isShowWithCode)
        {
            string sqlTxt = "";
            if (isShowWithCode == true)
            {
                sqlTxt = "SELECT " + mvalue + "+' | '+" + mtext + " as " + mtext + "," + mvalue + " FROM " + tablename + " order by " + mvalue;
            }
            else
            {
                sqlTxt = "SELECT " + mtext + "," + mvalue + " FROM " + tablename + " order by " + mvalue;
            }
            DDL_Items.DataSource = CSql.CreateDataSet(sqlTxt);
            DDL_Items.Items.Clear();
            DDL_Items.SelectedIndex = -1;
            DDL_Items.DataTextField = mtext;
            DDL_Items.DataValueField = mvalue;

            DDL_Items.DataBind();
        }

        /// <summary>
        /// 根据特定条件,下拉框数据加载
        /// </summary>
        /// <param name="DDL_Items">下拉框</param>
        /// <param name="tablename">表名</param>
        /// <param name="mtext">名称字段</param>
        /// <param name="mvalue">编号字段</param>
        /// <param name="ConStr">条件字段</param>
        /// <param name="isTextShowCode">文本带编号显示</param>
        public static void LoadSelection(ref System.Web.UI.WebControls.DropDownList DDL_Items, string tablename, string mtext, string mvalue, string ConStr, bool isShowWithCode)
        {
            string sqlTxt = "";
            if (isShowWithCode == true)
            {
                sqlTxt = "SELECT " + mvalue + "+' | '+" + mtext + " as " + mtext + "," + mvalue + " FROM " + tablename + " where " + ConStr + " order by " + mvalue;
            }
            else
            {
                sqlTxt = "SELECT " + mtext + "," + mvalue + " FROM " + tablename + " where " + ConStr + " order by " + mvalue;
            }
         
            DDL_Items.DataSource = CSql.CreateDataSet(sqlTxt);
            DDL_Items.Items.Clear();
            DDL_Items.SelectedIndex = -1;
            DDL_Items.DataTextField = mtext;
            DDL_Items.DataValueField = mvalue;

            DDL_Items.DataBind();
        }


        /// <summary>
        /// 单选框数据加载
        /// </summary>
        /// <param name="DDL_Items"></param>
        /// <param name="tablename">表名</param>
        /// <param name="mtext">名称字段</param>
        /// <param name="mvalue">编号字段</param>
        public static void LoadSelection(ref System.Web.UI.WebControls.RadioButtonList DDL_Items, string tablename, string mtext, string mvalue)
        {
            DDL_Items.DataSource = GetSelections(tablename);
            DDL_Items.Items.Clear();
            DDL_Items.SelectedIndex = -1;
            DDL_Items.DataTextField = mtext;
            DDL_Items.DataValueField = mvalue;
            DDL_Items.DataBind();
        }

       
        /// <summary>
        /// 复选框数据加载
        /// </summary>
        /// <param name="DDL_Items"></param>
        /// <param name="tablename">表名</param>
        /// <param name="mtext">名称字段</param>
        /// <param name="mvalue">编号字段</param>
        public static void LoadSelection(ref System.Web.UI.WebControls.CheckBoxList DDL_Items, string tablename, string mtext, string mvalue)
        {
            DDL_Items.DataSource = GetSelections(tablename);
            DDL_Items.Items.Clear();
            DDL_Items.SelectedIndex = -1;
            DDL_Items.DataTextField = mtext;
            DDL_Items.DataValueField = mvalue;
            DDL_Items.DataBind();
        }

        /// <summary>
        /// 根据表名数据获取
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <returns>数据集</returns>
        public static DataSet GetSelections(string TableName)
        {
            string sqlTxt = "SELECT * FROM " + TableName;
            return CSql.CreateDataSet(sqlTxt);
        }

        /// <summary>
        /// 特定条件信息获取
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="RowName">条件字段</param>
        /// <param name="mValue">条件值</param>
        /// <returns></returns>
        public static DataSet GetSelections(string TableName, string RowName, string mValue)
        {
            string sqlTxt = "SELECT * FROM " + TableName + " where " + RowName + " = '" + mValue + "'" + " order by " + mValue; ;
            return CSql.CreateDataSet(sqlTxt);
        }



        /// <summary>
        /// 根据编号获取对应名称
        /// </summary>
        /// <param name="TableName">表格名称</param>
        /// <param name="mText">名称字段</param>
        /// <param name="mID">编号字段</param>
        /// <param name="mValue">编号值</param>
        /// <returns>名称，-1：表示获取失败</returns>
        public static string GetTextByID(string TableName, string mText, string mID, string mValue)
        {
            string Text = "-1";
            string sql = "SELECT " + mText + " FROM " + TableName + " where " + mID + " = '" + mValue + "'";
            DataSet ds = new DataSet();
            ds = CSql.CreateDataSet(sql);
            if (ds.Tables[0].Rows.Count >= 0)
            {
                Text = ds.Tables[0].Rows[0][0].ToString();
            }

            return Text;
        }

        /// <summary>
        /// 根据名称获取对应编号
        /// </summary>
        /// <param name="TableName">表格名称</param>
        /// <param name="mText">名称字段</param>
        /// <param name="mID">编号字段</param>
        /// <param name="mValue">编号值</param>
        /// <returns>名称</returns>
        public static string GetIDByText(string TableName, string mText, string mID, string mValue)
        {
            string ID = "不详";
            string sql = "SELECT " + mID + " FROM " + TableName + " where " + mText + " = '" + mValue + "'";
            DataSet ds = new DataSet();
            ds = CSql.CreateDataSet(sql);
            if (ds.Tables[0].Rows.Count >= 0)
            {
                ID = ds.Tables[0].Rows[0][0].ToString();
            }

            return ID;
        }


        /// <summary>
        /// 根据值返回下拉框数据INDEX
        /// </summary>
        /// <param name="DDL_Items">下拉框</param>
        /// <param name="Value">值</param>
        /// <returns>INDEX</returns>
        public static int GetIndex(ref System.Web.UI.WebControls.DropDownList DDL_Items, string Value)
        {
            if (Value.Trim() == "")
                return 0;
            for (int i = 0; i < DDL_Items.Items.Count; i++)
            {
                if (DDL_Items.Items[i].Text.Trim() == Value.Trim())
                {
                    return i;
                }
            }
            return 0;
        }

        /// <summary>
        /// 根据值设定下拉框选中项
        /// </summary>
        /// <param name="DDL_Items">下拉框</param>
        /// <param name="Value">值</param>
        public static void SetIndex(ref System.Web.UI.WebControls.DropDownList DDL_Items, string Value)
        {
            DDL_Items.SelectedIndex = -1;
            DDL_Items.SelectedValue = Value;
        }



        /// <summary>
        /// 根据值设定复选框选中项
        /// </summary>
        /// <param name="DDL_Items">复选框</param>
        /// <param name="Value">值</param>
        public static void SetIndex(ref System.Web.UI.WebControls.CheckBoxList DDL_Items, string Value)
        {
            for (int i = 0; i < DDL_Items.Items.Count; i++)
            {
                if (DDL_Items.Items[i].Value.Trim() == Value.Trim())
                {
                    DDL_Items.Items[i].Selected = true;
                }
            }
        }

       
        /// <summary>
        /// 根据值设定单选框选中项
        /// </summary>
        /// <param name="RB_Items">单选框</param>
        /// <param name="Value">值</param>
        public static void SetIndex(ref System.Web.UI.WebControls.RadioButtonList RB_Items, string Value)
        {
            if (RB_Items.SelectedIndex > 0)
            {
                RB_Items.Items[RB_Items.SelectedIndex].Selected = false;
            }
            if (Value.Trim() == "")
                RB_Items.Items[0].Selected = true;
            else
            {
                for (int i = 0; i < RB_Items.Items.Count; i++)
                {
                    if (RB_Items.Items[i].Value.Trim() == Value.Trim())
                    {
                        RB_Items.Items[i].Selected = true;
                        break;
                    }
                }
            }

        }

    }
}
