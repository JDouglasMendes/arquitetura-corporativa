using System;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Infra.Data.Tables
{
    public class FieldDatetimeCustom
    {
        private DateTime? _data;

        public FieldDatetimeCustom(DateTime dateTime)
            => _data = dateTime;

        public FieldDatetimeCustom(DateTime? dateTime)
            => _data = dateTime ?? null;

        public FieldDatetimeCustom(int data)
            => ToDatetime(data);

        public int ToInt()
            => _data.HasValue ?
               int.Parse($"{_data.Value.Year}{_data.Value.Month:00}{_data.Value.Day:00}") :
               0;

        public DateTime? ToDateTime()
            => _data ?? null;

        private DateTime ToDatetime(int data) =>
            new DateTime(int.Parse(data.ToString().Substring(0, 4)),
                                int.Parse(data.ToString().Substring(3, 2)),
                                int.Parse(data.ToString().Substring(5, 2)));
    }
}