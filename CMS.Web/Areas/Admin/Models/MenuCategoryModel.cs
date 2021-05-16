using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Core.Models
{
    public class MenuCategoryModel
    {
        [Display(Name = "ID")]
        public long menu_category_id { get; set; }

        [Display(Name = "Category Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Category Name is required")]
        public string name { get; set; }

        //[Display(Name = "Description")]
        //public string description { get; set; }

        //[Display(Name = "Category")]
        //[Required(AllowEmptyStrings = true)]
        //public long parent_id { get; set; }
        //[Display(Name = "Image")]
        //public string image_name { get; set; }

        public bool is_enabled { get; set; } = true;
    }
}
