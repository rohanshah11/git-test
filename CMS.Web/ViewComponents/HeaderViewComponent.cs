using CMS.Core.Dto;
using CMS.Core.Exceptions;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CMS.Web.ViewComponents
{
    [ViewComponent(Name = "HeaderView")]
    public class HeaderViewComponent : ViewComponent
    {
        public GalleryRepository _galleryRepo { get; set; }
        public NoticeRepository _noticeRepo { get; set; }
        public EventRepository _eventRepo { get; set; }
        public SetupRepository _setupRepo { get; set; }
        public PageCategoryRepository _pageCategoryRepo { get; set; }
        public AppointmentRepository _appointmentRepo { get; set; }
        public ServicesRepository _serviceRepository { get; set; }
        private EmailSenderService _emailSenderService { get; set; }
        public HeaderViewComponent(EmailSenderService emailSenderService, GalleryRepository galleryRepo, AppointmentRepository appoitmentRepo, NoticeRepository noticeRepo, SetupRepository setupRepo, PageCategoryRepository pageCategoryRepo, EventRepository eventRepo, ServicesRepository serviceRepository)
        {
            _galleryRepo = galleryRepo;
            _noticeRepo = noticeRepo;
            _setupRepo = setupRepo;
            _pageCategoryRepo = pageCategoryRepo;
            _eventRepo = eventRepo;
            _appointmentRepo = appoitmentRepo;
            _serviceRepository = serviceRepository;
            _emailSenderService = emailSenderService;

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {


            var setupValues = _setupRepo.getQueryable().ToList();
            ViewBag.setup = setupValues;

            var pageCategory = _pageCategoryRepo.getQueryable().Where(a => a.is_enabled == true).ToList();
            ViewBag.pageCategories = pageCategory;

            var events = _eventRepo.getQueryable().Where(a => a.event_to_date.Date > DateTime.Now.Date).ToList().Count;
            ViewBag.eventsList = events;

            var service = _serviceRepository.getQueryable().Where(a => a.is_active == true).ToList();
            ViewBag.services = service;

            var notices = _noticeRepo.getQueryable().Where(a => a.notice_expiry_date.Date > DateTime.Now.Date).ToList().Count;
            ViewBag.noticesList = notices;
            var notice = _noticeRepo.getQueryable().Where(n => n.notice_expiry_date.Date >= TimeZoneInfo.ConvertTime(DateTime.Now,
                TimeZoneInfo.FindSystemTimeZoneById("Nepal Standard Time")).Date && n.is_closed == false).ToList();

            notice = notice.Where(a => a.notice_date <= a.notice_expiry_date).ToList();

            ViewBag.notices = notice;
            var eventValue = _eventRepo.getQueryable().Where(a => a.event_from_date <= a.event_to_date).ToList();
            ViewBag.events = eventValue;

            var appointment = new AppointmentDto();
            TempData["email-message"] = TempData["email-message"];
            return View(appointment);

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
       
        private IActionResult RedirectToAction(string v)
        {
            throw new NotImplementedException();
        }
    }
}

