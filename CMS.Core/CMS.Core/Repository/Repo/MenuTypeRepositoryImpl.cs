using CMS.Core.Data;
using CMS.Core.Entity;
using CMS.Core.Helper;
using CMS.Core.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Repository.Repo
{
    public class MenuTypeRepositoryImpl : BaseRepositoryImpl<MenuType>, MenuTypeRepository
    {
        private readonly AppDbContext _appDbContext;
        public MenuTypeRepositoryImpl(AppDbContext context, DetailsEncoder<MenuType> detailsEncoder, HtmlEncodingClassHelper htmlEncodingClassHelper) : base(context, detailsEncoder, htmlEncodingClassHelper)
        {
            _appDbContext = context;
        }
       
    }
}
 