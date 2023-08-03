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
    public class CategoriaRepository
    {
        public List<Categoria> GetAll()
        {
            var query = @"
                SELECT * FROM CATEGORIA
                ORDER BY DESCRICAO
            ";

            using (var connection = new SqlConnection(SqlServerSettings.GetConnectionString()))
            {
                return connection.Query<Categoria>(query).ToList();
            }
        }
    }
}
