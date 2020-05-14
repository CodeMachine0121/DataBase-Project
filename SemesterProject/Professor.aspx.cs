using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Professor : System.Web.UI.Page
{
    public string strConn = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["connectionWord"].ConnectionString;
    MyFunctions myFunctions = new MyFunctions();
    protected void Page_Load(object sender, EventArgs e)
    {

        string Session_ID = (string)Session["ID"];

        if (!Is_Session_Exist(Session_ID))
        {
            // 回登入頁
            Response.Write("<script>alert('非法登入')</script>");
            Response.Redirect("Default.aspx");
        }
        string command = "select pro_ID from Professor where Session_ID = '" + Session_ID + "'";
        string ID =myFunctions.Get_ID(command, 2);
        string Name = myFunctions.Get_Name_from_proID(ID);
        Response.Write("<h3>" + Name + "  老師您好</h3>");
      


        string[] cid = myFunctions.Get_Course_By_ProID(ID);
        string[] ctime = myFunctions.Get_CourseTime(cid);

        SetTable(ctime, cid);

    }

    public bool Is_Session_Exist(string id)
    {
        string command = "";
        SqlConnection myConn;
        myConn = new SqlConnection(strConn);
        myConn.Open();

        command = "select count(1) from Professor where Session_ID = '" + id + "';";

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


    public void SetTable(string[] ctime, string[] course)
    {
        // 使用者輸入=> 星期節數 : 01020304
        // Label ID => C0502
        //Response.Write((ctime.Length - 2) / 2);
        // string[] time = new string[ ((ctime.Length - 2) / 2) *ctime.Length ]; // 算幾門課

        

        course = myFunctions.Course_ID_Name_Change(course);

       
        for (int i = 0; i < ctime.Length; i++)
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