using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Core.ViewModels
{
    public class OrderViewIndexViewModel
    {
        public List<OrderDetailModel> order_details { get; set; }
        public DateTime start_date { get; set; } = DateTime.Now.AddMonths(-1);
        public DateTime end_date { get; set; } = DateTime.Now.AddMonths(1);
        public string title { get; set; }
    }
    public class OrderDetailModel
    {
        public long order_id { get; set; }
        public decimal total_amount { get; set; }
        public DateTime order_date { get; set; } = DateTime.Now;
        public string customer_name { get; set; }
        public string primary_contact { get; set; }
        public string secondary_contact { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public bool is_completed { get; set; } = false;
     
        public List<OrderDetails> order_details { get; set; }
    }
    public class OrderDetails
    {
        public long menu_id { get; set; }
        public long quantity { get; set; }
        public virtual Menu menu { get; set; }
        public decimal rate { get; set; }
    }
}

