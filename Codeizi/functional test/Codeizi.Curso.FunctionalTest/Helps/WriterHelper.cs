using System;
using System.Collections.Generic;
using System.Text;

namespace Codeizi.Curso.FunctionalTest.Helps
{
    public static class WriterHelper
    {
        public static void W(this string valor)
        {            
            Console.Write(valor);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void WL(this string valor)
        {
            Console.WriteLine(valor);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static string Green(this string valor)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            return valor;
        }

        public static string Blue(this string valor)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            return valor;
        }

        public static string Red(this string valor)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            return valor;
        }
        

    }
}
