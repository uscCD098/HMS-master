using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StdHMS.Models
{
    public class AvailabilityDAL
    {
        public int hour { get; set; }
        public string drName { get; set; }
        public bool isBooked { get; set; }

        public AvailabilityDAL() { }

        public AvailabilityDAL(int hour, string drName, bool isBooked) {
            this.hour = hour;
            this.drName = drName;
            this.isBooked = isBooked;
        }

    }
}