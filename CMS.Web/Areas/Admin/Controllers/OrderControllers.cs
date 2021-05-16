using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CMS.Core.Entity;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using CMS.Web.Areas.Admin.FilterModel;
using CMS.Web.Areas.Admin.ViewModels;
using CMS.Web.Areas.Core.ViewModels;
using CMS.Web.Helpers;
using CMS.Web.LEPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("admin")]
    [Route("admin/order")]
    public class OrderControllers : Controller
    {
        private readonly OrderRepository _orderRepository;
        private readonly OrderDetailService _orderDetailService;
        private readonly OrderService _orderService;
        private readonly OrderDetailRepository _orderDetailRepository;
        private readonly PaginatedMetaService _paginatedMetaService;
        private readonly IMapper _mapper;
        private readonly SetupRepository _setupRepo;

        public OrderControllers(SetupRepository setupRepository, OrderDetailService orderDetailService, IMapper mapper, PaginatedMetaService paginatedMetaService, OrderService orderService, OrderRepository orderRepository,OrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
            _orderRepository = orderRepository;
            _orderService = orderService;
            _paginatedMetaService = paginatedMetaService;
            _mapper = mapper;
            _orderDetailService = orderDetailService;
            _setupRepo = setupRepository;
        }
        [Route("")]
        [Route("index")]
        public IActionResult Index(OrderFilter filter = null)
        {
            try
            {
                var menu_type = _orderRepository.getQueryable();
                if (!string.IsNullOrWhiteSpace(filter.name))
                {
                    menu_type = menu_type.Where(a => a.customer_name.Contains(filter.name));
                }
                ViewBag.pagerInfo = _paginatedMetaService.GetMetaData(menu_type.Count(), filter.page, filter.number_of_rows);
                menu_type = menu_type.Skip(filter.number_of_rows * (filter.page - 1)).Take(filter.number_of_rows);
                var orderDetails = menu_type.ToList();

                var orderIndexVM = getViewModelFromOrder(orderDetails);
                return View(orderIndexVM);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return Redirect("/admin");
            }
        }
        [HttpGet]
        [Route("orderDelete/{order_id}")]
        public IActionResult orderDelete(long order_id)
        {
            try
            {
                _orderService.delete(order_id);
                AlertHelper.setMessage(this, "Order deleted successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [Route("ViewOrder")]
        public IActionResult ViewOrder()
        {
            return View();
        }
        [HttpGet]
        [Route("ViewOrder/{order_id}")]
        public ActionResult ViewOrder(long order_id)
        {
            var order = _orderDetailRepository.getQueryable().Where(a => a.order_id == order_id).ToList();
            OrderDetailIndexViewModel vm = new OrderDetailIndexViewModel();
            vm.order_details1 = new List<OrderDetailModel1>();
            foreach (var orderDetail in order)
            {
                var detail = _mapper.Map<OrderDetailModel1>(orderDetail);
                vm.order_details1.Add(detail);
            }
            vm.TotalAmount =vm.order_details1.Sum(x => x.order?.total_amount).GetValueOrDefault();
            return View(vm);

        }
        [HttpGet]
        [Route("report")]
        public IActionResult report(OrderViewIndexViewModel vm)
        {
            vm = setReportViewModel(vm);
            return View(vm);
        }
        private OrderViewIndexViewModel setReportViewModel(OrderViewIndexViewModel vm)
        {
            var startDate = vm.start_date.Date;
            var endDate = vm.end_date.Date;
            var details = _orderRepository.getQueryable().Where(a => a.order_date.Date >= startDate && a.order_date.Date <= endDate).ToList();
            if (!string.IsNullOrWhiteSpace(vm.title))
            {
                details = _orderRepository.getQueryable().Where(a => a.order_date.Date >= startDate && a.order_date.Date <= endDate && vm.title == a.customer_name).ToList();
            }
            var list = getViewModelFromOrder(details);
            return (OrderViewIndexViewModel)list;
        }

        [HttpGet]
        [Route("order-report-print")]
        public IActionResult print(OrderViewIndexViewModel vm1)
        {
            vm1 = getViewPrintFromMenu(vm1);
            return View(vm1);

        }

        private OrderViewIndexViewModel getViewPrintFromMenu(OrderViewIndexViewModel vm1)
        {
            var startDate = vm1.start_date.Date;
            var endDate = vm1.end_date.Date;
            var details = _orderRepository.getQueryable().Where(a => a.order_date.Date >= startDate && a.order_date.Date <= endDate).ToList();
            if (!string.IsNullOrWhiteSpace(vm1.title))
            {
                details = _orderRepository.getQueryable().Where(a => a.order_date.Date >= startDate && a.order_date.Date <= endDate && vm1.title == a.customer_name).ToList();
            }
            details = _orderRepository.getQueryable().ToList();
            var setupValues = _setupRepo.getQueryable().ToList();
            ViewBag.setup = setupValues;
            var list = getViewModelFromOrder(details);
            return (OrderViewIndexViewModel)list;
        }




        [HttpGet]
        [Route("enable/{order_id}")]
        public IActionResult enable(long order_id)
        {
            try
            {
                _orderService.completed(order_id);
                AlertHelper.setMessage(this, "Order enabled successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("disable/{order_id}")]
        public IActionResult disable(long order_id)
        {
            try
            {
                _orderService.remained(order_id);
                AlertHelper.setMessage(this, "Order disabled successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        private object getViewModelFromOrder(List<Order> menuDetails)
        {
            OrderViewIndexViewModel vm = new OrderViewIndexViewModel();
            vm.order_details = new List<OrderDetailModel>();
            foreach (var order in menuDetails)
            {
                var orderDetail = _mapper.Map<OrderDetailModel>(order);
                vm.order_details.Add(orderDetail);
            }

            return vm;
        }
    }
}
