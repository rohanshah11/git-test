using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Core.Repository.Interface;
using CMS.Web.Areas.Admin.FilterModel;
using CMS.Web.LEPagination;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Controllers
{
    [Route("event")]
    public class EventController : Controller
    {

        private readonly EventRepository _eventRepo;
        private readonly NoticeRepository _noticeRepo;
        private readonly PaginatedMetaService _paginatedMetaService;
        private readonly SetupRepository _setupRepo;
        private readonly CareerRepository _careerRepo;
        private readonly TestimonialRepository _testimonialRepo;
        public EventController(EventRepository eventRepo, NoticeRepository noticeRepo, PaginatedMetaService paginatedMetaService, SetupRepository setupRepo, CareerRepository careerRepo, TestimonialRepository testimonialRepo)
        {
            _eventRepo = eventRepo;
            _noticeRepo = noticeRepo;
            _paginatedMetaService = paginatedMetaService;
            _setupRepo = setupRepo;
            _careerRepo = careerRepo;
            _testimonialRepo = testimonialRepo;


        }
        [Route("")]
        [Route("index")]
        public IActionResult Index(EventFilter filter = null)
        {
            var testimonialValues = _testimonialRepo.getQueryable().ToList();
            ViewBag.testimonial = testimonialValues;
            var events = _eventRepo.getQueryable().Where(a=>a.is_closed==false);
            ViewBag.pagerInfo = _paginatedMetaService.GetMetaData(events.Count(), filter.page, 5);
            events = events.Skip(filter.number_of_rows * (filter.page - 1)).Take(5);
            return View(events.OrderByDescending(a => a.event_to_date).ToList());

        }

        [HttpGet]
        [Route("detail/{slug}")]
        public IActionResult detail(string slug)
        {
            var eventDetail = _eventRepo.getBySlug(slug);
            return View(eventDetail);
        }
    }
}
