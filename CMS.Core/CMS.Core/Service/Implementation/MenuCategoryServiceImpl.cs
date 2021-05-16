using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Exceptions;
using CMS.Core.Makers.Interface;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Transactions;

namespace CMS.Core.Service.Implementation
{
    public class MenuCategoryServiceImpl : MenuCategoryService
    {
        private readonly MenuCategoryRepository _menuCategoryRepo;
        private readonly MenuCategoryMaker _menuCategoryMaker;
        private readonly IHostingEnvironment _hostingEnvironment;

        public MenuCategoryServiceImpl(MenuCategoryRepository menuCategoryRepo, MenuCategoryMaker menuCategoryMaker, IHostingEnvironment hostingEnvironment)
        {
            _menuCategoryRepo = menuCategoryRepo;
            _menuCategoryMaker = menuCategoryMaker;
            _hostingEnvironment = hostingEnvironment;
        }
        public void delete(long menu_category_id)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    var menuCategory = _menuCategoryRepo.getById(menu_category_id);
                    //var hasChildCategories = _menuCategoryRepo.getQueryable().Where(a => a.parent_id == menu_category_id).ToList();
                    //if (hasChildCategories.Count > 0)
                    //{
                    //    throw new ItemUsedException("This Category has child categories. Delete child categories and try again.");
                    //}

                    if (menuCategory == null)
                        throw new ItemNotFoundException($"The Item Category with {menu_category_id} doesnot exist.");
                    //if (menuCategory.hasmenuType())
                    //    throw new ItemUsedException($"The Menu Category with id {menu_category_id} already has menu type.You cannot delete at this moment.");

                    _menuCategoryRepo.delete(menuCategory);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void disable(long menu_category_id)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    var menuCategory = _menuCategoryRepo.getById(menu_category_id);

                    if (menuCategory == null)
                        throw new ItemNotFoundException($"The Item Category with id {menu_category_id} doesnot exist.");

                    menuCategory.disable();
                    _menuCategoryRepo.update(menuCategory);

                    tx.Complete();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void enable(long menu_category_id)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    var menuCategory = _menuCategoryRepo.getById(menu_category_id);
                    if (menuCategory == null)
                        throw new ItemNotFoundException($"The Item Category with id {menu_category_id} doesnot exist.");

                    menuCategory.enable();
                    _menuCategoryRepo.update(menuCategory);
                    tx.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void save(MenuCategoryDto menu_category_dto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    var menuCategory = _menuCategoryRepo.getQueryable().Where(a => a.name == menu_category_dto.name).SingleOrDefault();
                    if (menuCategory != null)
                    {
                        throw new ItemUsedException($"Category Name with {menu_category_dto.name} already exist.");
                    }
                    MenuCategory menu_category = new MenuCategory();
                    _menuCategoryMaker.copy( menu_category, menu_category_dto);
                    _menuCategoryRepo.insert(menu_category);
                    tx.Complete();
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void update(MenuCategoryDto menu_category_dto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    MenuCategory menuCategory = _menuCategoryRepo.getById(menu_category_dto.menu_category_id);
                    if (menuCategory == null)
                        throw new ItemNotFoundException($"The Item Category with id {menu_category_dto.menu_category_id} doesnot exist");
                    //if (menu_category_dto.image_name == null)
                    //{
                    //    menu_category_dto.image_name = menuCategory.image_name;
                    //}
                    //string oldImage = menuCategory.image_name;
                    //if (!string.IsNullOrWhiteSpace(menu_category_dto.image_name))
                    //{
                    //    if (!string.IsNullOrWhiteSpace(oldImage))
                    //    {
                    //        deleteImage(oldImage);
                    //    }
                    //}
                    _menuCategoryMaker.copy(menuCategory, menu_category_dto);
                    _menuCategoryRepo.update(menuCategory);
                    tx.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        //protected void deleteImage(string image_path)
        //{
        //    var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images/custom");
        //    if (File.Exists(Path.Combine(filePath, image_path)))
        //    {
        //        File.Delete(Path.Combine(filePath, image_path));

        //    }
        //}
    }
}
