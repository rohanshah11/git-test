using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Interface
{
    public interface MenuCategoryRepository
    {
        void insert(MenuCategory menu_category);
        void update(MenuCategory menu_category);
        void delete(MenuCategory menu_category);
        List<MenuCategory> getAll();
        MenuCategory getById(long menu_category_id);
        IQueryable<MenuCategory> getQueryable();
    }
}
