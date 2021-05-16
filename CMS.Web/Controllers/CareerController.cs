using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Core.Repository.Interface;
using CMS.Web.Areas.Core.FilterModel;
using CMS.Web.LEPagination;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Controllers
{
    [Route("career")]
    public class CareerController : Controller
    {
        private readonly CareerRepository _careerRepo;
        private readonly SetupRepository _setupRepo;
        private readonly PaginatedMetaService _paginatedMetaService;

        public CareerController(CareerRepository careerRepo,SetupRepository setupRepo, PaginatedMetaService paginatedMetaService)
        {
            _careerRepo = careerRepo;
            _setupRepo = setupRepo;
            _paginatedMetaService = paginatedMetaService;
        }

        [Route("")]
        public IActionResult Index(CareerFilter filter = null)
        {
            var setupValues = _setupRepo.getQueryable().ToList();
            ViewBag.setup = setupValues;
            var careers = _careerRepo.getQueryable().Where(a => a.is_closed == false && (a.closing_date == null ? true : (DateTime.Now <= a.closing_date)));

            ViewBag.pagerInfo = _paginatedMetaService.GetMetaData(careers.Count(), filter.page, 2);


            careers = careers.Skip(filter.number_of_rows * (filter.page - 1)).Take(3);
            ViewBag.careers = careers.ToList();
            return View(careers.ToList());
           
        }
        [HttpGet]
        [Route("detail/{career_id}")]
        public IActionResult detail(long career_id)
        {
            var careerDetail = _careerRepo.getById(career_id);
            return View(careerDetail);
        }
    }
}