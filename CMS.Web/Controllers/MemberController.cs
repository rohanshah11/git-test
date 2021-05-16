using CMS.Core.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Controllers
{
    [Route("member")]
    public class MemberController : BaseController
    {
        private readonly MembersRepository _membersRepo;
        private readonly SetupRepository _setupRepository;
        private readonly TestimonialRepository _testimonialRepo;

        public MemberController(MembersRepository membersRepo, SetupRepository setupRepository, TestimonialRepository testimonialRepository)
        {
            _membersRepo = membersRepo;
            _setupRepository = setupRepository;
            _testimonialRepo = testimonialRepository;
        }
        [Route("")]
        [Route("index")]
        public IActionResult index()
        {
            var members = _membersRepo.getQueryable().OrderBy(a => a.Designation.position).ToList();
         

            
            return View(members);
        }
        [HttpGet]
        [Route("detail/{slug}")]
        public IActionResult detail(string slug)
        {
            var setupValues = _setupRepository.getQueryable().ToList();
            ViewBag.setup = setupValues;
            var testimonialValues = _testimonialRepo.getQueryable().ToList();
            ViewBag.testimonial = testimonialValues;
            var memberDetail = _membersRepo.getBySlug(slug);
            return View(memberDetail);





        }
    }
}
