using StackExchange.Redis;

namespace Codeizi.Curso.Infra.CrossCutting.Redis
{
    public class DatabaseRedis
    {
        private readonly MultiplexerRedis multiplexerRedis;

        public DatabaseRedis(MultiplexerRedis multiplexerRedis)
            => this.multiplexerRedis = multiplexerRedis;

        public IDatabase GetClient()
            => multiplexerRedis.GetConnection().GetDatabase();
    }
}