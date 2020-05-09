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


    public string strConn = "Data Source=DESKTOP-9SL6SMA\\A05050121;Initial Catalog=Lab2;Integrated Security=True;User ID=Test;Password=mcuite";
    public SqlConnection myConn;
    public string strComm;
    public MyFunctions myFunc = new MyFunctions();
    public string Session_ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        Label label = FindControl("text") as Label;

        Session_ID = (string)Session["ID"];
        
        myConn = new SqlConnection(strConn);
        myConn.Open();
       
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
        string cid = CID.Text;
        string cname = CName.Text;
        string cpro = CPro.Text;
        string croom = Croom.Text;
        string ccredit = Ccredit.Text;
        bool is_course_exist = Is_Course_Exist(cid);
        if (is_course_exist)
        {
            Response.Write("<script>alert('該課程已存在')</script>");
        }
        else
        {
            // 先有老師才有課
            try
            {
                
                string command = "INSERT INTO [dbo].[Courses]([pro_ID],[CID] ,[Name],[Credit],[Classroom]) VALUES('" + cpro + "','" + cid + "','" + cname + "','" + ccredit + "','" + croom + "');";
                SqlCommand reader = new SqlCommand(command, myConn);
                reader.ExecuteNonQuery();

                // 需額外新增 Table 紀錄有哪些學生

                command = "CREATE TABLE "+cname+ " ( std_ID VARCHAR(8) NOT NULL ) ;";
                reader = new SqlCommand(command, myConn);
                reader.ExecuteNonQuery();

                Response.Write("<script>alert('成功寫入')</script>");
            }
            catch
            {
               Response.Write("<script>alert('資料庫出現問題')</script>");
            }
            CID.Text = "";
            CName.Text = "";
            CPro.Text = "";
            Croom.Text = "";
            Ccredit.Text = ""; 
            
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
            SqlCommand reader = new SqlCommand(command, myConn);
            reader.ExecuteNonQuery();

            command = "DROP TABLE " + cname + ";";
            reader = new SqlCommand(command, myConn);
            reader.ExecuteNonQuery();
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

        string command = "select count(1) from courses where CID = '"+cid+"';";
        SqlCommand scalar = new SqlCommand(command, myConn);

        int CourseExist = (int)scalar.ExecuteScalar();

        if (CourseExist == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }


    public bool Is_Session_Exist(string id)
    {
        string command = "";

        command = "select count(1) from Admin where Session_ID = '" + id + "';";

        SqlCommand scalar = new SqlCommand(command, myConn);

        int UserExist = (int)scalar.ExecuteScalar();

        if (UserExist == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

}