using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPage : System.Web.UI.Page
{


    public string strConn = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["connectionWord"].ConnectionString;
    
   
    public MyFunctions myFunc = new MyFunctions();
    public string Session_ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        Label label = FindControl("text") as Label;

        Session_ID = (string)Session["ID"];
        
        
       
        if (!Is_Session_Exist(Session_ID))
        {
            // 回登入頁
            Response.Write("<script>alert('非法登入')</script>");
            Response.Redirect("Default.aspx");
        }

    
       
    }

    protected void CAdd_Click(object sender, EventArgs e)
    {

        TextBox CID = FindControl("CID") as TextBox;
        TextBox CName = FindControl("Cname") as TextBox;
        TextBox CPro = FindControl("CPro") as TextBox;
        TextBox Croom = FindControl("Croom") as TextBox;
        TextBox Ccredit = FindControl("Ccredit") as TextBox;
        TextBox CTime = FindControl("CTime") as TextBox;
            
        string cid = CID.Text;
        string cname = CName.Text;
        string cpro = CPro.Text;
        string croom = Croom.Text;
        string ccredit = Ccredit.Text;
        string ctime = CTime.Text;
        bool is_course_exist = Is_Course_Exist(cid);

        string adm_id =myFunc.Get_ID("select adm_ID from Admin where Session_ID = '" + (string)Session_ID + "';",2);

        if (adm_id == null)
        {
            Response.Write("<script>alert('資料庫發生錯誤 歹勢啦')</script>");
            return;
        }

        if (is_course_exist)
        {
            Response.Write("<script>alert('該課程已存在')</script>");
            return;
        }
        else
        {
             
            try
            {
                
                string command = "INSERT INTO [dbo].[Courses]([adm_ID],[pro_ID],[CID],[Ctime] ,[Name],[Credit],[Classroom]) VALUES('"+adm_id+"','" + cpro + "','" + cid + "','" +ctime+"','"+ cname + "','" + ccredit + "','" + croom + "');";
                SQL_cmd(command);

                // 需額外新增 Table 紀錄有哪些學生  [*] 使用關聯性來帶入 Function 在 選課page
               // command = "CREATE TABLE "+cname+ " ( std_ID VARCHAR(8) NOT NULL, std_Name VARCHAR(80) NOT NULL ) ALTER TABLE"+cname+" ADD CONSTRAINT Student_"+cname+" FOREIGN KEY(std_ID) REFERENCES Student(std_ID) ON DELETE NO ACTIONON UPDATE NO ACTION ; ";

                Response.Write("<script>alert('成功寫入')</script>");

            }
            catch
            {
                 Response.Write("<script>alert('先有教授才能有課')</script>");
            }
            CID.Text = "";
            CName.Text = "";
            CPro.Text = "";
            Croom.Text = "";
            Ccredit.Text = "";
            return;
        }
    }

    protected void CDelete_Click(object sender, EventArgs e)
    {

        TextBox CID = FindControl("CID") as TextBox;
        TextBox CName = FindControl("Cname") as TextBox;
        string cid = CID.Text;
        string cname = CName.Text;
        bool is_course_exist = Is_Course_Exist(cid);

        if (!is_course_exist)
        {
            Response.Write("<script>alert('課程不存在')</script>");
            return;
        }
        try
        {
            string command = "DELETE FROM [dbo].[Courses] WHERE CID = '" + cid + "';";
            SQL_cmd(command);

            command = "DROP TABLE " + cname + ";";
            SQL_cmd(command);
            Response.Write("<script>alert('成功移除該課程')</script>");
        }
        catch
        {
            Response.Write("<script>alert('資料庫出現問題')</script>");
        }
        CID.Text = "";
        CName.Text = "";
    }
    
    // 查看課程是否存在
    public bool Is_Course_Exist(string cid)
    {

        SqlConnection myConn;
        myConn = new SqlConnection(strConn);
        myConn.Open();

        string command = "select count(1) from Courses where CID = '"+cid+"';";
        SqlCommand scalar = new SqlCommand(command, myConn);

        int CourseExist = (int)scalar.ExecuteScalar();

        if (CourseExist == 0)
        {
            scalar.Dispose();
            myConn.Close();
            myConn.Dispose();
            return false;
        }
        else
        {
            scalar.Dispose();
            myConn.Close();
            myConn.Dispose();
            return true;
        }
    }


    public bool Is_Session_Exist(string id)
    {
        string command = "";
        SqlConnection myConn;
        myConn = new SqlConnection(strConn);
        myConn.Open();

        command = "select count(1) from Admin where Session_ID = '" + id + "';";

        SqlCommand scalar = new SqlCommand(command, myConn);

        int UserExist = (int)scalar.ExecuteScalar();

        if (UserExist == 0)
        {
            scalar.Dispose();
            myConn.Close();
            myConn.Dispose();
            return false;
        }
        else
        {
            scalar.Dispose();
            myConn.Close();
            myConn.Dispose();
            return true;
        }
    }

    // None Query
    public void SQL_cmd(string command)
    {
        SqlConnection myConn;
        myConn = new SqlConnection(strConn);
        myConn.Open();
        SqlCommand reader = new SqlCommand(command, myConn);

        reader.ExecuteNonQuery();
        reader.Dispose();
        myConn.Close();
        myConn.Dispose();

    }


    public string Get_Admin_ID(string command)
    {
        SqlConnection myConn;
        myConn = new SqlConnection(strConn);
        myConn.Open();
        SqlCommand reader = new SqlCommand(command, myConn);
        SqlDataReader data = reader.ExecuteReader();
        if (data.Read())
        {
            string id = data["adm_ID"].ToString();
            data.Close();
            reader.Dispose();
            myConn.Close();
            myConn.Dispose();
            return id;

        }
        else
        {
            data.Close();
            reader.Dispose();
            myConn.Close();
            myConn.Dispose();
            return null;
        }
    }

    protected void Back_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }

    protected void Table_Click(object sender, EventArgs e)
    {
        Response.Write("<script> window.open('Table.aspx', '_blank')</script>");
    }
}