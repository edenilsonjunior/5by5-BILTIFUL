namespace BILTIFUL.Modulo1.ManipuladorArquivos
{
    internal class ManipularProduto
    {
        private string _caminho;
        private string _arquivo;

        /// <summary>
        /// Inicializa uma nova instância da classe ManipularProduto.
        /// </summary>
        /// <param name="caminho">O caminho do diretório.</param>
        /// <param name="arquivo">O nome do arquivo.</param>
        public ManipularProduto(string caminho, string arquivo)
        {
            _caminho = caminho;
            _arquivo = arquivo;
            MainModulo1.CriarDiretorioArquivo(_caminho, _arquivo);
        }


        /// <summary>
        /// Recupera a lista de produtos do arquivo.
        /// </summary>
        /// <returns>A lista de produtos.</returns>
        public List<Produto> Recuperar()
        {
            var produtos = new List<Produto>();

            foreach (string linha in File.ReadAllLines(_caminho + _arquivo))
            {
                produtos.Add(new(linha));
            }

            return produtos;
        }


        /// <summary>
        /// Salva a lista de produtos no arquivo.
        /// </summary>
        /// <param name="produtos">A lista de produtos.</param>
        public void Salvar(List<Produto> produtos)
        {
            using var sw = new StreamWriter(_caminho + _arquivo);

            foreach (var item in produtos)
            {
                sw.WriteLine(item.FormatarParaArquivo());
            }
        }


        /// <summary>
        /// Cadastra um novo produto.
        /// </summary>
        public void Cadastrar()
        {
            Console.Clear();
            Console.WriteLine("=====Cadastrar novo produto=====");

            var produtos = Recuperar();
            string codigoBarras;

            bool existeCodigoBarras = true;
            do
            {
                codigoBarras = LerCodigoBarras();

                if(produtos.Exists(p => p.CodigoBarras.Equals(codigoBarras)))
                    Console.WriteLine("Código de barras inválido!");
                else
                    existeCodigoBarras = false;

            } while (existeCodigoBarras);


            string nome = MainModulo1.LerString("Digite o nome: ");
            float valorVenda = LerValorVenda();

            produtos.Add(new Produto(codigoBarras, nome, valorVenda));
            Salvar(produtos);

            Console.WriteLine(">>>Produto cadastrado!<<<");
        }


        /// <summary>
        /// Edita um produto existente.
        /// </summary>
        public void Editar()
        {
            Console.Clear();
            Console.WriteLine("=====Editar produto=====");
            var produtos = Recuperar();

            Produto? produto = produtos.Find(p => p.CodigoBarras == EscolherProduto());

            if(produto == null)
            {
                Console.WriteLine("Produto nao existe!");
                return;
            }

            bool terminouEdicao = false;
            do
            {
                switch (MenuEditar(produto.Nome))
                {
                    case 1:
                        produto.Nome = MainModulo1.LerString("Digite o novo nome: ");
                        break;
                    case 2:
                        produto.ValorVenda = LerValorVenda();
                        break;
                    case 0:
                        Console.WriteLine("Opcao de voltar...");
                        terminouEdicao = true;
                        break;
                    default:
                        break;
                }
            } while (!terminouEdicao);

            Console.WriteLine("\nProduto editado:");
            Console.WriteLine(produto.Print());

            Salvar(produtos);
        }


        /// <summary>
        /// Localiza produtos pelo termo de busca.
        /// </summary>
        public void Localizar()
        {
            Console.Clear();
            Console.WriteLine("=====Imprimir um produto especifico=====");

            Produto? produto = Recuperar().Find(p => p.CodigoBarras == EscolherProduto());

            if (produto == null)
            {
                Console.WriteLine("Produto nao existe!");
                return;
            }

            Console.WriteLine("Produto encontrado:");
            Console.WriteLine(produto.Print());
        }


        /// <summary>
        /// Imprime a lista de produtos.
        /// </summary>
        public void Imprimir()
        {
            Console.Clear();
            Console.WriteLine("=====Imprimir todos os produtos=====");

            var risco = Recuperar();

            if (risco.Count == 0)
            {
                Console.WriteLine("-->Nenhum produto cadastrado!");
                return;
            }

            int indice = 0;
            int opcao;

            do
            {
                bool numeroCerto = false;
                bool opcaoValida = true;
                bool isNumero = true;

                Console.Clear();
                do
                {
                    Console.WriteLine("Produto atual:");
                    Console.WriteLine(risco[indice].Print() + $"\n\n");
                    ExibirMenuImprimir(isNumero, opcaoValida);

                    if (int.TryParse(Console.ReadLine(), out opcao))
                    {
                        if (opcao >= 0 && opcao <= 4)
                            numeroCerto = opcaoValida = true;
                        else
                            opcaoValida = false;
                    }
                    else
                        isNumero = false;

                } while (!numeroCerto);

                switch (opcao)
                {
                    case 1:
                        indice = indice == risco.Count - 1 ? 0 : indice + 1;
                        break;
                    case 2:
                        indice = indice == 0 ? risco.Count - 1 : indice - 1;
                        break;
                    case 3:
                        indice = 0;
                        break;
                    case 4:
                        indice = risco.Count - 1;
                        break;
                }
            } while (opcao != 0);
        }

        /// <summary>
        /// Exibe o menu de impressão.
        /// </summary>
        /// <param name="isNumero">Se o usuario nao digitou um numero</param>
        /// <param name="opcaoValida">Se a opcao que o usuario digitou é invalida</param>
        private void ExibirMenuImprimir(bool isNumero, bool opcaoValida)
        {
            Console.WriteLine("Navegar pelos produtos:");
            Console.WriteLine("Opcoes: ");
            Console.WriteLine("1- Proximo da lista");
            Console.WriteLine("2- Anterior da lista");
            Console.WriteLine("3- Final da lista");
            Console.WriteLine("0- Parar navegacao");

            if (!isNumero)
                Console.WriteLine("Voce deve digitar um numero!");

            if (!opcaoValida)
                Console.WriteLine("Opcao invalida!");

            Console.Write("R: ");
        }


        /// <summary>
        /// Escolhe um produto da lista.
        /// </summary>
        /// <returns>O produto escolhido.</returns>
        private string EscolherProduto()
        {
            var produtos = Recuperar();
            var codigosBarras = new List<string>();
            int escolha;

            if(produtos.Count == 0)
            {
                return "";
            }

            // Adiciona os códigos de barras dos produtos à lista
            foreach (var produto in produtos)
                codigosBarras.Add(produto.CodigoBarras);


            Console.WriteLine("Escolha um produto pelo código de barras:");

            for (int i = 0; i < codigosBarras.Count; i++)
                Console.WriteLine($"{i + 1}. {codigosBarras[i]}");

            bool valido = false;
            do
            {
                Console.Write("Digite o número correspondente ao código de barras: ");

                if (int.TryParse(Console.ReadLine(), out escolha))
                    if (escolha < 1 || escolha > codigosBarras.Count)
                        Console.WriteLine("Índice inválido! Digite um número correspondente ao código de barras.");
                    else
                        valido = true;

            } while (!valido);

            return produtos[escolha - 1].CodigoBarras;
        }


        /// <summary>
        /// Exibe o menu de edição do produto.
        /// </summary>
        /// <param name="nomeProduto">O nome do produto.</param>
        /// <returns>A opção escolhida.</returns>
        private int MenuEditar(string nomeProduto)
        {
            Console.Clear();
            Console.WriteLine("======Editar Produto======");
            Console.WriteLine($"Produto a ser editado: {nomeProduto}");
            Console.WriteLine("==========================\n");

            Console.WriteLine("Opcoes: ");
            Console.WriteLine("1- Editar nome");
            Console.WriteLine("2- Editar valor de venda");
            Console.WriteLine("3- Inverter situacao");
            Console.WriteLine("0- Parar edicao");
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
                return MenuEditar(nomeProduto);
            }
        }


        /// <summary>
        /// Le o valor de venda
        /// </summary>
        /// <returns>
        /// O valor de venda digitado pelo usuario
        /// </returns>
        private float LerValorVenda()
        {
            float valorVenda;

            do
            {
                valorVenda = MainModulo1.LerFloat("Digite o valor de venda: ");
                if (valorVenda < 0 || valorVenda > 999.99)
                    Console.WriteLine("Valor de venda inválido!");

            } while (valorVenda < 0 || valorVenda > 999.99);

            return valorVenda;
        }


        /// <summary>
        /// Le o codigo de barras
        /// </summary>
        /// <returns>
        /// O codigo de barras digitado pelo usuario
        /// </returns>
        private string LerCodigoBarras()
        {
            string codigoBarras;
            bool valido = false;
            bool existe = true;
            var produtos = Recuperar();

            do
            {
                codigoBarras = MainModulo1.LerString("Digite o código de barras: ");

                if (!Produto.VerificarCodigoBarras(codigoBarras))
                    Console.WriteLine("Código de barras inválido!");
                else
                    valido = true;


                if (produtos.Exists(p => p.CodigoBarras == codigoBarras))
                    Console.WriteLine("Código de barras já cadastrado!");
                else
                    existe = false;


            } while (!valido || existe);

            return codigoBarras;
        }
    }
}
