using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CMS.Core.Entity
{
    public class Menu
    {
        private string _name;
        private string _description;
        [Key]
        public long menu_id { get; set; }
    
        public long menu_category_id { get; set; }

        [ForeignKey("menu_category_id")]
        public virtual MenuCategory menu_category { get; set; }
        [Required]
        [MaxLength(150)]
        public string name { 
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
        [Required]
        public DateTime menu_date { get; set; } = DateTime.Now;
        [Required]
        [MaxLength(120)]
        public string slug { get; set; }

        [Required]
        [MaxLength(2000)]
        public string description
        {
            get => _description;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NonEmptyValueException("Description is required.");
                }
                _description = value;
            }
        }
        [Required]
        public decimal unit_price { get; set; }

        public decimal fake_price { get; set; }


        [Required]
        public string image_name { get; set; }


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
