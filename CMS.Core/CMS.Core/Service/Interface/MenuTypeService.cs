using CMS.Core.Dto;
using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Service.Interface
{
    public interface MenuTypeService
    {
        void save(MenuTypeDto menu_type_dto);
        void update(MenuTypeDto menu_type_dto);
        void delete(long menu_type_id);
    }
}
