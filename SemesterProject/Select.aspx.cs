using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Select : System.Web.UI.Page
{
    public string strConn = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["connectionWord"].ConnectionString;
    public MyFunctions myFunc = new MyFunctions();

    // 選課頁面必為學生
    protected void Page_Load(object sender, EventArgs e)
    {

        Label label = FindControl("text") as Label;

        string  Session_ID = (string) Session["ID"];


        if (!Is_Session_Exist(Session_ID))
        {
            // 回登入頁
            Response.Write("<script>alert('非法登入')</script>");
            Response.Redirect("Default.aspx");
        }
    }
    // Delete Course
    protected void Delete_Click(object sender, EventArgs e)
    {
        TextBox CID = FindControl("CID") as TextBox;
        string cid = CID.Text;
        string std_id = Get_ID("Select std_ID from Student where Session_ID = '" + (string)Session["ID"] + "'", 1);
        string pro_id = Get_ID("Select pro_ID from Courses where CID= '" + cid + "';", 2);
        string adm_id = Get_ID("Select adm_ID from Courses where CID='" + cid + "';", 3);

        if (!Is_Course_Exist(cid))
        {
            Response.Write("<script>alert('查無此課程 或 你根本沒選')</script>");
            return;
        }

        if (std_id == null || pro_id == null || adm_id == null)
        {
            Response.Write("<script>alert('資料庫發生錯誤 歹勢啦')</script>");
            return;
        }

        // 學生:課程:教授 放入 課程池
        string command = "DELETE FROM [dbo].[Course_Pool] WHERE (CID='"+cid+"' AND std_ID='"+std_id+"');";
        SQL_cmd(command);
        Response.Write("<script>alert('成功移除!')</script>");

        CID.Text = "";

        return;

    }

    // ADD Course
    protected void Confirm_Click(object sender, EventArgs e)
    {
        TextBox CID = FindControl("CID") as TextBox;
        string cid = CID.Text;
        string std_id = Get_ID( "Select std_ID from Student where Session_ID = '" + (string)Session["ID"] + "'" , 1);
        string pro_id = Get_ID("Select pro_ID from Courses where CID= '" + cid + "';",2);
        string adm_id = Get_ID("Select adm_ID from Courses where CID='" + cid + "';", 3);

        // 重複選課問題
        if (Is_Course_Choosed(cid, std_id))
        {
            Response.Write("<script>alert('已選過該課程')</script>");
            return;
        }
        
        if (!Is_Course_Exist(cid))
        {
            Response.Write("<script>alert('查無此課程 ')</script>");
            return;
        }

        if(std_id==null || pro_id == null || adm_id == null)
        {
            Response.Write("<script>alert('資料庫發生錯誤 歹勢啦')</script>");
            return;
        }

        // 學生:課程:教授 放入 課程池
        string command = "INSERT INTO [dbo].[Course_Pool] ([std_ID] ,[pro_ID] ,[CID] ,[adm_ID]) VALUES('"+std_id+"','"+pro_id+"','"+cid+"','"+adm_id+"');";
        SQL_cmd(command);
        Response.Write("<script>alert('成功加入!')</script>");

        CID.Text = "";

        return;
    }


    public string Get_ID(string command,int status)
    {
        SqlConnection myConn;
        myConn = new SqlConnection(strConn);
        myConn.Open();
        SqlCommand reader = new SqlCommand(command, myConn);
        SqlDataReader data = reader.ExecuteReader();

        string identify = "";
        if (status == 1)
            identify = "std_ID";
        else if (status == 2)
            identify = "pro_ID";
        else if (status == 3)
            identify = "adm_ID";

        if (data.Read())
        {
            string id = data[identify].ToString();
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
    public bool Is_Session_Exist(string id)
    {
        string command = "";
        SqlConnection myConn;
        myConn = new SqlConnection(strConn);
        myConn.Open();

        command = "select count(1) from Student where Session_ID = '" + id + "';";

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

    public bool Is_Course_Exist(string cid)
    {

        SqlConnection myConn;
        myConn = new SqlConnection(strConn);
        myConn.Open();

        string command = "select count(1) from Courses where CID = '" + cid + "';";
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

    public bool Is_Course_Choosed(string cid , string std_ID)
    {
        SqlConnection myConn;
        myConn = new SqlConnection(strConn);
        myConn.Open();

        string command = "select count(1) from Course_Pool where (CID = '" + cid + "' AND std_ID = '" + std_ID + "');";

        SqlCommand scalar = new SqlCommand(command, myConn);

        int CourseChoose = (int)scalar.ExecuteScalar();

        if (CourseChoose == 0)
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
}