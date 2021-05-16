using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Makers.Interface;
using CMS.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Makers.Implementations
{
    public class OrderMakerImpl : OrderMaker
    {
        private SlugGenerator _slugGenerator;

        public OrderMakerImpl(SlugGenerator slugGenerator)
        {
            _slugGenerator = slugGenerator;
        }
        public void copy(Order order, OrderDto dto)
        {
            order.address = dto.address;
            order.customer_name = dto.customer_name;
            order.email = dto.email;
            order.is_completed = dto.is_completed;
            order.order_code = _slugGenerator.codeGenerate(dto.customer_name, dto.primary_contact);
            order.order_date = dto.order_date;
            order.order_id = dto.order_id;
            order.primary_contact = dto.primary_contact;
            order.secondary_contact = dto.secondary_contact;
            order.total_amount = dto.total_amount;
            
        }
    }
}
