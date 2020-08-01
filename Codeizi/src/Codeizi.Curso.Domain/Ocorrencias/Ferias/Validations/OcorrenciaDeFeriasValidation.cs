using Codeizi.Curso.RH.Domain.Ocorrencias.Ferias.Commands;
using Codeizi.Curso.RH.Domain.Ocorrencias.Ferias.Contracts;
using Codeizi.Curso.RH.Domain.SharedKernel.Commands;
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

        protected IRuleBuilder<TCommand, Guid> ValideContratoInfomado()
            => RuleFor(x => x.IdContrato)
                .NotEmpty()
                .WithMessage(Mensagens.IDContratoNaoInformado);

        protected void ValidePeriodoAquisitivoInformado()
            => RuleFor(x => x.PeriodoAquisitivo)
                .Must((command, pa) => pa != default)
                .WithMessage(Mensagens.PeriodoAquisitivoNaoInformado);

        protected void ValideDataDeInicioInformado()
            => RuleFor(x => x.DataDeInicio)
              .Must((command, dt) => dt != default)
                .WithMessage(Mensagens.DatadeInicioNaoInformada);

        protected void ValideDiasDeFeriasInformado()
            => RuleFor(x => x.DiasDeFerias)
                .Must(x => x != default)
                .WithMessage(Mensagens.DiasDeFeriasNaoInformada);

        protected void ValideMaximoDeFerias()
            => RuleFor(x => x.DiasDeFerias)
                .Must((command, diasDeFerias) => diasDeFerias + command.DiasDeAbono <= OcorrenciaDeFerias.QuantidadeMaximaDeDiasDeFerias)
                .WithMessage(Mensagens.MaximoDiasDeFerias);

        protected void ValideDiasDeAbono()
            => RuleFor(x => x.DiasDeAbono)
                .LessThanOrEqualTo(OcorrenciaDeFerias.QuantidadeMaximaDeDiasDeAbono);

        protected void ValideSaldoFeriasPeriodoAquisitivo()
            => RuleFor(x => x.DiasDeFerias)
               .MustAsync(async (command, cancellationToken, diasDeFerias) =>
               {
                   var ferias = await _ocorrenciaDeDeriasRepository.ObtenhaOcorrenciasDoPeriodoAquisitivo(command.IdContrato, command.PeriodoAquisitivo);
                   var diasJaGozados = ferias.Sum(x => x.DiasDeFerias);
                   return ((command.DiasDeFerias + command.DiasDeAbono) + diasJaGozados) <= OcorrenciaDeFerias.QuantidadeMaximaDeDiasDeFerias;
               })
            .WithMessage(Mensagens.SaldoDeFerias);

        protected void ValideFeriasAposUmAnoDeContrato()
            => RuleFor(x => x.DataDeInicio)
                .Must((command, dataInicio) => command.PeriodoAquisitivo.AddYears(1) <= dataInicio)
            .WithMessage(Mensagens.FeriasAposAnoContrato);
    }
}