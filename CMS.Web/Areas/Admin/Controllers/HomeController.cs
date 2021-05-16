using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Core.Entity;
using CMS.Core.Repository.Interface;
using CMS.Web.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("admin")]
    [Route("admin")]
    public class HomeController : Controller
    {
        private readonly NoticeRepository _noticeRepo;
        private readonly CareerRepository _careerRepo;
        private readonly EventRepository _productRepo;
        private readonly PageRepository _pageRepo;
        private readonly SetupRepository _setupRepo;
        private readonly AppointmentRepository _appointmentRepository;
        private readonly OrderRepository _orderRepository;
        private readonly MenuRepository _menuRepository;

        public HomeController(MenuRepository menuRepository, OrderRepository orderRepository, NoticeRepository noticeRepo, AppointmentRepository appointmentRepository, EventRepository productRepo, CareerRepository careerRepo, PageRepository pageRepo, SetupRepository setupRepo)
        {
            _noticeRepo = noticeRepo;
            _careerRepo = careerRepo;
            _productRepo = productRepo;
            _pageRepo = pageRepo;
            _setupRepo = setupRepo;
            _appointmentRepository = appointmentRepository;
            _orderRepository = orderRepository;
            _menuRepository = menuRepository;
        }

        [Route("")]
        public IActionResult Index()
        {
            HomeIndexViewModel homeIndexVM = new HomeIndexViewModel();
            homeIndexVM.active_careers_count = _careerRepo.getQueryable().Where(a => a.is_closed == false && (a.closing_date == null ? true : (DateTime.Now <= a.closing_date))).Count();


            homeIndexVM.active_products_count = _productRepo.getQueryable().Where(a => a.is_closed == false && a.event_to_date.Date > DateTime.Now.Date).Count();

            homeIndexVM.active_notices_count = _noticeRepo.getQueryable().Where(a => a.notice_expiry_date.Date >= DateTime.Now.Date).Count();
            homeIndexVM.active_order_count = _orderRepository.getQueryable().Where(a => a.is_completed == false).Count();
            homeIndexVM.active_order_count1 = _orderRepository.getQueryable().Where(a => a.is_completed == true).Count();
            homeIndexVM.active_menu_count = _menuRepository.getQueryable().Where(a => a.is_enabled == true).Count();

            homeIndexVM.pages_count = _pageRepo.getQueryable().Count();
            homeIndexVM.active_appointment_count = _appointmentRepository.getQueryable().Where(a => a.type == CMS.Core.Enums.AppointmentEnum.pending).Count();

            var setup = _setupRepo.getQueryable().Where(a => a.key == CMS.Web.Models.SetupKeys.getOrganisationNameKey).SingleOrDefault();
            var address = _setupRepo.getQueryable().Where(a => a.key == CMS.Web.Models.SetupKeys.getAddressKey).SingleOrDefault();

            homeIndexVM.company_name = setup?.value;
            homeIndexVM.address = address?.value;

            return View(homeIndexVM);
        }
      
      
    }
}