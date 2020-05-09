using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Label = System.Web.UI.WebControls.Label;

public partial class Signup : System.Web.UI.Page
{

    public string strConn = "Data Source=DESKTOP-9SL6SMA\\A05050121;Initial Catalog=Lab2;Integrated Security=True;User ID=Test;Password=mcuite";
    public SqlConnection myConn;
    public string strComm;
    public MyFunctions myFunc = new MyFunctions();
    protected void Page_Load(object sender, EventArgs e)
    {

        myConn = new SqlConnection(strConn);
        myConn.Open();
        
        Session["ID"] = myFunc.SHA256((string)Session.SessionID);
    }

    protected void Register_Click(object sender, EventArgs e)
    {
        Label text = FindControl("Label1") as Label;

        TextBox id = FindControl("id") as TextBox;
        TextBox name = FindControl("name") as TextBox;
        TextBox email = FindControl("email") as TextBox;
        TextBox password = FindControl("password") as TextBox;

        string in_name=name.Text;
        string in_mail = email.Text;
        string in_cellphone = cellphone.Text;
        string in_password = myFunc.SHA256( password.Text);
        string in_id = id.Text;
  
       

        RadioButton radio_STD = FindControl("radio_STD") as RadioButton;
        RadioButton radio_PRO = FindControl("radio_PRO") as RadioButton;

        string SessionID = myFunc.SHA256((string)Session["ID"]); // 重複性

        string command="";
        int status;
        if (radio_STD.Checked)
        {
            command = "INSERT INTO [dbo].[Student]([std_ID],[total_Credit],[Name],[Cellphone],[Password],[Email],[Session_ID]) VALUES('" + in_id + "',0,'" + in_name + "','" + in_cellphone + "','" + in_password + "','" + in_mail +"','"+SessionID+ "');";
            status = 0;   
        }
        else if (radio_PRO.Checked)
        {
            command = "INSERT INTO [dbo].[Professor] ([pro_ID],[Name],[email],[Session_ID],[Cellphone],[Password]) VALUES('" + in_id + "','" + in_name +"','" + in_mail+ "','" + SessionID + "','" + in_cellphone + "','" + in_password +"');";
            //Response.Write("<script>alert('session:"+SessionID+"\npassword:"+in_password+" ')</script>");

            status = 1;
        }
        else
        {
            Label1.Text = "請先確認身分";
            return;
        }

        if (IsExist(in_id,status)){
            // account already exist
            Label1.Text = "該學號已被註冊";
            return;
        }
        
        

        SqlCommand reader = new SqlCommand(command, myConn);
        reader.ExecuteNonQuery();
        
        Response.Redirect("Default.aspx");

    }
    
    public bool IsExist(string id,int status)
    {
        string command = "";
        // 0:std, 1:pro
        if (status == 0)
            command = "select count(1) from Student where std_ID = '" + id + "';"; 
        else
            command = "select count(1) from Professor where pro_ID = '" + id + "';";

        SqlCommand scalar = new SqlCommand(command, myConn);

        int UserExist = (int) scalar.ExecuteScalar();

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
 