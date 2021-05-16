using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Admin.ViewModels
{
    public class OrderDetailIndexViewModel
    {
        public List<OrderDetailModel1> order_details1 { get; set; }
        public decimal TotalAmount { get; set; }
       
    }
    public class OrderDetailModel1
    {

        public long order_detail_id { get; set; }

        public long order_id { get; set; }
       
        public long menu_id { get; set; }
        public long quantity { get; set; }
        public decimal rate { get; set; }
        [ForeignKey("order_id")]
        public virtual Order order { get; set; }
        [ForeignKey("menu_id")]
        public virtual Menu menu { get; set; }
    }
}
