using BILTIFUL.Modulo4.Entidades;
using BILTIFUL.Modulo1;
using BILTIFUL.Modulo4.ManipuladorArquivos;
namespace BILTIFUL.Modulo4
{
    internal class MainModulo4
    {
        public MainModulo4()
        {
            string path = @"C:\BILTIFUL\";
            // Carrega Listas pelos arquivos da pasta
            ArquivoProducao.ChecarCaminho(path);
            List<Producao> listaProducao = new(ArquivoProducao.importarProducao(path, "Producao.dat"));
            List<ItemProducao> listaItemProducao = new(ArquivoProducao.importarItemProducao(path, "ItemProducao.dat"));
            List<MPrima> listaMPrima = new(ArquivoProducao.importarMPrima(path, "Materia.dat"));
            List<Produto> listaProduto = new(ArquivoProducao.importarProduto(path, "Cosmetico.dat"));

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
                    opcao = retornarInt();

                    switch (opcao)
                    {
                        case 0:
                            Console.WriteLine("Saindo do módulo Produção");
                            break;
                        case 1:
                            inserirProducao();
                            break;
                        case 2:
                            localizarProducao();
                            break;
                        case 3:
                            excluirProducao();
                            break;
                        case 4:
                            imprimirProducao();
                            break;
                        default:
                            Console.WriteLine("Opção inválida.");
                            Console.Write("Pressione qualquer tecla para continuar...");
                            Console.ReadKey();
                            break;
                    }
                }
            }
            void inserirProducao()
            {
                int Id, Contador = 0;
                DateOnly DataProducao = DateOnly.FromDateTime(DateTime.Now);
                string Cosmetico = "", opcao = "";
                float Quantidade = 0;
                bool Abortar = false;
                if (listaProducao.Count == 0)
                {
                    Id = 1;
                }
                else
                {
                    Id = listaProducao.Last().Id + 1;
                }

                Console.WriteLine("Informe o Produto a ser produzido (Cod.Barras):");
                Cosmetico = Console.ReadLine();

                if (listaProduto.Find(x => x.CodigoBarras == Cosmetico && x.Situacao.ToString() == "A") == null)
                {
                    do
                    {
                        Console.WriteLine("Atenção: O produto não existe ou não se encontra na situação Ativo!");
                        Console.WriteLine("Deseja tentar novamente?");
                        Console.WriteLine("[ S - Sim ] [ Qualquer tecla - Não ]");
                        opcao = Console.ReadLine();
                        if (opcao.ToLower() == "s")
                        {
                            Console.WriteLine("Informe o Produto a ser produzido (Cod.Barras):");
                            Cosmetico = Console.ReadLine();
                        }
                        else
                        {
                            Abortar = true;
                        }
                    } while ((listaProduto.Find(x => x.CodigoBarras == Cosmetico && x.Situacao.ToString() == "A") == null) && !Abortar);
                }
                if (!Abortar)
                {
                    Console.WriteLine("Informe a quantidade a ser produzida:");
                    Quantidade = retornarFloat();
                }
                if (Quantidade >= 1000 && !Abortar)
                {
                    do
                    {
                        Console.WriteLine("Atenção: A quantidade máxima permitida é de 999,99.");
                        Console.WriteLine("Informe a quantidade a ser produzida:");
                        Quantidade = retornarFloat();
                    }
                    while (Quantidade >= 1000);
                }

                // Add na lista item producao os itens de materia prima
                while (opcao.ToLower() != "n" && opcao.ToLower() != "x" && !Abortar)
                {
                    if (Contador > 1 && !Abortar)
                    {
                        Console.WriteLine("Deseja inserir mais uma matéria prima?\nS - Sim\nN - Nao\nX - Anular insercao");
                        opcao = Console.ReadLine();
                        if (opcao.ToLower() == "s")
                        {
                            ItemProducao tempitem = inserirItemProducao(Id);
                            if (tempitem.MateriaPrima != null)
                            {
                                listaItemProducao.Add(tempitem);
                            }
                            else
                            {
                                Console.WriteLine("Matéria Prima não inserida.");
                            }
                        }
                        else if (opcao.ToLower() == "x")
                        {
                            Abortar = true;
                        }
                    }
                    else
                    {
                        ItemProducao tempitem = inserirItemProducao(Id);
                        if (tempitem.MateriaPrima != null)
                        {
                            listaItemProducao.Add(tempitem);
                        }
                        else
                        {
                            Abortar = true;
                        }
                    }
                    Contador++;
                }

                // Por fim cria a producao em si
                if (opcao.ToLower() != "x" && !Abortar)
                {
                    Producao tempProducao = new(Id, DataProducao, Cosmetico, Quantidade);
                    listaProducao.Add(tempProducao);
                    ArquivoProducao.salvarArquivo(listaProducao, path, "Producao.dat");
                    ArquivoProducao.salvarArquivo(listaItemProducao, path, "ItemProducao.dat");
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
                string opcao = "";
                bool Abortar = false;
                DateOnly DataProducao = DateOnly.FromDateTime(DateTime.Now);
                string MateriaPrima = "";
                float QuantidadeMateriaPrima = 0;

                ItemProducao itemProducao = new();
                Console.WriteLine("Informe a Matéria Prima a ser utilizada (Código da MP):");
                MateriaPrima = Console.ReadLine().ToUpper();

                if (listaMPrima.Find(x => x.Id == MateriaPrima && x.Situacao.ToString() == "A") == null)
                {
                    do
                    {
                        Console.WriteLine("Atenção: A Matéria Prima não existe ou não se encontra na situação Ativa!");
                        Console.WriteLine("Deseja tentar novamente?");
                        Console.WriteLine("[ S - Sim ] [ Qualquer tecla - Não ]");
                        opcao = Console.ReadLine();
                        if (opcao.ToLower() == "s")
                        {
                            Console.WriteLine("Informe a Matéria Prima a ser utilizada (Código da MP):");
                            MateriaPrima = Console.ReadLine().ToUpper();
                        }
                        else
                        {
                            Abortar = true;
                        }
                    } while ((listaMPrima.Find(x => x.Id == MateriaPrima && x.Situacao.ToString() == "A") == null) && !Abortar);
                }

                if (!Abortar)
                {
                    Console.WriteLine("Informe a quantidade da matéria prima a ser utilizada:");
                    QuantidadeMateriaPrima = retornarFloat();
                    if (QuantidadeMateriaPrima >= 1000)
                    {
                        do
                        {
                            Console.WriteLine("Atenção: A quantidade máxima permitida é de 999,99.");
                            Console.WriteLine("Informe a quantidade da matéria prima a ser utilizada:");
                            QuantidadeMateriaPrima = retornarFloat();
                        }
                        while (QuantidadeMateriaPrima >= 1000);
                    }
                    itemProducao = new ItemProducao(Id, DataProducao, MateriaPrima, QuantidadeMateriaPrima);
                }
                return itemProducao;
            }
            void localizarProducao()
            {
                int Id;
                Console.WriteLine("Informe o Id da produção que deseja localizar:");
                Id = retornarInt();
                imprimirProducaoAux(Id);
                Console.WriteLine("Pressione qualquer tecla para continuar.");
                Console.ReadKey();
            }
            void excluirProducao()
            {
                string opcao = "";
                int Id;
                Console.WriteLine("Informe o Id da produção que deseja EXCLUIR:");
                Id = retornarInt();

                var producaoLocalizada = listaProducao.Find(x => x.Id == Id);
                if (producaoLocalizada != null)
                {
                    imprimirProducaoAux(Id);
                    Console.WriteLine("Pressione qualquer tecla para continuar.");
                    Console.ReadKey();
                    Console.WriteLine("Confirma exclusão da Produção?");
                    Console.WriteLine("[ S - Sim ] [ Qualquer tecla - Não ]");
                    opcao = Console.ReadLine();
                    if (opcao.ToLower() == "s")
                    {
                        listaItemProducao.RemoveAll(x => x.Id == Id);
                        listaProducao.Remove(producaoLocalizada);
                        ArquivoProducao.salvarArquivo(listaProducao, path, "Producao.dat");
                        ArquivoProducao.salvarArquivo(listaItemProducao, path, "ItemProducao.dat");
                        Console.WriteLine($"Exclusão do Id [ {Id} ] realizada com sucesso!");
                    }
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
                if (listaProducao.Count == 0)
                {
                    Console.WriteLine("Não há Produção cadastrada!");
                    Console.WriteLine("Pressione qualquer tecla para continuar.");
                    Console.ReadKey();
                }
                else
                {
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
                        opcao = retornarInt();
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
            }
            void imprimirProducaoAux(int Id)
            {
                var producaoLocalizada = listaProducao.Find(x => x.Id == Id);
                var producaoItemLocalizada = listaItemProducao.FindAll(x => x.Id == Id);

                if (producaoLocalizada != null)
                {
                    Console.Clear();
                    Console.WriteLine("-".PadLeft(115, '-'));
                    Console.WriteLine(producaoLocalizada.imprimirNaTela(listaProduto));
                    Console.WriteLine("-".PadLeft(115, '-'));
                    foreach (ItemProducao item in producaoItemLocalizada)
                    {
                        Console.WriteLine(item.imprimirNaTela(listaMPrima));
                    }
                    Console.WriteLine("-".PadLeft(115, '-'));
                }
                else
                {
                    Console.WriteLine("Não foi localizada uma Produção com esse Id.");
                }
            }
        }
        static float retornarFloat()
        {
            float Quantidade = 0;
            bool ex = false;

            while (!ex)
            {
                if (float.TryParse(Console.ReadLine(), out float qtde))
                {
                    Quantidade = qtde;
                    ex = true;
                }
                else
                {
                    Console.WriteLine("Formato inválido. Preencha por exemplo 0,01 1,00 10,00 100,00");
                }
            }
            return Quantidade;
        }
        static int retornarInt()
        {
            int Inteiro = 0;
            bool ex = false;

            while (!ex)
            {
                if (int.TryParse(Console.ReadLine(), out int varint))
                {
                    Inteiro = varint;
                    ex = true;
                }
                else
                {
                    Console.WriteLine("Formato inválido. Informe números inteiros apenas.");
                }
            }
            return Inteiro;
        }
    }
}
