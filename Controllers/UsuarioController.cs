using LivrariaVirtual.Models;
using LivrariaVirtual.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace LivrariaVirtual.Controllers
{
    public class UsuarioController : Controller
    {

        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public IActionResult Index()
        {
            List<UsuarioModel> usuarios = _usuarioRepositorio.BuscarTodos();

            return View(usuarios);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.BuscarPorID(id);
            return View(usuario);
            // quando clicar em apagar confirmacao, vai cair nessa rota, buscar o usuario
            // e retornar os dados na página
        }

        // agora apaga mesmo
        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _usuarioRepositorio.Apagar(id);

                if(apagado)
                {
                    TempData["MensagemSucesso"] = "Contato apagado com sucesso!";
                } else
                {
                    TempData["MensagemErro"] = "Não conseguimos apagar o usuário.";
                }
                return RedirectToAction("Index");

            } catch(Exception erro)
            
            {
                TempData["MensagemErro"] = $"Não conseguimos apagar o usuário. Tente novamente. Detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        // metodo de editar que lista (exibe) a página
        // com isso, consegue ENTRAR na página de edição com os dados do usuário preenchidos
        public IActionResult Editar(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.BuscarPorID(id);
            return View(usuario);
        }

        // metodo de editar post que de fato faz a edição do usuário
        [HttpPost]
        public IActionResult Editar(UsuarioModel usuario)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    usuario = _usuarioRepositorio.Atualizar(usuario);
                    TempData["MensagemSucesso"] = "Usuário alterado com sucesso!";
                    return RedirectToAction("Index");
                }
                return View(usuario);

            } catch(Exception erro)
            {
                TempData["MensagemSucesso"] = $"O usuário não pôde ser alterado pelo seguinte erro: { erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Criar(UsuarioModel usuario)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    usuario = _usuarioRepositorio.Adicionar(usuario);

                    TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(usuario);

            } catch(Exception erro)
            {
                TempData["MensagemErro"] = $"Não conseguimos cadastrar seu usuário. Erro: { erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
