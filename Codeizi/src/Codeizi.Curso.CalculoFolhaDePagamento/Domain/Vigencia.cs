﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Domain
{
    public struct Vigencia : IEquatable<Vigencia>
    {
        public Vigencia(DateTime inicio)
        {
            Inicio = inicio;
            Fim = null;
        }

        public Vigencia(DateTime inicio, DateTime fim)
        {
            Inicio = inicio;
            Fim = fim;
        }

        public DateTime Inicio { get;  }
        public DateTime? Fim { get; private set; }

        public override bool Equals(object obj)
            => obj is Vigencia vigencia && vigencia == this;

        public override int GetHashCode()
            => HashCode.Combine(Inicio, Fim);

        public bool Equals([AllowNull] Vigencia other)
            => this.Inicio == other.Inicio && this.Fim == other.Fim;

        public static bool operator ==(Vigencia left, Vigencia right)
            => left.Equals(right);

        public static bool operator !=(Vigencia left, Vigencia right)
            => !(left == right);
    }
}
