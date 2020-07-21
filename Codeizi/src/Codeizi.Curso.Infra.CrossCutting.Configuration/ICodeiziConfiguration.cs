namespace Codeizi.Curso.Infra.CrossCutting.Configuration
{
    public interface ICodeiziConfiguration
    {
        string ConnectionStringEventSource { get; }
        string DatabaseEventSource { get; }
        string ConnectionStringRedis { get; }
    }
}