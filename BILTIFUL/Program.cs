using BILTIFUL.Modulo1;
using BILTIFUL.Modulo3;
using BILTIFUL.Modulo4;

namespace BILTIFUL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Executar();
        }

        static void Executar()
        {
            bool terminouPrograma = false;
            do
            {
                switch (MenuPrincipal())
                {
                    case 1:
                        new MainModulo1().Executar();
                        break;
                    case 2:
                        //new MainModulo2().Executar();
                        break;
                    case 3:
                        new MainModulo3();
                        break;
                    case 4:
                        _ = new MainModulo4();
                        break;
                    case 0:
                        terminouPrograma = true;
                        break;
                    default:
                        Console.WriteLine("Opcao invalida");
                        break;
                }

            } while (!terminouPrograma);
        }

        static private int MenuPrincipal()
        {
            Console.Clear();
            Console.WriteLine("======BILTIFUL======");

            Console.WriteLine("Opcoes: ");
            Console.WriteLine("1- MODULO 1");
            Console.WriteLine("2- MODULO 2");
            Console.WriteLine("3- MODULO 3");
            Console.WriteLine("4- MODULO 4");
            Console.WriteLine("0- SAIR");
            Console.Write("R: ");

            if (int.TryParse(Console.ReadLine(), out int option))
            {
                return option;
            }
            else
            {
                Console.WriteLine("Voce deve digitar um numero!");
                Console.Write("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                return MenuPrincipal();
            }
        }

    }
}