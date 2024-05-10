using BILTIFUL.Modulo1;
using BILTIFUL.Modulo2.ManipuladorArquivos;

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
            Venda.ChecarCaminho(@"C:\Biltiful\", "Venda.dat");
            List<Cliente> listaCliente = new List<Cliente>(ArquivoVenda.importarCliente(@"C:\Biltiful\", "Cliente.dat"));
            List<Venda> listaVenda = new List<Venda>(ArquivoVenda.importarVenda(@"C:\Biltiful\", "Venda.dat"));
            List<Produto> listaProduto = new List<Produto>(ArquivoVenda.importarProduto(@"C:\Biltiful\", "Cosmetico.dat"));
            do
            {
                switch (Menu())
                {
                    case 1:
                        ManipularVenda.CadastrarVenda(listaVenda, listaCliente);
                        break;
                    case 2:
                        //ExcluirVenda();
                        break;
                    case 3:
                        //LocalizarVenda();
                        break;
                    case 4:
                        //ImprimirRegisVenda();
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




