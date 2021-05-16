using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Dto
{
   public class FaqDto
    {
        private string _question;
        private string _answer;
        [Key]

        public long faq_id { get; set; }
        [Display(Name = "Question")]
        public string question { get; set; }
        [Display(Name = "Answer")]
        public string answer { get; set; }
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

