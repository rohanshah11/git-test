using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Core.Repository.Interface;
using CMS.Web.LEPagination;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Controllers
{
  
        [Route("doctors")]
        public class DoctorsController : Controller
        {
            private readonly DoctorsRepository _doctorsRepository;
        private readonly TestimonialRepository _testimonialRepo;
        private readonly SetupRepository _setupRepository;

            public DoctorsController(DoctorsRepository doctorsRepository, SetupRepository setupRepository, TestimonialRepository testimonialRepository )
            {
                _doctorsRepository = doctorsRepository;
            _testimonialRepo = testimonialRepository;
                _setupRepository = setupRepository;
            }
            [Route("")]
            [Route("index")]
            public IActionResult Index()
            {
                var doctors = _doctorsRepository.getQueryable().Where(e => e.is_active == true).Take(3).ToList();
                return View(doctors);
            }
        [HttpGet]
        [Route("detail/{slug}")]
        public IActionResult detail(string slug)
        {
            var setupValues = _setupRepository.getQueryable().ToList();
            ViewBag.setup = setupValues;
            var testimonialValues = _testimonialRepo.getQueryable().ToList();
            ViewBag.testimonial = testimonialValues;
            var doctorsDetail = _doctorsRepository.getBySlug(slug);
            return View(doctorsDetail);
        }
    }
    }
