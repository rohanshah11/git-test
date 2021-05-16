using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Makers.Interface;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using CMS.Web.Areas.Core.FilterModel;
using CMS.Web.Areas.Core.Models;
using CMS.Web.Areas.Core.ViewModels;
using CMS.Web.Helpers;
using CMS.Web.LEPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CMS.Web.Areas.Core.Controllers
{
    [Authorize]
    [Area("admin")]
    [Route("admin/menu-category")]
    public class MenuCategoryController : Controller
    {
        private MenuCategoryMaker _menuCategoryMaker;
        private MenuCategoryService _menuCategoryService;
        private readonly MenuCategoryRepository _menuCategoryRepo;
        private PaginatedMetaService _paginatedMetaService;
        private IMapper _mapper;
        private readonly FileHelper _fileHelper;

        public MenuCategoryController(MenuCategoryMaker menuCategoryMaker, FileHelper fileHelper, MenuCategoryService menuCategoryService, IMapper mapper, MenuCategoryRepository menuCategoryRepo, PaginatedMetaService paginatedMetaService)
        {
            _menuCategoryMaker = menuCategoryMaker;
            _menuCategoryService = menuCategoryService;
            _mapper = mapper;
            _menuCategoryRepo = menuCategoryRepo;
            _fileHelper = fileHelper;
            _paginatedMetaService = paginatedMetaService;
        }

        public IActionResult Index(MenuCategoryFilter filter)
        {
            var menuCategory = _menuCategoryRepo.getQueryable();
            if (!string.IsNullOrWhiteSpace(filter.name))
            {
                menuCategory = menuCategory.Where(a => a.name.Contains(filter.name));
            }
            ViewBag.pagerInfo = _paginatedMetaService.GetMetaData(menuCategory.Count(), filter.page, filter.number_of_rows);


            menuCategory = menuCategory.Skip(filter.number_of_rows * (filter.page - 1)).Take(filter.number_of_rows);
            var categories = menuCategory.OrderByDescending(a => a.menu_category_id).ToList();
            //var topCategory = _menuCategoryRepo.getQueryable().Where(a => a.parent_id == 0 && a.is_enabled == true).ToList();
            //ViewBag.topCategories = new SelectList(topCategory, "menu_category_id", "name");
            //if (filter.parent_category_id > 0)
            //{
            //    categories = menuCategory.Where(a => a.parent_id == filter.parent_category_id).OrderByDescending(a => a.menu_category_id).ToList();
            //}
            MenuCategoryIndexViewModel menuCategoryIndexVM = getViewModelFrom(categories);
            return View(menuCategoryIndexVM);
        }

        [HttpGet]
        [Route("new")]
        public IActionResult add()
        {
            MenuCategoryModel stockCategoryModel = new MenuCategoryModel();
            //var topCategory = _menuCategoryRepo.getQueryable().Where(a => a.parent_id == 0 && a.is_enabled == true).ToList();

            //ViewBag.topCategories = topCategory;
            return View(stockCategoryModel);
        }

        [HttpPost]
        [Route("new")]
        public IActionResult add(MenuCategoryModel menu_category_model)
        {
            //var topCategory = _menuCategoryRepo.getQueryable().Where(a => a.parent_id == 0 && a.is_enabled == true).ToList();
            try
            {
                if (ModelState.IsValid)
                {
                    //if (file != null)
                    //{
                    //    string fileName = menu_category_model.name;
                    //    menu_category_model.image_name = _fileHelper.saveImageAndGetFileName(file, fileName);

                    //}
                    MenuCategoryDto menu_category_dto = getStockCategoryDtoFromModel(menu_category_model);
                    _menuCategoryService.save(menu_category_dto);
                    AlertHelper.setMessage(this, "Item Category added successfully.", messageType.success);
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return RedirectToAction("add");
            }
            return View(menu_category_model);
        }

        [HttpGet]
        [Route("delete/{menu_category_id}")]
        public IActionResult delete(long menu_category_id)
        {
            try
            {
                _menuCategoryService.delete(menu_category_id);
                AlertHelper.setMessage(this, "Item Category deleted successfully.");
                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return RedirectToAction("index");
            }
        }

        [HttpGet]
        [Route("edit/{menu_category_id}")]
        public IActionResult edit(long menu_category_id)
        {
            try
            {
                var menuCategories = _menuCategoryRepo.getQueryable().ToList();

                //var topCategory = _menuCategoryRepo.getQueryable().Where(a => a.parent_id == 0 && a.is_enabled == true).ToList();
                //ViewBag.topCategories = new SelectList(topCategory, "menu_category_id", "name");

                var categoryDetails = _menuCategoryRepo.getById(menu_category_id);
                var categoryModel = getStockCategoryModelFromCategoryModel(categoryDetails);
                return View(categoryModel);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return RedirectToAction("index");
            }
        }


        [HttpPost]
        [Route("edit")]
        public IActionResult edit(MenuCategoryModel menuCategoryModel)
        {

            try
            {
                //if (file != null)
                //{
                //    string fileName = menuCategoryModel.name;
                //    menuCategoryModel.image_name = _fileHelper.saveImageAndGetFileName(file, fileName);

                //}

                MenuCategoryDto menuCategoryDto = getStockCategoryDtoFromModel(menuCategoryModel);
                _menuCategoryService.update(menuCategoryDto);
                AlertHelper.setMessage(this, "Item Category Updated successfully.", messageType.success);
                return RedirectToAction("index");

            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return RedirectToAction("index");
            }
            return View(menuCategoryModel);


        }

        [HttpGet]
        [Route("enable/{menu_category_id}")]
        public IActionResult enable(long menu_category_id)
        {
            try
            {
                _menuCategoryService.enable(menu_category_id);
                AlertHelper.setMessage(this, "Item Category enabled successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("disable/{menu_category_id}")]
        public IActionResult disable(long menu_category_id)
        {
            try
            {
                _menuCategoryService.disable(menu_category_id);
                AlertHelper.setMessage(this, "Item Category disabled successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        private object getStockCategoryModelFromCategoryModel(MenuCategory categoryDetails)
        {
            return _mapper.Map<MenuCategoryModel>(categoryDetails);
        }

        private MenuCategoryIndexViewModel getViewModelFrom(List<MenuCategory> categories)
        {
            MenuCategoryIndexViewModel VM = new MenuCategoryIndexViewModel();
            VM.menu_categories = new List<MenuCategoriesDetail>();
            foreach (var category in categories)
            {
                MenuCategoriesDetail detail = new MenuCategoriesDetail();
                detail.is_enabled = category.is_enabled;
                detail.name = category.name;
                detail.menu_category_id = category.menu_category_id;
              
                VM.menu_categories.Add(detail);
            }
            return VM;
        }

        private MenuCategoryDto getStockCategoryDtoFromModel(MenuCategoryModel menu_category_model)
        {
            var menuCategoryDto = _mapper.Map<MenuCategoryDto>(menu_category_model);
            return menuCategoryDto;
        }

    }
}
