using Newtonsoft.Json;
using System;

namespace Infra.CrossCutting.EventBusRabbitMQ
{
    public static class FactoryPublishable
    {
        public static Publishable Get<T>(Guid id, string eventName, T data) where T : class
            => new Publishable(id, eventName, JsonConvert.SerializeObject(data));
    }
}