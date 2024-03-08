using MyWebFormApp.BLL.DTOs;

namespace SampleMVC.Model
{
    public class ViewArticle
    {
        public required IEnumerable<ArticleDTO> Articles { get; set;}
        public required IEnumerable<CategoryDTO> Category { get; set;}
    }
}
