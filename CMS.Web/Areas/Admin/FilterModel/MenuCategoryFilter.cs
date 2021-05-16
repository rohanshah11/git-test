using CMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Core.FilterModel
{
    public class MenuCategoryFilter : PaginationFilter
    {
        public string name { get; set; }
        public long parent_category_id { get; set; }
    }
}
