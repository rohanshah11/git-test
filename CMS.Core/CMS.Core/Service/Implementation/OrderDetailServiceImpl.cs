using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Makers.Interface;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace CMS.Core.Service.Implementation
{
    public class OrderDetailServiceImpl : OrderDetailService
    {
        private readonly OrderDetailRepository _orderDetailRepo;
        private readonly OrderDetailMaker _orderDetailMaker;
        private readonly OrderRepository _orderRepo;
        private readonly MenuRepository _menuRepository;
        public OrderDetailServiceImpl(OrderDetailRepository orderDetailRepository, OrderDetailMaker orderDetailMaker, OrderRepository orderRepo, MenuRepository menuRepository)
        {
            _orderDetailMaker = orderDetailMaker;
            _orderDetailRepo = orderDetailRepository;
            _orderRepo = orderRepo;
            _menuRepository = menuRepository;
        }
        public void record(List<OrderDetailDto> order_datas)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    foreach (var orderDataDto in order_datas)
                    {
                        var orderDatas = new OrderDetail();
                        _orderDetailMaker.copy(orderDatas, orderDataDto);
                        orderDatas.order = _orderRepo.getById(orderDataDto.order_id);
                        orderDatas.menu = _menuRepository.getById(orderDataDto.menu_id);

                        _orderDetailRepo.insert(orderDatas);

                    }

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
