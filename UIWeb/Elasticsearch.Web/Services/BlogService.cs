﻿using Elasticsearch.Web.Models;
using Elasticsearch.Web.Repositories;
using Elasticsearch.WEB.ViewModels;

namespace Elasticsearch.Web.Services
{
    public class BlogService
    {     
        private readonly BlogRepository _repository;
        public BlogService(BlogRepository repository)
        {
            _repository = repository;
        }
        public async Task<bool> SaveAsync(BlogCreateViewModel model)
        {
            var newBlog = new Blog
            {
                Title = model.Title,
                UserId = Guid.NewGuid(),
                Content = model.Content,
                Tags = model.Tags.Split(",")
            };
            var isCreatedBlog = await _repository.SaveAsync(newBlog);
            return isCreatedBlog != null;
        }
        public async Task<List<BlogViewModel>> SearchAsync(string searchText)
        {
            var blogList = await _repository.SearchAsync(searchText);
            return blogList.Select(b => new BlogViewModel()
            {
                Id = b.Id,
                Title = b.Title,
                Content = b.Content,
                Created = b.CreatedDate.ToShortDateString(),
                Tags = String.Join(",", b.Tags),
                UserId = b.UserId.ToString()

            }).ToList();

        }

    }
}
