using CMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Core.FilterModel
{
    public class AppointmentFilter:PaginationFilter
    {
        public string title { get; set; }
        public DateTime starting_date { get; set; } = DateTime.Now;
        public DateTime ending_date { get; set; } = DateTime.Now;

    }
}
