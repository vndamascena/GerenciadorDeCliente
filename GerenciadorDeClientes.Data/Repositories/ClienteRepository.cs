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
            
            var query = @"
                INSERT INTO CLIENTE(ID, NOME, CPF, MATRICULA, DATAN, IDADE, PAI, MAE, CIVIL,
                    CEP, ENDERECO, ESTADO, MUNICIPIO, BAIRRO, TELEFONE, CELULAR, EMAIL,
                    SALARIO, CATEGORIAID, USUARIOID, OBSERVACAO)
                VALUES(@Id, @Nome, @Cpf, @Matricula, @DataN, @Idade, @Pai, @Mae, @Civil, @Cep, @Endereco,
                        @Estado, @Municipio, @Bairro, @Telefone, @Celular, @Email,
                        @Salario, @CategoriaId, @UsuarioId, @Observacao)
                
            ";
            using (var connection = new SqlConnection(SqlServerSettings.GetConnectionString()))
            {
                connection.Execute(query, cliente);
                
            }
        }
        public void Update(Cliente cliente)
        {
            var query = @"
                UPDATE CLIENTE
                SET
                    NOME = @Nome,
                    MATRICULA = @Matricula,
                    DATAN = @DataN,
                    IDADE = @Idade,
                    PAI = @Pai,
                    MAE = @Mae,
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
                connection.Execute(query, cliente);

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
                SELECT * FROM CLIENTE cl
                INNER JOIN CATEGORIA ca
                ON cl.CATEGORIAID = ca.ID
                WHERE cl.USUARIOID =  @UsuarioId
                
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
                return connection.Query(query,
                (Cliente cl, Categoria ca) =>{cl.Categoria = ca;
                    return cl;},
                        new{@UsuarioId = usuarioId, },
                        splitOn: "CategoriaId").ToList();
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
