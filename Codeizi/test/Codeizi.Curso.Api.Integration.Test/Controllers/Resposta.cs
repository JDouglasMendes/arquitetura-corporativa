using Codeizi.Curso.Api.Integration.Test.Cenarios;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Codeizi.Curso.Api.Integration.Test.Controllers
{

    public class Resposta
    {
        public bool Success { get; set; }
        public object Data { get; set; }
        public List<string> Errors { get; set; }
    }
}
