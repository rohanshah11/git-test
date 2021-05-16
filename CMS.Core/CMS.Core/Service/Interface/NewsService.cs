using CMS.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Service.Interface
{
   public interface NewsService
    {
        void save(NewsDto newsDto);
        void update(NewsDto newsDto);
        void delete(long news_id);
        void enable(long news_id);
        void disable(long news_id);
    }
}
