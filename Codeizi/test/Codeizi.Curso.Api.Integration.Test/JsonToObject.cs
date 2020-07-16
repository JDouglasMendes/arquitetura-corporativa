using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Codeizi.Curso.Api.Integration.Test
{
    public static class JsonToObject<T>
    {
        public static async Task<T> Convert(HttpResponseMessage response)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseString);
        }
    }
}