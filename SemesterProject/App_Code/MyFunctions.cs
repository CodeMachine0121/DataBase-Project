using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

/// <summary>
/// MyFunctions 的摘要描述
/// </summary>
public class MyFunctions
{
    public MyFunctions()
    {
        //
        // TODO: 在這裡新增建構函式邏輯
        //
    }

    public string SHA256(string data)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(data);
        byte[] hash = SHA256Managed.Create().ComputeHash(bytes);

        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < hash.Length; i++)
        {
            builder.Append(hash[i].ToString("X2"));
        }
        return builder.ToString().ToLower();

    }

}