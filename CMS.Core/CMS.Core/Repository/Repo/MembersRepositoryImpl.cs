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
    public class MembersRepositoryImpl : BaseRepositoryImpl<Member> , MembersRepository
    {
        private readonly AppDbContext _appDbContext;

        public MembersRepositoryImpl(AppDbContext context, DetailsEncoder<Member> detailsEncoder, HtmlEncodingClassHelper htmlEncodingClassHelper) : base(context, detailsEncoder, htmlEncodingClassHelper)
        {
            _appDbContext = context;

        }

        public Member getBySlug(string slug)
        {
            return _appDbContext.members.Where(a => a.slug.Contains(slug)).SingleOrDefault();
        }
    }
}
