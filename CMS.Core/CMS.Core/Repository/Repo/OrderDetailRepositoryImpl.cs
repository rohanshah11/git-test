using CMS.Core.Data;
using CMS.Core.Entity;
using CMS.Core.Helper;
using CMS.Core.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Repository.Repo
{
    public class OrderDetailRepositoryImpl : BaseRepositoryImpl<OrderDetail>, OrderDetailRepository
    {
        public OrderDetailRepositoryImpl(AppDbContext context, DetailsEncoder<OrderDetail> detailsEncoder, HtmlEncodingClassHelper htmlEncodingClassHelper) : base(context, detailsEncoder, htmlEncodingClassHelper)
        {

        }
    }
}

