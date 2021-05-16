using CMS.Core.Enums;
using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Entity
{
    public class Appointment
    {
        private string _name;
        private string _contact_no;
        public string _description;
        private string _country;
        [Key]
        public long appointment_id { get; set; }
        [MaxLength(2000)]

        public string name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ItemNotFoundException("name is required");
                }
                _name = value;
            }
        }
        [MaxLength(2000)]

        public string address { get; set; }
        [MaxLength(100)]

        public string contact_no
        {
            get => _contact_no;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ItemNotFoundException("contact no is not found");
                }
                _contact_no = value;
            }
        }
        [MaxLength(2000)]
        public string description
        {
            get => _description;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ItemNotFoundException("description required");
                }
                _description = value;
            }
        }
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]

        [MaxLength(250)]
            public string email { get; set; }
        [Required]
        public DateTime appointment_date { get; set; }
        [Required]
        public DateTime entry_date { get; set; } = DateTime.Now;
        [Required]
        public string country { get; set; }

        [Required]
        public AppointmentEnum type { get; set; } = AppointmentEnum.pending;
        public void Approved()
        {
            type = AppointmentEnum.approved;
        }
        public void Cancelled()
        {
            type = AppointmentEnum.cancelled;
        }
    }
    }



