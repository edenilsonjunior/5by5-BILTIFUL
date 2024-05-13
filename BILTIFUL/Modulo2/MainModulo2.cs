using BILTIFUL.Modulo1;
using BILTIFUL.Modulo2.ManipuladorArquivos;
//*//
namespace BILTIFUL.Modulo2
{
    internal class MainModulo2
    {
        public MainModulo2()
        {
            ChamarMenu();
        }
        static int Menu()
        {


            Console.Clear();
            Console.WriteLine("======Vendas======");

            Console.WriteLine("Digite o número da opção desejada: ");
            Console.WriteLine("1-Cadastrar venda.");
            Console.WriteLine("2-Excluir venda.");
            Console.WriteLine("3-Localizar venda.");
            Console.WriteLine("4-Imprimir registro de venda.");
            Console.WriteLine("0- Sair");
            Console.Write("Escolha: ");

            if (int.TryParse(Console.ReadLine(), out int opcao))
            {
                return opcao;
            }
            else
            {
                Console.WriteLine("Você deve inserir um número!");
                Console.Write("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                return Menu();
            }
        }
        public void ChamarMenu()
        {
           
            do
            {
                switch (Menu())
                {
                    case 1:
                        //ManipularVenda.CadastrarVenda(listaVenda, listaCliente, listaBloqueados);
                        new ManipularVenda().CadastrarVenda();
                        break;
                    case 2:
                        //ManipularVenda.ExcluirVenda();
                        new ManipularVenda().ExcluirVenda();
                        break;
                    case 3:
                        //ManipularVenda.LocalizarVenda();
                        new ManipularVenda().LocalizarVenda();
                        break;
                    case 4:
                        //ManipularVenda.ImprimirRegisVenda(listaVenda);
                        new ManipularVenda().ImprimirRegisVenda();
                        break;
                    case 0:
                        Console.WriteLine("Encerrando o programa.");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            } while (true);
        }
    }
}




