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
using MedicalStoreManagement.Repository;
using System.Xml.Serialization;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Web.Helpers;

namespace MedicalStoreManagement.Controllers
{
    public class UserPanelController : Controller
    {

        Context cn = new Context();

        public SqlConnection con;    //To Handle connection related activities
        public void connection()   //Method of Connection
        {
            string constr = ConfigurationManager.ConnectionStrings["mycon"].ToString();
            con = new SqlConnection(constr);
        }
        /*========================================================================================================================== */
        public ActionResult MainLogin()    //Main Login Page
        {
            return View();
        }
        /*========================================================================================================================== */
        public ActionResult Logout()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("MainLogin");
            }
            Session.Abandon();
            return RedirectToAction("MainLogin", "UserPanel");
        }
        /*========================================================================================================================== */
        public ActionResult Login()    //Admin Login Action Method
        { return View(); }
        [HttpPost]
        public ActionResult Login(LoginM log)
        {
            connection();
            SqlCommand cmd = new SqlCommand("spADMIN_CRUD", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@Email_id", log.Email_id);
            cmd.Parameters.AddWithValue("@Password", log.Password);
            cmd.Parameters.AddWithValue("@choice", "LOGIN");
            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.Read())
            {
                FormsAuthentication.SetAuthCookie(log.Email_id, true);     //Used for User Authentication Purpose
                Session["username"] = log.Email_id;
                //Session["user_id"] = sdr["User_id"];
                ViewBag.Message = "Login Success !!";
                return RedirectToAction("Dashboard");
            }
            else
            { ViewData["Message"] = "Username or Password Incorrect, Try Again!!"; }
            con.Close();
            return View();
        }
        /*========================================================================================================================== */
        public ActionResult UserLogin() //User Login Action Method
        {
            return View();
        }
        [HttpPost]
        public ActionResult UserLogin(UserLogin Ulog)
        {
            connection();
            SqlCommand cmd = new SqlCommand("spUSERS_CRUD", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@Email_id", Ulog.Email_id);
            cmd.Parameters.AddWithValue("@Password", Ulog.Password);
            cmd.Parameters.AddWithValue("@choice", "LOGIN");
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                FormsAuthentication.SetAuthCookie(Ulog.Email_id, true);
                Session["username"] = Ulog.Email_id;
                Session["user_id"] = sdr["User_id"];
                string ID = Session["user_id"].ToString();
                ViewData["Message"] = "Logged in Successfully";
                return RedirectToAction("UDashboard");
            }
            else
            {
                ViewData["Message"] = "Email-Id or PassWord is Incorrect, Try Again";
            }
            con.Close();
            return View();
        }
        /*========================================================================================================================== */
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registration(Registration Reg)
        {
            connection();
            {
                using (SqlCommand cmd = new SqlCommand("spUSERS_CRUD", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email_id", Reg.Email_id);
                    cmd.Parameters.AddWithValue("@Name", Reg.Name);
                    cmd.Parameters.AddWithValue("@Age", Reg.Age);
                    cmd.Parameters.AddWithValue("@Gender", Reg.Gender);
                    cmd.Parameters.AddWithValue("@Phone_no", Reg.Phone_no);
                    cmd.Parameters.AddWithValue("@Address", Reg.Address);
                    cmd.Parameters.AddWithValue("@Password", Reg.Password);
                    cmd.Parameters.AddWithValue("@RePassword", Reg.RePassword);
                    cmd.Parameters.AddWithValue("@choice", "INSERT");
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    TempData["Message"] = "User " + Reg.Name + " Registered successfully..!";
                }
            }
            return RedirectToAction("UDashboard");
        }
        /*========================================================================================================================== */
        public ActionResult AddBrands()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("MainLogin");
            }
            return View();
        }
        [HttpPost]
        public ActionResult AddBrands(Brands br)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("MainLogin");
            }
            connection();
            {
                int i = 0;
                using (SqlCommand cmd = new SqlCommand("spBRANDS_CRUD", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@B_name", br.B_name);
                    cmd.Parameters.AddWithValue("@choice", "INSERT");
                    con.Open();
                    i = cmd.ExecuteNonQuery();
                }
                if (i > 0)
                {
                    TempData["Message"] = "Brand " + br.B_name + " added successfully..!";    //not working
                }
                else
                {
                    TempData["Message"] = "Same Brand name " + br.B_name + " is already in Database..!";
                }
                con.Close();
            }
            return RedirectToAction("ShowBrands");
        }
        /*========================================================================================================================== */
        public ActionResult AddMed()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("MainLogin");
            }
            return View();
        }
        [HttpPost]
        public ActionResult AddMed(Medicines obj)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("MainLogin");
            }
            connection();
            {
                int i = 0;
                using (SqlCommand cmd = new SqlCommand("spMED_CRUD", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@M_Name", obj.M_Name);
                    cmd.Parameters.AddWithValue("@M_Type", obj.M_Type);
                    cmd.Parameters.AddWithValue("@Brand", obj.Brand);
                    cmd.Parameters.AddWithValue("@Exp_Date", obj.Exp_Date);
                    cmd.Parameters.AddWithValue("@Quantity", obj.Quantity);
                    cmd.Parameters.AddWithValue("@Price", obj.Price);
                    cmd.Parameters.AddWithValue("@Information", obj.Information);
                    cmd.Parameters.AddWithValue("@choice", "INSERT");
                    con.Open();
                    i = cmd.ExecuteNonQuery();
                }
                if (i > 0)
                {
                    TempData["Message"] = "Medicine " + obj.M_Name + " added successfully..!";
                }
                else
                {
                    TempData["Message"] = "Failed!! " + obj.M_Name + " already Exists..!!!";
                }
                con.Close();
            }
            return RedirectToAction("ShowMed");
        }
        /*========================================================================================================================== */
        public ActionResult MyCart()
        {
            return View();
        }   
        [HttpPost]
        public ActionResult MyCart(Cart crt)    //Add meds to Cart
        {
            connection();
            SqlCommand cmd = new SqlCommand("spCART_CRUD", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@User_id", crt.User_id);
            cmd.Parameters.AddWithValue("@M_Code", crt.M_Code);
            cmd.Parameters.AddWithValue("@cart_id", crt.Cart_id);
            cmd.Parameters.AddWithValue("@choice", "ADDTOCART");

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 1)
            {
                ViewBag["Message"] = "Medicine added to Cart successfully";
            }
            TempData["Message"] = "Medicine added to Cart successfully..!";
            return RedirectToAction("ShowMedUser");
        }
        /*========================================================================================================================== */
        public ActionResult ShowBrands()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("MainLogin");
            }
            connection();
            List<Brands> brandsList = new List<Brands>();
            SqlCommand com = new SqlCommand("spBRANDS_CRUD", con);
            com.Parameters.AddWithValue("@choice", "SHOW");
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            con.Open();
            adp.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)      //Bind EmpModel generic list using dataRow   
            {
                brandsList.Add(new Brands
                {
                    B_name = Convert.ToString(dr["B_name"]),
                    B_id = Convert.ToInt32(dr["B_id"])
                });
            }
            ModelState.Clear();
            return View(brandsList);
        }
        /*========================================================================================================================== */
        public ActionResult ShowMed()   //Shows Meds to the Admin
        {
            connection();
            List<Medicines> MedicinesList = new List<Medicines>();
            SqlCommand com = new SqlCommand("spMED_CRUD", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@choice", "SHOW");
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            con.Open();
            adp.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)    
            {
                MedicinesList.Add(new Medicines
                {
                    M_Code = Convert.ToInt32(dr["M_Code"]),
                    M_Name = Convert.ToString(dr["M_Name"]),
                    M_Type = Convert.ToString(dr["M_Type"]),
                    Brand = Convert.ToString(dr["Brand"]),
                    Exp_Date = Convert.ToDateTime(dr["Exp_Date"]),
                    Quantity = Convert.ToString(dr["Quantity"]),
                    Price = Convert.ToString(dr["Price"]),
                    Information = Convert.ToString(dr["Information"]),
                });
            }
            ModelState.Clear();
            if (Session["username"] == null)
            {
                return RedirectToAction("MainLogin");
            }
            return View(MedicinesList);
        }
        /*========================================================================================================================== */
        public ActionResult DeleteMed(int id)    //DELETE MEDICINE
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("MainLogin");
            }
            try
            {
                if (cn.boolDeleteMed(id))
                {
                    TempData["Message"] = "Medicine Deleted successfully..!";
                }
                return RedirectToAction("ShowMed");
            }
            catch { return View(); }
        }
        /*========================================================================================================================== */
        public ActionResult DeleteCartMed(int id)    //DELETE CART MEDICINE
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("MainLogin");
            }
                Context cn = new Context();
                cn.boolDeleteCartMed(id);
                TempData["Message"] = "Medicine Deleted successfully..!";
                return RedirectToAction("ShowCart");
        }
        /*========================================================================================================================== */
        public ActionResult DeleteBrand(int id)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("MainLogin");
            }
            try
            {
                Context cn = new Context();
                if (cn.boolDeleteBrand(id))
                {
                    TempData["Message"] = "Brand Deleted successfully..!";
                }
                return RedirectToAction("ShowBrands");
            }
            catch { return View(); }
        }
        /*========================================================================================================================== */
        public ActionResult EditMed(int id)
        {
            
            Session["Med_code"] = id;
            ViewBag.id = Session["Med_code"];
            return View(cn.GetMedById().Find(med => med.M_Code == id));
        }
        /*===========================================================*/
        [HttpPost]
        public ActionResult EditMed( Medicines obj)
        {
            cn.UpdateMed(obj);
            TempData["Message"] = "Medicine Updated successfully..!";
            return RedirectToAction("ShowMed");
        }
        /*========================================================================================================================== */
        public ActionResult EditUser(string id)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("MainLogin");
            }
            ViewBag.id = id;
            return View(cn.GetUserById(Session["user_id"].ToString()).Find(uu => uu.Email_id == id));
        }
        [HttpPost]
        public ActionResult EditUser(string id, UserLogin ulog)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("MainLogin");
            }
            cn.UpdateUser(ulog);
            TempData["Message"] = "Profile Updated successfully..!";
            return RedirectToAction("MyProfile");
        }
        /*========================================================================================================================== */
        public ActionResult EditAdmin(int id)
        {
            ViewBag.id = id;
            return View();
        }
        [HttpPost]
        public ActionResult EditAdmin(LoginM Alog)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("MainLogin");
            }
            cn.UpdateAdmin(Alog);
            TempData["Message"] = "Password Updated successfully..!";
            return RedirectToAction("AdProfile");
        }
        /*========================================================================================================================== */
        public ActionResult MedDetails(int id)     //For ADMIN
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("MainLogin");
            }
            try
            {
                var med = cn.GetMedById().FirstOrDefault();
                if (med == null)
                {
                    TempData["Message"] = "Medicine not available with Medicine Code" + id.ToString();
                    return RedirectToAction("ShowMed");
                }
                return View(med);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
        /*========================================================================================================================== */
        public ActionResult MedDetailsUser(int id)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("MainLogin");
            }
            try
            {
                var med = cn.GetMedById().FirstOrDefault();
                if (med == null)
                {
                    TempData["Message"] = "Medicine not available with Medicine Code " + id.ToString();
                    return RedirectToAction("ShowMedUser");
                }
                return View(med);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
        /*========================================================================================================================== */
        public ActionResult Dashboard() //Admin Dashboard
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("MainLogin");
            }
            return View();
        }
        /*========================================================================================================================== */
        public ActionResult UDashboard()   //User Dashboard
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("MainLogin");
            }
            return View();
        }
        /*========================================================================================================================== */
        public ActionResult MyProfile(string id)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("MainLogin");
            }
            var row = cn.GetUserById(Session["username"].ToString());
            return View(row);
        }
        /*========================================================================================================================== */
        public ActionResult AdProfile(string id)
        {
            var row = cn.GetAdminById(Session["username"].ToString());
            return View(row);
        }

        /*========================================================================================================================== */
        public ActionResult ShowMedUser()
        {
            connection();
            List<Medicines> MedicinesList = new List<Medicines>();
            SqlCommand com = new SqlCommand("spMED_CRUD", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@choice", "SHOW");
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            con.Open();
            adp.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                MedicinesList.Add(new Medicines
                {
                    M_Name = Convert.ToString(dr["M_Name"]),
                    M_Code = Convert.ToInt16(dr["M_Code"]),
                    M_Type = Convert.ToString(dr["M_Type"]),
                    Brand = Convert.ToString(dr["Brand"]),
                    Exp_Date = Convert.ToDateTime(dr["Exp_Date"]),
                    Quantity = Convert.ToString(dr["Quantity"]),
                    Price = Convert.ToString(dr["Price"]),
                    Information = Convert.ToString(dr["Information"])
                });
            }
            ModelState.Clear();
            if (Session["username"] == null)
            {
                return RedirectToAction("MainLogin");
            }
            return View(MedicinesList);
        }
        /*========================================================================================================================== */
        public ActionResult ShowCart()
        {
            var row = cn.GetCartById(Int32.Parse(Session["user_id"].ToString()));
            return View(row);
        }
        /*========================================================================================================================== */
        public ActionResult PlaceOrder(int id)
        {
            ViewBag.id = id;
            Session["Cart_id"] = id;
            return View();
        }
        [HttpPost]
        public ActionResult PlaceOrder(PlacedOrder crt)
        {
            cn.UpdateCart(crt);
            TempData["message"] = "Order placed successfully..!";
            return RedirectToAction("Showcart");
        }
        /*========================================================================================================================== */
        public ActionResult Orders()    //For Users
        {
            var row = cn.GetOrderById(Int32.Parse(Session["user_id"].ToString()));
            return View(row);
        }
        /*========================================================================================================================== */
        public ActionResult AllOrders()    //For Users
        {
            var row = cn.GetAllOrders();

            return View(row);
        }
        /*========================================================================================================================== */
    }
}