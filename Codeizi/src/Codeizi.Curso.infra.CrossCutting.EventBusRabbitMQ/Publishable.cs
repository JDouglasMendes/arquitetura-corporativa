﻿using Newtonsoft.Json;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Codeizi.Curso.infra.CrossCutting.EventBusRabbitMQ
{
    [ExcludeFromCodeCoverage]
    public class Publishable
    {
        public Publishable(Guid id, string eventName, string data)
        {
            Id = id;
            EventName = eventName;
            Data = data;
        }

        public T ToObject<T>() 
            where T : class
            => (T)  JsonConvert.DeserializeObject<T>(Data);        

        public Guid Id { get; }
        public string EventName { get; }
        public string Data { get; set; }
        
    }
}