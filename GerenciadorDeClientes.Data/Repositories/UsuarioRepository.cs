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
    public class UsuarioRepository
    {
        public void Add(Usuario usuario)
        {
            var adicionar = @"
                INSERT INTO USUARIO(
                    ID,
                    NOME,
                    EMAIL,
                    SENHA,
                    DATAHORACRIACAO)
                VALUES(
                    @Id,
                    @Nome,
                    @Email,
                    @Senha,
                    @Datahoracriacao)
            ";

            using (var connection = new SqlConnection(SqlServerSettings.GetConnectionString())) 
            {
                connection.Execute(adicionar, usuario);
            }
        }

        public void Update(Usuario usuario)
        {
            var atualizar = @"
                UPDATE USUARIO
                SET
                NOME = @Nome,
                EMAIL = @Email,
                SENHA = @Senha

                WHERE
                ID = @Id

            ";

            using (var connection = new SqlConnection(SqlServerSettings.GetConnectionString()))
            {
                connection.Execute(atualizar, usuario);
            }
        }

        public void UpdatePassword(Guid idUsuario, string senha)
        {
            var atualizarSenha = @"
                UPDATE USUARIO
                SET
                    SENHA = CONVERT(VARCHAR(32), HASHBYTES('MD5', @Senha ), 2)
                WHERE
                    ID =@IdUsuario


            ";
            using (var connection = new SqlConnection(SqlServerSettings.GetConnectionString()))
            {
                connection.Execute(atualizarSenha, new { @IdUsuario = idUsuario, @Senha = senha });
            }
        }

        public void Delete(Usuario usuario)
        {
            var deletar = @"
                DELETE FROM USUARIO
                
                WHERE ID = @Id

            ";
            using (var connection = new SqlConnection(SqlServerSettings.GetConnectionString()))
            {
                connection.Execute(deletar, usuario);
            }
        }

        public Usuario? GetByEmail(string email)
        {
            var consultaemail = @"
                SELECT * FROM USUARIO
                
                WHERE EMAIL = @Email
            ";

            using (var connection = new SqlConnection(SqlServerSettings.GetConnectionString()))
            {
                return connection.Query<Usuario>(consultaemail,
                    
                   new {@Email = email}).FirstOrDefault();
            }
        }

        public Usuario? GetByEmailAndSenha(string email, string senha)
        {
            var emailsenha = @"
                SELECT * FROM USUARIO
                
                WHERE EMAIL = @Email AND SENHA = @Senha

            ";

            using(var connection = new SqlConnection(SqlServerSettings.GetConnectionString()))
            {
                return connection.Query<Usuario>(emailsenha,
                    new {@Email = email, @Senha = senha}).FirstOrDefault();
            }
        }



    }

   


}
