using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace StdHMS.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        [StringLength(50)]
        public String username { get; set; }

        [StringLength(50)]
        public String name { get; set; }


        [StringLength(50)]
        public String password { get; set; }


        [StringLength(50)]
        public String phoneNumber { get; set; }

        [StringLength(50)]
        public String role { get; set; }

        [StringLength(50)]
        public String hospitalId { get; set; }
        

        public User()
        {
            
        }

        public User(string name, string username, string password, string phoneNumber, string role, string hospitalId)
        {
            this.name = name;
            this.username = username;
            this.password = password;
            this.phoneNumber = phoneNumber;
            this.role = role;
            this.hospitalId = hospitalId;
        }


        public bool isHospital() {
            return false;
        }

        public bool isPatient()
        {
            return false;
        }

        public bool isDoctor()
        {
            return false;
        }
    }
}