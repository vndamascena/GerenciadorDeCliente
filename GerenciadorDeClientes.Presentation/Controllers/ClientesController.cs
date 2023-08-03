using GerenciadorDeClientes.Data.Entities;
using GerenciadorDeClientes.Data.Repositories;
using GerenciadorDeClientes.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Drawing;
using System.Text.Json.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace GerenciadorDeClientes.Presentation.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {
        public IActionResult Cadastrar()
        {
            ViewBag.Categoria = ObterCategoria();

            return View();

        }

        [HttpPost]
        public IActionResult Cadastrar(ClienteCadastroViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuario = JsonConvert.DeserializeObject
                                    <Usuario>(User.Identity.Name);

                    var cliente = new Cliente
                    {
                        Id = Guid.NewGuid(),
                        Nome = model.Nome,
                        Cpf = model.Cpf,
                        Matricula = model.Matricula,
                        Datan = model.DataN.Value,
                        Idade = model.Idade,
                        Civil = model.Civil,
                        Cep = model.Cep,
                        Endereco = model.Endereco,
                        Estado = model.Estado,
                        Municipio = model.Municipio,
                        Bairro = model.Bairro,
                        Telefone = model.Telefone,
                        Celular = model.Celular,
                        Email = model.Email,
                        Observacao = model.Observacao,
                        Salario = model.Salario,
                        CategoriaId = model.CategoriaId.Value,
                        UsuarioId = usuario.Id
                    };

                    var clienteRepository = new ClienteRepository();
                    clienteRepository.Add(cliente);

                    TempData["MensagemSucesso"] = "Cliente cadastrado com sucesso!";
                    ModelState.Clear();
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }


            }
            else
            {
                TempData["MensagemErro"] = "Ocorreram erro de validação no preenchimento. Favor verificar os campos preenchidos.";
            }



            ViewBag.Categoria = ObterCategoria();

            return View();
        }
        public IActionResult Consultar()
        {
            ViewBag.Categoria = ObterCategoria();

            return View();
        }

        [HttpPost]
        public IActionResult Consultar(ConsultaViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {//capturando o usuário autenticado no sistema
                    var usuario = JsonConvert.DeserializeObject
                    <Usuario>(User.Identity.Name);
                    //consultar as contas do usuário
                    var clienteRepository = new ClienteRepository();
                    var cliente = clienteRepository
                   .GetByCpfAndUsuario(model.Cpf,
                    model.Matricula, usuario.Id);


                    if (cliente.Count > 0)
                    {
                        ViewBag.Cliente = cliente;
                    }
                    else
                    {
                        TempData["Mensagem"] = "Nenhum cliente encontrado com os dados fonercido.";
                    }
                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = e.Message;
                }
            }
            else
            {
                TempData["MensagemAlerta"] = "Ocorreram erros no preenchimento do formulário de consulta," +
                    " por favor verifique.";
            }

            return View();
        }


        public IActionResult ConsultarCliente()
        {
            return View();
        }
        public IActionResult Editar(Guid id)
        {
            var model = new ClienteEdicaoViewModel();
            try
            {
                //buscar a conta no repositório através do ID
                var clienteRepository = new ClienteRepository();
                var cliente = clienteRepository.GetById(id);
                

                //preencher o objeto 'model' com os dados da conta
                model.Id = cliente.Id;
                model.Nome = cliente.Nome;
                model.Cpf= cliente.Cpf;
                model.Matricula = cliente.Matricula;
                model.DataN = cliente.Datan;
                model.Idade = cliente.Idade;
                model.Civil = cliente.Civil;
                model.Cep = cliente.Cep;
                model.Endereco = cliente.Endereco;
                model.Estado = cliente.Estado;
                model.Municipio = cliente.Municipio;
                model.Bairro = cliente.Bairro;
                model.Telefone = cliente.Telefone;
                model.Celular = cliente.Celular;
                model.Email = cliente.Email;
                model.Salario = cliente.Salario;
                model.Observacao = cliente.Observacao;
                model.CategoriaId = cliente.CategoriaId;
                //gerando os dados das categorias
                ViewBag.Categoria = ObterCategoria();
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Editar(ClienteEdicaoViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    

                    var cliente = new Cliente
                    {
                        Id = model.Id,
                        Nome = model.Nome,
                        Cpf = model.Cpf,
                        Matricula = model.Matricula,
                        Datan = model.DataN.Value,
                        Idade = model.Idade,
                        Civil = model.Civil,
                        Cep = model.Cep,
                        Endereco = model.Endereco,
                        Estado = model.Estado,
                        Municipio = model.Municipio,
                        Bairro = model.Bairro,
                        Telefone = model.Telefone,
                        Celular = model.Celular,
                        Email = model.Email,
                        Observacao = model.Observacao,
                        Salario = model.Salario.Value,
                        CategoriaId = model.CategoriaId.Value,
                        
                    };

                    var clienteRepository = new ClienteRepository();
                    clienteRepository.Update(cliente);

                    TempData["MensagemSucesso"] = $"Cliente '{cliente.Nome}', atualizada com sucesso.";
                    return RedirectToAction("Consultar");
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }


            }
            else
            {
                TempData["MensagemErro"] = "Ocorreram erro de validação no preenchimento. Favor verificar os campos preenchidos.";
            }



            ViewBag.Categoria = ObterCategoria();

            return View(model);


        }

        public IActionResult Exclusao(Guid id)
        {
            try
            {
                var clienteRepository = new ClienteRepository();
                var cliente = clienteRepository.GetById(id);

                clienteRepository.Delete(cliente);

                TempData["MensagemSucesso"] = $"Cliente '{cliente.Nome}', excluido com sucesso.";

            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }
            //redirecionando para a página de consulta
            return RedirectToAction("Consultar");
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
    }
}
