using LivrariaVirtual.Enums;
using System.ComponentModel.DataAnnotations;

namespace LivrariaVirtual.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o nome do usuário!")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Digite o login do usuário!")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Digite o email do usuário!")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Digite a senha do usuário!")]
        public string Senha { get; set; }
        [Required(ErrorMessage = "Digite o endereço do usuário!")]
        public string Endereco { get; set; }
        [Required(ErrorMessage = "Digite a cidade do usuário!")]
        public string Cidade { get; set; }
        [Required(ErrorMessage = "Digite o número do usuário!")]
        public string Numero { get; set; }

        public DateTime DataCadastro { get; set; }
        [Required(ErrorMessage = "Escolha o tipo de perfil do usuário!")]
        public PerfilEnum Perfil { get; set; }

        // recebe senha informada pelo usuario e compara com a senha guardada no usuário, se existir
        public bool SenhaValida(string senha)
        {
            return Senha == senha;
        }
    }
}
