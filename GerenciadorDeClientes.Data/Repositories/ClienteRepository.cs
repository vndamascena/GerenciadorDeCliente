using Dapper;
using GerenciadorDeClientes.Data.Entities;
using GerenciadorDeClientes.Data.Settings;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeClientes.Data.Repositories
{
    public class ClienteRepository
    {
        public void Add(Cliente cliente)
        {
            string dataFormatoBanco = cliente.Datan?.ToString("MM'/'dd'/'yyyy");

            var query = @"
                INSERT INTO CLIENTE(ID, NOME, CPF, MATRICULA, DATA, IDADE, CIVIL,
                    CEP, ENDERECO, ESTADO, MUNICIPIO, BAIRRO, TELEFONE, CELULAR, EMAIL,
                    SALARIO, CATEGORIAID, USUARIOID, OBSERVACAO)
                VALUES(@Id, @Nome, @Cpf, @Matricula, @Data, @Idade, @Civil, @Cep, @Endereco,
                        @Estado, @Municipio, @Bairro, @Telefone, @Celular, @Email,
                        @Salario, @CategoriaId, @UsuarioId, @Observacao)
                
            ";
            using (var connection = new SqlConnection(SqlServerSettings.GetConnectionString()))
            {
                connection.Execute(query, new
                {
                    cliente.Id,
                    cliente.Nome,
                    cliente.Cpf,
                    cliente.Matricula,
                    Data = dataFormatoBanco, // Utilizar o nome 'Data' aqui, como na consulta
                    cliente.Idade,
                    cliente.Civil,
                    cliente.Cep,
                    cliente.Endereco,
                    cliente.Estado,
                    cliente.Municipio,
                    cliente.Bairro,
                    cliente.Telefone,
                    cliente.Celular,
                    cliente.Email,
                    cliente.Observacao,
                    cliente.Salario,
                    cliente.CategoriaId,
                    cliente.UsuarioId
                });



            }
        }
        public void Update(Cliente cliente)
        {
            string dataFormatoBanco = cliente.Datan?.ToString("MM'/'dd'/'yyyy");

            var query = @"
                UPDATE CLIENTE
                SET
                    NOME = @Nome,
                    MATRICULA = @Matricula,
                    DATA = @Data,
                    IDADE = @Idade,
                    CEP = @Cep,
                    ENDERECO = @Endereco,
                    ESTADO = @Estado,
                    MUNICIPIO = @Municipio,
                    BAIRRO = @Bairro,
                    TELEFONE = @Telefone,
                    CELULAR = @Celular,
                    EMAIL = @Email,
                    SALARIO = @Salario,
                    CATEGORIAID =@CategoriaId
                WHERE
                    ID = @Id

            ";
            using (var connection = new SqlConnection(SqlServerSettings.GetConnectionString()))
            {
                connection.Execute(query, new
                {
                    cliente.Id,
                    cliente.Nome,
                    cliente.Cpf,
                    cliente.Matricula,
                    Data = dataFormatoBanco, // Utilizar o nome 'Data' aqui, como na consulta
                    cliente.Idade,
                    cliente.Civil,
                    cliente.Cep,
                    cliente.Endereco,
                    cliente.Estado,
                    cliente.Municipio,
                    cliente.Bairro,
                    cliente.Telefone,
                    cliente.Celular,
                    cliente.Email,
                    cliente.Observacao,
                    cliente.Salario,
                    cliente.CategoriaId,
                    cliente.UsuarioId
                });
            }
        }

        public void Delete(Cliente cliente)
        {
            var query = @"
                DELETE  FROM CLIENTE
                WHERE ID = @Id

            ";
            using (var connection = new SqlConnection(SqlServerSettings.GetConnectionString()))
            {
                connection.Execute(query, cliente);
            }
        }

        public List<Cliente> GetByCpfAndUsuario(string cpf, string matricula, Guid usuarioId) 
        {
            var query = @"
                SELECT * FROM CLIENTE
                WHERE USUARIOID = @UsuarioId
            ";

            if (!string.IsNullOrEmpty(cpf))
            {
                query += $" AND CPF = '{cpf}'";
            }

            if (!string.IsNullOrEmpty(matricula))
            {
                query += $" AND MATRICULA = '{matricula}'";
            }

            using (var connection = new SqlConnection(SqlServerSettings.GetConnectionString()))
            {
                return connection.Query<Cliente>(query, new {@UsuarioId = usuarioId}).ToList();
            }

        }
  
        public Cliente? GetById(Guid id)
        {
            var query = @"
                SELECT * FROM CLIENTE 
                WHERE ID = @Id

            ";
            using (var connection = new SqlConnection
                        (SqlServerSettings.GetConnectionString()))
            {
                return connection.Query<Cliente>
               (query, new { @Id = id }).FirstOrDefault();
            }

        }
    }
}
