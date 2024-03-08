using Microsoft.AspNetCore.Mvc;
using MyWebFormApp.BLL.DTOs;
using MyWebFormApp.BLL.Interfaces;
using SampleMVC.Models;

namespace SampleMVC.Controllers;

public class CategoriesController : Controller
{
    //private static List<Category> categories = new List<Category>()
    //{
    //    new Category { CategoryID = 1, CategoryName = "Beverages" },
    //    new Category { CategoryID = 2, CategoryName = "Condiments" },
    //    new Category { CategoryID = 3, CategoryName = "Confections" },
    //    new Category { CategoryID = 4, CategoryName = "Dairy Products" },
    //    new Category { CategoryID = 5, CategoryName = "Grains/Cereals" },
    //    new Category { CategoryID = 6, CategoryName = "Meat/Poultry" },
    //    new Category { CategoryID = 7, CategoryName = "Produce" },
    //};
    private readonly ICategoryBLL _categoryBLL;
    public CategoriesController(ICategoryBLL categoryBLL)
    {
        _categoryBLL = categoryBLL;
    }
    public IActionResult Index()
    {
        var models = _categoryBLL.GetAll();
        return View(models);
    }
    public IActionResult getParam(string name)
    {
        // return Content("Hello ASP.NET COre");

        return Content($"hai sayang {name}");
    }
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(CategoryCreateDTO categoryCreate)
    {
        // return Content($"Category ID : {category.CategoryID} Category Name : {category.CategoryName}");
        //categories.Add(category);
        
        _categoryBLL.Insert(categoryCreate);
        return RedirectToAction("Index");
    }
    public IActionResult Edit(int id){
        //var category = categories.FirstOrDefault(c=> c.CategoryID== id);
        //if (category == null){
        //    return NotFound();
        //}
        //return View(category);
        var model = _categoryBLL.GetById(id);
        if (model == null)
        {
            return RedirectToAction("Index");
        }
        return View(model);
    }
    [HttpPost]
    public IActionResult Edit(CategoryUpdateDTO category){
        //var existingCategory = categories.FirstOrDefault(c=> c.CategoryID==category.CategoryID);
        //if (existingCategory == null){
        //    return NotFound();
        //}
        //existingCategory.CategoryName=category.CategoryName;
        _categoryBLL.Update(category);
        return RedirectToAction("Index");
    }
    [HttpPost]
    public IActionResult Search( string search){
        //if (string.IsNullOrEmpty(search)){
        //    return View("Index",categories);
        //}
        //else{
        //    var searchResult = categories.Where(c=> c.CategoryName.Contains(search));
        //    return View("Index",searchResult);
        //}
        var searchResult = _categoryBLL.GetByName(search);

        return View("Index", searchResult);
    }
    public IActionResult Delete(int id){
        //var existingCategory = categories.FirstOrDefault(c=> c.CategoryID==id);
        //if (existingCategory == null){
        //    return NotFound();
        //}
        //categories.Remove(existingCategory);
        _categoryBLL.Delete(id);
        return RedirectToAction("Index");
    }
}
