using Codeizi.Curso.CalculoFolhaDePagamento.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Test.Domain
{
    public static class CenarioContrato
    {
        public static Contrato CrieCenarioConsistente(double salario)
            => new Contrato(Guid.NewGuid(), new Vigencia(), (ValorComponenteCalculo)salario);

        public static Contrato CrieCenarioValido()
            => new Contrato(Guid.NewGuid(), new Vigencia(DateTime.Now), (ValorComponenteCalculo)10000);
    }
}
