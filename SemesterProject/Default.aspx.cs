using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    /*
    protected void Register_Click(object sender, EventArgs e)
    {
        Label label = FindControl("test") as Label;
        TextBox name = FindControl("name") as TextBox;
        TextBox email = FindControl("email") as TextBox;
        TextBox password = FindControl("password") as TextBox;
        TextBox invite_code = FindControl("invite_conde") as TextBox;
        label.Text = password.Text;


        Console.WriteLine("Create");
    }
    */



    protected void Sign_in_Click(object sender, EventArgs e)
    {

    }

    protected void Sign_up_Click(object sender, EventArgs e)
    {
        Response.Redirect("Signup.aspx");
    }
}