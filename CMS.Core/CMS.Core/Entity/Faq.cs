using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Entity
{
   public class Faq
    {
        private string _question;
        private string _answer;
        [Key]
        public long faq_id { get; set; }
        [Required]
        [MaxLength(500)]
        public string question
        {
            get => _question;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NonEmptyValueException("Question is required.");
                }
                _question = value;
            }
        }
        [Required]
        [MaxLength(1000)]
        public string answer
        {
            get => _answer;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NonEmptyValueException("Answer is required.");
                }
                _answer = value;
            }
        }
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
