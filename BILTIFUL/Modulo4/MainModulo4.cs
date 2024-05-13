using BILTIFUL.Modulo4.Utils;
namespace BILTIFUL.Modulo4
{
    internal class MainModulo4
    {
        public MainModulo4()
        {
            Menu();
        }
        static void Menu()
        {
            int opcao = -1;
            while (opcao != 0)
            {
                Console.Clear();
                Console.WriteLine("======Produção======");
                Console.WriteLine("Opção:");
                Console.WriteLine("1 - Cadastrar Produção");
                Console.WriteLine("2 - Localizar Produção");
                Console.WriteLine("3 - Excluir Produção");
                Console.WriteLine("4 - Impressao Produção");
                Console.WriteLine("0 - Voltar ao Menu Inicial");
                Console.Write("R: ");
                opcao = Extra.retornarInt();
                switch (opcao)
                {
                    case 0:
                        Console.WriteLine("Saindo do módulo Produção");
                        break;
                    case 1:
                        new FuncoesProducao().inserirProducao();
                        break;
                    case 2:
                        new FuncoesProducao().localizarProducao();
                        break;
                    case 3:
                        new FuncoesProducao().excluirProducao();
                        break;
                    case 4:
                        new FuncoesProducao().imprimirProducao();
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        Console.Write("Pressione qualquer tecla para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
