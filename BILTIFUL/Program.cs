using BILTIFUL.Modulo1;
using BILTIFUL.Modulo2;
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


        /// <summary>
        /// Executa o programa
        /// </summary>
        static void Executar()
        {
            bool terminouPrograma = false;
            do
            {
                switch (MenuPrincipal())
                {
                    case 1:
                        _ = new MainModulo1();
                        break;
                    case 2:
                        _ = new MainModulo2();
                        break;
                    case 3:
                        _ = new MainModulo3();
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


        /// <summary>
        /// Exibe o menu principal e retorna a opção escolhida
        /// </summary>
        /// <returns></returns>
        static private int MenuPrincipal()
        {
            Console.Clear();
            Console.WriteLine("=========BILTIFUL=========");

            Console.WriteLine("Opcoes: ");
            Console.WriteLine("1- Cadastrar, editar ou imprimir Clientes, Fornecedores, Produtos ou Matéria-prima");
            Console.WriteLine("2- Venda de produto");
            Console.WriteLine("3- Compra de matéria-prima");
            Console.WriteLine("4- Produção de produtos");
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