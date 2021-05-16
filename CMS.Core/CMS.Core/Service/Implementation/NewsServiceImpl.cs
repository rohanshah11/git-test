using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Exceptions;
using CMS.Core.Makers.Interface;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Transactions;

namespace CMS.Core.Service.Implementation
{
    public class NewsServiceImpl : NewsService
    {
        private readonly NewsRepository _newsRepository;
        private readonly NewsMaker _newsMaker;
        private readonly IHostingEnvironment _hostingEnvironment;

        public NewsServiceImpl(NewsRepository newsRepository, NewsMaker newsMaker, IHostingEnvironment hostingEnvironment)
        {
            _newsRepository = newsRepository;
            _newsMaker = newsMaker;
            _hostingEnvironment = hostingEnvironment;
        }

        public void delete(long news_id)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    var news = _newsRepository.getById(news_id);
                    if (news == null)
                    {
                        throw new ItemNotFoundException($" {news_id}  not found");

                    }
                    _newsRepository.delete(news);
                    tx.Complete();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void disable(long news_id)
        {
            try
            {
                var news = _newsRepository.getById(news_id);
                if (news == null)
                    throw new ItemNotFoundException($"News with id {news_id} doesnot exist.");

                news.is_active = false;
                _newsRepository.update(news);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void enable(long news_id)
        {
            try
            {
                var news = _newsRepository.getById(news_id);
                if (news == null)
                    throw new ItemNotFoundException($"News with id {news_id} doesnot exist.");

                news.is_active = true;
                _newsRepository.update(news);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void save(NewsDto newsDto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    News news = new News();
                    _newsMaker.copy(news, newsDto);
                    _newsRepository.insert(news);
                    tx.Complete();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void update(NewsDto newsDto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    News news = _newsRepository.getById(newsDto.news_id);
                    if (news == null)
                    {
                        throw new ItemNotFoundException($"News with ID {newsDto.news_id} doesnot Exit.");
                    }
                    string oldImages = news.image;

                    _newsMaker.copy(news, newsDto);
                    _newsRepository.update(news);

                    if (!string.IsNullOrWhiteSpace(oldImages))
                    {
                        deleteImage(oldImages);
                    }
                    tx.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void deleteImage(string image_name)
        {
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images/custom");
            if (File.Exists(Path.Combine(filePath, image_name)))
            {
                File.Delete(Path.Combine(filePath, image_name));
            }
        }
    }
}

