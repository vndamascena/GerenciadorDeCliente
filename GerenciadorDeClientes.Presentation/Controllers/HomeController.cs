using GerenciadorDeClientes.Data.Entities;
using GerenciadorDeClientes.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Linq;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GerenciadorDeClientes.Presentation.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Categoria = ObterCategoria();
            ViewBag.Parametros = ObterParametro();
            ViewBag.TotalCategoria = ObterTotalCategorias();

            return View();
        }

        private List<SelectListItem> ObterCategoria()
        {
            var lista = new List<SelectListItem>();

            var categoriaRepository = new CategoriaRepository();
            var categoria = categoriaRepository.GetAll();

            foreach (var item in categoria)
            {
                lista.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Descricao });
            }
            return lista;
        }

        //método para gerar os dados do gráfico

        private List<object> ObterParametro()
        {
            var usuario = JsonConvert.DeserializeObject<Usuario>(User.Identity.Name);

            var clienteRepository = new ClienteRepository();
            var parametros = clienteRepository.GetByCpfAndUsuario(null, null, usuario.Id);

            var lista = ((List<SelectListItem>)ViewBag.Categoria).Select(cat => new
            {
                Nome = cat.Text,
                Qtd = clienteRepository.GetByCategoria(Guid.Parse(cat.Value), usuario.Id).Count(),
            }).ToList();

            return lista.Cast<object>().ToList();
        }







        private List<object> ObterTotalCategorias()
        {
            var usuario = JsonConvert.DeserializeObject<Usuario>(User.Identity.Name);

            var clienteRepository = new ClienteRepository();
            var clientes = clienteRepository.GetByCpfAndUsuario(null, null, usuario.Id);

            var lista = clientes.GroupBy(cliente => cliente.Categoria.Descricao)
                                .Select(grupo => new
                                {
                                    Categoria = grupo.Key,
                                    TotalClientes = grupo.Count()  // Conta o número de clientes em cada categoria
                                })
                                .ToList();

            return lista.Cast<object>().ToList();
        }



    }
}
