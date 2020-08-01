using Codeizi.Curso.RH.Domain.Ocorrencias.Ferias.Commands;
using Codeizi.Curso.RH.Domain.Ocorrencias.Ferias.Contracts;
using FluentValidation;
using System;
using System.Linq;

namespace Codeizi.Curso.RH.Domain.Ocorrencias.Ferias.Validations
{
    public abstract class OcorrenciaDeFeriasValidation<TCommand> : AbstractValidator<TCommand>
        where TCommand : OcorrenciaDeFeriasCommand
    {
        private readonly IOcorrenciaDeDeriasRepository _ocorrenciaDeDeriasRepository;

        protected OcorrenciaDeFeriasValidation(IOcorrenciaDeDeriasRepository ocorrenciaDeDeriasRepository)
            => _ocorrenciaDeDeriasRepository = ocorrenciaDeDeriasRepository;

        protected void ValideContratoInfomado()
            => RuleFor(x => x.IdContrato)
                .NotNull()
                .NotEmpty()
                .WithMessage(Mensagens.IDContratoNaoInformado);

        protected void ValidePeriodoAquisitivoInformado()
            => RuleFor(x => x.PeriodoAquisitivo)
                .NotEqual(default(DateTime))
                .WithMessage(Mensagens.PeriodoAquisitivoNaoInformado);

        protected void ValideDataDeInicioInformado()
            => RuleFor(x => x.DataDeInicio)
                .NotEqual(default(DateTime))
                .WithMessage(Mensagens.DatadeInicioNaoInformada);

        protected void ValideDiasDeFeriasInformado()
            => RuleFor(x => x.DiasDeFerias)
                .NotEqual(x => default)
                .WithMessage(Mensagens.DiasDeFeriasNaoInformada);

        protected void ValideMaximoDeFerias()
            => RuleFor(x => x.DiasDeFerias)
                .Must((command, diasDeFerias) => diasDeFerias + command.DiasDeAbono > OcorrenciaDeFerias.QuantidadeMaximaDeDiasDeFerias)
                .WithMessage(Mensagens.MaximoDiasDeFerias);

        protected void ValideDiasDeAbono()
            => RuleFor(x => x.DiasDeAbono)
                .GreaterThan(OcorrenciaDeFerias.QuantidadeMaximaDeDiasDeAbono);

        protected void ValideSaldoFeriasPeriodoAquisitivo()
            => RuleFor(x => x.DiasDeFerias)
               .MustAsync(async (command, cancellationToken, diasDeFerias) =>
               {
                   var ferias = await _ocorrenciaDeDeriasRepository.ObtenhaOcorrenciasDoPeriodoAquisitivo(command.IdContrato, command.PeriodoAquisitivo);
                   var diasJaGozados = ferias.Sum(x => x.DiasDeFerias);
                   return ((command.DiasDeFerias + command.DiasDeAbono) + diasJaGozados) > OcorrenciaDeFerias.QuantidadeMaximaDeDiasDeFerias;
               })
            .WithMessage(Mensagens.SaldoDeFerias);

        protected void ValideFeriasAposUmAnoDeContrato()
            => RuleFor(x => x.DataDeInicio)
                .Must((command, dataInicio) => dataInicio.AddYears(1) >= command.PeriodoAquisitivo)
            .WithMessage(Mensagens.FeriasAposAnoContrato);
    }
}