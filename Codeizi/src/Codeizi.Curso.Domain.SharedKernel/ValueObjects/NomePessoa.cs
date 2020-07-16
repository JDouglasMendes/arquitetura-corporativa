using System;
using System.Diagnostics.CodeAnalysis;

namespace Codeizi.Curso.Domain.SharedKernel.ValueObjects
{
    public sealed class NomePessoa : IEquatable<NomePessoa>, IComparable<NomePessoa>
    {
        public string Nome { get; }
        public string Sobrenome { get; }

        public NomePessoa(string nome, string sobrenome)
        {
            Nome = nome;
            Sobrenome = sobrenome;
        }

        public static NomePessoa Crie(string nome, string sobrenome)
            => new NomePessoa(nome, sobrenome);

        public override bool Equals(object obj)
        {
            return obj is NomePessoa pessoa &&
              Nome == pessoa.Nome &&
              Sobrenome == pessoa.Sobrenome;
        }

        public override int GetHashCode()
            => HashCode.Combine(Nome, Sobrenome);

        public override string ToString()
            => $"{Nome} {Sobrenome}";

        public bool Equals([AllowNull] NomePessoa other)
            => Nome == other.Nome &&
                    Sobrenome == other.Sobrenome;

        public int CompareTo([AllowNull] NomePessoa other)
                => ToString().CompareTo(other.ToString());

        public static bool operator ==(NomePessoa left, NomePessoa right)
        {
            if (left is null)
            {
                return right is null;
            }

            return left.Equals(right);
        }

        public static bool operator !=(NomePessoa left, NomePessoa right)
        {
            return !(left == right);
        }

        public static bool operator <(NomePessoa left, NomePessoa right)
        {
            return left is null ? right is object : left.CompareTo(right) < 0;
        }

        public static bool operator <=(NomePessoa left, NomePessoa right)
        {
            return left is null || left.CompareTo(right) <= 0;
        }

        public static bool operator >(NomePessoa left, NomePessoa right)
        {
            return left is object && left.CompareTo(right) > 0;
        }

        public static bool operator >=(NomePessoa left, NomePessoa right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }
    }
}