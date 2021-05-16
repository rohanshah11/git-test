using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Makers.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Makers.Implementations
{
    public class OrderDetailMakerImpl : OrderDetailMaker
    {
        public void copy(OrderDetail order_detail, OrderDetailDto dto)
        {
            order_detail.menu_id = dto.menu_id;
            order_detail.order_detail_id = dto.order_detail_id;
            order_detail.order_id = dto.order_id;
            order_detail.quantity = dto.quantity;
            order_detail.rate = dto.rate;
        }
    }
}
