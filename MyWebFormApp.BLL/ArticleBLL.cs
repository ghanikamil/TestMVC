using MyWebFormApp.BLL.DTOs;
using MyWebFormApp.BLL.Interfaces;
using MyWebFormApp.DAL.Interfaces;
using MyWebFormApp.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using MyWebFormApp.BO;

namespace MyWebFormApp.BLL
{
    public class ArticleBLL : IArticleBLL
    {
        private readonly IArticleDAL _articleDAL;
        public ArticleBLL()
        {
            _articleDAL = new ArticleDAL();
        }

        public void Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ArticleID is required");
            }

            try
            {
                _articleDAL.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<ArticleDTO> GetArticleByCategory(int categoryId)
        {
            List<ArticleDTO> articles = new List<ArticleDTO>();
            var articlesFromDAL = _articleDAL.GetArticleByCategory(categoryId);
            foreach (var article in articlesFromDAL)
            {
                articles.Add(new ArticleDTO
                {
                    ArticleID = article.ArticleID,
                    CategoryID = article.CategoryID,
                    Title = article.Title,
                    Details = article.Details,
                    PublishDate = article.PublishDate,
                    IsApproved = article.IsApproved,
                    Pic = article.Pic,
                    Category = new CategoryDTO
                    {
                        CategoryID = article.Category.CategoryID,
                        CategoryName = article.Category.CategoryName
                    }
                });
            }
            return articles;
        }

        public IEnumerable<ArticleDTO> GetArticleWithCategory()
        {
            List<ArticleDTO> articles = new List<ArticleDTO>();
            var articlesFromDAL = _articleDAL.GetArticlesWithCategory();
            foreach (var article in articlesFromDAL)
            {
                articles.Add(new ArticleDTO
                {
                    ArticleID = article.ArticleID,
                    CategoryID = article.CategoryID,
                    Title = article.Title,
                    Details = article.Details,
                    PublishDate = article.PublishDate,
                    IsApproved = article.IsApproved,
                    Pic = article.Pic,
                    Category = new CategoryDTO
                    {
                        CategoryID = article.Category.CategoryID,
                        CategoryName = article.Category.CategoryName
                    }
                });
            }
            return articles;
        }

        public void Insert(ArticleCreateDTO entity)
        {
            if (string.IsNullOrEmpty(entity.Title))
            {
                throw new ArgumentException("Title is required");
            }
            if (string.IsNullOrEmpty(entity.Details))
            {
                throw new ArgumentException("Details is required");
            }

            try
            {
                var article = new Article
                {
                    CategoryID = entity.CategoryID,
                    Title = entity.Title,
                    Details = entity.Details,
                    IsApproved = entity.IsApproved,
                    Pic = entity.Pic
                };
                _articleDAL.Insert(article);
            }
            catch (System.Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public void Update(ArticleUpdateDTO article)
        {
            if (article.ArticleID <= 0)
            {
                throw new ArgumentException("ArticleID is required");
            }
            if (string.IsNullOrEmpty(article.Title))
            {
                throw new ArgumentException("Title is required");
            }
            if (string.IsNullOrEmpty(article.Details))
            {
                throw new ArgumentException("Details is required");
            }

            try
            {
                var updateArticle = new Article
                {
                    ArticleID = article.ArticleID,
                    CategoryID = article.CategoryID,
                    Title = article.Title,
                    Details = article.Details,
                    IsApproved = article.IsApproved,
                    Pic = article.Pic
                };

                _articleDAL.Update(updateArticle);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
