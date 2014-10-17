using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ByYou.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase arquivo)
        {
            if (arquivo != null && arquivo.ContentLength > 0)
            {
                const string diretorio = @"C:\Users\bruno.marcondes\Documents\Visual Studio 2013\Projects\ByYou\ByYou\Content\csv\";
                var extension = Path.GetExtension(arquivo.FileName);
                
                if (extension != null)
                {
                    var extensaoArquivo = extension.Substring(1);

                    if (extensaoArquivo != "csv")
                    {
                        ModelState.AddModelError("arquivo", "Extensão inválida. Somente arquivos CSV são suportados");
                        return View();
                    }
                }

                if (arquivo.ContentLength > 1048576)
                {
                    ModelState.AddModelError("arquivo", "O arquivo não pode ter mais que 1Mb");
                    return View();
                }

                var filename = Path.GetFileName(arquivo.FileName);
                if (filename != null)
                {
                    arquivo.SaveAs(Path.Combine(diretorio, filename));
                    ViewBag.Sucesso = "Upload realizado com sucesso!";
                }
            }

            return View();
        }
    }
}