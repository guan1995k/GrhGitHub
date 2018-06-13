using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace GuanMethod
{
    public class CSql
    {
        #region SQL数据库连接字段
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        static string SqlServer = ".\\SQLEXPRESS";//服务器名称
        static string Database = "TestData";//数据库名称
        static bool PSI = true;//是否保存安全信息(例如：密码)true或false
        static string UserId = "sa";//用户
        static string Password = "sasa";//密码
        public static string ConnString = "Server = " + SqlServer + "; database = " + Database + "; Persist Security Info=" + PSI.ToString() + ";User ID=" + UserId + ";Password=" + Password;

        /// <summary>
        /// 数据库连接方法
        /// </summary>
        public static SqlConnection Connection;

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        static void DBOpen()
        {
            Connection = new SqlConnection(ConnString);
            Connection.Open();
        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        static void DBClose()
        {
            Connection.Close();
            Connection.Dispose();
        }
        #endregion

        #region SQL执行方法
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="StrSql">SQL语句</param>
        /// <returns>执行成功与否</returns>
        public static bool ExecuteSql(string StrSql)
        {
            try
            {
                DBOpen();
                SqlCommand sc = new SqlCommand(StrSql, Connection);
                sc.CommandType = CommandType.Text;
                sc.ExecuteNonQuery();
                sc.Dispose();
                DBClose();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 根据获取表信息的SQL语句获得相应的DataSet
        /// </summary>
        /// <param name="SqlStr">SQL语句</param>
        /// <returns>DataSet</returns>
        public static DataSet CreateDataSet(string SqlStr)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter(SqlStr, ConnString);
                sda.Fill(ds);
                sda.Dispose();
            }
            catch
            {
                return null;
            }
            return ds;
        }

        /// <summary>
        /// 根据获取表信息的SQL语句获得相应的DataTable
        /// </summary>
        /// <param name="SqlStr">SQL语句</param>
        /// <returns>DataTable</returns>
        public static DataTable CreateDataTable(string SqlStr)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter(SqlStr, ConnString);
                sda.Fill(dt);
                sda.Dispose();
            }
            catch
            {
                return null;
            }
            return dt;
        }

        /// <summary>
        /// 根据SQL查询语句判断是否存在该记录
        /// </summary>
        /// <param name="StrSql">SQL查询语句</param>
        /// <returns>该记录存在与否</returns>
        public static bool Exists(string StrSql)
        {
            string Result = GetStringSingle(StrSql);
            if (string.IsNullOrEmpty(Result) || Result == "0")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static string GetStringSingle(string StrSql)
        {
            string Str;
            try
            {
                DBOpen();
                SqlCommand sc = new SqlCommand(StrSql, Connection);
                SqlDataReader sdr = sc.ExecuteReader(CommandBehavior.SingleRow);
                if (sdr.Read())
                {
                    Str = sdr[0].ToString();
                    if (string.IsNullOrEmpty(Str))
                    {
                        Str = "";
                    }
                }
                else
                {
                    Str = "";
                }
                sdr.Close();
                sc.Dispose();
            }
            catch { return ""; }
            finally { DBClose(); }
            return Str;
        }
        #endregion

        #region SQL表生成功能
        /// <summary>
        /// SQL表生成语句的表名部分
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <returns></returns>
        public static string GetDataTableNameString(string TableName)
        {
            return "CREATE TABLE [dbo].[" + TableName + "](";
        }
        /// <summary>
        /// SQL表生成语句的列名部分
        /// </summary>
        /// <param name="ColumnName">列名</param>
        /// <param name="DataType">数据类型</param>
        /// <param name="IsNull">是否允许NULL值</param>
        /// <param name="IsFinal">是否为最后一列</param>
        /// <returns></returns>
        public static string GetDataTableColumnString(string ColumnName, string DataType, string Status, bool IsFinal)
        {
            string ColumnStr = "[" + ColumnName + "] " + DataType + " ";
            if (IsFinal)
            {
                if (CFunctions.IsInclude(Status, "[AK]"))
                    ColumnStr += "IDENTITY(1,1) ";
                if (CFunctions.IsInclude(Status, "[PK]"))
                {
                    ColumnStr += "PRIMARY KEY)";
                }
                else
                {
                    if (CFunctions.IsInclude(Status, "[NN]"))
                        ColumnStr += "Null)";
                    else
                        ColumnStr += "NOT NULL)";
                }
            }
            else
            {
                if (CFunctions.IsInclude(Status, "[AK]"))
                    ColumnStr += "IDENTITY(1,1) ";
                if (CFunctions.IsInclude(Status, "[PK]"))
                {
                    ColumnStr += "PRIMARY KEY,";
                }
                else
                {
                    if (CFunctions.IsInclude(Status, "[NN]"))
                        ColumnStr += "Null,";
                    else
                        ColumnStr += "NOT NULL,";
                }
            }
            return ColumnStr;
        }
        #endregion

        #region SQL数据库cs方法类

        #region cs方法类构成
        /// <summary>
        /// 生成cs类的引用部分
        /// </summary>
        /// <returns>using引用部分</returns>
        public static string GetCsUsingString()
        {
            string UsingStr = @"using System;
using System.Data;
using System.Text;
using GuanMethod;
";
            return UsingStr;
        }
        /// <summary>
        /// 建立公共类和构造方法
        /// </summary>
        /// <param name="ClassName">类名</param>
        /// <returns>公共类和构造方法</returns>
        public static string GetCsClassHeadString(string ClassName)
        {
            string ClassStr = @"
/// <summary>
/// " + ClassName + @"的类
/// </summary>

public class C" + ClassName + @"
{
    public C" + ClassName + @"()
    {
    }
";
            return ClassStr;
        }
        /// <summary>
        /// 生成属性定义
        /// </summary>
        /// <param name="Name">列名集合</param>
        /// <returns>属性定义</returns>
        public static string GetCsClassShuXingString(string[] Name)
        {
            string ShuXingStr = @"    #region 属性定义";
            foreach (string name in Name)
            {
                ShuXingStr += @"
    private string " + name + @"_;";
            }
            ShuXingStr += @"
    #endregion

";
            return ShuXingStr;

        }
        /// <summary>
        /// 生成域定义
        /// </summary>
        /// <param name="Name">列名集合</param>
        /// <param name="Mean">列名含义集合</param>
        /// <returns>域定义</returns>
        public static string GetCsClassRegionString(string[] Name, string[] Mean)
        {
            string RegionStr = @"    #region 域定义";
            for (int i = 0; i < Name.Length; i++)
            {
                RegionStr += @"
    /// <summary>
    /// " + Mean[i] + @"
    /// </summary>
    public string _" + Name[i] + @"
    {
        set { " + Name[i] + @"_ = value; }
        get { return " + Name[i] + @"_; }
    }
    ";
            }
            RegionStr += @" #endregion
";
            return RegionStr;
        }
        /// <summary>
        /// 生成成员方法定义
        /// </summary>
        /// <param name="Name">列名集合</param>
        /// <param name="TableName">表名</param>
        /// <returns>成员方法定义</returns>
        public static string GetCsClassFunctionString(string[] Name, string TableName, string[] Status)
        {
            string FunctionStr = @"
    #region 成员方法定义
    /// <summary>
    /// 添加一条数据
    /// </summary>
    " + GetSqlInsert(Name, TableName, Status) + @"
    
    /// <summary>
    /// 更新一条数据
    /// </summary>
    " + GetSqlUpdate(Name, TableName, Status) + @"

    /// <summary>
    /// 删除一条数据
    /// </summary>
    " + GetSqlDelete(Name, TableName, Status) + @"

    /// <summary>
    /// 得到一个数据列表
    /// </summary>
    " + GetSqlSelect(Name, TableName) + @"

    /// <summary>
    /// 得到一个对象实体
    /// </summary>
    " + GetSqlModel(Name, TableName, Status) + @"

    /// <summary>
    /// 判断是否存在该记录
    /// </summary>
    " + GetSqlExists(Name, TableName, Status) + @"
    #endregion
}
";
            return FunctionStr;
        }
        #endregion

        #region SQL语句构成
        /// <summary>
        /// 生成SQL数据库Insert语句
        /// </summary>
        /// <param name="Name">列名集合</param>
        /// <param name="TableName">表名</param>
        /// <returns>Insert语句</returns>
        static string GetSqlInsert(string[] Name, string TableName, string[] Status)
        {
            string InsertStr = @"public bool Add()
    {
        string StrSql = """";
        StrSql += ""INSERT INTO [" + TableName + "](";
            int num = 1;
            int i = 0;
            int j = 0;
            foreach (string name in Name)
            {
                if (!CFunctions.IsInclude(Status[i++], "[AK]"))
                {
                    if (num++ != Name.Length)
                    {
                        InsertStr += name + ",";
                    }
                    else
                    {
                        InsertStr += name;
                    }
                }
                else
                {
                    num++;
                }
            }
            InsertStr += @")"";
        StrSql += "" VALUES(""";
            num = 1;
            foreach (string name in Name)
            {
                if (!CFunctions.IsInclude(Status[j++], "[AK]"))
                {
                    if (num++ != Name.Length)
                    {
                        InsertStr += "+" + @"
        CSql.FormatInsert(_" + name + ", false)";
                    }
                    else
                    {
                        InsertStr += "+" + @"
        CSql.FormatInsert(_" + name + ", true)";
                    }
                }
                else
                {
                    num++;
                }
            }
            InsertStr += @";
        return CSql.ExecuteSql(StrSql);
    }";
            return InsertStr;
        }
        /// <summary>
        /// 生成SQL数据库Update语句
        /// </summary>
        /// <param name="Name">列名集合</param>
        /// <param name="TableName">表名</param>
        /// <returns>Update语句</returns>
        static string GetSqlUpdate(string[] Name, string TableName, string[] Status)
        {
            string UpdateStr = @"public bool Update(";
            int num = 1;
            foreach (string name in Name)
            {
                int j = 0;
                for (int i = 0; i < Name.Length; i++)
                {
                    if (CFunctions.IsInclude(Status[i], "[PK]"))
                        j++;
                }
                if (CFunctions.IsInclude(Status[num - 1], "[PK]"))
                {
                    if (num != j)
                    {
                        UpdateStr += "string " + name + ",";
                    }
                    else
                    {
                        UpdateStr += "string " + name;
                    }
                    num++;
                }
            }
            UpdateStr += @")
    {
        string StrSql = """";
        StrSql += ""UPDATE [" + TableName + @"]"";
        StrSql += "" SET """;
            num = 1;
            int m = 0;
            foreach (string name in Name)
            {
                if (!CFunctions.IsInclude(Status[m++], "[AK]"))
                {
                    if (num++ != Name.Length)
                    {
                        UpdateStr += "+" + @"
        CSql.FormatUpdate(""" + name + "\",_" + name + ", false)";
                    }
                    else
                    {
                        UpdateStr += "+" + @"
        CSql.FormatUpdate(""" + name + "\",_" + name + ", true);";
                    }
                }
                else
                {
                    num++;
                }
            }
            UpdateStr += @"
        StrSql += "" WHERE """;
            num = 1;
            foreach (string name in Name)
            {
                int j = 0;
                for (int i = 0; i < Name.Length; i++)
                {
                    if (CFunctions.IsInclude(Status[i], "[PK]"))
                        j++;
                }
                if (CFunctions.IsInclude(Status[num - 1], "[PK]"))
                {
                    if (num != j)
                    {
                        UpdateStr += "+" + @"
        CSql.FormatWhere(""" + name + "\"," + name + ", false)";
                    }
                    else
                    {
                        UpdateStr += "+" + @"
        CSql.FormatWhere(""" + name + "\"," + name + ", true)";
                    }
                    num++;
                }
            }
            UpdateStr += @";
        return CSql.ExecuteSql(StrSql);
    }";
            return UpdateStr;
        }
        /// <summary>
        /// 生成SQL数据库Delete语句
        /// </summary>
        /// <param name="Name">列名集合</param>
        /// <param name="TableName">表名</param>
        /// <returns>Delete语句</returns>
        static string GetSqlDelete(string[] Name, string TableName, string[] Status)
        {
            string DeleteStr = @"public bool Delete(";
            int num = 1;
            foreach (string name in Name)
            {
                int j = 0;
                for (int i = 0; i < Name.Length; i++)
                {
                    if (CFunctions.IsInclude(Status[i], "[PK]"))
                        j++;
                }
                if (CFunctions.IsInclude(Status[num - 1], "[PK]"))
                {
                    if (num != j)
                    {
                        DeleteStr += "string " + name + ",";
                    }
                    else
                    {
                        DeleteStr += "string " + name;
                    }
                    num++;
                }
            }
            DeleteStr += @")
    {
        string StrSql = """";
        StrSql += ""DELETE FROM [" + TableName + @"]"";";
            num = 1;
            DeleteStr += @"
        StrSql += "" WHERE """;
            foreach (string name in Name)
            {
                int j = 0;
                for (int i = 0; i < Name.Length; i++)
                {
                    if (CFunctions.IsInclude(Status[i], "[PK]"))
                        j++;
                }
                if (CFunctions.IsInclude(Status[num - 1], "[PK]"))
                {
                    if (num != j)
                    {
                        DeleteStr += "+" + @"
        CSql.FormatWhere(""" + name + "\"," + name + ", false)";
                    }
                    else
                    {
                        DeleteStr += "+" + @"
        CSql.FormatWhere(""" + name + "\"," + name + ", true);";
                    }
                    num++;
                }
            }
            DeleteStr += @"
        return CSql.ExecuteSql(StrSql);
    }";
            return DeleteStr;
        }
        /// <summary>
        /// 生成SQL数据库Select语句
        /// </summary>
        /// <param name="Name">列名集合</param>
        /// <param name="TableName">表名</param>
        /// <returns>Select语句</returns>
        static string GetSqlSelect(string[] Name, string TableName)
        {
            string SearchStr = @"public DataSet GetList(string StrWhere)
    {
        string StrSql = """";
        StrSql += ""SELECT ""; 
        StrSql += """;
            int num = 1;
            foreach (string name in Name)
            {
                if (num++ != Name.Length)
                {
                    SearchStr += name + ",";
                }
                else
                {
                    SearchStr += name;
                }
            }
            SearchStr += @""";
        StrSql += "" FROM [" + TableName + @"]"";
        StrSql += "" WHERE "" + StrWhere;
        return CSql.CreateDataSet(StrSql);
    }";
            return SearchStr;
        }
        /// <summary>
        /// 生成获取实体对象的语句
        /// </summary>
        /// <param name="Name">列名集合</param>
        /// <param name="TableName">表名</param>
        /// <returns>获取实体对象的语句</returns>
        static string GetSqlModel(string[] Name, string TableName, string[] Status)
        {
            string ModelStr = @"public void GetModel(";
            int num = 1;
            foreach (string name in Name)
            {
                int j = 0;
                for (int i = 0; i < Name.Length; i++)
                {
                    if (CFunctions.IsInclude(Status[i], "[PK]"))
                        j++;
                }
                if (CFunctions.IsInclude(Status[num - 1], "[PK]"))
                {
                    if (num != j)
                    {
                        ModelStr += "string " + name + ",";
                    }
                    else
                    {
                        ModelStr += "string " + name;
                    }
                    num++;
                }
            }
            ModelStr += @")
    {
        string StrSql = """";
        StrSql += ""SELECT "";
        StrSql += """;
            num = 1;
            foreach (string name in Name)
            {
                if (num++ != Name.Length)
                {
                    ModelStr += name + ",";
                }
                else
                {
                    ModelStr += name;
                }
            }
            ModelStr += @""";
        StrSql += "" FROM [" + TableName + @"]"";
        StrSql += "" WHERE """;
            num = 1;
            foreach (string name in Name)
            {
                int j = 0;
                for (int i = 0; i < Name.Length; i++)
                {
                    if (CFunctions.IsInclude(Status[i], "[PK]"))
                        j++;
                }
                if (CFunctions.IsInclude(Status[num - 1], "[PK]"))
                {
                    if (num != j)
                    {
                        ModelStr += "+" + @"
        CSql.FormatWhere(""" + name + "\"," + name + ", false)";
                    }
                    else
                    {
                        ModelStr += "+" + @"
        CSql.FormatWhere(""" + name + "\"," + name + ", true);";
                    }
                    num++;
                }
            }
            ModelStr += @"
        DataSet ds = CSql.CreateDataSet(StrSql);
        if(ds.Tables[0].Rows.Count > 0)
        {";
            num = 1;
            foreach (string name in Name)
            {
                ModelStr += @"
            _" + name + @" = ds.Tables[0].Rows[0][""" + name + @"""].ToString();";
            }
            ModelStr += @"
        }
    }";
            return ModelStr;
        }
        /// <summary>
        /// 生成判断是否存在该记录的语句
        /// </summary>
        /// <param name="Name">列名集合</param>
        /// <param name="TableName">表名</param>
        /// <returns>判断是否存在该记录的语句</returns>
        static string GetSqlExists(string[] Name, string TableName, string[] Status)
        {
            string ExistsStr = @"public bool Exists(";
            int num = 1;
            foreach (string name in Name)
            {
                int j = 0;
                for (int i = 0; i < Name.Length; i++)
                {
                    if (CFunctions.IsInclude(Status[i], "[PK]"))
                        j++;
                }
                if (CFunctions.IsInclude(Status[num - 1], "[PK]"))
                {
                    if (num != j)
                    {
                        ExistsStr += "string " + name + ",";
                    }
                    else
                    {
                        ExistsStr += "string " + name;
                    }
                    num++;
                }
            }
            ExistsStr += @")
    {
        string StrSql = """";
        StrSql += ""SELECT COUNT(1) FROM [" + TableName + @"]"";
        StrSql += "" WHERE """;
            num = 1;
            foreach (string name in Name)
            {
                int j = 0;
                for (int i = 0; i < Name.Length; i++)
                {
                    if (CFunctions.IsInclude(Status[i], "[PK]"))
                        j++;
                }
                if (CFunctions.IsInclude(Status[num - 1], "[PK]"))
                {
                    if (num != j)
                    {
                        ExistsStr += "+" + @"
        CSql.FormatWhere(""" + name + "\"," + name + ", false)";
                    }
                    else
                    {
                        ExistsStr += "+" + @"
        CSql.FormatWhere(""" + name + "\"," + name + ", true);";
                    }
                    num++;
                }
            }
            ExistsStr += @"
        return CSql.Exists(StrSql);
    }";
            return ExistsStr;
        }
        /// <summary>
        /// 生成SQL数据库Where语句
        /// </summary>
        /// <param name="Name">列名</param>
        /// <param name="Value">值</param>
        /// <param name="Islast">是否为最后一项</param>
        /// <returns>Where语句</returns>
        public static string FormatWhere(string Name, string Value, bool Islast)
        {
            string WhereStr;
            if (Value == null)
            {
                Value = "";
            }
            if (Islast)
                WhereStr = Name + " = '" + Value + "'";
            else
                WhereStr = Name + " = '" + Value + "' and ";
            return WhereStr;
        }
        /// <summary>
        /// 生成SQL数据库Insert中Values的语句
        /// </summary>
        /// <param name="Value">值</param>
        /// <param name="Islast">是否为最后一项</param>
        /// <returns>Insert中Values的语句</returns>
        public static string FormatInsert(string Value, bool Islast)
        {
            string InsertStr;
            if (Value == null)
            {
                Value = "";
            }
            if (Islast)
                InsertStr = "'" + Value + "')";
            else
                InsertStr = "'" + Value + "',";
            return InsertStr;
        }
        /// <summary>
        /// 生成SQL数据库Update中Set的语句
        /// </summary>
        /// <param name="Name">列名</param>
        /// <param name="Value">值</param>
        /// <param name="Islast">是否为最后一项</param>
        /// <returns>Update中Set的语句</returns>
        public static string FormatUpdate(string Name, string Value, bool Islast)
        {
            string UpdateStr;
            if (Value == null)
            {
                Value = "";
            }
            if (Islast)
                UpdateStr = Name + " = '" + Value + "'";
            else
                UpdateStr = Name + " = '" + Value + "',";
            return UpdateStr;
        }
        #endregion

        #endregion

        #region MH_SCADA 使用的SQL语句
        /// <summary>
        /// 写入数据库前判断值是否为空，为空则无法写入
        /// </summary>
        /// <param name="Name">列名集合</param>
        /// <param name="Status">备注集合</param>
        /// <returns>判断语句</returns>
        public static string SCADA_NotNull(string[] Name, string[] Status)
        {
            string SCADA_Str = "";
            int i = 0;
            foreach (string name in Name)
            {
                if (CFunctions.IsInclude(Status[i++], "[NN]"))
                {
                    SCADA_Str += @"
if (((TextBox)MyController[""" + name + @"""]).Text.Trim().Equals(""""))
{
    MessageBox.Show(""不能为空！"", ""提示信息"", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    return;
}
";
                }
            }
            return SCADA_Str;
        }

        /// <summary>
        /// 编写SCADA中SQL的Insert语句
        /// </summary>
        /// <param name="Name">列名集合</param>
        /// <param name="TableName">表名</param>
        /// <param name="Status">备注集合</param>
        /// <returns>SCADA中SQL的Insert语句</returns>
        public static string SCADA_Insert(string[] Name, string TableName, string[] Status)
        {
            string SCADA_Str = @"string InsertStr = ""INSERT INTO [" + TableName + @"](";
            int num = 1;
            int i = 0;
            int j = 0;
            foreach (string name in Name)
            {
                if (!CFunctions.IsInclude(Status[i++], "[AK]"))
                {
                    if (num++ != Name.Length)
                    {
                        SCADA_Str += name + ",";
                    }
                    else
                    {
                        SCADA_Str += name;
                    }
                }
                else
                {
                    num++;
                }
            }
            SCADA_Str += ") VALUES('\"";
            num = 1;
            foreach (string name in Name)
            {
                if (!CFunctions.IsInclude(Status[j++], "[AK]"))
                {
                    if (num++ != Name.Length)
                    {
                        SCADA_Str += "+" + @"
((TextBox)MyController[""" + name + @"""]).Text.Trim() + ""','""";
                    }
                    else
                    {
                        SCADA_Str += "+" + @"
((TextBox)MyController[""" + name + @"""]).Text.Trim() + ""')"";";
                    }
                }
                else
                {
                    num++;
                }
            }
            return SCADA_Str;
        }

        /// <summary>
        /// 编写SCADA中SQL的Update语句
        /// </summary>
        /// <param name="Name">列名集合</param>
        /// <param name="TableName">表名</param>
        /// <param name="Status">备注集合</param>
        /// <returns>SCADA中SQL的Update语句</returns>
        public static string SCADA_Update(string[] Name, string TableName, string[] Status)
        {
            int num = 1;
            int i = 0;
            int j = 0;
            string SCADA_Str = @"string InsertStr = ""UPDATE [" + TableName + @"] SET """;
            foreach (string name in Name)
            {
                if (!CFunctions.IsInclude(Status[i++], "[AK]"))
                {
                    if (num++ != Name.Length)
                    {
                        SCADA_Str += @"
+ """ + name + " = '\" + " + "((TextBox)MyController[\"" + name + "\"]).Text.Trim() + \"',\"";
                    }
                    else
                    {
                        SCADA_Str += @"
+ """ + name + " = '\" + " + "((TextBox)MyController[\"" + name + "\"]).Text.Trim() + \"'\";";
                    }
                }
                else
                {
                    num++;
                }
            }
            SCADA_Str += @"
InsertStr += "" WHERE """;
            num = 1;
            foreach (string name in Name)
            {
                for (i = 0; i < Name.Length; i++)
                {
                    if (CFunctions.IsInclude(Status[i], "[PK]"))
                        j++;
                }
                if (CFunctions.IsInclude(Status[num - 1], "[PK]"))
                {
                    if (num != j)
                    {
                        SCADA_Str += @"
+ """ + name + " = '\" + " + "((TextBox)MyController[\"" + name + "\"]).Text.Trim() + \"',\"";
                    }
                    else
                    {
                        SCADA_Str += @"
+ """ + name + " = '\" + " + "((TextBox)MyController[\"" + name + "\"]).Text.Trim() + \"'\";";
                    }
                    num++;
                }
            }
            return SCADA_Str;
        }

        /// <summary>
        /// 编写SCADA中SQL的Select语句
        /// </summary>
        /// <param name="Name">列名集合</param>
        /// <param name="TableName">表名</param>
        /// <param name="Status">备注集合</param>
        /// <returns>SCADA中SQL的Select语句</returns>
        public static string SCADA_Select(string[] Name, string TableName, string[] Mean)
        {
            int num = 1;
            string SCADA_Str = "string InsertStr = \"SELECT ";
            foreach (string name in Name)
            {
                if (num++ != Name.Length)
                {
                    SCADA_Str += name + ",";
                }
                else
                {
                    SCADA_Str += name;
                }
            }
            SCADA_Str += " FROM [" + TableName + "]\";";
            SCADA_Str += @"


            ********************
            ******英文查询******
            ********************
            ******中文查询******
            ********************


";
            num = 1;
            SCADA_Str += "string InsertStr = \"SELECT ";
            foreach (string name in Name)
            {
                if (num++ != Name.Length)
                {
                    SCADA_Str += name + " as " + Mean[num - 2] + ",";
                }
                else
                {
                    SCADA_Str += name + " as " + Mean[num - 2];
                }
            }
            SCADA_Str += " FROM [" + TableName + "]\";";
            return SCADA_Str;
        }

        /// <summary>
        /// 编写SCADA中SQL的Delete语句
        /// </summary>
        /// <param name="Name">列名集合</param>
        /// <param name="TableName">表名</param>
        /// <param name="Status">备注集合</param>
        /// <returns>SCADA中SQL的Delete语句</returns>
        public static string SCADA_Delete(string[] Name, string TableName, string[] Status)
        {
            int num = 1;
            int i = 0;
            int j = 0;
            string SCADA_Str = @"string InsertStr = ""DELETE FROM [" + TableName + @"] WHERE """;
            foreach (string name in Name)
            {
                for (i = 0; i < Name.Length; i++)
                {
                    if (CFunctions.IsInclude(Status[i], "[PK]"))
                        j++;
                }
                if (CFunctions.IsInclude(Status[num - 1], "[PK]"))
                {
                    if (num != j)
                    {
                        SCADA_Str += @"
+ """ + name + " = '\" + " + "((TextBox)MyController[\"" + name + "\"]).Text.Trim() + \"',\"";
                    }
                    else
                    {
                        SCADA_Str += @"
+ """ + name + " = '\" + " + "((TextBox)MyController[\"" + name + "\"]).Text.Trim() + \"'\";";
                    }
                    num++;
                }
            }
            return SCADA_Str;
        }

        #endregion

    }
}
