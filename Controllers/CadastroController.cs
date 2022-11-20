using Microsoft.AspNetCore.Mvc;

namespace LivrariaVirtual.Controllers
{
    public class CadastroController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
