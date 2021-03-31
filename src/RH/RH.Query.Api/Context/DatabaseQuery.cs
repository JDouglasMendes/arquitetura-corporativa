using Infra.CrossCutting.Configuration;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace RH.Query.Context
{
    public class DatabaseQuery
    {
        private readonly ICodeiziConfiguration _codeiziConfiguration;

        public DatabaseQuery(ICodeiziConfiguration codeiziConfiguration)
            => _codeiziConfiguration = codeiziConfiguration;

        public IMongoDatabase Get()
        {
            var dbClient = new MongoClient(_codeiziConfiguration.ConnectionStringQueryDatabase);
            var pack = new ConventionPack
            {
                new IgnoreExtraElementsConvention(true)
            };
            ConventionRegistry.Register("IgnoreExtraElementsConvention", pack, t => true);
            return dbClient.GetDatabase(_codeiziConfiguration.DatabaseEventSource);
        }
    }
}