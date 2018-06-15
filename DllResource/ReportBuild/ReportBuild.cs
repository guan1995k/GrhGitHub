using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Microsoft.Office.Interop.Excel;
using System.Data;
using System.Data.SqlClient;
using SqlOperation;

namespace ReportBuild
{
    public class ReportBuild
    {


        public static bool CreateByTemplate()
        {
            string TempletFileName = "D:/各DLL源文件/DllResource/dllTestStage/ExcelDemo/" + "测温报表模板5x7.xls";//模板文件
            string ReportFileName = "D:/各DLL源文件/DllResource/dllTestStage/ExcelOut/" + "_tmpAllRoomReport.xls";//导出文件
            string strTempletFile = Path.GetFileName(TempletFileName);
            //将模板文件复制到输出文件
            FileInfo mode = new FileInfo(TempletFileName);
            mode.CopyTo(ReportFileName, true);
            //打开excel
            object missing = Missing.Value;
            Microsoft.Office.Interop.Excel.Application app = null;
            Workbook wb = null;
            Worksheet ws = null;
            Range r = null;
            //
            try
            {
                app = new Microsoft.Office.Interop.Excel.Application();
                wb = app.Workbooks.Open(ReportFileName, false, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);
                app.Visible = true;
                //得到WorkSheet对象
                ws = (Worksheet)wb.Worksheets.get_Item(1);
                string Numsqlstr = "SELECT linepointnum,longlinenum,widelinenum,encirclenum,linesnum,encirclelinepointnum,house,linestartno FROM LineInfor WHERE houseno='14'";
                string Datasqlstr = "SELECT [time],[houseNo],[linesNo],[pointNo],[temp] FROM HousePosTempData WHERE [time]='2018-06-06 09:26:57.000' AND [houseNo]='14' order by linesNo,pointNo";
                DataSet Numds = SqlFun.SOSelectGetDataSet(Numsqlstr);
                DataSet Datads = SqlFun.SOSelectGetDataSet(Datasqlstr);
                #region 利用三循环完成温度报表的数据输入
                int longlinenum = Convert.ToInt32(Numds.Tables[0].Rows[0][1]);//缆数
                int widelinenum = Convert.ToInt32(Numds.Tables[0].Rows[0][2]);//节点数
                int linepointnum = Convert.ToInt32(Numds.Tables[0].Rows[0][0]);//探头数
                int num = 0;
                int lineno = 0;
                int dslineno = 0;
                int dspoint = 0;
                int mergecell = 0;
                for (int col = 1; col <= widelinenum; col++)
                {
                    for (int row = 1; row <= longlinenum; row++)
                    {
                        for (int point = linepointnum; point >= 1; point--)
                        {
                            lineno = longlinenum * (col - 1) + row;
                            dslineno = Convert.ToInt32(Datads.Tables[0].Rows[num][2]);
                            dspoint = Convert.ToInt32(Datads.Tables[0].Rows[num][3]);
                            if (dslineno == lineno && dspoint == (5 - point))
                            {
                                ws.Cells[row * 5 + point, col + mergecell] = Datads.Tables[0].Rows[num++][4].ToString();
                            }
                        }
                    }
                    r = (Range)ws.Cells[6, col + mergecell];
                    if (r.MergeArea.Cells.Columns.Count > 1)
                    {
                        mergecell = mergecell + r.MergeArea.Cells.Columns.Count - 1;
                    }
                }
                #endregion
                //添加或修改WorkSheet里的数据
                //ws.Cells[1, 1] = "求积";
                //ws.Cells[2, 1] = "100";
                //ws.Cells[2, 2] = "100";
                ////代码里写个公式
                //r = (Range)ws.Cells[2, 3];
                //r.Formula = "=A2*B2";
                //输出Excel文件并退出
                wb.Save();
                wb.Close(null, null, null);
                app.Workbooks.Close();
            }
            catch
            {
                return false;
            }
            finally
            {
                app.Application.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(ws);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(wb);
                app.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                GC.Collect();
                ws = null;
                wb = null;
                app = null;
            }
        }
    }
}
