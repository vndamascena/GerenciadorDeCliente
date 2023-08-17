using GerenciadorDeClientes.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorDeClientes.Presentation.Models
{
    public class ConsultarClienteViewModel
    {
        public Guid Id { get; set; }

       
        public string? Nome { get; set; }

       
        public string? Cpf { get; set; }

       
        public string? Matricula { get; set; }

       
        public Guid? CategoriaId { get; set; }

        
        public string? Telefone { get; set; }

       
        public Decimal? Salario { get; set; }

       
        public string? Idade { get; set; }

        public string? Observacao { get; set; }

        public DateTime? DataN { get; set; }

        public Decimal? Fator { get; set; } = 0.02589m;

        public Decimal? ValorLiberado { get; set; }
    }

}

