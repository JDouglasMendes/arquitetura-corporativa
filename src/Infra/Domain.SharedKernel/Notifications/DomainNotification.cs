﻿using Domain.SharedKernel.Events;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Domain.SharedKernel.Notifications
{
    [ExcludeFromCodeCoverage]
    public class DomainNotification : Event
    {
        public Guid DomainNotificationId { get; private set; }
        public string Key { get; private set; }
        public string Value { get; private set; }
        public int Version { get; private set; }
        public override string GetKeyQueues =>
            throw new NotImplementedException($"Não deve ser enviado {typeof(DomainNotification).FullName} para mensageria");

        public DomainNotification(string key, string value)
        {
            DomainNotificationId = Guid.NewGuid();
            Version = 1;
            Key = key;
            Value = value;
        }
    }
}