using CMS.Core.Entity;
using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Dto
{
   public class ClassesDto
    {
        [Key]
        public long class_id { get; set; }
        public bool is_active { get; set; } = true;
        public void active()
        {
            is_active = true;
        }
        public void inactive()
        {
            is_active = false;
        }
        private string _name;
        [Required]
        [MaxLength(200)]
        public string name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NonEmptyValueException("name is required");
                }
                _name = value;
            }
        }
    }
}
