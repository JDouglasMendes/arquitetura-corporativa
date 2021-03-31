using Infra.CrossCutting.Configuration;
using StackExchange.Redis;
using System.Diagnostics.CodeAnalysis;

namespace Infra.CrossCutting.Redis
{
    [ExcludeFromCodeCoverage]
    public class MultiplexerRedis
    {
        private ConnectionMultiplexer _instance;

        private readonly ICodeiziConfiguration codeiziConfiguration;

        public MultiplexerRedis(ICodeiziConfiguration codeiziConfiguration)
            => this.codeiziConfiguration = codeiziConfiguration;

        private static readonly object _lock = new object();

        public ConnectionMultiplexer GetConnection()
        {
            lock (_lock)
            {
                if (_instance == null)
                    _instance = ConnectionMultiplexer.Connect(codeiziConfiguration.ConnectionStringRedis);
            }

            return _instance;
        }
    }
}