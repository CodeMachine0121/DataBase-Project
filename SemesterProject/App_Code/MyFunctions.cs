using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

/// <summary>
/// MyFunctions 的摘要描述
/// </summary>
public class MyFunctions
{
    public string strConn = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["connectionWord"].ConnectionString;
    public MyFunctions()
    {
        //
        // TODO: 在這裡新增建構函式邏輯
        //
    }

    public string Token()
    {
        int length = 10;
        // creating a StringBuilder object
        StringBuilder str_build = new StringBuilder();
        Random random = new Random();

        char letter;
        for(int i = 0; i < length; i++)
        {
            double flt = random.NextDouble();
            int shift = Convert.ToInt32(Math.Floor(25 * flt));
            letter = Convert.ToChar(shift + 65); // 'A'=65
            str_build.Append(letter);
        }
        string token = str_build.ToString();

        return token;
        
    }
    public string SHA256(string data)
    {
        byte[] bytes = Encoding.UTF8.GetBytes((string)data);
        byte[] hash = SHA256Managed.Create().ComputeHash(bytes);

        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < hash.Length; i++)
        {
            builder.Append(hash[i].ToString("X2"));
        }
        return builder.ToString().ToLower();

    }

    //透過Session ID 取得使用者ID 
    public string Get_ID(string command, int status)
    {
        SqlConnection myConn;
        myConn = new SqlConnection(strConn);
        myConn.Open();
        SqlCommand reader = new SqlCommand(command, myConn);
        SqlDataReader data = reader.ExecuteReader();

        string identify = "";
        if (status == 1)
            identify = "std_ID";
        else if (status == 2)
            identify = "pro_ID";
        else if (status == 3)
            identify = "adm_ID";

        if (data.Read())
        {
            string id = data[identify].ToString();
            data.Close();
            reader.Dispose();
            myConn.Close();
            myConn.Dispose();
            return id;
        }
        else
        {
            data.Close();
            reader.Dispose();
            myConn.Close();
            myConn.Dispose();
            return null;
        }
    }


    //透過教授編號取得 教授名稱
    public string Get_Name_from_proID(string pro_ID)
    {
        string command = "select Name from professor where pro_ID = '" +pro_ID + "';";

        SqlConnection myConn;
        myConn = new SqlConnection(strConn);
        myConn.Open();

        SqlCommand reader = new SqlCommand(command, myConn);
        SqlDataReader data = reader.ExecuteReader();

        if (data.Read())
            return data["Name"].ToString();
        else
            return null;

    }

    // 取得全部課程邊號
    public string[] Get_CID_from_Courses()
    {
        SqlConnection myConn;
        myConn = new SqlConnection(strConn);
        myConn.Open();

        // how many courses had the student choosen
        string command = "select count(1) from Courses ;";
        SqlCommand scalar = new SqlCommand(command, myConn);
        int Amount_of_Courses = (int)scalar.ExecuteScalar();
        scalar.Dispose();

        string[] cid = new string[Amount_of_Courses];

        // get Course id 
        command = "select CID from Courses;";
        SqlCommand reader = new SqlCommand(command, myConn);
        SqlDataReader data = reader.ExecuteReader();

        int i = 0;
        while (data.Read())
        {
            cid[i++] = data["CID"].ToString();
        }

        data.Close();
        reader.Dispose();
        myConn.Close();
        myConn.Dispose();
        return cid;
    }
    // 更新學生總學分
    public bool UPDATE_Total_Credit(string ID,string CID,bool flag)
    {
        int credit=0;
        int total_credit = 0;
        string command;
        SqlConnection myConn;
        myConn = new SqlConnection(strConn);
        myConn.Open();

        // how many courses had the student choosen

        command = "select Credit from Courses where ( CID = '" + CID + "');";

        SqlCommand reader = new SqlCommand(command, myConn);
        SqlDataReader data = reader.ExecuteReader();
        if (data.Read())
            credit = Int32.Parse(data["Credit"].ToString());
        data.Close();
        reader.Dispose();


        //--------------------------------
        //目前總學分
        command = "select total_Credit from Student where ( std_ID = '" + ID + "');";

        reader = new SqlCommand(command, myConn);
        data = reader.ExecuteReader();
        if (data.Read())
            total_credit = Int32.Parse(data["total_Credit"].ToString());
        data.Close();
        reader.Dispose();

        //----------------------------------
        // 加起來
        if(flag)
            command = "UPDATE [dbo].[Student] SET [total_Credit]=" + (total_credit+credit).ToString() + " where std_ID = '" + ID + "';";
        else
            command = "UPDATE [dbo].[Student] SET [total_Credit]=" + (total_credit - credit).ToString() + " where std_ID = '" + ID + "';";

        reader = new SqlCommand(command, myConn);

        reader.ExecuteNonQuery();
        reader.Dispose();
        myConn.Close();
        myConn.Dispose();
        return true;
    }
    // 課程池取學生
    public string[] Get_Course_By_StdID(string ID)
    {
        
        string command;

        SqlConnection myConn;
        myConn = new SqlConnection(strConn);
        myConn.Open();
        
        // how many courses had the student choosen
        
        command = "select count(1) from Course_Pool where ( std_ID = '" + ID + "');";

        SqlCommand scalar = new SqlCommand(command, myConn);
        int Amount_of_Courses = (int)scalar.ExecuteScalar();
        scalar.Dispose();

        string[] cid = new string[Amount_of_Courses];

        // get Course id 
        command = "select CID from Course_Pool where std_ID = '" + ID + "';";
        SqlCommand reader = new SqlCommand(command, myConn);
        SqlDataReader data = reader.ExecuteReader();

        int i = 0;
        while (data.Read())
        {
            cid[i++] = data["CID"].ToString();
        }

        data.Close();
        reader.Dispose();
        myConn.Close();
        myConn.Dispose();
        return cid;
    }
   

    // 取得學生課程的時間
    public string[] Get_CourseTime(string[] cid)
    {
        SqlConnection myConn;
        myConn = new SqlConnection(strConn);
        myConn.Open();
        string[] ctime = new string[cid.Length];

        for (int i = 0; i < cid.Length; i++)
        {
            string command = "select Ctime from Courses where CID = '" + cid[i] + "';";
            SqlCommand reader = new SqlCommand(command, myConn);
            SqlDataReader data = reader.ExecuteReader();

            if (data.Read())
            {
                ctime[i] = data["Ctime"].ToString();
            }

            data.Close();
            reader.Dispose();
        }
        myConn.Close();
        myConn.Dispose();
        return ctime;
    }
    public string Get_CourseTime(string cid)
    {
        string ctime = "";
        SqlConnection myConn;
        myConn = new SqlConnection(strConn);
        myConn.Open();
        string command = "select Ctime from Courses where CID = '" + cid + "';";
        SqlCommand reader = new SqlCommand(command, myConn);
        SqlDataReader data = reader.ExecuteReader();

        if (data.Read())
        {
            ctime = data["Ctime"].ToString();
        }
        data.Close();
        reader.Dispose();
        myConn.Close();
        myConn.Dispose();
        return ctime;
    }

    // 透過課程標號 取得 教授名稱
    public string[] Get_Professor_Name(string[] cid)
    {
       
        string[] cpro = new string[cid.Length];
        SqlConnection myConn;
        myConn = new SqlConnection(strConn);
        myConn.Open();
        for (int i = 0; i < cid.Length; i++)
        {
          

            string command = "select pro_ID from Courses where CID = '" + cid[i] + "';";
            SqlCommand reader = new SqlCommand(command, myConn);
            SqlDataReader data = reader.ExecuteReader();

            if (data.Read())
            {
                string proid = data["pro_ID"].ToString();
                SqlConnection sqlConnection = new SqlConnection(strConn);

                SqlCommand GetName = new SqlCommand("select Name from Professor", myConn);
                SqlDataReader dataReader = GetName.ExecuteReader();
                if(dataReader.Read())
                    cpro[i] = dataReader["Name"].ToString();

                dataReader.Close();
                GetName.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            data.Close();
            reader.Dispose();

           
        }
        myConn.Close();
        myConn.Dispose();

        return cpro;
    }


    // 透過教授編號取得 課程標號
    public string[] Get_Course_By_ProID(string ID)
    {
        string command;

        SqlConnection myConn;
        myConn = new SqlConnection(strConn);
        myConn.Open();

        // how many courses had the student choosen

        command = "select count(1) from Courses where ( pro_ID = '" + ID + "');";

        SqlCommand scalar = new SqlCommand(command, myConn);
        int Amount_of_Courses = (int)scalar.ExecuteScalar();
        scalar.Dispose();


        string[] cid = new string[Amount_of_Courses];

        command = "select CID from Courses where pro_ID = '" + ID + "';";
        SqlCommand reader = new SqlCommand(command, myConn);
        SqlDataReader data = reader.ExecuteReader();

        int i = 0;
        while (data.Read())
        {
            cid[i++] = data["CID"].ToString();
        }
        data.Close();
        reader.Dispose();
        myConn.Close();
        myConn.Dispose();
        return cid;

    }


    //取得上課教室
    public string[] Get_Courses_room(string[] cid)
    {
        string[] croom = new string[cid.Length];
        SqlConnection myConn;
        myConn = new SqlConnection(strConn);
        myConn.Open();
        for (int i=0;i<cid.Length; i++)
        {

            string command = "select Classroom from Courses where CID = '" + cid[i] + "';";
            SqlCommand reader = new SqlCommand(command, myConn);
            SqlDataReader data = reader.ExecuteReader();
            
            if (data.Read())
            {
                croom[i] = data["Classroom"].ToString();
            }

            data.Close();
            reader.Dispose();

        }
        myConn.Close();
        myConn.Dispose();

        return croom;
    }
    // 檢查有無此課程
    public bool Is_Course_Exist(string cid)
    {

        SqlConnection myConn;
        myConn = new SqlConnection(strConn);
        myConn.Open();

        string command = "select count(1) from Courses where CID = '" + cid + "';";
        SqlCommand scalar = new SqlCommand(command, myConn);

        int CourseExist = (int)scalar.ExecuteScalar();

        if (CourseExist == 0)
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

    // 學生是不是重複選課
    public bool Is_Course_Choosed(string cid, string std_ID)
    {
        SqlConnection myConn;
        myConn = new SqlConnection(strConn);
        myConn.Open();

        string command = "select count(1) from Course_Pool where (CID = '" + cid + "' AND std_ID = '" + std_ID + "');";

        SqlCommand scalar = new SqlCommand(command, myConn);

        int CourseChoose = (int)scalar.ExecuteScalar();

        if (CourseChoose == 0)
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
    
    // 衝堂檢查
     public bool Is_Course_Conflict(string std_ID,string cid)
    {
       
        string[] CID = this.Get_Course_By_StdID (std_ID);
        string[] Ctime =this.Get_CourseTime(CID);
        //想選的課
        string ctime = this.Get_CourseTime(cid);
        //Hashtable hashtable = new Hashtable();
        // 01010203 vs 01020304
        string cl = "";
        for (int i = 2; i < ctime.Length; i++)
            cl += ctime[i];


        for (int i = 0; i < Ctime.Length; i++)
        {
            int counter = 0;
            string CL = "";
            for (int j = 2; i < ctime.Length; i++)
                CL += ctime[i];

            if((Ctime[i][0].ToString() + Ctime[i][1].ToString()) == (ctime[0].ToString() + ctime[1].ToString()))
            {

                for (int j = 1; j < ctime.ToCharArray().Length; j += 2)
                {
                    if (cl[j] == CL[j])
                    {
                        counter++;
                    }
                }
                if (counter != 0)
                    return true;
            }

        }
        return false;

    }
    //人數檢查
    public bool Is_Course_Full(string cid)
    { // true:滿 false:沒滿
        bool flag = false;
        SqlConnection myConn;
        myConn = new SqlConnection(strConn);
        myConn.Open();

        string command = "select Climit,CAmount from Courses where CID = '" + cid + "';";
        SqlCommand reader = new SqlCommand(command, myConn);
        SqlDataReader data = reader.ExecuteReader();

        if (data.Read())
        {
            int climit  =Int32.Parse( data["Climit"].ToString() );
            int camount = Int32.Parse(data["CAmount"].ToString());

            if (camount >= climit)
                flag = true;
        }

        data.Close();
        reader.Dispose();
        myConn.Close();
        myConn.Dispose();

        return flag;
    }
    // 取得目前選課人數
    private int Get_CAmount(string cid)
    {
        SqlConnection myConn;
        myConn = new SqlConnection(strConn);
        myConn.Open();

        string command = "select CAmount from Courses where CID = '" + cid + "';";
        SqlCommand reader = new SqlCommand(command, myConn);
        SqlDataReader data = reader.ExecuteReader();

        int camount = 0;
        if (data.Read())
        {
          camount = Int32.Parse(data["CAmount"].ToString());
        }

        data.Close();
        reader.Dispose();
        myConn.Close();
        myConn.Dispose();
        return camount;
    }
    // 選課人數 ++
    public bool Course_Amount_Plus(string cid)
    {
        SqlConnection myConn = new SqlConnection(strConn);
        myConn.Open();
        int camount = this.Get_CAmount(cid);

        string strComm = "UPDATE [dbo].[Courses] SET [CAmount]="+(++camount).ToString() + " where cid = '"+cid+"';";
        try
        {
            SqlCommand reader = new SqlCommand(strComm, myConn);

            reader.ExecuteNonQuery();
            reader.Dispose();
            myConn.Close();
            myConn.Dispose();

        }
        catch
        {
            return false;
        }

        return true;
    }
    // 選課人數 --
    public bool Course_Amount_Minus(string cid)
    {
        SqlConnection myConn = new SqlConnection(strConn);
        myConn.Open();
        int camount = this.Get_CAmount(cid);

        string strComm = "UPDATE [dbo].[Courses] SET [CAmount]=" + (--camount).ToString() + " where cid = '" + cid + "';";
        try
        {
            SqlCommand reader = new SqlCommand(strComm, myConn);

            reader.ExecuteNonQuery();
            reader.Dispose();
            myConn.Close();
            myConn.Dispose();

        }
        catch
        {
            return false;
        }

        return true;
    }
    // 課程ID -> 名稱
    public string[] Course_ID_Name_Change(string[] cid)
    {
        string[] name = new string[cid.Length];
        SqlConnection myConn;
        myConn = new SqlConnection(strConn);
        myConn.Open();

        for (int i = 0; i < cid.Length; i++)
        {

            string command = "Select Name from Courses where CID = '" + cid[i] + "';";
            SqlCommand reader = new SqlCommand(command, myConn);
            SqlDataReader data = reader.ExecuteReader();
            if (data.Read())
                name[i] = data["Name"].ToString();

            data.Close();
            reader.Dispose();
        }


        myConn.Close();
        myConn.Dispose();
        return name;
    }
}
