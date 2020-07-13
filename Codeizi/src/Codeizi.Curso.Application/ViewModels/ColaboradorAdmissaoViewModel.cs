using System;
using System.ComponentModel.DataAnnotations;

namespace Codeizi.Curso.Application.ViewModels
{
    public class ColaboradorAdmissaoViewModel
    {
        [Required(ErrorMessage = "Informe o nome do colaborador")]
        [StringLength(100, ErrorMessage = "O nome deve conter entre 2 e 100 caracteres", MinimumLength = 2)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o sobrenome do colaborador")]
        [StringLength(100, ErrorMessage = "O sobrenome deve conter entre {0} e 100 caracteres", MinimumLength = 1)]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "Data de admissão obrigatória")]
        [DataType(DataType.Date, ErrorMessage = "Data de admissão em formato inválido")]
        public DateTime DataDeAdmissao { get; set; }
        public double SalarioContratual { get; set; }

        [MaxLength(ErrorMessage = "Observação contratual deve conter no maximo 100 caracteres")]
        public string ObservacaoContratual { get; set; }

        [Required(ErrorMessage = "Data de nascimento obrigatória")]
        [DataType(DataType.Date, ErrorMessage = "Data de nascimento em formato inválido")]
        public DateTime DataNascimento { get; set; }
    }
}
