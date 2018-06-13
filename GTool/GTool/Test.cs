using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTool
{
    public partial class Test : Form
    {
        public Test()
        {
            InitializeComponent();
        }

        private void Test_Load(object sender, EventArgs e)
        {

            this.RV_Test.RefreshReport();
        }

        private void GetReport()
        {
            DataTable dt = new DataTable("SELECT * FROM Test1");
            RV_Test.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("Report1",dt));
            RV_Test.RefreshReport();
        }
    }
}
