using Infra.CrossCutting.Configuration;
using Infra.CrossCutting.EventBusRabbitMQ;
using RH.Query.BusModel;
using RH.Query.Context;
using System.Threading.Tasks;

namespace RH.Query.Bus
{
    // [ServiceMediatorBus("contrato-query")]
    public class ContratoQueryServiceBus : IConsumerServiceBus
    {
        private readonly DatabaseQuery _databaseQuery;
        private readonly ICodeiziConfiguration _codeiziConfiguration;

        public ContratoQueryServiceBus(DatabaseQuery databaseQuery,
                                       ICodeiziConfiguration codeiziConfiguration)
            => (_databaseQuery, _codeiziConfiguration) = (databaseQuery, codeiziConfiguration);

        public string RoutingKey => _codeiziConfiguration.ContratoQueryBus;

        public async Task Handle(Publishable publishable)
        {
            var contrato = publishable.ToObject<ContratoQueryViewModel>();
            var collection = _databaseQuery.Get().GetCollection<ContratoQueryViewModel>(ContratoQueryViewModel.ColletionName);
            await collection.InsertOneAsync(contrato);
        }
    }
}