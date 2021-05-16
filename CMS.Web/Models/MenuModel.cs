using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Models
{
    public class MenuModel
    {
        [Display(Name = "ID")]
        public long menu_id { get; set; }

        [Display(Name = "Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
        public string name { get; set; }
        public long menu_comment_id { get; set; }

        [Display(Name = "Description")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Description is required")]
        public string description { get; set; }
        public string slug { get; set; }

        [Display(Name = "Menu Type")]
        public long menu_type_id { get; set; }
        [Display(Name = "Image")]
        public string image_name { get; set; }
        [Display(Name = "Unit Price")]
        public decimal unit_price { get; set; }
        public bool is_enabled { get; set; } = true;
       
    }

  
}
