using CMS.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Service.Interface
{
    public interface OrderService
    {
        void save(OrderDto order_dto);
        void delete(long order_id);
        void completed(long order_id);
        void remained(long order_id);
    }
}
