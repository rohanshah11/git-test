using CMS.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Service.Interface
{
    public interface MenuCategoryService
    {
        void save(MenuCategoryDto menu_category_dto);
        void update(MenuCategoryDto menu_category_dto);
        void delete(long menu_category_id);
        void enable(long menu_category_id);
        void disable(long menu_category_id);
    }
}
