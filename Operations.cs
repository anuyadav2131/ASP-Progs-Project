using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MedicalStoreManagement.Models;

public class 
{
    public bool DelBrand(int id)
    {
        connection();
        using (SqlCommand cmd = new SqlCommand("spdeleteBrand", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@B_id", B_id);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
