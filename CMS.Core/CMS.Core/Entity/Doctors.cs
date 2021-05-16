using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Entity
{
    public class Doctors
    {
        private string _name;

        [Key]
        public long doctor_id { get; set; }
        [Required]
        [MaxLength(250)]
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
        [MaxLength(2000)]
        public string speciality { get; set; }
        [MaxLength(1000)]
        public string address { get; set; }
        [MaxLength(1000)]
        public string education { get; set; }
        [Required]
        [MaxLength(120)]
        public string slug { get; set; }

        [MaxLength(50)]
        public string contact_number { get; set; }

        [MaxLength(120)]
        public string image { get; set; }
        public bool is_contact_enabled { get; set; } = false;
        public void enable()
        {
            is_contact_enabled = true;
        }

        public void disable()
        {
            is_contact_enabled = false;
        }
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        [MaxLength(50)]
        public string email { get; set; }

        public bool is_active { get; set; } = true;
        public void active()
        {
            is_active = true;
        }
        public void inactive()
        {
            is_active = false;
        }
        [MaxLength(200)]
        public string website { get; set; }
        [MaxLength(200)]
        public string facebook { get; set; }
        [MaxLength(200)]
        public string twitter { get; set; }


        [MaxLength(200)]
        public string experience { get; set; }
    }
}
