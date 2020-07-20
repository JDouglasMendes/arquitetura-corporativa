using Codeizi.Curso.RH.Domain.Colaboradores.Commands;
using FluentValidation;

namespace Codeizi.Curso.RH.Domain.Colaboradores.Validations
{
    public abstract class ColaboradorValidation<TComand> : AbstractValidator<TComand>
        where TComand : ColaboradorCommand
    {
        protected void ValideNome()
           => RuleFor(c => c.Nome)
                   .Cascade(CascadeMode.StopOnFirstFailure)
                   .NotEmpty().WithMessage(Mensagens.NomeColaborador)
                   .Length(2, 100).WithMessage(Mensagens.TamanhoNomeColaborador);

        protected void ValideSobrenome()
            => RuleFor(c => c.Sobrenome)
                  .Cascade(CascadeMode.StopOnFirstFailure)
                  .NotEmpty().WithMessage(Mensagens.SobrenomeColaborador)
                  .Length(1, 100).WithMessage(Mensagens.TamanhoSobrenomeColaborador);

        public void ValideDataAdmisaoColaborador()
            => RuleFor(d => d.DataDeAdmissao)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().Must(data => data != default)
                .WithMessage(Mensagens.DataContratoObrigatoria);

        public void ValideSalarioContratual()
            => RuleFor(s => s.SalarioContratual)
             .GreaterThan(0)
             .WithMessage(Mensagens.SalarioContratual);

        public void ValideObservacaoContratual()
            => RuleFor(c => c.ObservacaoContratual)
              .MaximumLength(100)
              .WithMessage(Mensagens.ObservacaoContratual);

        public void ValideDataAdmisaoAntesDataNascimento()
            => RuleFor(x => x.DataDeAdmissao)
            .GreaterThan(y => y.DataNascimento);
    }
}