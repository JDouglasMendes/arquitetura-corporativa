﻿using System;

namespace Domain.SharedKernel.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }
    }
}