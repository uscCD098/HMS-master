using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace StdHMS.Models
{
    public class PatientDBContext : DbContext
    {
        public PatientDBContext()
        { }
        public DbSet<User> patients { get; set; }
        public DbSet<DoctorAvailability> doctorAvailability { get; set; }

        public DbSet<Appointment> appointment { get; set; }
    }
}