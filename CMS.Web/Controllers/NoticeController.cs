using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Core.Entity;
using CMS.Core.Repository.Interface;
using CMS.Web.Areas.Core.FilterModel;
using CMS.Web.LEPagination;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Controllers
{
    [Route("notice")]
    public class NoticeController : Controller
    {
        private readonly NoticeRepository _noticeRepo;
        private readonly EventRepository _eventRepo;
        private readonly SetupRepository _setupRepo;
        private readonly PaginatedMetaService _paginatedMetaService;
        private readonly CareerRepository _careerRepo;
        public NoticeController(NoticeRepository noticeRepo, EventRepository eventRepo, SetupRepository setupRepo, PaginatedMetaService paginatedMetaService, CareerRepository careerRepo)
        {
            _noticeRepo = noticeRepo;
            _eventRepo = eventRepo;
            _setupRepo = setupRepo;
            _paginatedMetaService = paginatedMetaService;
            _careerRepo = careerRepo;
        }
        [Route("")]
        [Route("index")]
        public IActionResult Index(NoticeFilter filter = null)
        {
            var setupValues = _setupRepo.getQueryable().ToList();
            ViewBag.setup = setupValues;
            var notices = _noticeRepo.getQueryable().Where(a=>a.is_closed==false);
            ViewBag.pagerInfo = _paginatedMetaService.GetMetaData(notices.Count(), filter.page, 5);
            notices = notices.Skip(filter.number_of_rows * (filter.page - 1)).Take(5);

            return View(notices.ToList());
        }

        [HttpGet]
        [Route("detail/{slug}")]
        public IActionResult detail(string slug)
        {
            var noticeDetail = _noticeRepo.getBySlug(slug);
            return View(noticeDetail);
        }
    }
}