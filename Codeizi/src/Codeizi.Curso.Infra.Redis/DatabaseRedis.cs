using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codeizi.Curso.Infra.Redis
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
