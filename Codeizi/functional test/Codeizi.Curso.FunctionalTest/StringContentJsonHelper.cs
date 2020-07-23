using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Codeizi.Curso.FunctionalTest
{
    public static class StringContentJsonHelper
    {
        public static StringContent ToJson(this object objeto)
        {
            var content = JsonConvert.SerializeObject(objeto);
            return new StringContent(content, Encoding.UTF8, "application/json");
        }
    }
}
