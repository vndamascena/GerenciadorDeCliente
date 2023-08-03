using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeClientes.Data.Settings
{
    public class SqlServerSettings
    {
        public static string GetConnectionString()
        {
            return @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BDGerenciadorDeClientes;
                    Integrated Security=True;Connect Timeout=30;Encrypt=False

            ";
        }
    }
}
