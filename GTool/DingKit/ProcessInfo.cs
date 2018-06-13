using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public class CProcessInfo
{
    public bool IsReady { get; set; }
    public int Current { get; set; }
    public int TotalCount{ get; set; }
    public string  Message { get; set; }
  
}
