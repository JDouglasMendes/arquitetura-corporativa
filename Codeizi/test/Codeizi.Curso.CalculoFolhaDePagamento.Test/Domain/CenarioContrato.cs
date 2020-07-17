using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Calculo;
using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Contratos;
using System;
using System.Collections.Generic;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Test.Domain
{
    public static class CenarioContrato
    {
        public static Contrato CrieCenarioConsistente(double salario)
            => new Contrato(Guid.NewGuid(), Guid.NewGuid(), new Vigencia(), (ValorComponenteCalculo)salario);

        public static Contrato CrieCenarioValido()
            => new Contrato(Guid.NewGuid(), Guid.NewGuid(), new Vigencia(DateTime.Now), (ValorComponenteCalculo)10000);

        public static List<Contrato> CrieCenarioValido(int quantidade)
        {
            var lista = new List<Contrato>(quantidade);
            while (quantidade > 0)
            {
                lista.Add(CrieCenarioConsistente(new Random().NextDouble() * 1000));
                quantidade--;
            }
            return lista;
        }
    }
}