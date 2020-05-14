using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Security.Policy;

public partial class _Default : System.Web.UI.Page
{
    public string strConn = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["connectionWord"].ConnectionString;

    public MyFunctions myfunc = new MyFunctions();

    protected void Page_Load(object sender, EventArgs e)
    {    

     // 辨認使用者 資料庫要再多新增一欄 放session -> H(Password)

        Session["ID"] = myfunc.SHA256((string)Session.SessionID);

    }

    protected void Sign_in_Click(object sender, EventArgs e)
    {
        
        Label label = FindControl("Label1") as Label; // tool

        RadioButton radio_STD = FindControl("radio_STD") as RadioButton;
        RadioButton radio_PRO = FindControl("radio_PRO") as RadioButton;
        RadioButton radio_ADM = FindControl("radio_ADM") as RadioButton;

        TextBox name = FindControl("name") as TextBox;
        TextBox password = FindControl("password") as TextBox;

        bool is_std=false, is_pro=false, is_adm = false;
        string strComm;
        

        if(name.Text.Trim(' ') == "" || password.Text.Trim(' ') == "") { Response.Write("<script>alert('賣亂')</script>"); }
        string Hash_password = myfunc.SHA256(password.Text);
        
        // 取得身分
        if (radio_STD.Checked)
        {
            strComm = "select Password from student where Student.std_ID='" + name.Text + "';";
            is_std = true;
        }
        else if( radio_PRO.Checked)
        {
            strComm = "select Password from Professor where pro_ID='" + name.Text + "';";
            is_pro = true;
        }
        else if( radio_ADM.Checked )
        {
            strComm = "select Password from Admin where Admin.adm_ID='" + name.Text + "';";
            is_adm = true;
        }
        else
        {
            label.Text="請先確認身分";
            name.Text = "";
            password.Text = "";
            return;
        }
        
     // 檢查密碼
       
        if (!Check_Password(Hash_password,strComm))
        {
            Response.Write("<script>alert('帳號或密碼錯誤')</script>");
            return;
        }


        // 把Session 塞入Table
        if (is_std)
        {
           
            Update_Session("UPDATE Student SET Session_ID = '" + Session["ID"] + "' where std_ID ='" + name.Text + "';");
            Response.Redirect("Select.aspx");
        }
        else if (is_pro)
        {
           
           
            Update_Session("UPDATE Professor SET Session_ID = '" + Session["ID"] + "' where pro_ID ='" + name.Text + "';");
            Response.Redirect("Professor.aspx");
        }
        else if (is_adm)
        {
            Update_Session("UPDATE Admin SET Session_ID = '" + (string)Session["ID"] + "' where adm_ID ='" + name.Text + "';");
            
            Response.Redirect("AdminPage.aspx");
        }
        name.Text ="";
        password.Text = "";


    }

    protected void Sign_up_Click(object sender, EventArgs e)
    {
        Response.Redirect("Signup.aspx");
    }

    // SQL cmd
   public  void Update_Session(string strComm)
    {
        SqlConnection myConn = new SqlConnection(strConn);
        myConn.Open();

        SqlCommand reader = new SqlCommand(strComm, myConn);

        reader.ExecuteNonQuery();

        reader.Dispose();
        myConn.Close();
        myConn.Dispose();
    }

   public bool Check_Password(string password,string command)
    {
        SqlConnection myConn = new SqlConnection(strConn);

        myConn.Open();

        SqlCommand reader = new SqlCommand(command, myConn);

        SqlDataReader data = reader.ExecuteReader();
        while (data.Read())
        {
            if (data["Password"].ToString() == password)
            {

                reader.Dispose();
                myConn.Close();
                myConn.Dispose();
                return true;
            }
            else
            {
                reader.Dispose();
                myConn.Close();
                myConn.Dispose();
                return false;
            }
        }

        return false;

        
    }
}