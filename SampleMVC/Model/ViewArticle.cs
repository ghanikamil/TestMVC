using MyWebFormApp.BLL.DTOs;

namespace SampleMVC.Model
{
    public class ViewArticle
    {
        public IEnumerable<ArticleDTO> Articles { get; set;}
        public IEnumerable<CategoryDTO> Category { get; set;}
    }
}
