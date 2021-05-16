using CMS.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Service.Interface
{
    public interface MenuService
    {
        void save(MenuDto menu_dto);
        void update(MenuDto menu_dto);
        void delete(long menu_id);
        void enable(long menu_id);
        void disable(long menu_id);
    }
}
