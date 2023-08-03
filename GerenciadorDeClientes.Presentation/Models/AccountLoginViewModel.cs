using System.ComponentModel.DataAnnotations;

namespace GerenciadorDeClientes.Presentation.Models
{
    public class AccountLoginViewModel
    {
        [Required(ErrorMessage ="Por favor, informe o e-mail de acesso.")]
        [EmailAddress(ErrorMessage ="Por favor, informe um e-mail válido.")]
        public string? Email { get; set; }

        [MinLength(8, ErrorMessage ="Por favor, informe no minimo {1} carateres.")]
        [Required(ErrorMessage ="Por favor, informe sua senha.")]
        public string? Senha { get; set; }

    }
}
