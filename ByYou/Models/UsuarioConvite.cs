using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ByYou.Models
{
    public class UsuarioConvite
    {
        [Display(Name = "CPF")]
        public string Cpf { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}