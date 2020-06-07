using Microsoft.AspNetCore.Mvc;

namespace BigBurguer.Api.Controllers
{
    public class ProductIngredientsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}