using BILTIFUL.Modulo1;
using BILTIFUL.Modulo1.ManipuladorArquivos;
using BILTIFUL.Modulo4.Entidades;
using BILTIFUL.Modulo4.Utils;
namespace BILTIFUL.Modulo4
{
    internal class MainModulo4
    {
        public MainModulo4()
        {
            string path = @"C:\BILTIFUL\";
            // Cria Listas como variável global
            // Carrega os dados na chamada da MainModulo4, buscando os arquivos na pasta designada.
            ArquivoProducao.ChecarCaminho(path);
            List<Producao> listaProducao = new(ArquivoProducao.importarProducao(path, "Producao.dat"));
            List<ItemProducao> listaItemProducao = new(ArquivoProducao.importarItemProducao(path, "ItemProducao.dat"));
            //List<MPrima> listaMPrima = new(ArquivoProducao.importarMPrima(path, "Materia.dat"));
            //List<Produto> listaProduto = new(ArquivoProducao.importarProduto(path, "Cosmetico.dat"));
            var listaMPrima = new ManipularMPrima(path, "Materia.dat").Recuperar();
            var listaProduto = new ManipularProduto(path, "Cosmetico.dat").Recuperar();


            // Chama a função do Menu que carrega as demais funções
            Menu(listaProducao, listaItemProducao, listaMPrima, listaProduto);

            void Menu(List<Producao> listaProducao, List<ItemProducao> listaItemProducao, List<MPrima> listaMPrima, List<Produto> listaProduto)
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
                            ManipularProducao.inserirProducao(listaProducao, listaItemProducao, listaMPrima, listaProduto, path);
                            break;
                        case 2:
                            ManipularProducao.localizarProducao(listaProducao, listaItemProducao, listaMPrima, listaProduto);
                            break;
                        case 3:
                            ManipularProducao.excluirProducao(listaProducao, listaItemProducao, listaMPrima, listaProduto, path);
                            break;
                        case 4:
                            ManipularProducao.imprimirProducao(listaProducao, listaItemProducao, listaMPrima, listaProduto);
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
}
