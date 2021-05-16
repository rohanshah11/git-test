using CMS.Core.Data;
using CMS.Core.Entity;
using CMS.Core.Helper;
using CMS.Core.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Repository.Repo
{
    public class MenuCategoryRepositoryImpl : BaseRepositoryImpl<MenuCategory>, MenuCategoryRepository
    {
        private readonly AppDbContext _appDbContext;
        public MenuCategoryRepositoryImpl(AppDbContext context, DetailsEncoder<MenuCategory> detailsEncoder, HtmlEncodingClassHelper htmlEncodingClassHelper) : base(context, detailsEncoder, htmlEncodingClassHelper)
        {
            _appDbContext = context;
        }
       
    }

}

