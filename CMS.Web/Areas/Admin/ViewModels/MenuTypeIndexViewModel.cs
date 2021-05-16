using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Core.ViewModels
{
    public class MenuTypeIndexViewModel
    {
        public List<MenuTypeDetails> menu_type_details { get; set; }
    }
    public class MenuTypeDetails
    {
        public long menu_type_id { get; set; }
        public string name { get; set; }
        public long menu_category_id { get; set; }
        public virtual MenuCategory menu_category { get; set; }
    }
}
