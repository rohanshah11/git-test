using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Core.Models
{
    public class MenuModel
    {
        [Display(Name = "ID")]
        public long menu_id { get; set; }

        [Display(Name = "Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
        public string name { get; set; }

        [Display(Name = "Description")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Description is required")]
        public string description { get; set; }

        [Display(Name = "Item Category")]
        public long menu_category_id { get; set; }
        [Display(Name = "Image")]
        public string image_name { get; set; }
        public long customer_order_id { get; set; }
        public string slug { get; set; }
        [Display(Name = "Discounted Price")]
        public decimal unit_price { get; set; }
        [Display(Name = "Actual Price")]

        public decimal fake_price { get; set; }
        [Display(Name = "Date")]

        public DateTime menu_date { get; set; } = DateTime.Now;


        public bool is_enabled { get; set; } = true;
     
    }

}
