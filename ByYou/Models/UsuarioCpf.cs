using System.ComponentModel.DataAnnotations;

namespace ByYou.Models
{
    public class UsuarioCpf
    {
        [Required]
        [Display(Name = "CPF")]
        public string Cpf { get; set; }
    }
}