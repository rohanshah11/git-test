using System;
using System.Collections.Generic;
using CMS.Core.Entity;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Core.ViewModels
{
    public class NewsIndexViewModel
    {
        public List<NewsDetailModel> news_detail { get; set; }
    }

    public class NewsDetailModel
    {
        public long news_id { get; set; }
        [Display(Name = "Title")]

        public string title { get; set; }
        [Display(Name = "Date")]
        public DateTime date { get; set; } = DateTime.Now;
        [Display(Name = "News By")]

        public string news_by { get; set; }
        [Display(Name = "Description")]

        public string description { get; set; }
        [Display(Name = "Image")]

        public string image { get; set; }

        public bool is_active { get; set; } = true;
    }
    }

