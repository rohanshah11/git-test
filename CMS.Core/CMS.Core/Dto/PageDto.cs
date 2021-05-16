using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Dto
{
    public class PageDto
    {
        private string _title,_description;
        public long page_id { get; set; }

        [Display(Name = "Page Category")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please choose a category")]
        public long page_category_id { get; set; }

        [Display(Name = "Title")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a title")]
        [MaxLength(60,ErrorMessage ="Title cannot be more than 60 letters.")]
        public string title
        {
            get => _title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NonEmptyValueException("Title is required.");
                }
                _title = value;
            }
        }

        [Display(Name = "Description")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter a description")]
        public string description
        {
            get => _description;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ItemNotFoundException("Description is required.");
                }
                _description = value;
            }
        }
        public string image_name { get; set; }

        [Display(Name ="Status")]
        public bool is_enabled { get; set; } = true;
    }
}
