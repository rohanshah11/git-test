using CMS.Core.Entity;
using CMS.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Models
{
    public class OrderModel
    {
        public decimal total_amount { get; set; }
        public string customer_name { get; set; }
        public string primary_contact { get; set; }
        public string secondary_contact { get; set; }
        public string email { get; set; }
        public string address { get; set; }
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
