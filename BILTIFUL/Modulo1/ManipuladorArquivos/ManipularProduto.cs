using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            CriarDiretorioArquivo();
        }

        /// <summary>
        /// Recupera a lista de produtos do arquivo.
        /// </summary>
        /// <returns>A lista de produtos.</returns>
        public List<Produto> Recuperar()
        {
            List<Produto> produtos = new();

            foreach (string linha in File.ReadAllLines(_caminho + _arquivo))
            {
                produtos.Add(new Produto(linha));
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
            string codigoBarras, nome;
            float valorVenda;
            List<Produto> produtos = Recuperar();

            do
            {
                codigoBarras = MainModulo1.LerString("Digite o código de barras: ");

                if (!Produto.VerificarCodigoBarras(codigoBarras))
                    Console.WriteLine("Código de barras inválido!");

            } while (!Produto.VerificarCodigoBarras(codigoBarras));

            if (produtos.Exists(p => p.CodigoBarras == codigoBarras))
            {
                Console.WriteLine("Código de barras já cadastrado!");
                return;
            }

            nome = MainModulo1.LerString("Digite o nome: ");

            do
            {
                valorVenda = MainModulo1.LerFloat("Digite o valor de venda: ");
                if (valorVenda < 0 || valorVenda > 999.99)
                    Console.WriteLine("Valor de venda inválido!");

            } while (valorVenda < 0 || valorVenda > 999.99);

            produtos.Add(new Produto(codigoBarras, nome, valorVenda));
            Salvar(produtos);
        }

        /// <summary>
        /// Edita um produto existente.
        /// </summary>
        public void Editar()
        {
            string codigoBarras;
            List<Produto> produtos = Recuperar();

            Produto produto = EscolherProduto();

            bool terminouEdicao = false;
            do
            {
                switch (MenuEditar(produto.Nome))
                {
                    case 0:
                        terminouEdicao = true;
                        break;
                    default:
                        break;
                }
            } while (!terminouEdicao);

            Console.WriteLine("Produto editado:");
            Console.WriteLine(produto.Print());

            Salvar(produtos);
        }

        /// <summary>
        /// Busca um produto pelo código de barras.
        /// </summary>
        /// <param name="codigoBarras">O código de barras.</param>
        /// <returns>O produto encontrado.</returns>
        public Produto BuscarPorCB(string codigoBarras)
        {
            List<Produto> produtos = Recuperar();

            Produto produto = produtos.Find(p => p.CodigoBarras == codigoBarras);

            return produto;
        }

        /// <summary>
        /// Localiza produtos pelo termo de busca.
        /// </summary>
        public void Localizar()
        {
            string termo;
            List<Produto> produtos = Recuperar();

            termo = MainModulo1.LerString("Digite o termo de busca: ");

            List<Produto> resultados = produtos.FindAll(p => p.Nome.Contains(termo));

            if (resultados.Count == 0)
            {
                Console.WriteLine("Nenhum produto encontrado!");
                return;
            }

            Console.WriteLine("Produtos encontrados:");
            foreach (var produto in resultados)
            {
                Console.WriteLine(produto);
            }
        }

        /// <summary>
        /// Imprime a lista de produtos.
        /// </summary>
        public void Imprimir()
        {
            List<Produto> produtos = Recuperar();

            Console.WriteLine("Lista de Produtos:");
            foreach (var produto in produtos)
            {
                Console.WriteLine(produto);
            }
        }



        /// <summary>
        /// Escolhe um produto da lista.
        /// </summary>
        /// <returns>O produto escolhido.</returns>
        private Produto EscolherProduto()
        {
            var produtos = Recuperar();

            return null;
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
        /// Cria o diretório e o arquivo se não existirem.
        /// </summary>
        private void CriarDiretorioArquivo()
        {
            if (!Directory.Exists(_caminho))
                Directory.CreateDirectory(_caminho);

            if (!File.Exists(_caminho + _arquivo))
            {
                var file = File.Create(_caminho + _arquivo);
                file.Close();
            }
        }
    }
}
