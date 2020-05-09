using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Select : System.Web.UI.Page
{
    public string strConn = "Data Source=DESKTOP-9SL6SMA\\A05050121;Initial Catalog=Lab;Integrated Security=True;User ID=Test;Password=mcuite";
    public SqlConnection myConn;
    public string strComm;
    public MyFunctions myFunc = new MyFunctions();
    public string Session_ID;

    // 選課頁面必為學生
    protected void Page_Load(object sender, EventArgs e)
    {

        Label label = FindControl("text") as Label;

        Session_ID = (string) Session["ID"];
       
        myConn = new SqlConnection(strConn);
        myConn.Open();

        if (Is_Session_Exist(Session_ID))
        {
            label.Text = "valid login";
        }
        else
        {
            label.Text = "invalid login";
        }
    }


    public bool Is_Session_Exist(string id )
    {
        string command = "";
       
        command = "select count(1) from Student where Session_ID = '" + id + "';";
        
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