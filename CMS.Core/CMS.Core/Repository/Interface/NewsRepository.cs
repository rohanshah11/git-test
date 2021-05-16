using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Interface
{
    public interface NewsRepository
    {
        void insert(News news);
        void update(News news);
        void delete(News news);
        List<News> getAll();
        News getById(long news_id);
        IQueryable<News> getQueryable();
        News getBySlug(string slug);

    }
}
