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

    public string strConn = "Data Source=DESKTOP-9SL6SMA\\A05050121;Initial Catalog=Lab;Integrated Security=True;User ID=Test;Password=mcuite";
    public SqlConnection myConn;
    public string strComm;
    public MyFunctions myFunc = new MyFunctions();
    protected void Page_Load(object sender, EventArgs e)
    {

        myConn = new SqlConnection(strConn);
        myConn.Open();

    }

    protected void Register_Click(object sender, EventArgs e)
    {
        //debug tool
        Label label = FindControl("Label1") as Label;

        TextBox id = FindControl("id") as TextBox;
        TextBox name = FindControl("name") as TextBox;
        TextBox email = FindControl("email") as TextBox;
        TextBox password = FindControl("password") as TextBox;

        string in_name=name.Text;
        string in_mail = email.Text;
        string in_cellphone = cellphone.Text;
        string in_password = myFunc.SHA256( password.Text);
        string in_id = id.Text;
        int in_total_credit = 0;

        RadioButton radio_STD = FindControl("radio_STD") as RadioButton;
        RadioButton radio_PRO = FindControl("radio_PRO") as RadioButton;
        RadioButton radio_ADM = FindControl("radio_ADM") as RadioButton;

        bool is_std = false, is_pro = false;
        string command="";
        int status;
        if (radio_STD.Checked)
        {
            command = "INSERT INTO [dbo].[Student]([std_ID],[total_Credit],[Name],[Cellphone],[Password],[Email]) VALUES('" + in_id + "',0,'" + in_name + "','" + in_cellphone + "','" + in_password + "','" + in_mail + "');";
            status = 0;   
        }
        else if (radio_PRO.Checked)
        {
            command = "INSERT INTO [dbo].[Professor] ([pro_ID],[Name],[email],[Cellphone],[Password]) VALUES('" +in_id + "','" + in_name +"','" + in_mail+ "','" + in_cellphone + "','" + in_password + "');";

            status = 1;
        }
        else
        {
            label.Text = "請先行確認身分";
            return;
        }

        if (IsExist(in_id,status)){
            // account already exist
            label.Text = "該學號已註冊";
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
 