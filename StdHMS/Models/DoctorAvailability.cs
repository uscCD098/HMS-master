using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace StdHMS.Models
{
    [Table("DoctorAvailability")]
    public class DoctorAvailability
    {
        [Key]
        [StringLength(50)]
        public String username { get; set; }
        public int startTime { get; set; }

        public int endTime { get; set; }

        public DoctorAvailability()
        {

        }

        public DoctorAvailability(string username, int startTime, int endTime)
        {
            this.username = username;
            this.startTime = startTime;
            this.endTime = endTime;
        }
    }
}