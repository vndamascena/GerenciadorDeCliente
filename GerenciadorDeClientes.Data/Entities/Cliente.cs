using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeClientes.Data.Entities
{
    public class Cliente
    {
        #region Atributos
        private Guid _id;
        private string? _nome;
        private string? _cpf;
        private string? _matricula;
        private DateTime? _datan;
        private string? _idade;
        private string? _pai;
        private string? _mae;
        private string? _civil;
        private string? _cep;
        private string? _endereco;
        private string? _estado;
        private string? _municipio;
        private string? _bairro;
        private string? _telefone;
        private string? _celular;
        private string? _email;
        private string? _observacao;
        private Decimal? _salario;
        private Guid _categoriaId;
        private Guid _usuarioId;
        private Categoria? _categoria;




        #endregion

        #region Propriedades

        public Guid Id { get => _id; set => _id = value; }
        public string? Nome { get => _nome; set => _nome = value; }
        public string? Cpf { get => _cpf; set => _cpf = value; }
        public string? Matricula { get => _matricula; set => _matricula = value; }
        public DateTime? Datan { get => _datan; set => _datan = value; }
        public string? Idade { get => _idade; set => _idade = value; }
        public string? Civil { get => _civil; set => _civil = value; }
        public string? Cep { get => _cep; set => _cep = value; }
        public string? Endereco { get => _endereco; set => _endereco = value; }
        public string? Estado { get => _estado; set => _estado = value; }
        public string? Municipio { get => _municipio; set => _municipio = value; }
        public string? Bairro { get => _bairro; set => _bairro = value; }
        public string? Telefone { get => _telefone; set => _telefone = value; }
        public string? Celular { get => _celular; set => _celular = value; }
        public string? Email { get => _email; set => _email = value; }
        public string? Observacao { get => _observacao; set => _observacao = value; }
        public decimal? Salario { get => _salario; set => _salario = value; }
        public Guid CategoriaId { get => _categoriaId; set => _categoriaId = value; }
        public Guid UsuarioId { get => _usuarioId; set => _usuarioId = value; }
        public string? Pai { get => _pai; set => _pai = value; }
        public string? Mae { get => _mae; set => _mae = value; }
        public Categoria? Categoria { get => _categoria; set => _categoria = value; }
        public int Tipo { get; set; }
        public Decimal Qtd { get; set; }



        #endregion

    }
}