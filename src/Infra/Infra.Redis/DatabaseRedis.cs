using StackExchange.Redis;
using System.Diagnostics.CodeAnalysis;

namespace Infra.CrossCutting.Redis
{
    [ExcludeFromCodeCoverage]
    public class DatabaseRedis
    {
        private readonly MultiplexerRedis multiplexerRedis;

        public DatabaseRedis(MultiplexerRedis multiplexerRedis)
            => this.multiplexerRedis = multiplexerRedis;

        public IDatabase GetClient()
            => multiplexerRedis.GetConnection().GetDatabase();
    }
}