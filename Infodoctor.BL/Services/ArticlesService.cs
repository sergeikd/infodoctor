﻿using Infodoctor.BL.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Infodoctor.BL.DtoModels;
using Infodoctor.DAL;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain.Entities;

namespace Infodoctor.BL.Services
{
    public class ArticlesService : IArticlesService
    {
        private readonly IArticlesRepository _articlesRepository;

        public ArticlesService(IArticlesRepository articlesRepository)
        {
            if (articlesRepository == null)
                throw new ArgumentNullException(nameof(articlesRepository));
            _articlesRepository = articlesRepository;
        }

        public IEnumerable<Article> GetAllArticles()
        {
            return _articlesRepository.GetAllArticles();
        }

        public Article GetArticleById(int id)
        {
            return _articlesRepository.GetArticleById(id);
        }

        public DtoPagedArticles GetPagedArticles(int perPage, int numPage, string pathToImage)
        {
            if (perPage < 1 || numPage < 1)
            {
                throw new ApplicationException("Incorrect request parameter");
            }
            var articles = _articlesRepository.GetAllArticles();
            var pagedList = new PagedList<Article>(articles, perPage, numPage);
            if (!pagedList.Any())
            {
                return null;
            }
            
            var paged = new DtoPagedArticles()
            {
                Articles = pagedList,
                Page = pagedList.Page,
                PageSize = pagedList.PageSize,
                TotalCount = pagedList.TotalCount
            };
            return paged;
        }

        public void Add(Article art)
        {
            if (art == null)
                throw new ArgumentNullException(nameof(art));
            _articlesRepository.Add(art);
        }

        public void Update(int id, Article newArt)
        {
            if (newArt == null)
                throw new ArgumentNullException(nameof(newArt));
            var updated = _articlesRepository.GetArticleById(id);
            if (updated != null)
            {
                updated.Title = newArt.Title;
                updated.Content = newArt.Content;
                updated.PublishDate = newArt.PublishDate;

                _articlesRepository.Update(updated);
            }
        }

        public void Delete(int id)
        {
            var deleted = _articlesRepository.GetArticleById(id);
            _articlesRepository.Delete(deleted);
        }
    }
}
