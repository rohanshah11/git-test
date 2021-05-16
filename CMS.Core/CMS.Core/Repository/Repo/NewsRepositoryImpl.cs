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
   public class NewsRepositoryImpl : BaseRepositoryImpl<News> ,NewsRepository
    {
        private readonly AppDbContext _appDbContext;
        public NewsRepositoryImpl(AppDbContext context, DetailsEncoder<News> detailsEncoder, HtmlEncodingClassHelper htmlEncodingClassHelper) : base(context, detailsEncoder, htmlEncodingClassHelper)
        {
            _appDbContext = context;
        }

        public News getBySlug(string slug)
        {
            return _appDbContext.news.Where(a => a.slug == slug).SingleOrDefault();
        }
    }
}
