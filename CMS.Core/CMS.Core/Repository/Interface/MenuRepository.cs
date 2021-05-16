using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Interface
{
    public interface MenuRepository
    {
        void insert(Menu menu);
        void update(Menu menu);
        void delete(Menu menu);
        List<Menu> getAll();
        Menu getById(long menu_id);
        List<Menu> getmenuWithinDate(DateTime start_date, DateTime end_date);
        IQueryable<Menu> getQueryable();
        Menu getBySlug(string slug);
    }
}
