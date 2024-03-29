﻿using CalculoFolhaDePagamento.Domain.Domain.Contratos;
using CalculoFolhaDePagamento.Domain.Services.Repositories;
using CalculoFolhaDePagamento.Domain.Services.ServiceDomain;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CalculoFolhaDePagamento.Domain.Domain.Calculo
{
    public class CalculoBuilder
    {
        private readonly ICalculo _calculo;
        private readonly ICalculoRepository _calculoRepository;
        private readonly IFeedbackExecucaoCalculoServiceDomain _feedbackExecucaoCalculo;
        private readonly DateTime _referencia;
        private List<Contrato> _contratos;

        public CalculoBuilder(DateTime referencia,
                              EnumFolhaDePagamento enumFolhaDePagamento,
                              ICalculoRepository calculoRepository,
                              IFeedbackExecucaoCalculoServiceDomain feedbackExecucaoCalculo)
        {
            _calculo = FabricaCalculo.Crie(enumFolhaDePagamento, referencia);
            _calculoRepository = calculoRepository;
            _referencia = referencia;
            _feedbackExecucaoCalculo = feedbackExecucaoCalculo;
        }

        public CalculoBuilder InicieCalculo(List<Contrato> contratos)
        {
            _contratos = contratos;
            _quantidadeTotal = _contratos.Count;
            IdExecucao = Guid.NewGuid();
            _feedbackExecucaoCalculo.IniciarProcessamento(IdExecucao, _quantidadeTotal);
            return this;
        }

        public CalculoBuilder InicieCalculo(IContratoRepository repository)
            => InicieCalculo(repository.ObterContratosVigentes(_referencia));

        private int _quantidadeTotal;
        private int _quantidadeProcessada;

        public Guid IdExecucao { get; private set; }

        public async Task CalculeContratos()
        {
            CheckParameters();

            await Task.Run(() =>
            {
                Parallel.For(0, _quantidadeTotal, async index =>
                {
                    var valores = _calculo.Calcule(_contratos[index]);
                    await _calculoRepository.InsiraValoresCalculados(valores);
                    Interlocked.Increment(ref _quantidadeProcessada);
                    _feedbackExecucaoCalculo.AtualizarPercentualExecucao(IdExecucao, _quantidadeProcessada, _quantidadeTotal);
                });
            });
        }

        private void CheckParameters()
        {
            if (_contratos == null)
                throw new ArgumentException(nameof(_contratos));
        }
    }
}