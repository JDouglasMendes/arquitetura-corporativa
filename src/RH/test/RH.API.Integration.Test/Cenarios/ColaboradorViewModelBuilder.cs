﻿using RH.Application.ViewModels;
using System;

namespace Api.Integration.Test.Cenarios
{
    public sealed class ColaboradorViewModelBuilder
    {
        public static ColaboradorAdmissaoViewModel CrieAdmissaoSucesso()
        {
            return new ColaboradorAdmissaoViewModel
            {
                DataDeAdmissao = DateTime.Now,
                Nome = "Codeizi",
                Sobrenome = "Treinamentos",
                SalarioContratual = 1000,
                ObservacaoContratual = "Teste de integração",
                DataNascimento = DateTime.Now.AddYears(-18),
            };
        }
    }
}