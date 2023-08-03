using System.ComponentModel.DataAnnotations;

namespace GerenciadorDeClientes.Presentation.Models
{
    public class AccountForgotPasswordViewModel
    {
        [EmailAddress(ErrorMessage = "Por favor, informe um e-mail valido.")]
        [Required(ErrorMessage = "Por favor, informe o e-mail de acesso.")]
        public string Email { get; set; }

    }
}
