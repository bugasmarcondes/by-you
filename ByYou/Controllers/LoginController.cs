using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ByYou.Models;

namespace ByYou.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Login login)
        {
            if (ModelState.IsValid)
            {
                // ReSharper disable once CSharpWarnings::CS0618
                var isValidLogin = FormsAuthentication.Authenticate(login.Usuario, login.Senha);

                if (isValidLogin)
                {
                    Session["login"] = "true";
                    return RedirectToAction("Index", "Upload");
                }
            }

            ModelState.AddModelError("login", "Login inválido");
            return View(login);
        }
    }
}