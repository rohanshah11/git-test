using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using CMS.Web.Areas.Admin.FilterModel;
using CMS.Web.Helpers;
using CMS.Web.LEPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("admin")]
    [Route("admin/faq")]
    public class FaqController : Controller
    {
        private readonly FaqRepository _faqRepository;
        private readonly FaqService _faqService;
        private readonly IMapper _mapper;
        private readonly PaginatedMetaService _paginatedMetaService;

        public FaqController(FaqRepository faqRepository, FaqService faqService, IMapper mapper, PaginatedMetaService paginatedMetaService)
        {
            _faqRepository = faqRepository;
            _faqService = faqService;
            _mapper = mapper;
            _paginatedMetaService = paginatedMetaService;
        }
        public IActionResult Index()
        {
            try
            {
                var faq = _faqRepository.getQueryable();

                var faqs = faq.ToList();
                return View(faqs);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return Redirect("index");
            }
        }

     

        [Route("new")]
        public IActionResult add()
        {
            return View();
        }

        [HttpPost]
        [Route("new")]
        public IActionResult add(FaqDto faqDto)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    _faqService.save(faqDto);
                    AlertHelper.setMessage(this, "Faq saved successfully.", messageType.success);

                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return View(faqDto);
        }

        [HttpGet]
        [Route("edit/{faq_id}")]
        public IActionResult edit(long faq_id)
        {
            try
            {
                var faq = _faqRepository.getById(faq_id);
                FaqDto faqDto  = _mapper.Map<FaqDto>(faq);

                RouteData.Values.Remove("faq_id");
                return View(faqDto);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return RedirectToAction("index");
            }
        }

        [HttpPost]
        [Route("edit")]
        public IActionResult edit(FaqDto faqDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _faqService.update(faqDto);
                    AlertHelper.setMessage(this, "Faq updated successfully.");
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return View(faqDto);
        }

        [HttpGet]
        [Route("delete/{faq_id}")]
        public IActionResult delete(long faq_id)
        {
            try
            {
                _faqService.delete(faq_id);
                AlertHelper.setMessage(this, "Faq deleted successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("enable/{faq_id}")]
        public IActionResult enable(long faq_id)
        {
            try
            {
                _faqService.enable(faq_id);
                AlertHelper.setMessage(this, "Faq enabled successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("disable/{faq_id}")]
        public IActionResult disable(long faq_id)
        {
            try
            {
                _faqService.disable(faq_id);
                AlertHelper.setMessage(this, "Faq disabled successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
   