using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Web;
using ByYou.Models;
using Microsoft.Ajax.Utilities;

namespace ByYou.Utils
{
    public class Utils
    {
        public static bool VerificaCpf(string cpf, string csvPath)
        {
            var csv = File.ReadLines(csvPath)
                          .Skip(1)
                          .Where(l => l != "")
                          .Select(l => l.Split(new[] { ';' }))
                          .Select(u => new UsuariosCsv()
                          {
                              Id = u[0],
                              Nome = u[1],
                              Cpf = u[2]
                          }).ToList();

            if (cpf != null && csvPath != null)
            {
                var userList = csv.Where(x => x.Cpf == cpf).ToList();

                if (userList.Count > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}