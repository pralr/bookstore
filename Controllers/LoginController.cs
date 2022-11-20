using LivrariaVirtual.Models;
using LivrariaVirtual.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace LivrariaVirtual.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio; // para se comunicar com o banco

        // injeta dentro do construtor para resolver a questao de injecao de dependencia

        // para listar a página de login
        public LoginController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }
        public IActionResult Index()
        {
            return View();
        }

        // para fazer a requisição de login para receber usuário e senha para autenticar no sistema
        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorLogin(loginModel.Login);

                    // so vai dar o redirect para home se: 

                    if(usuario != null)
                    {
                        if(usuario.SenhaValida(loginModel.Senha))
                        {
                            return RedirectToAction("Index", "Home");
                        }

                        TempData["MensagemErro"] = $"Senha inválida!";

                        return RedirectToAction("Index", "Home");
                        // redirect to action é qual action e qual controller
                    }
                    

                }

                TempData["MensagemErro"] = $"Usuário e/ou senha inválido(s). Por favor, tente novamente.";
                // se nao for valida
                return View("Index"); 
                // vai para index, pois se deixar view, vai procurar uma view com nome entrar, mas nao tenho uma view com nome entrar
                // entao ele vai pra view index da login 
            } catch(Exception erro)
            {
                TempData["MensagemErro"] = $"Não foi possível acessar sua conta, pois: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

    }
}
