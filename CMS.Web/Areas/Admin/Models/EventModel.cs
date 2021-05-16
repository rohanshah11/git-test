using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Core.Models
{
    public class EventModel
    {

        public long event_id { get; set; }

        [Display(Name = "Title")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Title is required.")]
        public string title { get; set; }

        [Display(Name = "Description")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a description")]
        public string description { get; set; }

        [Display(Name = "From Date")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a Date")]
        public DateTime event_from_date { get; set; } = DateTime.Now;
        [Display(Name = "To Date")]
        public DateTime event_to_date { get; set; } = DateTime.Now.AddDays(7);

        [Display(Name = "Image")]
        public string image_name { get; set; }

        [Display(Name = "File Name")]
        public string file_name { get; set; }

        [Display(Name = "Event Time")]
        public string time { get; set; }

        [Display(Name = "Venue")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a Venue.")]
        public string venue { get; set; }

        public bool is_closed { get; set; } = false;

        public void close()
        {
            is_closed = true;
        }

        public void unclose()
        {
            is_closed = false;
        }
    }
}
