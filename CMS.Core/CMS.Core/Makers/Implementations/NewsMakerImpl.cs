using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Makers.Interface;
using CMS.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Makers.Implementations
{
    public class NewsMakerImpl : NewsMaker
    {
        private readonly SlugGenerator _slugGenerator;

        public NewsMakerImpl(SlugGenerator slugGenerator)
        {
            _slugGenerator = slugGenerator;
        }
        public void copy(News news, NewsDto newsDto)
        {
            news.news_id = newsDto.news_id;
            news.title = newsDto.title;
            news.date = newsDto.date;
            news.news_by = newsDto.news_by;
            news.description = newsDto.description;
            if (!string.IsNullOrWhiteSpace(newsDto.image))
            {
                news.image = newsDto.image;
            }
            news.is_active = newsDto.is_active;
            news.slug = _slugGenerator.generate(newsDto.title);

        }
    }
}
