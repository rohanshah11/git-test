using CMS.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Service.Interface
{
    public interface OrderDetailService
    {
        void record(List<OrderDetailDto> order_datas);
    }
}
