using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Entity
{
    public class Order
    {
        [Key]
        public long order_id { get; set; }
        [Required]
        public string order_code { get; set; }
        [Required]
        public DateTime order_date { get; set; } = DateTime.Now;
        [Required]
        public decimal total_amount { get; set; }
        public string customer_name { get; set; }
        [Required]
        public string primary_contact { get; set; }
        public string secondary_contact { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public bool is_completed { get; set; } = false;
        public void completed()
        {
            is_completed = true;
        }

        public void remained()
        {
            is_completed = false;
        }
        public virtual List<OrderDetail> order_detail { get; set; }
    }
}
