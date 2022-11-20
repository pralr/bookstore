using LivrariaVirtual.Data;
using LivrariaVirtual.Models;

namespace LivrariaVirtual.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {

        private readonly BancoContext _context;

        public UsuarioRepositorio(BancoContext bancoContext)
        {
            _context = bancoContext;
        }

        public UsuarioModel Adicionar(UsuarioModel usuario)
        {
            usuario.DataCadastro = DateTime.Now;
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return usuario;
        }

        public bool Apagar(int id)
        {
            UsuarioModel usuarioDB = BuscarPorID(id);

            if (usuarioDB == null) throw new Exception("Houve um erro na deleção do usuário!");

            _context.Usuarios.Remove(usuarioDB);
            _context.SaveChanges();

            return true;
        }

        public UsuarioModel Atualizar(UsuarioModel usuario)
        {
            UsuarioModel usuarioDB = BuscarPorID(usuario.Id);

            if (usuarioDB == null) throw new Exception("Houve um erro na atualização do contato!");

            usuarioDB.Nome = usuario.Nome;
            usuarioDB.Email = usuario.Email;
            usuarioDB.Login = usuario.Login;
            usuarioDB.Endereco = usuario.Endereco;
            usuarioDB.Senha = usuario.Senha;
            usuarioDB.Cidade = usuario.Cidade;
            usuarioDB.Numero = usuario.Numero;
            usuarioDB.Perfil = usuario.Perfil;

            _context.Usuarios.Update(usuarioDB);
            _context.SaveChanges();

            return usuarioDB;
        }

        public UsuarioModel BuscarPorID(int id)
        {
            return _context.Usuarios.FirstOrDefault(x => x.Id == id);
        }

        public UsuarioModel BuscarPorLogin(string login)
        {
            // retornar o primeiro ou único registro que existe dentro do banco onde o login for igual ao login
            // tire o case sensitive
            return _context.Usuarios.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());
        }

        public List<UsuarioModel> BuscarTodos()
        {
            return _context.Usuarios.ToList();
        }
    }
}
