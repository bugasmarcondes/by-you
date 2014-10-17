using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using ByYou.Models;

namespace ByYou.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult VerificaCpf(UsuarioCpf usuario)
        {
            try
            {
                var csvPath = Server.MapPath("/Content/csv/lista.csv");
                var hasCpf = Utils.Utils.VerificaCpf(usuario.Cpf, csvPath);

                if (hasCpf)
                {
                    ViewBag.Cpf = usuario.Cpf;

                    return PartialView("_FormEmail");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error", ex.Message);
                return PartialView("_FormCpf");
            }

            return Content("<p><strong>E-mail não encontrado.</strong></p><p>Por favor, entre em contato pelo: e-mail@estacio.com.br</p>");
        }

        [HttpPost]
        public ActionResult EnviaConvite(UsuarioConvite usuario)
        {
            if (ModelState.IsValid)
            {
                return Json("chamada ao servico", JsonRequestBehavior.AllowGet);
            }

            return PartialView("_FormEmail", usuario);
        }
    }
}
