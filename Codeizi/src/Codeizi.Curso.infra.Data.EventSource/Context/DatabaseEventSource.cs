using Codeizi.Curso.Infra.CrossCutting.Configuration;
using MongoDB.Driver;

namespace Codeizi.Curso.RH.infra.Data.EventSource.Context
{
    public class DatabaseEventSource
    {
        private readonly ICodeiziConfiguration _codeiziConfiguration;
        public DatabaseEventSource(ICodeiziConfiguration codeiziConfiguration)
            => _codeiziConfiguration = codeiziConfiguration;

        public IMongoDatabase Get()
        {
            var dbClient = new MongoClient(_codeiziConfiguration.ConnectionStringEventSource);
            return dbClient.GetDatabase(_codeiziConfiguration.DatabaseEventSource);
        } 
    }
}