using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Exceptions;
using CMS.Core.Makers.Interface;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace CMS.Core.Service.Implementation
{
    public class MenuTypeServiceImpl : MenuTypeService
    {
        private readonly MenuTypeMaker _menuTypeMaker;
        private readonly MenuTypeRepository _menuTypeRepository;
        public MenuTypeServiceImpl(MenuTypeMaker menuTypeMaker, MenuTypeRepository menuTypeRepository)
        {
            _menuTypeMaker = menuTypeMaker;
            _menuTypeRepository = menuTypeRepository;
        }
        public void delete(long menu_type_id)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    var menuType = _menuTypeRepository.getById(menu_type_id);

                    if (menuType == null)
                    {
                        throw new ItemNotFoundException($"Menu Type  with id {menu_type_id} doesnot exist.");
                    }
                    if (menuType.hasmenu())
                        throw new ItemUsedException($"The Menu Type with id {menu_type_id} already has menu.You cannot delete at this moment.");


                    _menuTypeRepository.delete(menuType);
                    tx.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void save(MenuTypeDto menu_type_dto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    MenuType menuType = new MenuType();
                    _menuTypeMaker.copy(menuType, menu_type_dto);
                    _menuTypeRepository.insert(menuType);

                    tx.Complete();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void update(MenuTypeDto menu_type_dto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    MenuType menu_type = _menuTypeRepository.getById(menu_type_dto.menu_type_id);
                    if (menu_type == null)
                    {
                        throw new ItemNotFoundException($"Menu Type with ID {menu_type_dto.menu_type_id} doesnot Exit.");
                    }

                    _menuTypeMaker.copy(menu_type, menu_type_dto);
                    _menuTypeRepository.update(menu_type);

                    tx.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
