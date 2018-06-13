using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GuanMethod;

namespace GTool
{
    public partial class GTool : Form
    {
        public GTool()
        {
            InitializeComponent();
            RTB_Content.Text = CFunctions.RemoveInvalidZero("P0000001");
        }

        #region 文件浏览
        //浏览按钮
        //浏览文件后将文件路径显示出来
        private void BT_Browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "请选择Excel表格";
            ofd.InitialDirectory = @"c:\";
            ofd.Filter = "Excel表格|*.xls;*.xlsx";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                TB_BrowseFileName.Text = ofd.FileName;
            }
        }

        #endregion

        #region Visual Studio

        private void BT_BuildSql_Click(object sender, EventArgs e)
        {
            RTB_Content.Clear();
            DataSet ds = CExcel.Import_Excel(TB_BrowseFileName.Text);
            if (ds != null)
            {
                int final = ds.Tables[0].Rows.Count - 1;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    if (i == 0)
                    {
                        RTB_Content.Text += CSql.GetDataTableNameString(TB_Name.Text) + "\n";
                    }
                    else if (i == ds.Tables[0].Rows.Count - 1)
                    {
                        RTB_Content.Text += CSql.GetDataTableColumnString(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][3].ToString(), true) + "\n";
                    }
                    else
                    {
                        RTB_Content.Text += CSql.GetDataTableColumnString(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][3].ToString(), false) + "\n";
                    }
                }
            }
        }

        private void BT_BuildCs_Click(object sender, EventArgs e)
        {
            RTB_Content.Clear();
            DataSet ds = CExcel.Import_Excel(TB_BrowseFileName.Text);
            if (ds != null)
            {
                string[] ColumnName = new string[ds.Tables[0].Rows.Count - 1];
                string[] ColumnMean = new string[ds.Tables[0].Rows.Count - 1];
                string[] ColumnStatus = new string[ds.Tables[0].Rows.Count - 1];
                for (int i = 0; i < ds.Tables[0].Rows.Count - 1; i++)
                {
                    ColumnName[i] = ds.Tables[0].Rows[i + 1][0].ToString();
                    ColumnMean[i] = ds.Tables[0].Rows[i + 1][2].ToString();
                    ColumnStatus[i] = ds.Tables[0].Rows[i + 1][3].ToString();
                }
                RTB_Content.Text += CSql.GetCsUsingString();
                RTB_Content.Text += CSql.GetCsClassHeadString(TB_Name.Text);
                RTB_Content.Text += CSql.GetCsClassShuXingString(ColumnName);
                RTB_Content.Text += CSql.GetCsClassRegionString(ColumnName, ColumnMean);
                RTB_Content.Text += CSql.GetCsClassFunctionString(ColumnName, TB_Name.Text, ColumnStatus);
            }
        }

        #endregion
        
        #region MH_SCADA

        private void BT_SqlInsert_Click(object sender, EventArgs e)
        {
            RTB_Content.Clear();
            DataSet ds = CExcel.Import_Excel(TB_BrowseFileName.Text);
            string[] ColumnName = new string[ds.Tables[0].Rows.Count - 1];
            string[] ColumnMean = new string[ds.Tables[0].Rows.Count - 1];
            string[] ColumnStatus = new string[ds.Tables[0].Rows.Count - 1];
            for (int i = 0; i < ds.Tables[0].Rows.Count - 1; i++)
            {
                ColumnName[i] = ds.Tables[0].Rows[i + 1][0].ToString();
                ColumnMean[i] = ds.Tables[0].Rows[i + 1][2].ToString();
                ColumnStatus[i] = ds.Tables[0].Rows[i + 1][3].ToString();
            }
            if (ds != null)
            {
                RTB_Content.Text = CSql.SCADA_Insert(ColumnName, TB_Name.Text, ColumnStatus);
            }
        }

        private void BT_SqlUpdate_Click(object sender, EventArgs e)
        {
            RTB_Content.Clear();
            DataSet ds = CExcel.Import_Excel(TB_BrowseFileName.Text);
            string[] ColumnName = new string[ds.Tables[0].Rows.Count - 1];
            string[] ColumnMean = new string[ds.Tables[0].Rows.Count - 1];
            string[] ColumnStatus = new string[ds.Tables[0].Rows.Count - 1];
            for (int i = 0; i < ds.Tables[0].Rows.Count - 1; i++)
            {
                ColumnName[i] = ds.Tables[0].Rows[i + 1][0].ToString();
                ColumnMean[i] = ds.Tables[0].Rows[i + 1][2].ToString();
                ColumnStatus[i] = ds.Tables[0].Rows[i + 1][3].ToString();
            }
            if (ds != null)
            {
                RTB_Content.Text = CSql.SCADA_Update(ColumnName, TB_Name.Text, ColumnStatus);
            }
        }

        private void BT_SqlSearch_Click(object sender, EventArgs e)
        {
            RTB_Content.Clear();
            DataSet ds = CExcel.Import_Excel(TB_BrowseFileName.Text);
            string[] ColumnName = new string[ds.Tables[0].Rows.Count - 1];
            string[] ColumnMean = new string[ds.Tables[0].Rows.Count - 1];
            string[] ColumnStatus = new string[ds.Tables[0].Rows.Count - 1];
            for (int i = 0; i < ds.Tables[0].Rows.Count - 1; i++)
            {
                ColumnName[i] = ds.Tables[0].Rows[i + 1][0].ToString();
                ColumnMean[i] = ds.Tables[0].Rows[i + 1][2].ToString();
                ColumnStatus[i] = ds.Tables[0].Rows[i + 1][3].ToString();
            }
            if (ds != null)
            {
                RTB_Content.Text = CSql.SCADA_Select(ColumnName, TB_Name.Text, ColumnMean);
            }
        }

        #endregion

        private void BT_SqlDelete_Click(object sender, EventArgs e)
        {
            RTB_Content.Clear();
            DataSet ds = CExcel.Import_Excel(TB_BrowseFileName.Text);
            string[] ColumnName = new string[ds.Tables[0].Rows.Count - 1];
            string[] ColumnMean = new string[ds.Tables[0].Rows.Count - 1];
            string[] ColumnStatus = new string[ds.Tables[0].Rows.Count - 1];
            for (int i = 0; i < ds.Tables[0].Rows.Count - 1; i++)
            {
                ColumnName[i] = ds.Tables[0].Rows[i + 1][0].ToString();
                ColumnMean[i] = ds.Tables[0].Rows[i + 1][2].ToString();
                ColumnStatus[i] = ds.Tables[0].Rows[i + 1][3].ToString();
            }
            if (ds != null)
            {
                RTB_Content.Text = CSql.SCADA_Delete(ColumnName, TB_Name.Text, ColumnStatus);
            }
        }

        private void BT_NotNull_Click(object sender, EventArgs e)
        {
            RTB_Content.Clear();
            DataSet ds = CExcel.Import_Excel(TB_BrowseFileName.Text);
            string[] ColumnName = new string[ds.Tables[0].Rows.Count - 1];
            string[] ColumnMean = new string[ds.Tables[0].Rows.Count - 1];
            string[] ColumnStatus = new string[ds.Tables[0].Rows.Count - 1];
            for (int i = 0; i < ds.Tables[0].Rows.Count - 1; i++)
            {
                ColumnName[i] = ds.Tables[0].Rows[i + 1][0].ToString();
                ColumnMean[i] = ds.Tables[0].Rows[i + 1][2].ToString();
                ColumnStatus[i] = ds.Tables[0].Rows[i + 1][3].ToString();
            }
            if (ds != null)
            {
                RTB_Content.Text = CSql.SCADA_NotNull(ColumnName, ColumnStatus);
            }
        }
    }
}
