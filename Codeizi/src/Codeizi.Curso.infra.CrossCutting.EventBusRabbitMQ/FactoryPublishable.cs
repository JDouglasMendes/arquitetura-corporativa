using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codeizi.Curso.infra.CrossCutting.EventBusRabbitMQ
{
    public static class FactoryPublishable
    {
        public static Publishable Get<T>(Guid id, string eventName, T data) where T : class
            =>  new Publishable(id, eventName, JsonConvert.SerializeObject(data));        
    }
}
