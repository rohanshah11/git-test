using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Interface
{
    public interface MenuTypeRepository
    {
        void insert(MenuType menu_type);
        void update(MenuType menu_type);
        void delete(MenuType menu_type);
        List<MenuType> getAll();
        MenuType getById(long menu_type_id);
        IQueryable<MenuType> getQueryable();
    }
}
