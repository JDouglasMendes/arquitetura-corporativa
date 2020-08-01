using Codeizi.Curso.RH.Domain.Colaboradores;
using Codeizi.Curso.RH.Domain.Ocorrencias.Ferias;
using Codeizi.Curso.RH.Domain.Ocorrencias.Ferias.Commands;
using Codeizi.Curso.RH.Domain.Ocorrencias.Ferias.Contracts;
using Codeizi.Curso.RH.Domain.Ocorrencias.Ferias.Validations;
using FluentValidation.TestHelper;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace Codeizi.Curso.Domain.Test.Ocorrencias.Ferias.Validations
{
    [Trait("Categoria", "Domain")]
    public class RegistrarOcorrenciaDeFeriasCommandValidationTest
    {
        [Fact]
        public void Id_contrato_vazio()
            => ExecuteCenarioTesteBasicoComFalha(x => x.IdContrato, x => x.IdContrato = Guid.Empty);

        [Fact]
        public void Id_contrato_informado()
            => ExecuteCenarioTesteBasicoComSucesso(x => x.IdContrato);

        [Fact]
        public void Periodo_aquisitivo_vazio()
            => ExecuteCenarioTesteBasicoComFalha(x => x.PeriodoAquisitivo, x => x.PeriodoAquisitivo = default);

        [Fact]
        public void Periodo_aquisitivo_informado()
            => ExecuteCenarioTesteBasicoComSucesso(x => x.PeriodoAquisitivo);

        [Fact]
        public void Data_de_inicio_vazia()
            => ExecuteCenarioTesteBasicoComFalha(x => x.DataDeInicio, x => x.DataDeInicio = default);

        [Fact]
        public void Data_de_inicio_informado()
            => ExecuteCenarioTesteBasicoComSucesso(x => x.DataDeInicio);

        [Fact]
        public void Dias_de_ferias_vazio()
            => ExecuteCenarioTesteBasicoComFalha(x => x.DiasDeFerias, x => x.DiasDeFerias = default);

        [Fact]
        public void Dias_de_ferias_informado()
            => ExecuteCenarioTesteBasicoComSucesso(x => x.DiasDeFerias);

        [Fact]
        public void Dias_de_ferias_acima_do_permitido()
            => ExecuteCenarioTesteBasicoComFalha(x => x.DiasDeFerias, x => x.DiasDeFerias = OcorrenciaDeFerias.QuantidadeMaximaDeDiasDeFerias + 1);

        [Fact]
        public void Dias_de_ferias_acima_do_permitido_com_abono()
            => ExecuteCenarioTesteBasicoComFalha(x => x.DiasDeFerias, x =>
            {
                x.DiasDeFerias = OcorrenciaDeFerias.QuantidadeMaximaDeDiasDeFerias;
                x.DiasDeAbono = OcorrenciaDeFerias.QuantidadeMaximaDeDiasDeAbono;
            });

        [Fact]
        public void Dias_de_ferias_dentro_do_permitido()
        {
            ExecuteCenarioTesteBasicoComSucesso(x => x.DiasDeFerias, x =>
            {
                x.DiasDeFerias = OcorrenciaDeFerias.QuantidadeMaximaDeDiasDeFerias - OcorrenciaDeFerias.QuantidadeMaximaDeDiasDeAbono;
                x.DiasDeAbono = OcorrenciaDeFerias.QuantidadeMaximaDeDiasDeAbono;
            });

            ExecuteCenarioTesteBasicoComFalha(x => x.DiasDeFerias, x => x.DiasDeFerias = OcorrenciaDeFerias.QuantidadeMaximaDeDiasDeFerias);
        }

        [Fact]
        public void Dias_de_abono_acima_do_permitido()
            => ExecuteCenarioTesteBasicoComFalha(x => x.DiasDeAbono, x => x.DiasDeAbono = OcorrenciaDeFerias.QuantidadeMaximaDeDiasDeAbono + 1);

        [Fact]
        public void Dias_de_abono_dentro_do_permitido()
            => ExecuteCenarioTesteBasicoComSucesso(x => x.DiasDeAbono, x => x.DiasDeAbono = OcorrenciaDeFerias.QuantidadeMaximaDeDiasDeAbono);

        [Fact]
        public void Ferias_antes_de_um_ano_de_contrato()
            => ExecuteCenarioTesteBasicoComFalha(x => x.DataDeInicio, x => x.DataDeInicio = x.PeriodoAquisitivo.AddMonths(11));

        [Fact]
        public void Ferias_apos_um_ano_de_contrato()
            => ExecuteCenarioTesteBasicoComSucesso(x => x.DataDeInicio, x => x.DataDeInicio = x.PeriodoAquisitivo.AddYears(1));

        [Fact]
        public void Sem_saldo_de_ferias_periodo_aquisitivo()
        {
            // Arrange
            var mock = Substitute.For<IOcorrenciaDeDeriasRepository>();
            var contratoMock = Substitute.For<Contrato>();
            mock.ObtenhaOcorrenciasDoPeriodoAquisitivo(Arg.Any<Guid>(), Arg.Any<DateTime>())
                .Returns(new List<OcorrenciaDeFerias>()
                {
                    new OcorrenciaDeFerias(contratoMock, new DateTime(2020, 1, 2), 30, 0),
                });
            var command = CenarioOcorrenciaDeFeriasCommandBuilder.Crie(mock);

            // act
            var validation = new RegistrarOcorrenciaDeFeriasCommandValidation(mock);

            // Assert
            validation.ShouldHaveValidationErrorFor(x => x.DiasDeFerias, command);
        }

        [Fact]
        public void Saldo_ferias_periodo_aquisitivo_com_parcela()
        {
            // Arrange
            var mock = Substitute.For<IOcorrenciaDeDeriasRepository>();
            var contratoMock = Substitute.For<Contrato>();
            mock.ObtenhaOcorrenciasDoPeriodoAquisitivo(Arg.Any<Guid>(), Arg.Any<DateTime>())
                .Returns(new List<OcorrenciaDeFerias>()
                {
                    new OcorrenciaDeFerias(contratoMock, new DateTime(2020, 1, 2), 15, 0),
                });
            var command = CenarioOcorrenciaDeFeriasCommandBuilder.Crie(mock);
            command.DiasDeAbono = 0;
            command.DiasDeFerias = 15;

            // act
            var validation = new RegistrarOcorrenciaDeFeriasCommandValidation(mock);

            // Assert
            validation.ShouldNotHaveValidationErrorFor(x => x.DiasDeFerias, command);
        }

        private void ExecuteCenarioTesteBasicoComFalha<T>(Expression<Func<RegistrarOcorrenciaDeFeriasCommand, T>> assert,
                                                          Action<RegistrarOcorrenciaDeFeriasCommand> transforme)
        {
            // Arrange
            var mock = Substitute.For<IOcorrenciaDeDeriasRepository>();
            mock.ObtenhaOcorrenciasDoPeriodoAquisitivo(Arg.Any<Guid>(), Arg.Any<DateTime>())
                .Returns(new List<OcorrenciaDeFerias>());
            var command = CenarioOcorrenciaDeFeriasCommandBuilder.Crie(mock);
            transforme(command);

            // act
            var validation = new RegistrarOcorrenciaDeFeriasCommandValidation(mock);

            // Assert
            validation.ShouldHaveValidationErrorFor(assert, command);
        }

        private void ExecuteCenarioTesteBasicoComSucesso<T>(Expression<Func<RegistrarOcorrenciaDeFeriasCommand, T>> assert)
        {
            // Arrange
            var mock = Substitute.For<IOcorrenciaDeDeriasRepository>();
            mock.ObtenhaOcorrenciasDoPeriodoAquisitivo(Arg.Any<Guid>(), Arg.Any<DateTime>())
                .Returns(new List<OcorrenciaDeFerias>());
            var command = CenarioOcorrenciaDeFeriasCommandBuilder.Crie(mock);

            // act
            var validation = new RegistrarOcorrenciaDeFeriasCommandValidation(mock);

            // Assert
            validation.ShouldNotHaveValidationErrorFor(assert, command);
        }

        private void ExecuteCenarioTesteBasicoComSucesso<T>(Expression<Func<RegistrarOcorrenciaDeFeriasCommand, T>> assert,
                                                            Action<RegistrarOcorrenciaDeFeriasCommand> transforme)
        {
            // Arrange
            var mock = Substitute.For<IOcorrenciaDeDeriasRepository>();
            mock.ObtenhaOcorrenciasDoPeriodoAquisitivo(Arg.Any<Guid>(), Arg.Any<DateTime>())
                .Returns(new List<OcorrenciaDeFerias>());
            var command = CenarioOcorrenciaDeFeriasCommandBuilder.Crie(mock);
            transforme(command);

            // act
            var validation = new RegistrarOcorrenciaDeFeriasCommandValidation(mock);

            // Assert
            validation.ShouldNotHaveValidationErrorFor(assert, command);
        }
    }
}