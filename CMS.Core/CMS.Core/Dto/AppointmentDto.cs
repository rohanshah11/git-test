using CMS.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Dto
{
    public class AppointmentDto
    {
        [Key]
        public long appointment_id { get; set; }
        [Display(Name ="Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a Name")]

        public string name { get; set; }
      
        
        [Display(Name = "Address")]

        public string address { get; set; }
        [Display(Name = "Contact")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a Contact No")]

        public string contact_no { get; set; }
      
        [Display(Name = "Description")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a Description")]

        public string description { get; set; }
       
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]


        [Display(Name = "Email")]

        public string email { get; set; }
        [Display(Name = "Appointment Date")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a Appointment Date")]

        public DateTime appointment_date { get; set; } = DateTime.Now;
        [Required]
        [Display(Name = "Date")]

        public DateTime entry_date { get; set; } = DateTime.Now;
        [Display(Name = "Country")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select a Country")]

        public string country { get; set; }

        public AppointmentEnum type { get; set; } = AppointmentEnum.pending;
        public void Approved()
        {
            type = AppointmentEnum.approved;
        }
        public void Cancelled()
        {
            type = AppointmentEnum.cancelled;
        }
    }
}
