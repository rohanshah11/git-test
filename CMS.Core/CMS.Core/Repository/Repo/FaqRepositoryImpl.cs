using CMS.Core.Data;
using CMS.Core.Entity;
using CMS.Core.Helper;
using CMS.Core.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Repository.Repo
{
    public class FaqRepositoryImpl : BaseRepositoryImpl<Faq>, FaqRepository
    {
        private readonly AppDbContext _appDbContext;

        public FaqRepositoryImpl(AppDbContext appDbContext, DetailsEncoder<Faq> detailsEncoder, HtmlEncodingClassHelper htmlEncodingClassHelper) : base(appDbContext, detailsEncoder, htmlEncodingClassHelper)
        {
            _appDbContext = appDbContext;
        }
    }
}
