using RH.Domain;
using System.Threading;
using Xunit;

namespace Domain.Test
{
    public class MensagensResxTest
    {
        [Fact]
        public void MensagensTest()
        {
            Assert.Null(Mensagens.Culture);
            Mensagens.Culture = Thread.CurrentThread.CurrentCulture;
            Assert.NotNull(Mensagens.Culture);
        }
    }
}