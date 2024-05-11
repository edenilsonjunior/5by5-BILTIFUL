using BILTIFUL.Modulo1;
using BILTIFUL.Modulo4.Entidades;
using System.IO;
namespace BILTIFUL.Modulo4.Utils
{
    internal class ManipularProducao
    {
        public ManipularProducao()
        {

        }
        /// <summary>
        /// Método que insere a Produção na lista de Produção.
        /// </summary>
        public static void inserirProducao(List<Producao> listaProducao, List<ItemProducao> listaItemProducao, List<MPrima> listaMPrima, List<Produto> listaProduto, string path)
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
                Quantidade = Extra.retornarFloat();
            }
            if (Quantidade >= 1000 && !Abortar)
            {
                do
                {
                    Console.WriteLine("Atenção: A quantidade máxima permitida é de 999,99.");
                    Console.WriteLine("Informe a quantidade a ser produzida:");
                    Quantidade = Extra.retornarFloat();
                }
                while (Quantidade >= 1000);
            }
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

            ItemProducao inserirItemProducao(int Id) // funcao dentro da funcao
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
                    QuantidadeMateriaPrima = Extra.retornarFloat();
                    if (QuantidadeMateriaPrima >= 1000)
                    {
                        do
                        {
                            Console.WriteLine("Atenção: A quantidade máxima permitida é de 999,99.");
                            Console.WriteLine("Informe a quantidade da matéria prima a ser utilizada:");
                            QuantidadeMateriaPrima = Extra.retornarFloat();
                        }
                        while (QuantidadeMateriaPrima >= 1000);
                    }
                    itemProducao = new ItemProducao(Id, DataProducao, MateriaPrima, QuantidadeMateriaPrima);
                }
                return itemProducao;
            }
        }
        /// <summary>
        /// Método que localiza a Produção.
        /// </summary>
        public static void localizarProducao(List<Producao> listaProducao, List<ItemProducao> listaItemProducao, List<MPrima> listaMPrima, List<Produto> listaProduto)
        {
            int Id;
            Console.WriteLine("Informe o Id da produção que deseja localizar:");
            Id = Extra.retornarInt();
            imprimirProducaoAux(Id, listaProducao, listaItemProducao, listaMPrima, listaProduto);
            Console.WriteLine("Pressione qualquer tecla para continuar.");
            Console.ReadKey();
        }
        /// <summary>
        /// Método que exclui a Produção da lista de Produção.
        /// </summary>
        public static void excluirProducao(List<Producao> listaProducao, List<ItemProducao> listaItemProducao, List<MPrima> listaMPrima, List<Produto> listaProduto, string path)
        {
            string opcao = "";
            int Id;
            Console.WriteLine("Informe o Id da produção que deseja EXCLUIR:");
            Id = Extra.retornarInt();

            var producaoLocalizada = listaProducao.Find(x => x.Id == Id);
            if (producaoLocalizada != null)
            {
                imprimirProducaoAux(Id, listaProducao, listaItemProducao, listaMPrima, listaProduto);
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
        /// <summary>
        /// Método que imprime a lista de Produção.
        /// </summary>
        public static void imprimirProducao(List<Producao> listaProducao, List<ItemProducao> listaItemProducao, List<MPrima> listaMPrima, List<Produto> listaProduto)
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
                imprimirProducaoAux(IdInicial, listaProducao, listaItemProducao, listaMPrima, listaProduto);
                while (opcao != 9)
                {
                    Console.WriteLine("Digite:");
                    Console.WriteLine("[ 1 - Voltar ]           [ 2 - Avançar ]");
                    Console.WriteLine("[ 0 - Voltar ao Início ] [ 3 - Avançar ao Final ]");
                    Console.WriteLine("[ 9 - Sair ]");
                    opcao = Extra.retornarInt();
                    switch (opcao)
                    {
                        case 0:
                            Id = IdInicial;
                            imprimirProducaoAux(Id, listaProducao, listaItemProducao, listaMPrima, listaProduto);
                            break;
                        case 1:
                            if (Id > IdInicial)
                            {
                                do
                                {
                                    Id--;
                                    listaTemporaria = listaProducao.Find(x => x.Id == Id);
                                } while (listaTemporaria == null);
                                imprimirProducaoAux(Id, listaProducao, listaItemProducao, listaMPrima, listaProduto);
                            }
                            else
                            {
                                imprimirProducaoAux(IdInicial, listaProducao, listaItemProducao, listaMPrima, listaProduto);
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
                                imprimirProducaoAux(Id, listaProducao, listaItemProducao, listaMPrima, listaProduto);
                            }
                            else
                            {
                                imprimirProducaoAux(IdFinal, listaProducao, listaItemProducao, listaMPrima, listaProduto);
                                Console.WriteLine("Fim da Lista!");
                            }
                            break;
                        case 3:
                            Id = IdFinal;
                            imprimirProducaoAux(Id, listaProducao, listaItemProducao, listaMPrima, listaProduto);
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
        public static void imprimirProducaoAux(int Id, List<Producao> listaProducao, List<ItemProducao> listaItemProducao, List<MPrima> listaMPrima, List<Produto> listaProduto)
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
}
