using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using CMS.Web.Areas.Core.FilterModel;
using CMS.Web.Areas.Core.ViewModels;
using CMS.Web.Helpers;
using CMS.Web.LEPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Areas.Core.Controllers
{
    [Authorize]
    [Area("admin")]
    [Route("admin/news")]
    public class NewsController : Controller
    {
        private readonly NewsRepository _newsRepository;
        private readonly NewsService _newsService;
        private readonly PaginatedMetaService _paginatedMetaService;
        private IMapper _mapper;
        private FileHelper _fileHelper;
        public NewsController(NewsRepository newsRepository, NewsService newsService, FileHelper fileHelper, IMapper mapper, PaginatedMetaService paginatedMetaService)
        {
            _newsRepository = newsRepository;
            _newsService = newsService;
            _paginatedMetaService = paginatedMetaService;
            _mapper = mapper;
            _fileHelper = fileHelper;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index(NewsFilter newsFilter = null)
        {
            try
            {
                var news = _newsRepository.getQueryable();
                if (!string.IsNullOrWhiteSpace(newsFilter.title))
                {
                    news = news.Where(a => a.title.Contains(newsFilter.title));
                }
                ViewBag.pagerInfo = _paginatedMetaService.GetMetaData(news.Count(), newsFilter.page, newsFilter.number_of_rows);
                news = news.Skip(newsFilter.number_of_rows * (newsFilter.page - 1)).Take(newsFilter.number_of_rows);
                var news1 = news.ToList();
                var filesIndexVM = getViewModelFrom(news1);
                return View(filesIndexVM);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return Redirect("index");
            }
        }

        private object getViewModelFrom(List<News> news1)
        {
            NewsIndexViewModel gvm = new NewsIndexViewModel();
            gvm.news_detail = new List<NewsDetailModel>();
            foreach (var news in news1)
            {
                var galleryDetail = _mapper.Map<NewsDetailModel>(news);
                gvm.news_detail.Add(galleryDetail);
            }

            return gvm;
        }
       
        [Route("new")]
        public IActionResult add()
        {
            NewsDto dto = new NewsDto();
            return View(dto);
        }

        [HttpPost]
        [Route("new")]
        public IActionResult add(NewsDto newsDto, IFormFile file = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        string fileName = newsDto.title;
                        newsDto.image = _fileHelper.saveImageAndGetFileName(file, fileName);

                    }
                    _newsService.save(newsDto);
                    AlertHelper.setMessage(this, "News saved successfully.", messageType.success);

                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return View(newsDto);
        }
        [HttpGet]
        [Route("edit/{news_id}")]
        public IActionResult edit(long news_id)
        {
            try
            {
                var news = _newsRepository.getById(news_id);
                NewsDto dto = _mapper.Map<NewsDto>(news);

                RouteData.Values.Remove("news_id");
                return View(dto);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return RedirectToAction("index");
            }
        }

        [HttpPost]
        [Route("edit")]
        public IActionResult edit(NewsDto newsDto, IFormFile file = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        string fileName = newsDto.title;
                        newsDto.image = _fileHelper.saveImageAndGetFileName(file, fileName);

                    }
                    _newsService.update(newsDto);
                    AlertHelper.setMessage(this, "News updated successfully.");
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return View(newsDto);
        }

        [HttpGet]
        [Route("delete/{news_id}")]
        public IActionResult delete(long news_id)
        {
            try
            {
                _newsService.delete(news_id);
                AlertHelper.setMessage(this, "News deleted successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Route("enable/{news_id}")]
        public IActionResult enable(long news_id)
        {
            try
            {
                _newsService.enable(news_id);
                AlertHelper.setMessage(this, "News enabled successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("disable/{news_id}")]
        public IActionResult disable(long news_id)
        {
            try
            {
                _newsService.disable(news_id);
                AlertHelper.setMessage(this, "News disabled successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }


    }

}