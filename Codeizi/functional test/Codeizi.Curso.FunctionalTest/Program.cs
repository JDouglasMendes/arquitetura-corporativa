using Codeizi.Curso.FunctionalTest.Helps;
using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Codeizi.Curso.FunctionalTest
{
    public class Program
    {
        static HttpClient client = new HttpClient();

#pragma warning disable IDE0060 // Remove unused parameter
        static async Task Main(string[] args)
#pragma warning restore IDE0060 // Remove unused parameter
        {
            "Bem vindo a execução dos testes funcionais do curso de Aplicações Corporativas!!!".Blue().WL();
            NewLine();
            $"Verificando Conexão com SQL Server     :".WL();            
            $"Verificando Conexão com MongoBD        :".WL();


            $"Verificando Conexão com API RH                         :".WL();
            $"Verificando Conexão com API Cálculo                    :".WL();
            $"Verificando Conexão com API Cálculo BackgroundTasks    :".WL();
            $"Verificando Conexão com API Signalrl                   :".WL();

            var cadastro = new CadastrarColaborador(client);
            await cadastro.CadastrarColaboradorValidos(null);
            Console.Read();
        }


        public static void NewLine()
        {
            Console.WriteLine();
        }

        private static async Task<string> VerificaConexaoSQLServer()
        {
            await Task.Delay(10000);
            return "OK";
        }
    }
}
