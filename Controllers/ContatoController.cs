using Microsoft.AspNetCore.Mvc;

namespace LivrariaVirtual.Controllers
{
    public class ContatoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
