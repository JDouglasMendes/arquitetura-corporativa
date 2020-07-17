using Codeizi.Curso.Infra.CrossCutting.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codeizi.Curso.Infra.Redis
{
    public class MultiplexerRedis
    {
        private ConnectionMultiplexer _instance;

        private readonly ICodeiziConfiguration codeiziConfiguration;

        public MultiplexerRedis(ICodeiziConfiguration codeiziConfiguration)
            => this.codeiziConfiguration = codeiziConfiguration;

        private static object _lock = new object();
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
