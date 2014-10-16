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
        public ActionResult VerificaCpf(string cpf)
        {
            var csvPath = Server.MapPath("/Content/csv/lista.csv");
            var hasCpf = Utils.Utils.VerificaCpf(cpf, csvPath);

            if (hasCpf)
            {
                ViewBag.Cpf = cpf;

                return PartialView("_FormEmail");
            }

            return Json(false, JsonRequestBehavior.AllowGet);
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
