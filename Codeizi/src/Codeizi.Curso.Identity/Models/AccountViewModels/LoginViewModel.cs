using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Codeizi.Curso.Identity.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email deve ser informado")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage ="Senha deve ser informada")]
        [DataType(DataType.Password)]
        [Display(Name="Senha")]
        public string Password { get; set; }

        [Display(Name = "Manter conectado?")]
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}