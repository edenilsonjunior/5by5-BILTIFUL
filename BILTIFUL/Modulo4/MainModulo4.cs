using BILTIFUL.Modulo4.Entidades;
using BILTIFUL.Modulo4.ManipuladorArquivos;
using BILTIFUL.Modulo1;

namespace BILTIFUL.Modulo4
{
    internal class MainModulo4
    {
        public MainModulo4()
        {
            // carrega listas pelos arquivos da pasta
            List<Producao> listaProducao = new(ManipuladorArquivosProducao.importarProducao(@"C:\BILTIFUL\", "Producao.dat"));
            List<ItemProducao> listaItemProducao = new(ManipuladorArquivosProducao.importarItemProducao(@"C:\BILTIFUL\", "ItemProducao.dat"));
            List<MPrima> listaMPrima = new(ManipuladorArquivosProducao.importarMPrima(@"C:\BILTIFUL\", "Materia.dat"));
            List<Produto> listaProduto = new(ManipuladorArquivosProducao.importarProduto(@"C:\BILTIFUL\", "Cosmetico.dat"));

            Menu(listaProducao, listaItemProducao);
        }
        static void Menu(List<Producao> listaProducao, List<ItemProducao> listaItemProducao)
        {
            int opcao = -1;
            while (opcao != 0)
            {
                Console.Clear();
                Console.WriteLine("Escolha sua opçao:");
                Console.WriteLine("1 - Cadastrar Producao");
                Console.WriteLine("2 - Localizar Producao");
                Console.WriteLine("3 - Excluir Producao");
                Console.WriteLine("4 - Impressao Producao");
                Console.WriteLine("0 - Voltar ao Menu Inicial");
                opcao = int.Parse(Console.ReadLine());
                switch (opcao)
                {
                    // sair
                    case 0:
                        break;
                    // cadastrar
                    case 1:
                        inserirProducao();
                        break;
                    // localizar
                    case 2:
                        localizarProducao();
                        break;
                    // excluir
                    case 3:
                        excluirProducao();
                        break;
                    // impressao
                    case 4:
                        imprimirProducao();
                        break;
                    default:
                        Console.WriteLine("Opcao invalida");
                        break;
                }
            }

            void inserirProducao()
            {
                int Id, contador = 0;
                DateOnly DataProducao = DateOnly.FromDateTime(DateTime.Now);
                string Produto = "", opcao = "";
                float Quantidade = 0;

                Console.WriteLine("Informe o Produto a ser produzido (Cod.Barras):");
                Produto = Console.ReadLine();
                Console.WriteLine("Informe a quantidade a ser produzida:");
                Quantidade = float.Parse(Console.ReadLine());
                Id = listaProducao.Last().Id + 1;

                // Add na lista item producao os itens de materia prima
                do
                {
                    if (contador > 1)
                    {
                        Console.WriteLine("Deseja inserir mais uma matéria prima?\nS - Sim\nN - Nao\nX - Anular insercao");
                        opcao = Console.ReadLine();
                        if (opcao.ToLower() == "s")
                        {
                            listaItemProducao.Add(inserirItemProducao(Id));
                        }
                    }
                    else
                    {
                        listaItemProducao.Add(inserirItemProducao(Id));
                    }
                    contador++;
                } while (opcao.ToLower() != "n" && opcao.ToLower() != "x");

                // Por fim cria a producao em si
                if (opcao.ToLower() != "x")
                {
                    Producao tempProducao = new(Id, DataProducao, Produto, Quantidade);
                    listaProducao.Add(tempProducao);
                    ManipuladorArquivosProducao.salvarArquivo(listaProducao, "Producao.dat");
                    ManipuladorArquivosProducao.salvarArquivo(listaItemProducao, "ItemProducao.dat");
                    Console.WriteLine("Produção criada com suceso!");
                }
                else
                {
                    listaItemProducao.RemoveAll(x => x.Id == Id); // se for escolhido anular, exclui da lista producao item os itens
                    Console.WriteLine("Produção anulada!");
                }
                Console.WriteLine("Pressione qualquer tecla para continuar.");
                Console.ReadKey();
            }
            ItemProducao inserirItemProducao(int Id)
            {
                DateOnly DataProducao = DateOnly.FromDateTime(DateTime.Now);
                string MateriaPrima = "";
                float QuantidadeMateriaPrima = 0;

                Console.WriteLine("Informe a Matéria Prima a ser utilizada (Código da MP):");
                MateriaPrima = Console.ReadLine();
                Console.WriteLine("Informe a quantidade da matéria prima a ser inserida");
                QuantidadeMateriaPrima = float.Parse(Console.ReadLine());

                ItemProducao itemProducao = new ItemProducao(Id, DataProducao, MateriaPrima, QuantidadeMateriaPrima);
                return itemProducao;
            }
            void localizarProducao()
            {
                int Id;
                Console.WriteLine("Informe o Id da produção que deseja localizar:");
                Id = int.Parse(Console.ReadLine());
                imprimirProducaoAux(Id);
                Console.WriteLine("Pressione qualquer tecla para continuar.");
                Console.ReadKey();
            }
            void excluirProducao()
            {
                int Id;
                Console.WriteLine("Informe o Id da produção que deseja EXCLUIR:");
                Id = int.Parse(Console.ReadLine());

                var producaoLocalizada = listaProducao.Find(x => x.Id == Id);

                if (producaoLocalizada != null)
                {
                    listaItemProducao.RemoveAll(x => x.Id == Id);
                    listaProducao.Remove(producaoLocalizada);
                    ManipuladorArquivosProducao.salvarArquivo(listaProducao, "Producao.dat");
                    ManipuladorArquivosProducao.salvarArquivo(listaItemProducao, "ItemProducao.dat");
                    Console.WriteLine($"Exclusão do Id [ {Id} ] realizada com sucesso!");
                }
                else
                {
                    Console.WriteLine("Não foi localizado uma Produção com esse Id, tente novamente.");
                }
                Console.WriteLine("Pressione qualquer tecla para continuar.");
                Console.ReadKey();
            }
            void imprimirProducao()
            {
                int Id, IdInicial, IdFinal, opcao = 10;
                Producao listaTemporaria;
                IdInicial = listaProducao.First().Id;
                Id = IdInicial;
                IdFinal = listaProducao.Last().Id;
                imprimirProducaoAux(IdInicial);
                while (opcao != 9)
                {
                    Console.WriteLine("Digite:");
                    Console.WriteLine("[ 1 - Voltar ]           [ 2 - Avançar ]");
                    Console.WriteLine("[ 0 - Voltar ao Início ] [ 3 - Avançar ao Final ]");
                    Console.WriteLine("[ 9 - Sair ]");
                    opcao = int.Parse(Console.ReadLine());
                    switch (opcao)
                    {
                        case 0:
                            Id = IdInicial;
                            imprimirProducaoAux(Id);
                            break;
                        case 1:
                            if (Id > IdInicial)
                            {
                                do
                                {
                                    Id--;
                                    listaTemporaria = listaProducao.Find(x => x.Id == Id);
                                } while (listaTemporaria == null);
                                imprimirProducaoAux(Id);
                            }
                            else
                            {
                                imprimirProducaoAux(IdInicial);
                                Console.WriteLine("Inicio da Lista!");
                            }
                            break;
                        case 2:
                            if (Id < IdFinal)
                            {
                                do
                                {
                                    Id++;
                                    listaTemporaria = listaProducao.Find(x => x.Id == Id);
                                } while (listaTemporaria == null);
                                imprimirProducaoAux(Id);
                            }
                            else
                            {
                                imprimirProducaoAux(IdFinal);
                                Console.WriteLine("Fim da Lista!");
                            }
                            break;
                        case 3:
                            Id = IdFinal;
                            imprimirProducaoAux(Id);
                            break;
                        case 9:
                            break;
                        default:
                            Console.WriteLine("Opção inválida.");
                            break;
                    }
                }
            }
            void imprimirProducaoAux(int Id)
            {
                var producaoLocalizada = listaProducao.Find(x => x.Id == Id);
                var producaoItemLocalizada = listaItemProducao.FindAll(x => x.Id == Id);

                if (producaoLocalizada != null)
                {
                    Console.Clear();
                    Console.WriteLine("-".PadLeft(110, '-'));
                    Console.WriteLine(producaoLocalizada.imprimirNaTela());
                    Console.WriteLine("-".PadLeft(110, '-'));
                    foreach (ItemProducao item in producaoItemLocalizada)
                    {
                        Console.WriteLine(item.imprimirNaTela());
                    }
                    Console.WriteLine("-".PadLeft(110, '-'));
                }
                else
                {
                    Console.WriteLine("Não foi localizada uma Produção com esse Id.");
                }
            }
        }
    }
}
