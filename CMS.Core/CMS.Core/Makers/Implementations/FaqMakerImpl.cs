using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Makers.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Makers.Implementations
{
   public class FaqMakerImpl : FaqMaker
    {
        public void copy(Faq faq, FaqDto faqDto)
        {
            faq.faq_id = faqDto.faq_id;
            faq.question = faqDto.question;
            faq.answer = faqDto.answer;
            faq.is_active = faqDto.is_active;
        }
    }
}
