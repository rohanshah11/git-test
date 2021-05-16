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
using CMS.Web.Areas.Core.Models;
using CMS.Web.Areas.Core.ViewModels;
using CMS.Web.Helpers;
using CMS.Web.LEPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CMS.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("admin")]
    [Route("admin/menu")]
    public class MenuController : Controller
    {

        private readonly MenuRepository _menuRepo;
        private readonly MenuService _menuService;
        private readonly MenuCategoryRepository _menuCategoryRepository;
        private PaginatedMetaService _paginatedMetaService;
        private IMapper _mapper;
        private readonly FileHelper _fileHelper;
        private readonly SetupRepository _setupRepo;

        public MenuController(SetupRepository setupRepository, MenuCategoryRepository menuCategoryRepository, MenuRepository menuRepo, MenuService menuService, PaginatedMetaService paginatedMetaService, IMapper mapper, FileHelper fileHelper)
        {
            _menuRepo = menuRepo;
            _menuService = menuService;
            _menuCategoryRepository = menuCategoryRepository;
            _paginatedMetaService = paginatedMetaService;
            _mapper = mapper;
            _fileHelper = fileHelper;
            _setupRepo = setupRepository;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index(MenuFilter filter = null)
        {
            try
            {
                var menu_type = _menuRepo.getQueryable();
                if (!string.IsNullOrWhiteSpace(filter.title))
                {
                    menu_type = menu_type.Where(a => a.name.Contains(filter.title));
                }
                ViewBag.pagerInfo = _paginatedMetaService.GetMetaData(menu_type.Count(), filter.page, filter.number_of_rows);
                menu_type = menu_type.Skip(filter.number_of_rows * (filter.page - 1)).Take(filter.number_of_rows);
                var menuDetails = menu_type.ToList();

                var menuIndexVM = getViewModelFrom(menuDetails);
                return View(menuIndexVM);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return Redirect("/admin");
            }
        }

     
        [Route("new")]
        public IActionResult add()
        {
            MenuModel model = new MenuModel();
            var menuCategory = _menuCategoryRepository.getQueryable().ToList();
            ViewBag.menuCategory = new SelectList(menuCategory, "menu_category_id", "name");
            return View(model);
        }

        [HttpPost]
        [Route("new")]
        public IActionResult add(MenuModel model, IFormFile file = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        string fileName = model.name;
                        model.image_name = _fileHelper.saveImageAndGetFileName(file, fileName);

                    }
                    MenuDto dto = getStockCategoryDtoFromModel(model);

                    _menuService.save(dto);
                    AlertHelper.setMessage(this, "Item saved successfully.", messageType.success);
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return RedirectToAction("new");
            }
            var menucategories = _menuCategoryRepository.getQueryable().ToList();
            ViewBag.menuCategory = new SelectList(menucategories, "menu_category_id", "name");
            return View(model);
        }



        [HttpGet]
        [Route("edit/{menu_id}")]
        public IActionResult edit(long menu_id)
        {
            try
            {
                var menuCategories = _menuCategoryRepository.getQueryable().ToList();
                ViewBag.menuCategory = new SelectList(menuCategories, "menu_category_id", "name");
                Menu menu = _menuRepo.getById(menu_id);
                MenuModel menuModel = _mapper.Map<MenuModel>(menu);

                return View(menuModel);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return RedirectToAction("index");
            }
        }


        [HttpPost]
        [Route("edit")]
        public IActionResult edit(MenuModel model, IFormFile file = null)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        string fileName = model.name;
                        model.image_name = _fileHelper.saveImageAndGetFileName(file, fileName);

                    }
                    MenuDto dto = getStockCategoryDtoFromModel(model);



                    _menuService.update(dto);
                    AlertHelper.setMessage(this, "Item updated successfully.");
                    return RedirectToAction("index");
                }

            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            var menuCategories = _menuCategoryRepository.getQueryable().ToList();
            ViewBag.menuCategory = new SelectList(menuCategories, "menu_category_id", "name");
            return View(model);
        }
        [HttpGet]
        [Route("delete/{menu_id}")]
        public IActionResult delete(long menu_id)
        {
            try
            {
                _menuService.delete(menu_id);
                AlertHelper.setMessage(this, "Item deleted successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        [Route("enable/{menu_id}")]
        public IActionResult enable(long menu_id)
        {
            try
            {
                _menuService.enable(menu_id);
                AlertHelper.setMessage(this, "Item enabled successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("disable/{menu_id}")]
        public IActionResult disable(long menu_id)
        {
            try
            {
                _menuService.disable(menu_id);
                AlertHelper.setMessage(this, "Item disabled successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        private MenuDto getStockCategoryDtoFromModel(MenuModel model)
        {
            var menuDto = _mapper.Map<MenuDto>(model);
            return menuDto;
        }

        private MenuIndexViewModel getViewModelFrom(List<CMS.Core.Entity.Menu> menus)
        {
            MenuIndexViewModel vm = new MenuIndexViewModel();
            vm.menu_details = new List<MenuDetails>();
            foreach (var menu in menus)
            {
                var Menu = _mapper.Map<MenuDetails>(menu);
                vm.menu_details.Add(Menu);
            }

            return vm;
        }
    }
}
