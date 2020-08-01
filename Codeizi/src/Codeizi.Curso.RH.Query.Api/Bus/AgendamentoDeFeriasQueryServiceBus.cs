using Codeizi.Curso.infra.CrossCutting.EventBusRabbitMQ;
using Codeizi.Curso.RH.Query.Api.BusModel;
using Codeizi.Curso.RH.Query.Api.Context;
using System.Threading.Tasks;

namespace Codeizi.Curso.RH.Query.Api.Bus
{
    [ServiceMediatorBus("agendamento-ferias-query")]
    public class AgendamentoDeFeriasQueryServiceBus
    {
        private readonly DatabaseQuery databaseQuery;

        public AgendamentoDeFeriasQueryServiceBus(DatabaseQuery databaseQuery)
            => this.databaseQuery = databaseQuery;

        public async Task Handle(Publishable publishable)
        {
            var contrato = publishable.ToObject<AgendamentoDeFeriasViewModel>();
            var collection = databaseQuery.Get().GetCollection<AgendamentoDeFeriasViewModel>(AgendamentoDeFeriasViewModel.ColletionName);
            await collection.InsertOneAsync(contrato);
        }
    }
}