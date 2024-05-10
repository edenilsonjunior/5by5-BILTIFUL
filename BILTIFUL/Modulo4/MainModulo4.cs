using BILTIFUL.Modulo4.Entidades;
using BILTIFUL.Modulo1;
using BILTIFUL.Modulo4.ManipuladorArquivos;
namespace BILTIFUL.Modulo4
{
    internal class MainModulo4
    {
        public MainModulo4()
        {
            // Carrega Listas pelos arquivos da pasta
            List<Producao> listaProducao = new(ArquivoProducao.importarProducao(@"C:\BILTIFUL\", "Producao.dat"));
            List<ItemProducao> listaItemProducao = new(ArquivoProducao.importarItemProducao(@"C:\BILTIFUL\", "ItemProducao.dat"));
            List<MPrima> listaMPrima = new(ArquivoProducao.importarMPrima(@"C:\BILTIFUL\", "Materia.dat"));
            List<Produto> listaProduto = new(ArquivoProducao.importarProduto(@"C:\BILTIFUL\", "Cosmetico.dat"));
            
            Menu(listaProducao, listaItemProducao, listaMPrima, listaProduto);
        }
        static void Menu(List<Producao> listaProducao, List<ItemProducao> listaItemProducao, List<MPrima> listaMPrima, List<Produto> listaProduto)
        {
            int opcao = -1;
            while (opcao != 0)
            {
                Console.Clear();
                Console.WriteLine("====== Cadastrar Produção ======");

                Console.WriteLine("Opção:");
                Console.WriteLine("1 - Cadastrar Produção");
                Console.WriteLine("2 - Localizar Produção");
                Console.WriteLine("3 - Excluir Produção");
                Console.WriteLine("4 - Impressao Produção");
                Console.WriteLine("0 - Voltar ao Menu Inicial");
                Console.Write("R: ");
                if (int.TryParse(Console.ReadLine(), out int option))
                {
                    switch (option)
                    {
                        // sair
                        case 0:
                            Console.WriteLine("Saindo do módulo Produção");
                            break;
                        // cadastrar
                        case 1:
                            inserirProducao(listaProducao, listaItemProducao, listaProduto);
                            break;
                        // localizar
                        case 2:
                            localizarProducao(listaProducao, listaItemProducao);
                            break;
                        // excluir
                        case 3:
                            excluirProducao(listaProducao, listaItemProducao);
                            break;
                        // impressao
                        case 4:
                            imprimirProducao(listaProducao, listaItemProducao);
                            break;
                        default:
                            Console.WriteLine("Opção inválida.");
                            Console.Write("Pressione qualquer tecla para continuar...");
                            Console.ReadKey();
                            break;
                    }
                    opcao = option;
                }
                else
                {
                    Console.WriteLine("Por favor, informe um número!");
                    Console.Write("Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    opcao = -1;
                }
            }
        }
        static void inserirProducao(List<Producao> listaProducao, List<ItemProducao> listaItemProducao, List<Produto> listaProduto)
        {
            int Id, Contador = 0;
            DateOnly DataProducao = DateOnly.FromDateTime(DateTime.Now);
            string Produto = "", opcao = "";
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
            Produto = Console.ReadLine();

            if (listaProduto.Find(x => x.CodigoBarras == Produto) == null)
            {
                do
                {
                    Console.WriteLine("Atenção: O produto não existe!");
                    Console.WriteLine("Deseja informar o produto novamente?");
                    Console.WriteLine("[ S - Sim ] [ Qualquer tecla - Não ]");
                    opcao = Console.ReadLine();
                    if (opcao == "s")
                    {
                        Console.WriteLine("Informe o Produto a ser produzido (Cod.Barras):");
                        Produto = Console.ReadLine();
                    }
                    else
                    {
                        Abortar = true;
                    }
                } while ((listaProduto.Find(x => x.Nome == Produto) == null) && !Abortar);
            }

            if (!Abortar)
            {
                Console.WriteLine("Informe a quantidade a ser produzida:");
                Quantidade = float.Parse(Console.ReadLine());
            }
            if (Quantidade >= 1000)
            {
                do
                {
                    Console.WriteLine("Atenção: A quantidade máxima permitida é de 999,99.");
                    Console.WriteLine("Deseja corrigir a quantidade?");
                    Console.WriteLine("[ S - Sim ] [ Qualquer tecla - Não ]");
                    opcao = Console.ReadLine();
                    if (opcao == "s")
                    {
                        Console.WriteLine("Informe a quantidade a ser produzida:");
                        Quantidade = float.Parse(Console.ReadLine());
                    }
                    else
                    {
                        Abortar = true;
                    }
                }
                while (Quantidade >= 1000 && !Abortar);
            }

            // Add na lista item producao os itens de materia prima
            while (opcao.ToLower() != "n" && opcao.ToLower() != "x" && !Abortar)
            {
                if (Contador > 1)
                {
                    Console.WriteLine("Deseja inserir mais uma matéria prima?\nS - Sim\nN - Nao\nX - Anular insercao");
                    opcao = Console.ReadLine();
                    if (opcao.ToLower() == "s")
                    {
                        listaItemProducao.Add(inserirItemProducao(Id));
                    }
                    else if (opcao.ToLower() == "x")
                    {
                        Abortar = true;
                    }
                }
                else
                {
                    listaItemProducao.Add(inserirItemProducao(Id));
                }
                Contador++;
            }

            // Por fim cria a producao em si
            if (opcao.ToLower() != "x" && !Abortar)
            {
                Producao tempProducao = new(Id, DataProducao, Produto, Quantidade);
                listaProducao.Add(tempProducao);
                ArquivoProducao.salvarArquivo(listaProducao, "Producao.dat");
                ArquivoProducao.salvarArquivo(listaItemProducao, "ItemProducao.dat");
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

        static ItemProducao inserirItemProducao(int Id)
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
        static void localizarProducao(List<Producao> listaProducao, List<ItemProducao> listaItemProducao)
        {
            Console.WriteLine("Informe o Id da produção que deseja localizar:");
            if (int.TryParse(Console.ReadLine(), out int Id))
            {
                imprimirProducaoAux(Id, listaProducao, listaItemProducao);
                Console.WriteLine("Pressione qualquer tecla para continuar.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Por favor, informe um número!");
                Console.Write("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }
        static void excluirProducao(List<Producao> listaProducao, List<ItemProducao> listaItemProducao)
        {
            string opcao = "";
            Console.WriteLine("Informe o Id da produção que deseja EXCLUIR:");
            if (int.TryParse(Console.ReadLine(), out int Id))
            {
                var producaoLocalizada = listaProducao.Find(x => x.Id == Id);

                if (producaoLocalizada != null)
                {
                    imprimirProducaoAux(Id, listaProducao, listaItemProducao);
                    Console.WriteLine("Pressione qualquer tecla para continuar.");
                    Console.ReadKey();
                    Console.WriteLine("Confirma exclusão da Produção?");
                    Console.WriteLine("[ S - Sim ] [ Qualquer tecla - Não ]");
                    opcao = Console.ReadLine();
                    if (opcao.ToLower() == "s")
                    {
                        listaItemProducao.RemoveAll(x => x.Id == Id);
                        listaProducao.Remove(producaoLocalizada);
                        ArquivoProducao.salvarArquivo(listaProducao, "Producao.dat");
                        ArquivoProducao.salvarArquivo(listaItemProducao, "ItemProducao.dat");
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
            else
            {
                Console.WriteLine("Por favor, informe um número!");
                Console.Write("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }
        static void imprimirProducao(List<Producao> listaProducao, List<ItemProducao> listaItemProducao)
        {
            int Id, IdInicial, IdFinal, opcao = 10;
            Producao listaTemporaria;
            IdInicial = listaProducao.First().Id;
            Id = IdInicial;
            IdFinal = listaProducao.Last().Id;
            imprimirProducaoAux(IdInicial, listaProducao, listaItemProducao);
            while (opcao != 9)
            {
                Console.WriteLine("Digite:");
                Console.WriteLine("[ 1 - Voltar ]           [ 2 - Avançar ]");
                Console.WriteLine("[ 0 - Voltar ao Início ] [ 3 - Avançar ao Final ]");
                Console.WriteLine("[ 9 - Sair ]");
                if (int.TryParse(Console.ReadLine(), out int option))
                {
                    switch (option)
                    {
                        case 0:
                            Id = IdInicial;
                            imprimirProducaoAux(Id, listaProducao, listaItemProducao);
                            break;
                        case 1:
                            if (Id > IdInicial)
                            {
                                do
                                {
                                    Id--;
                                    listaTemporaria = listaProducao.Find(x => x.Id == Id);
                                } while (listaTemporaria == null);
                                imprimirProducaoAux(Id, listaProducao, listaItemProducao);
                            }
                            else
                            {
                                imprimirProducaoAux(IdInicial, listaProducao, listaItemProducao);
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
                                imprimirProducaoAux(Id, listaProducao, listaItemProducao);
                            }
                            else
                            {
                                imprimirProducaoAux(IdFinal, listaProducao, listaItemProducao);
                                Console.WriteLine("Fim da Lista!");
                            }
                            break;
                        case 3:
                            Id = IdFinal;
                            imprimirProducaoAux(Id, listaProducao, listaItemProducao);
                            break;
                        case 9:
                            break;
                        default:
                            Console.WriteLine("Opção inválida.");
                            break;
                    }
                    opcao = option;
                }
                else
                {
                    Console.WriteLine("Por favor, informe um número!");
                    Console.Write("Pressione qualquer tecla para continuar...");
                    Console.WriteLine();
                    Console.ReadKey();
                }
            }
        }
        static void imprimirProducaoAux(int Id, List<Producao> listaProducao, List<ItemProducao> listaItemProducao)
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
