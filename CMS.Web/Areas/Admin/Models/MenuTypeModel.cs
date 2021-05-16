using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Core.Models
{
    public class MenuTypeModel
    {
        [Key]
        [Display(Name = "ID")]
        public long menu_type_id { get; set; }

        [Display(Name = "Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a Title.")]
        public string name { get; set; }

        [Display(Name = "Item Category")]
        public long menu_category_id { get; set; }
    }
}
