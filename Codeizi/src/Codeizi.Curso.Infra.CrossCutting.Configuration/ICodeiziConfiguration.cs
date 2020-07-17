namespace Codeizi.Curso.Infra.CrossCutting.Configuration
{
    public interface ICodeiziConfiguration
    {
        string ConnectionStringRavenDB { get; }
        string DatabaseRavenDB { get; }
        string ConnectionStringRedis { get; }
    }
}