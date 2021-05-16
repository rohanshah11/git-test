﻿using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Core.Models
{
    public class BlogModel
    {
        private string _description, _artical_by,_title;

        [Key]
        [Display(Name = "ID")]
        public long blog_id { get; set; }

        [Display(Name = "Title")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a Title.")]
        public string title { get; set; }

        [Display(Name = "Date")]
        public DateTime posted_on { get; set; } = DateTime.Now;

        [Display(Name = "Artical By")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a Artical By.")]
        public string artical_by { get; set; }

        [Display(Name = "Description")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a Description.")]
        public string description { get; set; }

        [Display(Name = "Image")]
        public string image_name { get; set; }

        [Display(Name = "Comment By")]
        public string comment_by { get; set; }

        [Display(Name = "Email")]
        public string email { get; set; }

        [Display(Name = "Comment Date")]
        public DateTime comment_date { get; set; } = DateTime.Now;

        [Display(Name = "Comments")]
        public string comments { get; set; }

        public bool is_enabled { get; set; } = true;

        public void enable()
        {
            is_enabled = true;
        }

        public void disable()
        {
            is_enabled = false;
        }
    }
}
