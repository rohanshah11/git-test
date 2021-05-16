using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Entity
{
    public class News
    {
        private string _title,_description;
        [Key]
        public long news_id { get; set; }
        [Required]
        [MaxLength(250)]
        public string title
        {
            get => _title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ItemNotFoundException("title is missing");

                }
                _title = value;
            }
        }
        [Required]
        public DateTime date { get; set; } = DateTime.Now;
        public string news_by { get; set; } 
        
        [Required]
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
        public string image { get; set; }
        [Required]
        [MaxLength(120)]
        public string slug { get; set; }
        public bool is_active { get; set; } = true;
        public void active()
        {
            is_active = true;
        }
        public void inactive()
        {
            is_active = false;
        }
    }
}
