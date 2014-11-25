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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FormEmail(string cpf)
        {
            ViewBag.Cpf = cpf;

            return PartialView("_FormEmail");
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

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EnviaConvite(UsuarioConvite usuario)
        {
            if (ModelState.IsValid)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            return PartialView("_FormEmail", usuario);
        }
    }
}
