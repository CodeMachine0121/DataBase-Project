using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Table : System.Web.UI.Page
{
    public string strConn = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["connectionWord"].ConnectionString;
    public MyFunctions myFunctions = new MyFunctions();
    protected void Page_Load(object sender, EventArgs e)
    {
        string[] cid = myFunctions.Get_CID_from_Courses();
        string[] cname = myFunctions.Course_ID_Name_Change(cid);
        string[] ctime = myFunctions.Get_CourseTime(cid);
        string[] croom = myFunctions.Get_Courses_room(cid);
        string[] proName = myFunctions.Get_Professor_Name(cid);

        string html = "<table BORDER='2'> <tr align=center> <td>課程編號</td> <td>課程名稱</td> <td>授課教授</td> <td>授課教室</td> <td>上課時間</td> </tr> ";

        for (int i = 0; i < cid.Length; i++)
        {
            html += "<tr align=center>";

            html += "<td>" + cid[i] + "</td>";
            html += "<td>" + cname[i] + "</td>";
            html += "<td>" + proName[i] + "</td>";
            html += "<td>" + croom[i] + "</td>";
            html += "<td>" + ctime[i] + "</td>";
            html += "</tr>";

        }
        html += "</table>";

        Response.Write(html);
    }

    
    


  

}