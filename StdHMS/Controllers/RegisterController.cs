using StdHMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace StdHMS.Controllers
{
    public class RegisterController : Controller
    {
        private PatientDBContext db = new PatientDBContext();
        public ActionResult register()
        {
            ViewBag.Title = "Register";

            return View();
        }

        public List<User> getAllDoctors()
        {
            string hospital = Response.Cookies["username"].Value;
            return db.patients.Where(co => co.role == "doctor" && co.hospitalId == hospital).ToList();
        }


        public String registerUser(String nameOnly, String username, String password, String phoneNumber, string role)
        {
            //if at registration time, role is doctor, we have to add the hospital id, which would be the id of the user that is registering
            string hospitalId = "";

            if (role.Equals("doctor")) {
                //set hospital id to the current user id which should be hospital id
                hospitalId = Request.Cookies["username"].Value;
            }

            User register = new User(nameOnly, username, password, phoneNumber, role, hospitalId);

            //Now this User object is ready to insert in database.
            try
            {

                db.patients.Add(register);
                db.SaveChanges();
                return "Sign Up Successful";
            }
            catch(Exception ex)
            {
                return "This username already exists "+ ex.Message;
            }
        }
    }
}
