using BILTIFUL.Modulo1;
using System.Security.Cryptography;

namespace BILTIFUL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string cpf = "39446479800";

            Console.WriteLine("resultado final: "+Cliente.VerificarCpf(cpf));
        }
    }
}
