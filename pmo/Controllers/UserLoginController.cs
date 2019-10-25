using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using pmo.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace pmo.Controllers
{
    public class UserLoginController : Controller
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);

        // GET: UserLogin
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult index(User usr)
        {
            SqlCommand cmd = new SqlCommand("Select count(*) from admin where user_name='" + usr.userName + "' and Password='" + usr.password + "'", conn);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            int cnt = (int)cmd.ExecuteScalar();
            if (cnt > 0)
            {
                Session["UserID"] = Guid.NewGuid();
                return RedirectToAction("Home","pmoadmin");
            }
            else
            {
                ModelState.AddModelError("", "Invalid Login Attempt");
                return View(usr);
            }
        }

    }
}