using Microsoft.AspNetCore.Mvc;

namespace SampleMVC.Controllers;

public class HomeController : Controller
{
    public IActionResult index(){
        // return Content("Hello ASP.NET COre");
        return View(); 
    }
    public IActionResult about(){
        return View();
    }
}
