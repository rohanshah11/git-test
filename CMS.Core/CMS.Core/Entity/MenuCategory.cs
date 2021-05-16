using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Entity
{
    public class MenuCategory
    {
        private string _name;

        [Key]
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

        //[MaxLength(100)]
        //public string image_name { get; set; }


        public bool is_enabled { get; set; } = true;

        public void enable()
        {
            is_enabled = true;
        }

        public void disable()
        {
            is_enabled = false;
        }

        //public virtual List<MenuType> menuType { get; set; }


        //public bool hasmenuType()
        //{
        //    return menuType.Count > 0;
        //}

    }
}
