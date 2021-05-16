﻿using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Dto
{
    public class NoticeDto
    {
        private string _title, _description;

        public long notice_id { get; set; }

        [Display(Name ="Notice Date")]
        public DateTime notice_date { get; set; } = DateTime.Now;

        [Display(Name = "Notice End Date")]
        public DateTime notice_expiry_date { get; set; } = DateTime.Now.AddDays(7);

        [Display(Name = "Description")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a Description")]
        public string description { get; set; }

        [Display(Name = "Image")]
        public string image_name { get; set; }

        [Display(Name = "Title")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a Title")]
        public string title { get; set; }
        
        public bool is_closed { get; set; } = false;
    }
}
