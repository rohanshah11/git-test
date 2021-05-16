using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Core.ViewModels
{
    public class MenuCategoryIndexViewModel
    {
        public List<MenuCategoriesDetail> menu_categories { get; set; }
    }

    public class MenuCategoriesDetail
    {
        public long menu_category_id { get; set; }
        public string name { get; set; }
        //public string description { get; set; }
        //public long parent_id { get; set; }
        public bool is_enabled { get; set; }
        //public string image_name { get; set; }
        //public string parent_category_name { get; set; }

    }
}
