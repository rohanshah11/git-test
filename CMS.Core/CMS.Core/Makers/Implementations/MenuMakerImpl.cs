using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Makers.Interface;
using CMS.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Makers.Implementations
{
    public class MenuMakerImpl : MenuMaker
    {
        private readonly SlugGenerator _slugGenerator;

        public MenuMakerImpl(SlugGenerator slugGenerator)
        {
            _slugGenerator = slugGenerator;
        }
        public void copy(Menu menu, MenuDto dto)
        {
            menu.description = dto.description;
            menu.image_name = dto.image_name;
            menu.is_enabled = dto.is_enabled;
            menu.menu_id = dto.menu_id;
            menu.menu_category_id = dto.menu_category_id;
            menu.name = dto.name; 
            menu.unit_price = dto.unit_price;
            menu.fake_price = dto.fake_price;
            menu.menu_date = dto.menu_date;
            menu.slug = _slugGenerator.generate(dto.name);
        }
    }
}
