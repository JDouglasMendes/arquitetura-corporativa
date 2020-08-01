using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Codeizi.Curso.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {        
        [Required(ErrorMessage = "Nome deve ser informado")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Sobrenome deve ser informado")]
        public string LastName { get; set; }
    }
}