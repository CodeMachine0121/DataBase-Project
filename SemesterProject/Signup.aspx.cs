using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Signup : System.Web.UI.Page
{

    public static string in_name;
    public static string in_mail;
    public static string in_birth;
    public static string in_cellphone;
    public static int in_total_credit;
    public static string in_password;
    public static string in_address;
    public static string in_id;
    public string strConn = "Data Source=AA201-32\\A05050121;Initial Catalog=vr;Integrated Security=True;User ID=Test;Password=test";
    public SqlConnection myConn;
    public string strComm;

    protected void Page_Load(object sender, EventArgs e)
    {

        Label label = FindControl("Label1") as Label; // debug tool


        myConn = new SqlConnection(strConn);
        myConn.Open();

    }

    protected void Register_Click(object sender, EventArgs e)
    {
        strComm = "INSERT INTO [dbo].[Student]([std_ID],[Birth],[Name],[Cellphone],[Password],[Address],[Email]) VALUES";

        //debug tool
        Label label = FindControl("Label1") as Label;

        TextBox id = FindControl("id") as TextBox;
        TextBox name = FindControl("name") as TextBox;
        TextBox email = FindControl("email") as TextBox;
        TextBox password = FindControl("password") as TextBox;
        TextBox birth = FindControl("birth") as TextBox;
        TextBox address = FindControl("address") as TextBox;
        Get_User_Data(in_id, name.Text, email.Text, password.Text, birth.Text, address.Text);

        RadioButton radio_STD = FindControl("radio_STD") as RadioButton;
        RadioButton radio_PRO = FindControl("radio_PRO") as RadioButton;
        RadioButton radio_ADM = FindControl("radio_ADM") as RadioButton;

        bool is_std = false, is_pro = false, is_adm = false;
        if (radio_STD.Checked)
        {
            strComm += "(" + in_id + "," + in_birth + "," + in_name + "," + in_cellphone + "," + in_password + "," + in_address + "," + in_mail + ");"; ;
            is_std = true;
        }
        else if (radio_PRO.Checked)
        {


            is_pro = true;
        }
        else
        {
            label.Text = "請先行確認身分";
            return;
        }

        SqlCommand reader = new SqlCommand(strComm, myConn);

        SqlDataReader data = reader.ExecuteReader();
        label.Text = data.ToString();

    }

    public static void Get_User_Data(string id, string name, string email, string password, string birth, string address)
    {
        in_id = id;
        in_name = name;
        in_mail = email;
        in_password = password;
        in_birth = birth;
        in_address = address;

    }

}
 