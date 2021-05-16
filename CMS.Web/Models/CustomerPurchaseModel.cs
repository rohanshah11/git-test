using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Models
{
    public class CustomerPurchaseModel
    {
        public long customer_id { get; set; }
        public string full_name { get; set; }
        public string description { get; set; }
        public string contact_number { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public decimal total_price { get; set; }
    

        public List<PurchaseItems> purchase_items { get; set; }


    }
    public class PurchaseItems
    {
        public long menu_id { get; set; }
        public long menu_type_id { get; set; }
        public string name { get; set; }
        public long quantity { get; set; }
        public string description { get; set; }
        public string image_name { get; set; }
        public decimal unit_price { get; set; }
    }
}
