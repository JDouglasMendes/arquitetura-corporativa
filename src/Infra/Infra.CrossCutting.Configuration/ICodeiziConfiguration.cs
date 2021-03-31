namespace Infra.CrossCutting.Configuration
{
    public interface ICodeiziConfiguration
    {
        string ConnectionStringEventSource { get; }
        string DatabaseEventSource { get; }
        string ConnectionStringRedis { get; }
        string ConnectionStringQueryDatabase { get; }
        string CalculoFolhaDePagamentoQueue { get; }
        string RHQueryQueue { get; }
        string SignalHbQueue { get; }        
        string AgendamentoDeFeriasQueryBus { get; }
        string ContratoQueryBus { get; }
        string NotificarUsuarioRoutingKey { get; }
    }
}