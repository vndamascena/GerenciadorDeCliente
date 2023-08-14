using GerenciadorDeClientes.Data.Entities;
using GerenciadorDeClientes.Data.Repositories;
using GerenciadorDeClientes.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace GerenciadorDeClientes.Presentation.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        
        public IActionResult AlterarSenha()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AlterarSenha(UsuarioAlterarSenhaViewModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var usuario = JsonConvert.DeserializeObject<Usuario>(User.Identity.Name);

                    var usuarioRepository = new UsuarioRepository();

                    if(usuarioRepository.GetByEmailAndSenha(usuario.Email, model.SenhaAtual) != null)
                    {
                        usuarioRepository.UpdatePassword(usuario.Id, model.NovaSenha);
                        TempData["MensagemSucesso"] = "Senha atualizada com sucesso.";

                    }
                    else
                    {
                        TempData["MensagemAlerta"] = "Senha atual invalida, por favor informe a senha correta.";
                    }
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;

                }
                

            }
            else
            {
                TempData["MensagemAlerta"] = "Preenchimento incorreto, por favor verifque";
            }

            return View();
        }
    }
}
