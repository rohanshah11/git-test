using CMS.Core.Entity;
using System;
using System.Collections.Generic;

namespace CMS.Web.Areas.Core.ViewModels
{
    public class MenuIndexViewModel
    {
        public List<MenuDetails> menu_details { get; set; }
        public DateTime start_date { get; set; } = DateTime.Now.AddMonths(-1);
        public DateTime end_date { get; set; } = DateTime.Now.AddMonths(1);
        public string title { get; set; }
        //public decimal TotalAmount { get; set; }


    }
    public class MenuDetails
    {
        public long menu_id { get; set; }
        public string name { get; set; }
        public long menu_category_id { get; set; }
        public MenuCategory menu_category { get; set; }
        public decimal unit_price { get; set; }
        public decimal fake_price { get; set; }
        public DateTime menu_date { get; set; } = DateTime.Now;

        public string description { get; set; }
        public string image_name { get; set; }
        public bool is_enabled { get; set; } = true;
    }
   

}