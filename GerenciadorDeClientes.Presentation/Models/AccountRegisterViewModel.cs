using System.ComponentModel.DataAnnotations;

namespace GerenciadorDeClientes.Presentation.Models
{
    public class AccountRegisterViewModel
    {
        [MaxLength(150, ErrorMessage ="Por favor informe no maximo {1} caracteres.")]
        [MinLength(8,ErrorMessage ="Por favor informe no minimo {1} caracteres.")]
        [Required(ErrorMessage ="Por favor informe um nome.")]
        public string? Nome { get; set; }

        [EmailAddress(ErrorMessage ="Por favor informe um e-mail valido.")]
        [Required(ErrorMessage ="Por favor informe o e-mail do usuário.")]
        public string? Email { get; set; }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da - zA - Z]).{8,}$",
            ErrorMessage = "Informe uma senha com letras maíusculas, letras minúsculas, números, símbolos e no mínimo 8 caracteres.")]
        [Required(ErrorMessage = "Por favor, informe a senha do usuário.")]
        public string? Senha { get; set; }

        [Compare("Senha", ErrorMessage ="As senhas não conferem, por favor verifique. ")]
        [Required(ErrorMessage ="Por favor Confirme a senha do usuário.")]
        public string? ConfirmaSenha { get; set; }
    }
}
