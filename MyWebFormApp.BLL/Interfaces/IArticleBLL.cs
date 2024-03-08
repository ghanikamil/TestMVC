using MyWebFormApp.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebFormApp.BLL.Interfaces
{
    public interface IArticleBLL
    {
        void Insert(ArticleCreateDTO entity);
        IEnumerable<ArticleDTO> GetArticleWithCategory();
        IEnumerable<ArticleDTO> GetArticleByCategory(int categoryId);
        ArticleDTO GetArticleById(int id);
        void Update(ArticleUpdateDTO article);
        void Delete(int id);
    }
}
