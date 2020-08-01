using Codeizi.Curso.Infra.CrossCutting.Configuration;
using MongoDB.Bson.Serialization.Conventions;
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
            var pack = new ConventionPack
            {
                new IgnoreExtraElementsConvention(true)
            };
            ConventionRegistry.Register("IgnoreExtraElementsConvention", pack, t => true);
            return dbClient.GetDatabase(_codeiziConfiguration.DatabaseEventSource);
        } 
    }
}