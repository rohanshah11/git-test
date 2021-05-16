using CMS.Core.Data;
using CMS.Core.Entity;
using CMS.Core.Helper;
using CMS.Core.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Repo
{
    public class MenuRepositoryImpl : BaseRepositoryImpl<Menu>, MenuRepository
    {
        private readonly AppDbContext _appDbContext;
        public MenuRepositoryImpl(AppDbContext context, DetailsEncoder<Menu> detailsEncoder, HtmlEncodingClassHelper htmlEncodingClassHelper) : base(context, detailsEncoder, htmlEncodingClassHelper)
        {
            _appDbContext = context;
        }
        public Menu getBySlug(string slug)
        {
            return _appDbContext.menu.Where(a => a.slug == slug).SingleOrDefault();
        }
        public List<Menu> getmenuWithinDate(DateTime start_date, DateTime end_date)
        {
            return _appDbContext.menu.Where(a => a.menu_date.Date >= start_date.Date && a.menu_date.Date <= end_date.Date).ToList();
        }

    }
}
