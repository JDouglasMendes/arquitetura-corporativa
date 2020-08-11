using Codeizi.Curso.infra.CrossCutting.EventBusRabbitMQ;
using Codeizi.Curso.Infra.CrossCutting.Configuration;
using Codeizi.Curso.RH.Query.Api.BusModel;
using Codeizi.Curso.RH.Query.Api.Context;
using System.Threading.Tasks;

namespace Codeizi.Curso.RH.Query.Api.Bus
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