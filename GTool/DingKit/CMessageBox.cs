using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DingKit;
using System.Text;

/// <summary>
///CMessageBox 的摘要说明
/// </summary>
public class CMessageBox
{
    public const string Skin = "aero";
	public CMessageBox()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    /// <summary>
    /// 弹出框（不带按钮）
    /// </summary>
    /// <param name="page">当前Page对象</param>
    /// <param name="msg">消息</param>
    public static void  AlertBox(System.Web.UI.Page page,string msg)
    {
        string str = "art.dialog.alert('" + msg + "');";
        string js = CFun.FortmatJavaScript(str);

        if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "AlertBox"))
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "AlertBox", js);
        }
    }

    /// <summary>
    /// 出错提示框
    /// </summary>
    /// <param name="page">当前Page对象</param>
    /// <param name="msg">消息</param>
    public static void ErrorBox(System.Web.UI.Page page, string msg)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("document.onLoad=art.dialog({icon: 'error',skin: '" + Skin + "',content: '" + msg + "'});");

        string js = CFun.FortmatJavaScript(sb.ToString());

        if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "ErrorBox"))
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "ErrorBox", js);
        }
    }

    /// <summary>
    /// 重新登录对话框
    /// </summary>
    /// <param name="page">当前Page对象</param>
    /// <param name="msg">消息</param>
    /// <param name="Url">登录页面Url</param>
    public static void ReLoadBox(System.Web.UI.Page page, string msg,string Url)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("document.onLoad=art.dialog({icon: 'error',lock: true,skin: '" + Skin + "',window: 'top',content: '" + msg + "',yesText:'确定',yesFn:function(){window.top.location='" + Url + "';}});");

        string js = CFun.FortmatJavaScript(sb.ToString());

        if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "ReLoadBox"))
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "ReLoadBox", js);
        }
    }

    /// <summary>
    /// 警告框
    /// </summary>
    /// <param name="page">当前Page对象</param>
    /// <param name="msg">消息</param>
    public static void WornBox(System.Web.UI.Page page, string msg)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("document.onLoad=art.dialog({icon: 'alert',skin: '" + Skin + "',content: '" + msg + "'});");

        string js = CFun.FortmatJavaScript(sb.ToString());

        if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "ErrorBox"))
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "ErrorBox", js);
        }
    }


    /// <summary>
    /// 成功提示框
    /// </summary>
    /// <param name="page">当前Page对象</param>
    /// <param name="msg">消息</param>
    public static void SucceedBox(System.Web.UI.Page page, string msg)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("document.onLoad=art.dialog({icon: 'succeed',time:1,skin: '" + Skin + "',content: '" + msg + "'});");

        string js = CFun.FortmatJavaScript(sb.ToString());

        if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "SucceedBox"))
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "SucceedBox", js);
        }

    }


    public static string GetConfirmJs(string msg)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("art.dialog({icon: 'alert',lock: true,skin: '" + Skin + "',content: '" + msg + "',yesFn: true,noFn: function(){return false;}});");
        return sb.ToString(); 
    }
    /// <summary>
    /// 成功提示框
    /// </summary>
    /// <param name="msg">消息</param>
    public static void Reflesh(System.Web.UI.Page page)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("location.reload();");

        string js = CFun.FortmatJavaScript(sb.ToString());

        if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "Reflesh"))
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "Reflesh", js);
        }

    }

/// <summary>
/// 弹出进度条页面
/// </summary>
/// <param name="page">所在页面对象</param>
/// <param name="DoPage">页面名称（不含后缀）</param>
/// <param name="Title">标题</param>
    public static void OpenProcessPage(System.Web.UI.Page page,string DoUrl,string DoPage, string Title,string Reflash)
    {
        string str = "StartProcess('" + DoUrl + "', '" + DoPage + "', '" + Title + "','" + Reflash + "')";
        string js = CFun.FortmatJavaScript(str);

        if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "OpenProcessPage"))
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "OpenProcessPage", js);
        }
    }


    /// <summary>
    /// 弹出进度条页面
    /// </summary>
    /// <param name="page">所在页面对象</param>
    /// <param name="DoPage">页面名称（不含后缀）</param>
    /// <param name="Title">标题</param>
    public static void OpenProcessPage(System.Web.UI.Page page, string DoUrl, string DoPage, string Title)
    {
        string str = "StartProcessNoFlesh('" + DoUrl + "', '" + DoPage + "', '" + Title + "')";
        string js = CFun.FortmatJavaScript(str);

        if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "OpenProcessPage"))
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "OpenProcessPage", js);
        }
    }


    /// <summary>
    /// 弹出页面
    /// </summary>
    /// <param name="page">所在页面对象</param>
    /// <param name="DoPage">页面名称（不含后缀）</param>
    /// <param name="Title">标题</param>
    public static void OpenPage(System.Web.UI.Page page, string DoUrl, string Title)
    {
        string str = "OpenPage('" + DoUrl  + "', '" + Title + "')";
        string js = CFun.FortmatJavaScript(str);

        if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "OpenProcessPage"))
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "OpenProcessPage", js);
        }
    }

    /// <summary>
    /// 弹出页面
    /// </summary>
    /// <param name="page">所在页面对象</param>
    /// <param name="DoPage">页面名称（不含后缀）</param>
    /// <param name="Title">标题</param>
    public static void OpenPage(System.Web.UI.Page page, string DoUrl,string Title,string Reflash)
    {
        string str = "OpenPageNew('" + Title + "', '" + DoUrl + "','" + Reflash + "')";
        string js = CFun.FortmatJavaScript(str);

        if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "OpenProcessPage"))
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "OpenProcessPage", js);
        }
    }
    /*执行JS方法*/
    public static string JSFunction(string msg)
    {
        return "<script>" + msg + "</script>";
    }
    /*弹窗提示*/
    public static string JSAlert(string msg)
    {
        return "<script>alert('" + msg + "');</script>";
    }
    public static string JSAlert(string msg, string aspx)
    {
        return "<script>alert('" + msg + "');window.location.href=\"" + aspx + "\";</script>";
    }
}