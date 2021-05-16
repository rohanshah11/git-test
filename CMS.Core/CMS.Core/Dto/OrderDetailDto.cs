using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CMS.Core.Dto
{
    public class OrderDetailDto
    {
        [Key]
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
