using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Providers.Entities;
using System.Web.Security;
using ByYou.Models;
using Microsoft.Ajax.Utilities;

namespace ByYou.Utils
{
    public class Utils
    {
        public static UsuariosCsv VerificaCpf(string cpf, string csvPath)
        {
            var usuarioCsv = new UsuariosCsv();

            try
            {
                var csv = File.ReadLines(csvPath)
                          .Skip(1)
                          .Where(l => l != "")
                          .Select(l => l.Split(new[] { ';' }))
                          .Select(u => new UsuariosCsv()
                          {
                              Id = u[0],
                              Nome = u[1],
                              Cpf = u[2],
                              Matricula = u[3]
                          }).ToList();

                if (cpf != null && csvPath != null)
                {
                    var userList = csv.Where(x => x.Cpf == cpf).ToList();

                    if (userList.Count > 0)
                    {
                        usuarioCsv = userList.FirstOrDefault();

                        return usuarioCsv;
                    }
                }
            }
            catch (FileNotFoundException e1)
            {
                throw e1;
            }
            catch (IOException e2)
            {
                throw e2;
            }

            return usuarioCsv;
        }
    }
}