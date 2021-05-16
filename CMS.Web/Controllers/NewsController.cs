using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Core.Repository.Interface;
using CMS.Web.Helpers;
using CMS.Web.LEPagination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CMS.Web.Controllers
{
    [Route("news")]
    public class NewsController : Controller
    {
        private readonly NewsRepository _newsRepository;
        private readonly PaginatedMetaService _paginatedMetaService;
        private readonly SetupRepository _setupRepository;

        public NewsController(NewsRepository newsRepository, SetupRepository setupRepository, PaginatedMetaService paginatedMetaService)
        {
            _newsRepository = newsRepository;
            _paginatedMetaService = paginatedMetaService;
            _setupRepository = setupRepository;
        }
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {

                var news = _newsRepository.getQueryable().Where(e => e.is_active == true).ToList();
                return View(news);
           
        }
        [HttpGet]
        [Route("detail/{slug}")]
        public IActionResult detail(string slug)
        {
            var setupValues = _setupRepository.getQueryable().ToList();
            ViewBag.setup = setupValues;
            var newsdetail = _newsRepository.getBySlug(slug);
            return View(newsdetail);
        }

    }
}