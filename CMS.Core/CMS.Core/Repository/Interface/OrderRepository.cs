using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Interface
{
    public interface OrderRepository
    {
        void insert(Order order);
        void update(Order order);
        void delete(Order order);
        List<Order> getAll();
        Order getById(long rrder_id);
        IQueryable<Order> getQueryable();
    }
}
