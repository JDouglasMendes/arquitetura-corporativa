using Codeizi.Curso.Domain.Colaboradores;
using System;

namespace Codeizi.Curso.Domain.Ocorrencias
{
    public class Ferias
    {
        public DateTime DataDeInicio { get; set; }
        public DateTime DataFim { get; set; }
        public Colaborador Colaborador { get; set; }
    }
}
