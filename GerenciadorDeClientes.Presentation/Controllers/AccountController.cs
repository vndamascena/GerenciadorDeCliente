using GerenciadorDeClientes.Data.Entities;
using GerenciadorDeClientes.Data.Repositories;
using GerenciadorDeClientes.Presentation.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace GerenciadorDeClientes.Presentation.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AccountLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuarioRepository = new UsuarioRepository();
                    var usuario = usuarioRepository.GetByEmailAndSenha(model.Email, model.Senha);

                    if (usuario != null)
                    {
                        //gravar o cookie no navegador com os dados do
                        //usuário autenticado
                        //este cookie irá gerar a autorização
                        //do usuário para acessar as
                        //páginas restritas do sistema [Authorize]
                        //serializando o objeto usuário para JSON
                        var json = JsonConvert.SerializeObject(usuario);
                        //criando a identificação do usuário no Asp.NET
                        var claimsIdentity = new ClaimsIdentity
                       (new[] { new Claim(ClaimTypes.Name, json) },
                        CookieAuthenticationDefaults
                       .AuthenticationScheme);
                        //gravando os dados no Cookie
                        //de autenticação do Asp.Net
                        var claimsPrincipal = new ClaimsPrincipal
                       (claimsIdentity);
                        HttpContext.SignInAsync
                       (CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);


                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["Mensagem"] = "Acesso negado, usuário inválido.";
                    }

                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = e.Message;
                }
            }

            return View();
        }



        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(AccountRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuarioRepository = new UsuarioRepository();

                    if (usuarioRepository.GetByEmail(model.Email) != null)
                    {
                        TempData["Mensagem"] = "O e-mail informado ja possui cadastro. Favor informar outro e-mail.";

                    }

                    else
                    {
                        var usuario = new Usuario();

                        usuario.Id = Guid.NewGuid();
                        usuario.Nome = model.Nome;
                        usuario.Email = model.Email;
                        usuario.Senha = model.Senha;
                        usuario.DataHoraCriacao = DateTime.Now;


                        usuarioRepository.Add(usuario);

                        TempData["Mensagem"] = "Cadstro realizado com sucesso! Acesse a pagina de login para acessar sua conta.";
                        ModelState.Clear();
                    }


                }

                catch (Exception e)
                {
                    TempData["Mensagem"] = e.Message;

                }
            }

            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(AccountForgotPasswordViewModel model)
        {
            return View();
        }

        public IActionResult Logout()
        {
            //apagar o cookir de autenticação gravado no navegador
            HttpContext.SignOutAsync
           (CookieAuthenticationDefaults.AuthenticationScheme);
            //redirecionar o usuário de volta para a página de login
            return RedirectToAction("Login");
        }
    }

}
