using BILTIFUL.Modulo1;
using BILTIFUL.Modulo1.ManipuladorArquivos;
using BILTIFUL.Modulo4.Entidades;
using BILTIFUL.Modulo4.ManipuladorArquivos;

namespace BILTIFUL.Modulo4.Utils
{
    internal class FuncoesProducao
    {
        private string _path = @"C:\BILTIFUL\";
        private List<Producao> _listaProducao = new ManipuladorProducao(@"C:\BILTIFUL\", "Producao.dat").Recuperar();
        private List<ItemProducao> _listaItemProducao = new ManipuladorItemProducao(@"C:\BILTIFUL\", "ItemProducao.dat").Recuperar();
        private List<MPrima> _listaMPrima = new ManipularMPrima(@"C:\BILTIFUL\", "Materia.dat").Recuperar();
        private List<Produto> _listaProduto = new ManipularProduto(@"C:\BILTIFUL\", "Cosmetico.dat").Recuperar();
        public FuncoesProducao()
        {

        }
        /// <summary>
        /// Método que insere a Produção na lista de Produção.
        /// </summary>
        public void inserirProducao()
        {
            int Id, Contador = 0, ContadorItem = 1;
            DateOnly DataProducao = DateOnly.FromDateTime(DateTime.Now);
            string Cosmetico = "", opcao = "";
            float Quantidade = 0;
            bool Abortar = false;

            if (_listaProducao.Count == 0)
            {
                Id = 1;
            }
            else
            {
                Id = _listaProducao.Last().Id + 1;
            }

            Console.WriteLine("Informe o Produto a ser produzido (Cod.Barras):");
            Cosmetico = Console.ReadLine();

            if (_listaProduto.Find(x => x.CodigoBarras == Cosmetico && x.Situacao.ToString() == "A") == null)
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
                } while ((_listaProduto.Find(x => x.CodigoBarras == Cosmetico && x.Situacao.ToString() == "A") == null) && !Abortar);
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
                        ItemProducao tempitem = inserirItemProducao(Id, ContadorItem);
                        if (tempitem.MateriaPrima != null)
                        {
                            _listaItemProducao.Add(tempitem);
                        }
                        else
                        {
                            ContadorItem--;
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
                    ItemProducao tempitem = inserirItemProducao(Id, ContadorItem);
                    if (tempitem.MateriaPrima != null)
                    {
                        _listaItemProducao.Add(tempitem);
                    }
                    else
                    {
                        Abortar = true;
                    }
                }
                ContadorItem++;
                Contador++;
            }
            if (!Abortar)
            {
                Producao tempProducao = new(Id, DataProducao, Cosmetico, Quantidade);
                _listaProducao.Add(tempProducao);
                Extra.salvarArquivo(_listaProducao, _path, "Producao.dat");
                Extra.salvarArquivo(_listaItemProducao, _path, "ItemProducao.dat");
                Console.WriteLine("Produção criada com suceso!");
            }
            else
            {
                _listaItemProducao.RemoveAll(x => x.Id == Id);
                Console.WriteLine("Produção anulada!");
            }
            Console.WriteLine("Pressione qualquer tecla para continuar.");
            Console.ReadKey();
        }

        /// <summary>
        /// Método que insere a o item Produção na lista de ItemProdução.
        /// </summary>
        ItemProducao inserirItemProducao(int Id, int contadorItem)
        {
            string opcao = "";
            bool Abortar = false;
            DateOnly DataProducao = DateOnly.FromDateTime(DateTime.Now);
            string MateriaPrima = "";
            float QuantidadeMateriaPrima = 0;

            ItemProducao itemProducao = new();
            Console.WriteLine($"Informe a {contadorItem}a Matéria Prima a ser utilizada (MPxxxx):");
            MateriaPrima = Console.ReadLine().ToUpper();

            if (_listaMPrima.Find(x => x.Id == MateriaPrima && x.Situacao.ToString() == "A") == null)
            {
                do
                {
                    Console.WriteLine("Atenção: A Matéria Prima não existe ou não se encontra na situação Ativa!");
                    Console.WriteLine("Deseja tentar novamente?");
                    Console.WriteLine("[ S - Sim ] [ Qualquer tecla - Não ]");
                    opcao = Console.ReadLine();
                    if (opcao.ToLower() == "s")
                    {
                        Console.WriteLine($"Informe a {contadorItem}a Matéria Prima a ser utilizada (MPxxxx):");
                        MateriaPrima = Console.ReadLine().ToUpper();
                    }
                    else
                    {
                        Abortar = true;
                    }
                } while ((_listaMPrima.Find(x => x.Id == MateriaPrima && x.Situacao.ToString() == "A") == null) && !Abortar);
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

        /// <summary>
        /// Método que localiza a Produção.
        /// </summary>
        public void localizarProducao()
        {
            int Id;
            Console.WriteLine("Informe o Id da produção que deseja localizar:");
            Id = Extra.retornarInt();
            imprimirProducaoAux(Id);
            Console.WriteLine("Pressione qualquer tecla para continuar.");
            Console.ReadKey();
        }

        /// <summary>
        /// Método que exclui a Produção da lista de Produção.
        /// </summary>
        public void excluirProducao()
        {
            string opcao = "";
            int Id;

            Console.WriteLine("Informe o Id da produção que deseja EXCLUIR:");
            Id = Extra.retornarInt();

            var producaoLocalizada = _listaProducao.Find(x => x.Id == Id);
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
                    _listaItemProducao.RemoveAll(x => x.Id == Id);
                    _listaProducao.Remove(producaoLocalizada);
                    Extra.salvarArquivo(_listaProducao, _path, "Producao.dat");
                    Extra.salvarArquivo(_listaItemProducao, _path, "ItemProducao.dat");
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
        public void imprimirProducao()
        {
            int Id, IdInicial, IdFinal, opcao = 10;
            Producao listaTemporaria;

            if (_listaProducao.Count == 0)
            {
                Console.WriteLine("Não há Produção cadastrada!");
                Console.WriteLine("Pressione qualquer tecla para continuar.");
                Console.ReadKey();
            }
            else
            {
                IdInicial = _listaProducao.First().Id;
                Id = IdInicial;
                IdFinal = _listaProducao.Last().Id;
                imprimirProducaoAux(IdInicial);
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
                            imprimirProducaoAux(Id);
                            break;
                        case 1:
                            if (Id > IdInicial)
                            {
                                do
                                {
                                    Id--;
                                    listaTemporaria = _listaProducao.Find(x => x.Id == Id);
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
                                    listaTemporaria = _listaProducao.Find(x => x.Id == Id);
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
            var producaoLocalizada = _listaProducao.Find(x => x.Id == Id);
            var producaoItemLocalizada = _listaItemProducao.FindAll(x => x.Id == Id);

            if (producaoLocalizada != null)
            {
                Console.Clear();
                Console.WriteLine("-".PadLeft(115, '-'));
                Console.WriteLine(producaoLocalizada.imprimirNaTela(_listaProduto));
                Console.WriteLine("-".PadLeft(115, '-'));
                foreach (ItemProducao item in producaoItemLocalizada)
                {
                    Console.WriteLine(item.imprimirNaTela(_listaMPrima));
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
