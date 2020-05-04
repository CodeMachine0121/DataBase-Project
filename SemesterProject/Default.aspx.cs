using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page
{
    public  string strConn = "Data Source=AA201-32\\A05050121;Initial Catalog=Lab;Integrated Security=True;User ID=Test;Password=test";
    public SqlConnection myConn;
    public string strComm;


    protected void Page_Load(object sender, EventArgs e)
    {
        
        myConn = new SqlConnection(strConn);

        myConn.Open();
        
    }

    protected void Sign_in_Click(object sender, EventArgs e)
    {
        Label label1 = FindControl("Label1") as Label; // debug tool
        
        RadioButton radio_STD = FindControl("radio_STD") as RadioButton;
        RadioButton radio_PRO = FindControl("radio_PRO") as RadioButton;
        RadioButton radio_ADM = FindControl("radio_ADM") as RadioButton;

        TextBox name = FindControl("name") as TextBox;
        TextBox password = FindControl("password") as TextBox;

        bool is_std=false, is_pro=false, is_adm = false;

        if(name.Text.Trim(' ') == "" || password.Text.Trim(' ') == ""){return;}
        string Hash_password = SHA256(password.Text);

        if (radio_STD.Checked)
        {
            strComm = "select password from student where student.std_ID='" + name.Text + "';";
            is_std = true;
        }
        else if( radio_PRO.Checked)
        {
            strComm = "select password from Professor where Professor.pro_ID='" + name.Text + "';";
            is_pro = true;
        }
        else if( radio_ADM.Checked )
        {
            strComm = "select password from Admin where Admin.adm_ID='" + name.Text + "';";
            is_adm = true;
        }
        else
        {
            label1.Text = "請先行確認身分";
            return;
        }
        label1.Text = strComm;
        SqlCommand reader = new SqlCommand (strComm, myConn);

        SqlDataReader data = reader.ExecuteReader();

        try{
            string data_password = data["Password"].ToString();
        }
        catch
        {
            label1.Text = "帳號或密碼錯誤";
        }
       
        if (data.Read())
        {
            if(data["Password"].ToString() == Hash_password)
            {
                if (is_std)
                    label1.Text = "student";
                else if (is_pro)
                    label1.Text = "professor";
                else if (is_adm)
                    label1.Text = "admin";        
            }
            else
            {
                label1.Text = "帳號或密碼錯誤";
            }
        }
        name.Text ="";
        password.Text = "";


    }

    protected void Sign_up_Click(object sender, EventArgs e)
    {
        Response.Redirect("Signup.aspx");
    }


    public string SHA256(string data)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(data);
        byte[] hash = SHA256Managed.Create().ComputeHash(bytes);

        StringBuilder builder = new StringBuilder();
        for (int i=0;i<hash.Length; i++)
        {
            builder.Append(hash[i].ToString("X2"));
        }
        return builder.ToString().ToLower();

    }


}