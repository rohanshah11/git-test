using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Exceptions;
using CMS.Core.Makers.Interface;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace CMS.Core.Service.Implementation
{
    public class OrderServiceImpl : OrderService
    {
        private readonly OrderMaker _orderMaker;
        private readonly OrderRepository _orderRepository;
        private readonly OrderDetailService _orderDetailService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public OrderServiceImpl(OrderMaker orderMaker, OrderRepository orderRepository, IHostingEnvironment hostingEnvironment , OrderDetailService orderDetailService)
        {
            _orderMaker = orderMaker;
            _hostingEnvironment = hostingEnvironment;
            _orderRepository = orderRepository;
            _orderDetailService = orderDetailService;
        }

        public void completed(long order_id)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    var orders = _orderRepository.getById(order_id);
                    if (orders == null)
                    {
                        throw new ItemNotFoundException($"Order with Id {order_id} doesnot exist.");
                    }

                    orders.completed();
                    _orderRepository.update(orders);
                    tx.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void delete(long order_id)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    var orders = _orderRepository.getById(order_id);
                    if (orders == null)
                    {
                        throw new ItemNotFoundException($"Blog Category With Id {order_id} doesnot Exist.");
                    }

                    _orderRepository.delete(orders);
                    tx.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void remained(long order_id)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    var orders = _orderRepository.getById(order_id);
                    if (orders == null)
                    {
                        throw new ItemNotFoundException($"Order with Id {order_id} doesnot exist.");
                    }

                    orders.remained();
                    _orderRepository.update(orders);
                    tx.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void save(OrderDto order_dto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                   
                    var order = new Order();

                    _orderMaker.copy(order, order_dto);
                    _orderRepository.insert(order);
                    order_dto.order_id = order.order_id;
                    saveOrderDetails(order_dto);

                    _orderRepository.update(order);
                    tx.Complete();
                   
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void saveOrderDetails(OrderDto order_dto)
        {
            order_dto.getOrderDatas().ForEach(a => a.order_id = order_dto.order_id);
            _orderDetailService.record(order_dto.getOrderDatas());
        }
    
    }
}
