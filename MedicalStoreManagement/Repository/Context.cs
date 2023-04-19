using System.Drawing;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.EnterpriseServices;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Services.Description;
using MedicalStoreManagement.Models;
using System.Xml.Serialization;
using System.Web.Helpers;
using MedicalStoreManagement.Controllers;
using System.Security.Cryptography;
using System.Reflection.Emit;
using System.Xml.Linq;


namespace MedicalStoreManagement.Repository
{
    public class Context
    {
        public SqlConnection con;    //To Handle connection related activities
        public void connection()   //Method of Connection
        {
            string constr = ConfigurationManager.ConnectionStrings["mycon"].ToString();
            con = new SqlConnection(constr);
        }
        /*========================================================================================================================== */
        public bool boolDeleteBrand(int id)
        {
            connection();
            SqlCommand com = new SqlCommand("spBRANDS_CRUD", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@B_id", id);
            com.Parameters.AddWithValue("@choice", "DELETE");
            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            { return true; }
            else
            { return false; }
        }
        /*========================================================================================================================== */
        public bool boolDeleteMed(int Id)
        {
            connection();
            SqlCommand com = new SqlCommand("spMED_CRUD", con); //Earlier spDeleteMed
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@M_Code", Id);
            com.Parameters.AddWithValue("@choice", "DELETE");
            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            { return false; }
        }
        /*========================================================================================================================== */
        public bool boolDeleteCartMed(int Id)
        {
            connection();
            SqlCommand com = new SqlCommand("spCART_CRUD", con); //Earlier spDeleteMed
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Cart_id", Id);
            com.Parameters.AddWithValue("@choice", "DELETE");
            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            { return false; }
        }
        /*========================================================================================================================== */
        public List<Medicines> GetMedById()
        {
            connection();
            List<Medicines> MedList = new List<Medicines>();
            SqlCommand cmd = new SqlCommand("spMED_CRUD", con);
            cmd.Parameters.AddWithValue("@choice", "SHOW");
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)      //Bind EmpModel generic list using dataRow   
            {
                MedList.Add(new Medicines
                {
                    M_Code = Convert.ToInt32(dr["M_Code"]),
                    M_Name = Convert.ToString(dr["M_Name"]),
                    M_Type = Convert.ToString(dr["M_Type"]),
                    Brand = Convert.ToString(dr["Brand"]),
                    Exp_Date = Convert.ToDateTime(dr["Exp_Date"]),
                    Price = Convert.ToString(dr["Price"]),
                    Quantity = Convert.ToString(dr["Quantity"]),
                    Information = Convert.ToString(dr["Information"]),
                });
            }
            return MedList;
        }
        /*========================================================================================================================== */
        public List<UserLogin> GetUserById(string email)   //User Profile
        {
            connection();
            List<UserLogin> UserList = new List<UserLogin>();
            SqlCommand cmd = new SqlCommand("spUSERS_CRUD", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Email_id", email);
            cmd.Parameters.AddWithValue("@choice", "SHOWBYID");
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open(); //BDW :In case of Data Adapter U don't need to open or close connection
            adp.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)      //Bind EmpModel generic list using dataRow   
            {
                UserList.Add(new UserLogin
                {
                    User_id = Convert.ToInt32(dr["User_id"]),
                    Email_id = Convert.ToString(dr["Email_id"]),
                    Password = Convert.ToString(dr["Password"]),
                    Name = Convert.ToString(dr["Name"]),
                    Age = Convert.ToString(dr["Age"]),
                    Gender = Convert.ToString(dr["Gender"]),
                    Phone_no = Convert.ToString(dr["Phone_no"]),
                    Adderess = Convert.ToString(dr["Address"])
                });
            }
            return UserList;
        }
        /*========================================================================================================================== */
        public List<LoginM> GetAdminById(string email)   //User Profile
        {
            connection();
            List<LoginM> AdminList = new List<LoginM>();
            SqlCommand cmd = new SqlCommand("spADMIN_CRUD", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Email_id", email);
            cmd.Parameters.AddWithValue("@choice", "SHOWBYID");
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open(); //BDW :In case of Data Adapter U don't need to open or close connection
            adp.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)      //Bind EmpModel generic list using dataRow   
            {
                AdminList.Add(new LoginM
                {
                    User_id = Convert.ToInt32(dr["id"]),
                    Email_id = Convert.ToString(dr["Email_id"]),
                    Password = Convert.ToString(dr["Password"])
                });
            }
            return AdminList;
        }
        /*========================================================================================================================== */
        public List<Cart> GetCartById(int user_id)   //to Get the Medicines added to cart
        {
            connection();
            List<Cart> CartList = new List<Cart>();
            SqlCommand cmd = new SqlCommand("spCART_CRUD", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@user_id", user_id);
            cmd.Parameters.AddWithValue("@choice", "SHOWMYCART");
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open(); //BDW :In case of Data Adapter V don't need to open or close connection
            adp.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)      //Bind EmpModel generic list using dataRow   
            {
                CartList.Add(new Cart
                {
                    Cart_id = Convert.ToInt32(dr["Cart_id"]),
                    User_id = Convert.ToInt32(dr["User_id"]),
                    M_Code = Convert.ToInt32(dr["M_Code"]),
                    M_Name = Convert.ToString(dr["M_Name"]),
                    Qty = Convert.ToString(dr["Qty"]),
                    Price = Convert.ToInt32(dr["Price"]),
                    Information = Convert.ToString(dr["Information"])
                });
            }
            return CartList;
        }
        /*========================================================================================================================== */
        public List<Cart> GetOrderById(int user_id)   //to Get the Order List
        {
            connection();
            List<Cart> CartList = new List<Cart>();
            SqlCommand cmd = new SqlCommand("spCART_CRUD", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@user_id", user_id);
            cmd.Parameters.AddWithValue("@choice", "SHOWMYORDER");
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open(); //BDW :In case of Data Adapter V don't need to open or close connection
            adp.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)      //Bind EmpModel generic list using dataRow   
            {
                CartList.Add(new Cart
                {
                    M_Name = Convert.ToString(dr["M_Name"]),
                    Sale_date = Convert.ToString(dr["Sale_date"]),
                    Qty = Convert.ToString(dr["Qty"]),
                    Price = Convert.ToInt32(dr["Price"]),
                    Total_price = Convert.ToString(dr["Total_price"])
                });
            }
            return CartList;
        }
        /*========================================================================================================================== */
        public List<Cart> GetAllOrders()   //to Get the Order List
        {
            connection();
            List<Cart> CartList = new List<Cart>();
            SqlCommand cmd = new SqlCommand("spCART_CRUD", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@choice", "SHOWALLORDERS");
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open(); //BDW :In case of Data Adapter V don't need to open or close connection
            adp.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)      //Bind EmpModel generic list using dataRow   
            {
                CartList.Add(new Cart
                {
                    M_Name = Convert.ToString(dr["M_Name"]),
                    Sale_date = Convert.ToString(dr["Sale_date"]),
                    Qty = Convert.ToString(dr["Qty"]),
                    Price = Convert.ToInt32(dr["Price"]),
                    Total_price = Convert.ToString(dr["Total_price"])
                });
            }
            return CartList;
        }
        /*========================================================================================================================== */
        public bool UpdateMed(Medicines med)    //Model's obj is being passed as a Parameter
        {
            try
            {
                connection();
                SqlCommand com = new SqlCommand("spMED_CRUD", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@M_Code", med.M_Code);
                com.Parameters.AddWithValue("@M_Name", med.M_Name);
                com.Parameters.AddWithValue("@M_Type", med.M_Type);
                com.Parameters.AddWithValue("@Brand", med.Brand);
                com.Parameters.AddWithValue("@Exp_Date", med.Exp_Date);
                com.Parameters.AddWithValue("@Quantity", med.Quantity);
                com.Parameters.AddWithValue("@Price", med.Price);
                com.Parameters.AddWithValue("@Information", med.Information);
                com.Parameters.AddWithValue("@choice", "UPDATE");
                con.Open();
                int i = com.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {   Console.WriteLine("Error" + ex);    }
            return true;
        }
        /*========================================================================================================================== */
        public bool UpdateUser(UserLogin uu)
        {
            connection();
            SqlCommand cmd = new SqlCommand("spUSERS_CRUD", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@User_id", uu.User_id);
            cmd.Parameters.AddWithValue("@Password", uu.Password);
            cmd.Parameters.AddWithValue("@Name", uu.Name);
            cmd.Parameters.AddWithValue("@Age", uu.Age);
            cmd.Parameters.AddWithValue("Phone_no", uu.Phone_no);
            cmd.Parameters.AddWithValue("@Address", uu.Adderess);
            cmd.Parameters.AddWithValue("@choice", "UPDATE");
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 1) { return true; }
            else { return false; }
        }
        /*========================================================================================================================== */
        public bool UpdateAdmin(LoginM uu)
        {
            connection();
            SqlCommand cmd = new SqlCommand("spADMIN_CRUD", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", uu.User_id);
            cmd.Parameters.AddWithValue("@Password", uu.Password);
            cmd.Parameters.AddWithValue("@choice", "UPDATE");
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 1) { return true; }
            else { return false; }
        }
        /*========================================================================================================================== */
        public bool UpdateCart(PlacedOrder uc) //To Place Order
        {
            try
            {
                connection();
                SqlCommand cmd = new SqlCommand("spCART_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Cart_id", uc.Cart_id);
                cmd.Parameters.AddWithValue("@Qty", Convert.ToInt32(uc.Qty));
                cmd.Parameters.AddWithValue("@choice", "PlaceOrder");
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch(Exception ex) 
            {
                Console.WriteLine("Error" + ex);
            }
            return true;
        }
    }
}