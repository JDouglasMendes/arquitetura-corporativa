using System.Collections.Generic;

namespace Codeizi.Curso.Api.Errors
{
    public class Error
    {
        public int Id { get; set; }
        public string Controller { get; set; }
        public List<ErrorDetail> ErrosDetails { get; set; }
        public Error()
        {
            ErrosDetails = new List<ErrorDetail>();
        }
    }
}
