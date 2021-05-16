using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CMS.Core.Entity
{
    public class MenuType
    {
        private string _name;
        [Key]
        public long menu_type_id { get; set; }
        [Required]
        [MaxLength(150)]
        public string name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NonEmptyValueException("Name is Required.");
                }
                _name = value;
            }
        }
        [ForeignKey("menu_category_id")]
        public virtual MenuCategory menu_category { get; set; }

        public long menu_category_id { get; set; }
        public virtual List<Menu> menu { get; set; }


        public bool hasmenu()
        {
            return menu.Count > 0;
        }
    }
}
