using Codeizi.Curso.infra.CrossCutting.EventBusRabbitMQ;
using Codeizi.Curso.Infra.CrossCutting.Configuration;
using Codeizi.Curso.RH.Query.Api.BusModel;
using Codeizi.Curso.RH.Query.Api.Context;
using System.Threading.Tasks;

namespace Codeizi.Curso.RH.Query.Api.Bus
{
    // [ServiceMediatorBus("agendamento-ferias-query")]
    public class AgendamentoDeFeriasQueryServiceBus : IConsumerServiceBus
    {
        private readonly DatabaseQuery _databaseQuery;
        private readonly ICodeiziConfiguration _codeiziConfiguration;

        public AgendamentoDeFeriasQueryServiceBus(DatabaseQuery databaseQuery,
                                                  ICodeiziConfiguration codeiziConfiguration)
            => (_databaseQuery, _codeiziConfiguration) = (databaseQuery, codeiziConfiguration);

        public string RoutingKey => _codeiziConfiguration.AgendamentoDeFeriasQueryBus;

        public async Task Handle(Publishable publishable)
        {
            var contrato = publishable.ToObject<AgendamentoDeFeriasViewModel>();
            var collection = _databaseQuery.Get().GetCollection<AgendamentoDeFeriasViewModel>(AgendamentoDeFeriasViewModel.ColletionName);
            await collection.InsertOneAsync(contrato);
        }
    }
}