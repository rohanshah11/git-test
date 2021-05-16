using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CMS.Web.Models;
using CMS.Core.Repository.Interface;
using AutoMapper;
using CMS.Web.ViewModels;
using CMS.Core.Dto;
using CMS.Core.Service.Interface;
using CMS.Core.Exceptions;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Localization;
using CMS.Web.Helpers;
using CMS.Core.Makers.Interface;
using CMS.Core.Entity;

namespace CMS.Web.Controllers
{

    public class HomeController : BaseController
    {
        private readonly BlogRepository _blogRepo;
        private readonly EventRepository _eventRepo;
        private readonly GalleryImageRepository _galleryRepo;
        private readonly NoticeRepository _noticeRepo;
        private readonly PageRepository _pageRepo;
        private readonly CoursesRepository _productRepo;
        private readonly SetupRepository _setupRepo;
        private readonly TestimonialRepository _testimonialRepo;
        private readonly EmailSenderService _emailSenderService;
        private readonly NewsRepository _newsRepository;
        private readonly DoctorsRepository _doctorsRepository;
        private readonly ServicesRepository _servicesRepository;
        private readonly IMapper _mapper;
        private readonly AppointmentService _appointmentService;
        private readonly VideoRepository _videoRepository;
        private readonly AppointmentRepository _appointmentRepository;
        private readonly AppointmentMaker _appointmentMaker;
        private readonly MenuCategoryRepository _menuCategoryRepo;
        private readonly MenuRepository _menuRepo;
        private readonly MembersRepository _membersRepository;
        private readonly PartnersRepository _partnersRepository;



        public HomeController(PartnersRepository partnersRepository, MembersRepository membersRepository, GalleryImageRepository galleryRepo,MenuRepository menuRepo, MenuCategoryRepository menuCategoryRepo, NoticeRepository noticeRepo, PageRepository pageRepo, CoursesRepository productRepo, IMapper mapper, SetupRepository setupRepo, EmailSenderService emailSenderService, TestimonialRepository testimonialRepo, BlogRepository blogRepo, EventRepository eventRepo, VideoRepository videoRepository, NewsRepository newsRepository, DoctorsRepository doctorsRepository, ServicesRepository servicesRepository, AppointmentService appointmentService, AppointmentRepository appointmentRepository, AppointmentMaker appointmentMaker)
        {
            _galleryRepo = galleryRepo;
            _newsRepository = newsRepository;
            _noticeRepo = noticeRepo;
            _pageRepo = pageRepo;
            _productRepo = productRepo;
            _mapper = mapper;
            _setupRepo = setupRepo;
            _testimonialRepo = testimonialRepo;
            _emailSenderService = emailSenderService;
            _blogRepo = blogRepo;
            _eventRepo = eventRepo;
            _videoRepository = videoRepository;
            _doctorsRepository = doctorsRepository;
            _servicesRepository = servicesRepository;
            _appointmentService = appointmentService;
            _appointmentRepository = appointmentRepository;
            _appointmentMaker = appointmentMaker;
            _menuRepo = menuRepo;
            _menuCategoryRepo = menuCategoryRepo;
            _membersRepository = membersRepository;
            _partnersRepository = partnersRepository;
        }

