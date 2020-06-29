using StdHMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StdHMS.Controllers
{
    public class LoginController : Controller
    {
        private PatientDBContext db = new PatientDBContext();
        public ActionResult Login()
        {
            ViewBag.Title = "Login Page";

            return View();
        }

        public String loginAttempt(String username, String password)
        {
            User register = new User();

            //Now this User object is ready to insert in database.
            try
            {
                User patient = db.patients.Find(username);

                if (patient != null)
                {
                    Response.Cookies["username"].Value = username;
                    Response.Cookies["password"].Value = password;
                    Response.Cookies["role"].Value = patient.role;


                    return "Successful logged in";
                }
                else
                {
                    return "Username or Password is incorrect";
                }
                
            }
            catch (Exception ex)
            {
                return "Username or Password is incorrect";
            }
        }

        public bool isAlreadyLogin()
        {
            bool isAlreadyLogin = false;

            string username = "";
            string password = "";
            if(Request.Cookies["username"] !=null && Request.Cookies["password"] != null)
            {
                username = Request.Cookies["username"].Value;
            }
            if(username!=null && !username.Equals(""))
            {
                isAlreadyLogin = true;
            }

            return isAlreadyLogin;
        }
    }
}
