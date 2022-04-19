using Microsoft.AspNetCore.Mvc;
using Biblioteca.Models;

namespace Biblioteca.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Listagem()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);

            return View(new UsuarioService().Listar());
        }

        public IActionResult RegistrarUsuarios()
        {
           Autenticacao.CheckLogin(this);
           Autenticacao.verificaSeUsuarioEAdmin(this);

            return View();
        }

        [HttpPost]

        public IActionResult RegistrarUsuarios(Usuario u)
        {
            u.Senha = Criptografo.TextoCriptografado(u.Senha);
            new UsuarioService().Inserir(u);
            return RedirectToAction("cadastroRealizado");



        }

        public IActionResult EditarUsuario(int id)
        {
           Autenticacao.CheckLogin(this);
           Autenticacao.verificaSeUsuarioEAdmin(this);
            Usuario u = new UsuarioService().Listar(id);
            return View(u);
        }

        [HttpPost]

        public IActionResult EditarUsuario(Usuario u)
        {
            u.Senha = Criptografo.TextoCriptografado(u.Senha);
            new UsuarioService().Editar(u);
            return RedirectToAction("Listagem");



        }
        public IActionResult Sair()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult NeedAdmin()
        {
            Autenticacao.CheckLogin(this);
            return View();
        }


         public IActionResult cadastroRealizado()
        {
            
            return View();
        }



          public IActionResult ExcluirUsuario(int id)

        {
           Autenticacao.CheckLogin(this);
           Autenticacao.verificaSeUsuarioEAdmin(this);

            return View(new UsuarioService().Listar(id));
        }

        [HttpPost]

        public IActionResult  ExcluirUsuario(string decisao, int id)
        {
            if(decisao == "EXCLUIR")
            {

                new UsuarioService().Excluir(id);
           }
                return View("Listagem", new UsuarioService().Listar());
         
           



        }
    
    }

}