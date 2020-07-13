using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Domain
{
    public struct FolhaDePagamento : IEquatable<FolhaDePagamento>
    {
        public EnumFolhaDePagamento Id { get; }
        public string Descricao { get; }

        private FolhaDePagamento(EnumFolhaDePagamento id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }

        public static FolhaDePagamento FolhaMensal = new FolhaDePagamento(EnumFolhaDePagamento.Mensal, "Mensal");
        public static FolhaDePagamento FolhaFerias = new FolhaDePagamento(EnumFolhaDePagamento.Ferias, "Férias");

        public override int GetHashCode()
            => (int)Id;

        public bool Equals([AllowNull] FolhaDePagamento other)
            => this == other;

        public static bool operator ==(FolhaDePagamento left, FolhaDePagamento right)
            => left.Id == right.Id;

        public static bool operator !=(FolhaDePagamento left, FolhaDePagamento right)
             => left.Id != right.Id;

        public override bool Equals(object obj)
         => obj is FolhaDePagamento folha && folha == this;
    }
}
