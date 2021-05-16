using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Dto
{
  public class DoctorsDto
    {

        [Key]
        public long doctor_id { get; set; }
        [DisplayName("Name")]
        public string name { get; set; }
        [DisplayName("Speciality")]

        public string speciality { get; set; }
        [DisplayName("Address")]

        public string address { get; set; }
      
        public string slug { get; set; }
        [DisplayName("Education")]

        public string education { get; set; }
        [DisplayName("Experience")]

        public string experience { get; set; }
        [DisplayName("Phone Number")]


        public string contact_number { get; set; }
        [DisplayName("Image")]


        public string image { get; set; }
        [DisplayName("Enable Contact")]

        public bool is_contact_enabled { get; set; } = false;
        [DisplayName("Email")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        [MaxLength(50)]
        public string email { get; set; }

        public bool is_active { get; set; } = true;
        [DisplayName("Website")]


        public string website { get; set; }
        [DisplayName("Facebook")]

        public string facebook { get; set; }
        [DisplayName("Twitter")]

        public string twitter { get; set; }

    }
}

