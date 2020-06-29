using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StdHMS.Models
{
    [Table("Appointment")]
    public class Appointment
    {
        [Key]
        public int id { get; set; }

        [StringLength(50)]
        public String drName { get; set; }


        [StringLength(50)]
        public String patientName { get; set; }

        [StringLength(50)]
        public string appointmentDate { get; set; }

        public int bookingHour { get; set; }

        public Appointment() { }

        public Appointment(string drName, string patientName, string appointmentDate, int bookingHour) {
            this.appointmentDate = appointmentDate;
            this.drName = drName;
            this.patientName = patientName;
            this.bookingHour = bookingHour;
        }
    }
}