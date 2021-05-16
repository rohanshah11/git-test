using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Makers.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Makers.Implementations
{
    public class MenuTypeMakerImpl : MenuTypeMaker
    {
        public void copy(MenuType menu_type, MenuTypeDto dto)
        {
            menu_type.menu_category_id = dto.menu_category_id;
            menu_type.menu_type_id = dto.menu_type_id;
            menu_type.name = dto.name;
        }
    }
}
