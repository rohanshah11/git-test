using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Interface
{
    public interface OrderDetailRepository
    {
        void insert(OrderDetail order);
        void update(OrderDetail order);
        void delete(OrderDetail order);
        List<OrderDetail> getAll();
        OrderDetail getById(long rrder_id);
        IQueryable<OrderDetail> getQueryable();
    }
}
