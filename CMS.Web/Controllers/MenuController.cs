using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Core.Entity;
using CMS.Core.Repository.Interface;
using CMS.Web.Areas.Admin.FilterModel;
using CMS.Web.ViewModels;
using CMS.Web.LEPagination;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CMS.Web.Models;
using CMS.Web.Helpers;
using CMS.Core.Dto;
using CMS.Core.Service.Interface;

namespace CMS.Web.Controllers
{
    public class MenuController : Controller
    {
        private readonly MenuRepository _menuRepo;
        private readonly MenuCategoryRepository _menuCategoryRepository;
        private readonly PaginatedMetaService _paginatedMetaService;
        private readonly SetupRepository _setupRepo;
        private readonly OrderService _orderService;
        private readonly IMapper _mapper;

        public MenuController(MenuRepository menuRepo,OrderService orderService, MenuCategoryRepository menuCategoryRepository, PaginatedMetaService paginatedMetaService, SetupRepository setupRepo, IMapper mapper)
        {
            _menuRepo = menuRepo;
            _paginatedMetaService = paginatedMetaService;
            _setupRepo = setupRepo;
            _menuCategoryRepository = menuCategoryRepository;
            _mapper = mapper;
            _orderService = orderService;


        }
        public IActionResult Index(MenuIndexViewModel filter)
        {
            var setupValues = _setupRepo.getQueryable().ToList();
            ViewBag.setup = setupValues;
            var menus = _menuRepo.getQueryable().Where(a => a.is_enabled == true);
            var menuCategory = _menuCategoryRepository.getQueryable().ToList();
            ViewBag.menuCategories = menuCategory;
            if (!string.IsNullOrEmpty(filter.name))
            {
                menus = menus.Where(s => s.name.Contains(filter.name));
            }
            var menusDetails = menus.ToList();
            MenuIndexViewModel menuIndexVM = getMenuIndexVM(menusDetails);
            return View(menuIndexVM);
        }
     
        [HttpGet]
        [Route("detail/{slug}")]
        public IActionResult detail(MenuModel model)
        {

            var menuDetails = _menuRepo.getBySlug(model.slug);

            //Menu
            if (menuDetails == null)
            {
                return View(new MenuModel());
            }
            var mappingDetails = _mapper.Map<MenuModel>(menuDetails);

            return View(mappingDetails);
        }
        [HttpPost]
        [Route("order")]
        [IgnoreAntiforgeryToken]
        public IActionResult order( OrderModel model)
        {
            try
            {
                OrderDto orderDto = setOrderDtoFromModel(model);
             _orderService.save(orderDto);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false,msg=ex.Message });
            }
        }
        private OrderDto setOrderDtoFromModel(OrderModel model)
        {
            OrderDto orderdto = new OrderDto();

            foreach (var order_details in model.order_details)
            {
                OrderDetailDto order_detail_dto = new OrderDetailDto();
                order_detail_dto.menu_id = order_details.menu_id;
                order_detail_dto.quantity = order_details.quantity;
                order_detail_dto.rate = order_details.rate;
                orderdto.addOrderDatas(order_detail_dto);
            }
            orderdto.customer_name = model.customer_name;
            orderdto.address = model.address;
            orderdto.email = model.email;
            orderdto.order_date = DateTime.Now;
            orderdto.primary_contact = model.primary_contact;
            orderdto.secondary_contact = model.secondary_contact;
            orderdto.total_amount = model.total_amount;
          

       
            return orderdto;
        }


        private MenuIndexViewModel getMenuIndexVM(List<Menu> menuDetails)
        {
            MenuIndexViewModel vm = new MenuIndexViewModel();
            vm.menu_details = new List<MenuDetailModel>();

            foreach (var menu in menuDetails)
            {
                var menus = _mapper.Map<MenuDetailModel>(menu);
                vm.menu_details.Add(menus);
            }

            return vm;
        }
    }
}
