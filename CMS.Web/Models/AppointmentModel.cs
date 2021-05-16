using CMS.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Models
{
    public class AppointmentModel
    {

        [Key]
        public long appointment_id { get; set; }
      
        public string name { get; set; }


        public string address { get; set; }
   

        public string contact_no { get; set; }

        public string description { get; set; }

      
        public string email { get; set; }
      
        public DateTime appointment_date { get; set; }
     
        public DateTime entry_date { get; set; } = DateTime.Now;
       

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
