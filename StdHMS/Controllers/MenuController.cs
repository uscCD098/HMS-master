using StdHMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;

namespace StdHMS.Controllers
{
    public class MenuController : Controller
    {
        private PatientDBContext db = new PatientDBContext();

        public ActionResult Menu()
        {
            string role = Request.Cookies["role"].Value;
            ViewBag.Title = "Menu";
            if (role.ToLower().Equals(Constants.adminRole.ToLower()))
            {
                return View("AdminMenu");
            }
            else if (role.ToLower().Equals(Constants.patientRole.ToLower()))
            {
                return View("PatientMenu");
            }
            else
            {
                return View("DoctorMenu");
            }
        }

        public String logoutRequest()
        {
            Response.Cookies["username"].Value = "";
            Response.Cookies["password"].Value = "";
            Response.Cookies["role"].Value = "";

            return "You are successfully logged out!";
        }

        public String setDoctorTiming(int startTime, int endTime)
        {
            string username = Request.Cookies["username"].Value;
            DoctorAvailability doctorAvailability = new DoctorAvailability(username, startTime, endTime);
            try
            {
                db.doctorAvailability.AddOrUpdate(doctorAvailability);
                db.SaveChanges();
                return "Successfuly Updated!";
            }
            catch (Exception ex)
            {
                return "This username already exists";
            }
            
        }
        public string getAllDoctors()
        {
            List<User> u = db.patients.Where(co => co.role == "doctor").ToList();
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(u);
            return jsonString;
        }



        //We got to get doctors for that hospital only....

        public string getAllDoctorsForHospital() {
            string hostpitalId = Request.Cookies["username"].Value;
            List<User> u = db.patients.Where(co => co.role == "doctor" && co.hospitalId == hostpitalId).ToList();
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(u);
            return jsonString;
        }


        public List<String> getAllDoctorsForHospitalAsList()
        {
            string hostpitalId = Request.Cookies["username"].Value;
            List<String> u = db.patients.Where(co => co.role == "doctor" && co.hospitalId == hostpitalId).Select(doc=>doc.username).ToList();
            return u;
        }

        public string getDrAvailableTime(string username, string appointmentDate)
        {
            List<DoctorAvailability> availability = db.doctorAvailability.Where(co => co.username == username).ToList();

            if (availability == null)
                return "";

            List<Appointment> appointments = db.appointment.Where(ap => ap.drName == username && ap.appointmentDate==appointmentDate).ToList();


            DoctorAvailability doctorAvailability= null;
            var jsonString="";
            if (availability != null && availability.Count() > 0)
            {
                doctorAvailability = availability.First();

                int startTime = doctorAvailability.startTime;
                int endTime = doctorAvailability.endTime;
                string drName = doctorAvailability.username;
                bool isBooked = false;
                AvailabilityDAL availabilityDAL = new AvailabilityDAL();
                List<AvailabilityDAL> availabilityDALs = new List<AvailabilityDAL>();
                if (startTime < endTime)
                {
                    for (int i = startTime; i <= endTime; i++)
                    {
                        isBooked = isBookedAppointment(appointments,i);
                        
                        availabilityDAL = new AvailabilityDAL(i, drName, isBooked);
                        availabilityDALs.Add(availabilityDAL);

                    }
                }
                else
                {
                    for (int i = startTime; i < 24; i++)
                    {
                        isBooked = isBookedAppointment(appointments, i);

                        availabilityDAL = new AvailabilityDAL(i, drName, isBooked);
                        availabilityDALs.Add(availabilityDAL);

                    }
                    for (int i = 0; i <= endTime; i++)
                    {
                        isBooked = isBookedAppointment(appointments, i);

                        availabilityDAL = new AvailabilityDAL(i, drName, isBooked);
                        availabilityDALs.Add(availabilityDAL);

                    }

                }

                jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(availabilityDALs);
            }
            return jsonString;
        }

        private bool isBookedAppointment(List<Appointment> appointments, int loop)
        {
            bool result = false;
            if (appointments != null && appointments.Count() > 0)
            {
                for (int j = 0; j < appointments.Count(); j++)
                {
                    if (appointments[j].bookingHour == loop)
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        public string setAppointment(int bookingHour, string drName, string appointmentDate)
        {
            string patientName = Request.Cookies["username"].Value;
            string message = "";


            if (appointmentDate.Length <= 5)
                return "Please select date";

            Appointment appointment = new Appointment(drName, patientName, appointmentDate, bookingHour);

            db.appointment.Add(appointment);
            db.SaveChanges();

            message = "Your appointment has been confirmed at " + bookingHour + " to Dr. " + drName + " on date " + appointmentDate;
            return message;
        }

        public string getMyAppointments()
        {
            var jsonString = "";

            string username = Request.Cookies["username"].Value;
            List<Appointment> appointments = new List<Appointment>();


            appointments = db.appointment.Where(app => app.drName == username).ToList();

            jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(appointments);
            return jsonString;
        }


        public string getPatientAppointments()
        {
            var jsonString = "";

            string username = Request.Cookies["username"].Value;
            List<Appointment> appointments = new List<Appointment>();


            appointments = db.appointment.Where(app => app.patientName == username).ToList();

            jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(appointments);
            return jsonString;
        }


        public string getAllAppointments()
        {
            var jsonString = "";

            List<String> doctors = getAllDoctorsForHospitalAsList();

            string username = Request.Cookies["username"].Value;
            List<Appointment> appointments = new List<Appointment>();

            appointments = db.appointment.Where(app => doctors.Contains(app.drName) ).ToList();

            jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(appointments);
            return jsonString;
        }

        public string cancelAppointment(int id)
        {
            Appointment appointment = db.appointment.SingleOrDefault(ap => ap.id == id);

            if (appointment != null)
            {
                // The items exists. So we remove it and calling 
                // the db.SaveChanges this will be removed from the database.
                db.appointment.Remove(appointment);
                db.SaveChanges();
            }
            return "Cancelled";
        }

    }
}
