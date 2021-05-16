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
using CMS.Web.Areas.Admin.ViewModels;
using CMS.Web.Areas.Core.Models;
using CMS.Web.Areas.Core.ViewModels;
using CMS.Web.Helpers;
using CMS.Web.LEPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CMS.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("admin")]
    [Route("admin/menu-type")]
    public class MenuTypeController : Controller
    {
        private readonly MenuTypeRepository _menuTypeRepository;
        private readonly MenuTypeService _menuTypeService;
        private readonly IMapper _mapper;
        private readonly PaginatedMetaService _paginatedMetaService;
        private readonly MenuCategoryRepository _menuCategoryRepo;

        public MenuTypeController(MenuTypeRepository menuTypeRepository, MenuTypeService menuTypeService, MenuCategoryRepository menuCategoryRepo, IMapper mapper, PaginatedMetaService paginatedMetaService)
        {
            _menuTypeRepository = menuTypeRepository;
            _menuTypeService = menuTypeService;
            _mapper = mapper;
            _paginatedMetaService = paginatedMetaService;
            _menuCategoryRepo = menuCategoryRepo;
          
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index(MenuTypeFilter filter = null)
        {
            try
            {
                var menu_type = _menuTypeRepository.getQueryable();

                if (!string.IsNullOrWhiteSpace(filter.title))
                {
                    menu_type = menu_type.Where(a => a.name.Contains(filter.title));
                }


                ViewBag.pagerInfo = _paginatedMetaService.GetMetaData(menu_type.Count(), filter.page, filter.number_of_rows);


                menu_type = menu_type.Skip(filter.number_of_rows * (filter.page - 1)).Take(filter.number_of_rows);

                var eventDetails = menu_type.ToList();

                var eventlIndexVM = getViewModelFrom(eventDetails);
                return View(eventlIndexVM);
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
            MenuTypeModel model = new MenuTypeModel();
            var menuCategories = _menuCategoryRepo.getQueryable().Where(a => a.is_enabled == true).ToList();
            ViewBag.categories = new SelectList(menuCategories, "menu_category_id", "name");
            return View(model);
        }

        [HttpPost]
        [Route("new")]
        public IActionResult add(MenuTypeModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    MenuTypeDto dto = new MenuTypeDto();
                    dto.menu_type_id = model.menu_type_id;
                    dto.menu_category_id = model.menu_category_id;
                    dto.name = model.name;

                    _menuTypeService.save(dto);
                    AlertHelper.setMessage(this, "Item Category saved successfully.", messageType.success);
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return RedirectToAction("new");
            }
            var menuCategories = _menuCategoryRepo.getQueryable().Where(a => a.is_enabled == true).ToList();
            ViewBag.categories = new SelectList(menuCategories, "menu_category_id", "name");
            return View(model);
        }
        [HttpGet]
        [Route("edit/{menu_type_id}")]
        public IActionResult edit(long menu_type_id)
        {
            try
            {
                var menuCategories = _menuCategoryRepo.getQueryable().Where(a => a.is_enabled == true).ToList();
                ViewBag.categories = new SelectList(menuCategories, "menu_category_id", "name");
                MenuType menu_types = _menuTypeRepository.getById(menu_type_id);
                MenuTypeModel menuTypeModel = _mapper.Map<MenuTypeModel>(menu_types);

                return View(menuTypeModel);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return RedirectToAction("index");
            }
        }

        [HttpPost]
        [Route("edit")]
        public IActionResult edit(MenuTypeModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    MenuTypeDto dto = new MenuTypeDto();
                    dto.menu_type_id = model.menu_type_id;
                    dto.menu_category_id = model.menu_category_id;
                    dto.name = model.name;



                    _menuTypeService.update(dto);
                    AlertHelper.setMessage(this, "Item Type updated successfully.");
                    return RedirectToAction("index");
                }

            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            var menuCategories = _menuCategoryRepo.getQueryable().Where(a => a.is_enabled == true).ToList();
            ViewBag.categories = new SelectList(menuCategories, "menu_category_id", "name");
            return View(model);
        }
        [HttpGet]
        [Route("delete/{menu_type_id}")]
        public IActionResult delete(long menu_type_id)
        {
            try
            {
                _menuTypeService.delete(menu_type_id);
                AlertHelper.setMessage(this, "Item Type deleted successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }
        private MenuTypeIndexViewModel getViewModelFrom(List<CMS.Core.Entity.MenuType> menu_types)
        {
            MenuTypeIndexViewModel vm = new MenuTypeIndexViewModel();
            vm.menu_type_details = new List<MenuTypeDetails>();
            foreach (var menu_type in menu_types)
            {
                var Menu_Type = _mapper.Map<MenuTypeDetails>(menu_type);
                vm.menu_type_details.Add(Menu_Type);
            }

            return vm;
        }
    }
}
