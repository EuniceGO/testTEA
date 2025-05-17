using Microsoft.AspNetCore.Mvc;

namespace testTEA.Controllers
{
    public class PadreController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
