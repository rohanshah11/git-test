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
    public class MenuServiceImpl : MenuService
    {
        private readonly MenuRepository _menuRepository;
        private readonly MenuMaker _menuMaker;
        private readonly IHostingEnvironment _hostingEnvironment;
        public MenuServiceImpl(IHostingEnvironment hostingEnvironment, MenuRepository menuRepository, MenuMaker menuMaker)
        {
            _menuRepository = menuRepository;
            _menuMaker = menuMaker;
            _hostingEnvironment = hostingEnvironment;
        }
        public void delete(long menu_id)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    var gallery = _menuRepository.getById(menu_id);
                    if (gallery == null)
                    {
                        throw new ItemNotFoundException($"Item with id {menu_id} doesn't exist.");
                    }
                    string oldImage = gallery.image_name;
                    _menuRepository.delete(gallery);
                    deleteImage(oldImage);

                    tx.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }




        }
        protected void deleteImage(string image_path)
        {
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images/custom");
            if (File.Exists(Path.Combine(filePath, image_path)))
            {
                File.Delete(Path.Combine(filePath, image_path));

            }
        }
        public void disable(long menu_id)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    var menu = _menuRepository.getById(menu_id);

                    if (menu == null)
                        throw new ItemNotFoundException($"The Item with id {menu_id} doesnot exist.");

                    menu.disable();
                    _menuRepository.update(menu);

                    tx.Complete();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void enable(long menu_id)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    var menu = _menuRepository.getById(menu_id);
                    if (menu == null)
                        throw new ItemNotFoundException($"The Item with id {menu_id} doesnot exist.");

                    menu.enable();
                    _menuRepository.update(menu);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void save(MenuDto menu_dto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    var menu = _menuRepository.getQueryable().Where(a => a.name == menu_dto.name).SingleOrDefault();
                    if (menu != null)
                    {
                        throw new ItemUsedException($"Item Name with {menu_dto.name} already exist.");
                    }
                    Menu menus = new Menu();
                    _menuMaker.copy(menus, menu_dto);
                    _menuRepository.insert(menus);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void update(MenuDto menu_dto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    Menu menu = _menuRepository.getById(menu_dto.menu_id);
                    if (menu == null)
                        throw new ItemNotFoundException($"The Item with id {menu_dto.menu_id} doesnot exist");
                    if (menu_dto.image_name == null)
                    {
                        menu_dto.image_name = menu.image_name;
                    }
                    //string oldImage = menuCategory.image_name;
                    //if (!string.IsNullOrWhiteSpace(menu_category_dto.image_name))
                    //{
                    //    if (!string.IsNullOrWhiteSpace(oldImage))
                    //    {
                    //        deleteImage(oldImage);
                    //    }
                    //}
                    _menuMaker.copy(menu, menu_dto);
                    _menuRepository.update(menu);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
