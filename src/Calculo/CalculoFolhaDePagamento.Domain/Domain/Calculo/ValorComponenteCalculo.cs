using System;
using System.Diagnostics.CodeAnalysis;

namespace CalculoFolhaDePagamento.Domain.Domain.Calculo
{
    public struct ValorComponenteCalculo : IEquatable<ValorComponenteCalculo>, IComparable<ValorComponenteCalculo>
    {
        public ValorComponenteCalculo(double valor)
            => Valor = valor;

        public double Valor { get; }

        public static ValorComponenteCalculo Zero => new ValorComponenteCalculo(0);

        public int CompareTo([AllowNull] ValorComponenteCalculo other)
            => Valor.CompareTo(other.Valor);

        public override bool Equals(object obj)
            => obj is ValorComponenteCalculo valorCalculo &&
                valorCalculo.Valor == Valor;

        public bool Equals([AllowNull] ValorComponenteCalculo other)
            => other.Valor == Valor;

        public override int GetHashCode()
            => HashCode.Combine(Valor);

        public static ValorComponenteCalculo operator +(ValorComponenteCalculo v1, ValorComponenteCalculo v2)
            => new ValorComponenteCalculo(v1.Valor + v2.Valor);

        public static ValorComponenteCalculo operator -(ValorComponenteCalculo v1, ValorComponenteCalculo v2)
            => new ValorComponenteCalculo(v1.Valor - v2.Valor);

        public static ValorComponenteCalculo operator *(ValorComponenteCalculo v1, ValorComponenteCalculo v2)
            => new ValorComponenteCalculo(v1.Valor * v2.Valor);

        public static ValorComponenteCalculo operator /(ValorComponenteCalculo v1, ValorComponenteCalculo v2)
            => new ValorComponenteCalculo(v1.Valor / v2.Valor);

        public static bool operator ==(ValorComponenteCalculo left, ValorComponenteCalculo right)
            => left.Equals(right);

        public static bool operator !=(ValorComponenteCalculo left, ValorComponenteCalculo right)
            => !(left == right);

        public static explicit operator ValorComponenteCalculo(double valor)
            => new ValorComponenteCalculo(valor);

        public static bool operator <(ValorComponenteCalculo left, ValorComponenteCalculo right)
           => left.Valor < right.Valor;

        public static bool operator <=(ValorComponenteCalculo left, ValorComponenteCalculo right)
            => left.Valor <= right.Valor;

        public static bool operator >(ValorComponenteCalculo left, ValorComponenteCalculo right)
            => left.Valor > right.Valor;

        public static bool operator >=(ValorComponenteCalculo left, ValorComponenteCalculo right)
            => left.Valor >= right.Valor;
    }
}