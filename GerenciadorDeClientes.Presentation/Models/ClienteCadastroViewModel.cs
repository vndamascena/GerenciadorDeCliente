using System.ComponentModel.DataAnnotations;

namespace GerenciadorDeClientes.Presentation.Models
{
    public class ClienteCadastroViewModel
    {
        [Required(ErrorMessage ="Por favor, informe o nome completo")]
        [MaxLength(100,ErrorMessage ="Por favor, informe no maximo {1} caracteres")]
        [MinLength(8,ErrorMessage ="Por favor informe no minimo {1} caracteres")]
        public string? Nome { get; set; }

        [Required(ErrorMessage ="Por favor, informe o CPF")]
        [MaxLength(15,ErrorMessage ="Por favor, digite no maximo {1} caracteres")]
        [MinLength(11,ErrorMessage ="Prencha com no minimo {1} caracteres")]
        public string? Cpf { get; set; }

        [Required(ErrorMessage = "Por favor, informe o matricula")]
        [MaxLength(10, ErrorMessage = "Por favor, digite no maximo {1} caracteres")]
        [MinLength(6, ErrorMessage = "Prencha com no minimo {1} caracteres")]
        public string? Matricula { get; set; }

        [Required(ErrorMessage ="Por favor, informe a data de nascimento")]
        public DateTime? DataN { get; set; }

        [Required(ErrorMessage ="Informe uma categoria")]
        public Guid? CategoriaId { get; set; }

        [Required(ErrorMessage ="Por favor, informe o telefone")]
        [MaxLength(11,ErrorMessage ="Informe no maximo {1} caracteres")]
        [MinLength(10,ErrorMessage ="Informe no minimo {1} caracteres")]
        public string? Telefone {get; set; }

        [Required(ErrorMessage ="Infrome o salario")]
        public Decimal? Salario { get; set;}

        public string? Civil {get; set;}
        public string? Cep { get; set;}
        public string? Endereco { get; set; }
        public string? Estado { get; set; }
        public string? Municipio { get; set; }
        public string? Bairro { get; set; }
        public string? Observacao { get; set; }
        public string? Pai { get; set; }
        public string? Mae { get; set; }
        public string? Celular { get; set; }
        public string? Email { get; set; }
        public string? Idade {get; set;}
    }
}
