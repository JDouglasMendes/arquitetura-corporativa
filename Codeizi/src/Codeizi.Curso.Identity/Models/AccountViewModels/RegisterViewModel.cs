using System.ComponentModel.DataAnnotations;

namespace Codeizi.Curso.Identity.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email deve ser informado")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha deve ser informada")]
        [StringLength(100, ErrorMessage = "A {0} de conter entre {2} e {1} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmação de senha")]
        [Compare("Password", ErrorMessage = "A Senha e a Confirmação de Senha não são iguais.")]
        public string ConfirmPassword { get; set; }

        public ApplicationUser User { get; set; }
    }
}
