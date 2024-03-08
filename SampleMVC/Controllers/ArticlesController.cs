using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebFormApp.BLL;
using MyWebFormApp.BLL.DTOs;
using MyWebFormApp.BLL.Interfaces;
using SampleMVC.Model;

namespace SampleMVC.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly IArticleBLL _articleBLL;
        private readonly ICategoryBLL _categoryBLL;


        public ArticlesController(IArticleBLL articleBLL, ICategoryBLL categoryBLL)
        {
            _articleBLL = articleBLL;
            _categoryBLL = categoryBLL;
        }
        // GET: ArticlesController
        public ActionResult Index(int? CategoryID)
        {
            IEnumerable<ArticleDTO> articles;
            if (CategoryID == null || CategoryID==0)
            {
                articles = _articleBLL.GetArticleWithCategory();
            }
            else
            {
                articles = _articleBLL.GetArticleByCategory((int)CategoryID);
            }
            
            var categories = _categoryBLL.GetAll();
            var models = new ViewArticle { Articles = articles, Category = categories };
            return View(models);
        }
        
        public IActionResult SearchCategory(int id)
        {
            return RedirectToAction("Index", new {CategoryID = id} );
        }

        // GET: ArticlesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ArticlesController/Create
        public ActionResult Create()
        {
            var articles = _articleBLL.GetArticleWithCategory();
            var categories = _categoryBLL.GetAll();
            var models = new ViewArticle { Articles = articles, Category = categories };
            return View(models);
        }

        // POST: ArticlesController/Create
        [HttpPost]
        public IActionResult Create(ArticleCreateDTO articleCreateDTO)
        {
            try
            {
                _articleBLL.Insert(articleCreateDTO);
            }
            catch (Exception ex)
            {
                TempData["message"] = $"<div class='alert alert-danger'><strong>Error!</strong>{ex.Message}</div>";
            }
            return RedirectToAction("Index");
        }

        // GET: ArticlesController/Edit/5
        public ActionResult Edit(int id)
        {
            //var articles = _articleBLL.GetArticleByCategory(id);
            //var categories = _categoryBLL.GetAll();
            //var models = new ViewArticle { Articles = articles, Category = categories };
            var models = _articleBLL.GetArticleById(id);
            return View(models);
        }

        // POST: ArticlesController/Edit/5
        [HttpPost]
        public ActionResult Edit(ArticleUpdateDTO articleUpdateDTO)
        {
            try
            {
                _articleBLL.Update(articleUpdateDTO);
            }
            catch (Exception ex)
            {
                TempData["message"] = $"<div class='alert alert-danger'><strong>Error!</strong>{ex.Message}</div>";
            }
            return RedirectToAction("Index");
        }

        // GET: ArticlesController/Delete/5
        public IActionResult Delete(int id)
        {
            _articleBLL.Delete(id);
            return RedirectToAction("Index");
        }

        // POST: ArticlesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public IActionResult DisplayDropdownList()
        {
            var models = _articleBLL.GetArticleWithCategory();
            return View(models);
        }

        [HttpPost]
        public IActionResult DisplayDropdownList(string CategoryID)
        {
            //ViewBag.Message = $"You selected {CategoryID}";

            //ViewBag.Categories = _categoryBLL.GetAll();

            var models = _articleBLL.GetArticleWithCategory();
            return View(models);
        }
    }
}
