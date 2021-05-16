using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Core.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Controllers
{
    [Route("faq")]
    public class FaqController : Controller
    {
        private readonly FaqRepository _faqRepository ;
        private readonly SetupRepository _setupRepository;

        public FaqController(FaqRepository faqRepository, SetupRepository setupRepository)
        {
            _faqRepository = faqRepository;
            _setupRepository = setupRepository;
        }
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            var faq = _faqRepository.getQueryable().Where(e => e.is_active == true).ToList();
            return View(faq);
        }
    }
}