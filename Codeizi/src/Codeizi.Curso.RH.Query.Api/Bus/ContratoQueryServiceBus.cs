using Codeizi.Curso.infra.CrossCutting.EventBusRabbitMQ;
using Codeizi.Curso.RH.Query.Api.BusModel;
using Codeizi.Curso.RH.Query.Api.Context;
using System.Threading.Tasks;

namespace Codeizi.Curso.RH.Query.Api.Bus
{
    [ServiceMediatorBus("contrato-query")]
    public class ContratoQueryServiceBus
    {
        private readonly DatabaseQuery databaseQuery;

        public ContratoQueryServiceBus(DatabaseQuery databaseQuery)
            => this.databaseQuery = databaseQuery;

        public async Task Handle(Publishable publishable)
        {
            var contrato = publishable.ToObject<ContratoQueryViewModel>();
            var collection = databaseQuery.Get().GetCollection<ContratoQueryViewModel>(ContratoQueryViewModel.ColletionName);
            await collection.InsertOneAsync(contrato);
        }
    }
}