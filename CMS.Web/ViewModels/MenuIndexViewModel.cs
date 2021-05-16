using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.ViewModels
{
    public class MenuIndexViewModel
    {
        public List<MenuDetailModel> menu_details { get; set; }
        public string name { get; set; }
    }
    public class MenuDetailModel
    {
        public long menu_id { get; set; }
        public string name { get; set; }
        public long menu_category_id { get; set; }
        public MenuCategory menu_category { get; set; }
        public decimal unit_price { get; set; }
        public decimal fake_price { get; set; }
        public string description { get; set; }
        public string image_name { get; set; }
        public string slug { get; set; }
        public bool is_enabled { get; set; } = true;
    }
}
