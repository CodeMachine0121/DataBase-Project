using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Select : System.Web.UI.Page
{
    public string strConn = "Data Source=DESKTOP-9SL6SMA\\A05050121;Initial Catalog=Lab2;Integrated Security=True;User ID=Test;Password=mcuite";
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
}