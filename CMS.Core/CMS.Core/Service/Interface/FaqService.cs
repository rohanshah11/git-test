using CMS.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Service.Interface
{
   public interface FaqService
    {
        void save(FaqDto faqDto);
        void delete(long faq_id);
        void update(FaqDto faqDto);
        void enable(long faq_id);
        void disable(long faq_id);
    }
}
