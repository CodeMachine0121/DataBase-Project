using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
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
        
        string  Session_ID = (string) Session["ID"];


        if (!Is_Session_Exist(Session_ID))
        {
            // 回登入頁
            Response.Write("<script>alert('非法登入')</script>");
            Response.Redirect("Default.aspx");
        }
        // 初始化學生課表
        string[] lessons = myFunc.Get_Course_By_StdID(myFunc.Get_ID("Select std_ID from Student where Session_ID = '" + (string)Session["ID"] + "'", 1));
        string[] ctime = myFunc.Get_CourseTime(lessons);
        SetTable(ctime,lessons);
    }
    // Delete Course
    protected void Delete_Click(object sender, EventArgs e)
    {
        TextBox CID = FindControl("CID") as TextBox;
        string cid = CID.Text;
        string std_id = myFunc.Get_ID("Select std_ID from Student where Session_ID = '" + (string)Session["ID"] + "'", 1);
        string pro_id = myFunc.Get_ID("Select pro_ID from Courses where CID= '" + cid + "';", 2);
        string adm_id = myFunc.Get_ID("Select adm_ID from Courses where CID='" + cid + "';", 3);

        if (!myFunc.Is_Course_Exist(cid))
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
        string std_id = myFunc.Get_ID( "Select std_ID from Student where Session_ID = '" + (string)Session["ID"] + "'" , 1);
        string pro_id = myFunc.Get_ID("Select pro_ID from Courses where CID= '" + cid + "';",2);
        string adm_id = myFunc.Get_ID("Select adm_ID from Courses where CID='" + cid + "';", 3);

        // 重複選課問題
        if (myFunc.Is_Course_Choosed(cid, std_id))
        {
            Response.Write("<script>alert('已選過該課程')</script>");
            return;
        }
        
        if (myFunc.Is_Course_Exist(cid))
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


        if (myFunc.Is_Course_Conflict(std_id))
        {
            Response.Write("<script>alert('該課程與其他已有課程衝堂')</script>");
            CID.Text="";
            command = "Delete From [dbo].[Course_Pool] where CID = '" + cid + "';";
            SQL_cmd(command);
            return;
        }

        Response.Write("<script>alert('成功加入!')</script>");

        CID.Text = "";

        Response.Redirect("Select.aspx");
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

    public void SetTable(string[] ctime, string[] course)
    {
        // 使用者輸入=> 星期節數 : 01020304
        // Label ID => C0502
        //Response.Write((ctime.Length - 2) / 2);
       // string[] time = new string[ ((ctime.Length - 2) / 2) *ctime.Length ]; // 算幾門課
        
        Label label = FindControl("Label1") as Label;

        course = myFunc.Course_ID_Name_Change(course);

        int end = 0;
        for(int i=0; i<ctime.Length ; i++)
        {
            for (int j = 2; j < ctime[i].Length; j += 2)
            {
                string time = "C" + ctime[i][0] + ctime[i][1] + ctime[i][j] + ctime[i][j + 1];

                Label ct = FindControl(time) as Label;
                ct.Text = course[i];

            }
        }
      
    }




}