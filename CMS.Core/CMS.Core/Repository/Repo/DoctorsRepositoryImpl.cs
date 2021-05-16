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
   public class DoctorsRepositoryImpl: BaseRepositoryImpl<Doctors>, DoctorsRepository
    {
        private readonly AppDbContext _appDbContext;
        public DoctorsRepositoryImpl(AppDbContext context, DetailsEncoder<Doctors> detailsEncoder, HtmlEncodingClassHelper htmlEncodingClassHelper) : base(context, detailsEncoder, htmlEncodingClassHelper)
        {
            _appDbContext = context;

        }

        public Doctors getBySlug(string slug)
        {
            return _appDbContext.doctors.Where(a => a.slug == slug).SingleOrDefault();
        }
    }
}
