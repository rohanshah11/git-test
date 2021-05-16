using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Dto
{
    public class MenuCategoryDto
    {
        private string _name;

        public long menu_category_id { get; set; }

        [Required]
        [MaxLength(50)]
        public string name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NonEmptyValueException("Name is required.");
                }
                _name = value;
            }
        }

        //[MaxLength(100)]
        //public string description { get; set; }

        //public long parent_id { get; set; }

        public bool is_enabled { get; set; } = true;

        //[MaxLength(100)]
        //public string image_name { get; set; }

    }
}
