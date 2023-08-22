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
            return @"Data Source=SQL5106.site4now.net;Initial Catalog=db_a9de66_gerenciadorcliente;User Id=db_a9de66_gerenciadorcliente_admin;Password=Isadora03*


            ";
        }
    }
}
