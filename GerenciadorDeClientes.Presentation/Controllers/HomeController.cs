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
            
            //capturar o usuário autenticado no arquivo de cookie do Asp.Net
            var usuario = JsonConvert.DeserializeObject<Usuario>(User.Identity.Name);

            

            var clienteRepository = new ClienteRepository();
            var parametros =  clienteRepository.GetByCpfAndUsuario(null,null,usuario.Id);

            var lista = new List<object>();
            lista.Add(new
            {
                Nome = "Aeronautica",
                Qtd = parametros.Where(c => c.Tipo == 1).Sum(c => c.Qtd)
            });
            lista.Add(new
            {
                Nome = "Aposentado INSS",
                Qtd = parametros.Where(c => c.Tipo == 2).Sum(c => c.Qtd)
            });
            lista.Add(new
            {
                Nome = "Total de Contas a Receber",
                Qtd = parametros.Where(c => c.Tipo == 3).Sum(c => c.Qtd)
            });
            lista.Add(new
            {
                Nome = "Total de Contas a Pagar",
                Qtd = parametros.Where(c => c.Tipo == 4).Sum(c => c.Qtd)
            });


            return lista;
        }


        private List<object> ObterTotalCategorias()
        {
            //capturar o usuário autenticado no arquivo de cookie do Asp.Net
            var usuario = JsonConvert.DeserializeObject
           <Usuario>(User.Identity.Name);
            
            //consultando no banco de dados as contas do usuário no período
            var contaRepository = new ClienteRepository();
            var categoria = contaRepository.GetByCpfAndUsuario(null, null, usuario.Id);
            var lista = categoria.GroupBy(cliente => cliente.Categoria.Descricao).Select(grupo => new{Categoria = grupo.Key,
               Total = grupo.Sum(cliente => cliente.Qtd)}).ToList();

            return lista.Cast<object>().ToList();
        }
    } 
}
