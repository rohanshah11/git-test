using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Dto
{
    public class NewsDto
    {
        public long news_id { get; set; }
      
        [Display(Name = "Title")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Title is required.")]
        public string title { get; set; }
      
        [Display(Name = "Date")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Date is required.")]
        public DateTime date { get; set; } = DateTime.Now;
       
        [Display(Name = "News By")]
        public string news_by { get; set; }

        public string slug { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Description is required.")]
        [Display(Name = "Description")]
        public string description { get; set; }
       
        [Display(Name = "Image")]
        public string image { get; set; }

        public bool is_active { get; set; } = true;
    }
}
