using Infra.CrossCutting.Configuration;
using Infra.CrossCutting.EventBusRabbitMQ;
using RH.Query.BusModel;
using RH.Query.Context;
using System.Threading.Tasks;

namespace RH.Query.Bus
{
    // [ServiceMediatorBus("agendamento-ferias-query")]
    public class AgendamentoDeFeriasQueryServiceBus
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