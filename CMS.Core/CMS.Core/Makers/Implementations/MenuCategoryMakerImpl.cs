using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Makers.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Makers.Implementations
{
    public class MenuCategoryMakerImpl : MenuCategoryMaker
    {
        public void copy(MenuCategory menu_category, MenuCategoryDto dto)
        {
            menu_category.menu_category_id = dto.menu_category_id;
            //menu_category.parent_id = dto.parent_id;
            menu_category.is_enabled = dto.is_enabled;
            //menu_category.image_name = dto.image_name;
            //menu_category.description = dto.description;
            menu_category.name = dto.name;
        }
    }
}