        public IActionResult Index()
        {
            var homeImage = _galleryRepo.getQueryable().Where(g => g.is_slider_image == true && g.is_enabled == true).ToList();
            ViewBag.sliderImages = homeImage;

            var setupValues = _setupRepo.getQueryable().ToList();
            ViewBag.setup = setupValues;
            TempData["email-message"] = TempData["email-message"];

            var testimonialValues = _testimonialRepo.getQueryable().Where(a => a.is_visible == true).ToList();
            ViewBag.testimonial = testimonialValues;

            var newsValue = _newsRepository.getQueryable().Where(a => a.is_active == true).OrderByDescending(a=>a.news_id).ToList().Take(4);
            ViewBag.news = newsValue;

            var serviceValue = _servicesRepository.getQueryable().Where(a => a.is_active == true).ToList();
            ViewBag.services = serviceValue;

            var partners = _partnersRepository.getQueryable().Where(a => a.is_active == true).ToList();
            ViewBag.partners = partners;


            var menus = _menuRepo.getQueryable().Where(a => a.is_enabled == true);
            ViewBag.menu = menus.ToList();
            var menuCategory = _menuCategoryRepo.getQueryable().ToList();
            ViewBag.menuCategories = menuCategory;

            var doctorValue = _doctorsRepository.getQueryable().Where(a => a.is_active == true).ToList();
            ViewBag.doctors = doctorValue;

            var memberValue = _membersRepository.getQueryable().ToList();
            ViewBag.members = memberValue;

            var blogValue = _blogRepo.getQueryable().ToList();
            ViewBag.blog = blogValue;

            var galleryValue = _galleryRepo.getQueryable().Where(n => n.is_enabled == true).ToList();
            ViewBag.gallery = galleryValue;


            var eventValue = _eventRepo.getQueryable().Where(a => a.event_from_date <=a.event_to_date).ToList();
            ViewBag.events = eventValue;
            ViewBag.events12 = eventValue.Take(3);


            var notice = _noticeRepo.getQueryable().Where(n => n.notice_expiry_date.Date >= TimeZoneInfo.ConvertTime(DateTime.Now,
                 TimeZoneInfo.FindSystemTimeZoneById("Nepal Standard Time")).Date && n.is_closed == false).ToList();

            notice = notice.Where(a => a.notice_date <= a.notice_expiry_date).ToList();

            ViewBag.notices = notice;
            ViewBag.noticesForSide = notice.Take(3);

            var page = _pageRepo.getQueryable().Where(n => n.is_enabled == true && n.is_home_page == true).SingleOrDefault();
            ViewBag.homePage = page;

            var videoValue = _videoRepository.getQueryable().Where(n => n.is_enabled == true && n.is_home_video == true).ToList();
            ViewBag.homeVideo = videoValue;
            return View();
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [Route("new")]
        [IgnoreAntiforgeryToken]
        public JsonResult addAppointment([FromBody]AppointmentDto appontmentDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _appointmentService.save(appontmentDto);
                    return Json(1);
                }
            }
            catch (Exception ex)
            {
                return Json(new { error = true, responseText = ex.Message });
            }
            return Json(1);
        }
        [HttpPost]
        [Route("send-mailcontact")]
        public IActionResult sendEmail(ReceivedEmailDto dto)
        {
            var setupValue = _setupRepo.getQueryable().ToList();

            dto.receiver_email = setupValue.Where(a => a.key == Web.Models.SetupKeys.getEmailKey).SingleOrDefault()?.value;
            var email_password = setupValue.Where(a => a.key == Web.Models.SetupKeys.getEmailPasswordKey).SingleOrDefault()?.value;
            var email_host = setupValue.Where(a => a.key == Web.Models.SetupKeys.getEmailHostKey).SingleOrDefault()?.value;
            var email_port = setupValue.Where(a => a.key == Web.Models.SetupKeys.getEmailPortKey).SingleOrDefault()?.value;
           
                dto.message = $"Hello Mr/Mrs {dto.first_name}{dto.last_name}, Your appointment has been recived";
                dto.message = $"Sender Email {dto.sender_email}";
                dto.message = $"Receiver  Email {dto.receiver_email}";
                _emailSenderService.send(dto, email_password, email_host, email_port);
            
            if (ModelState.IsValid)
            {
                try
                {
                    _emailSenderService.send(dto, email_password, email_host, email_port);
                    TempData["email-message"] = "Message sent";

                }
                catch (EmailSendFailureException ex)
                {
                    TempData["email-message"] = "Failed to send email";
                }
                catch (Exception ex)
                {
                    TempData["email-message"] = ex.Message;

                }
            }
            return RedirectToAction("index");
        }
    }
}
