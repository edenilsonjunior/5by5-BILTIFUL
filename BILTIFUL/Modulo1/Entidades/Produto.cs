namespace BILTIFUL.Modulo1
{
    internal class Produto
    {
        private string _codigoBarras;               //13  (0-12)
        private string _nome;                      //20  (13-32)

        public string CodigoBarras
        {
            get => _codigoBarras;
            set { _codigoBarras = Formatar(value, 13); }
        }
        public string Nome
        {
            get => _nome;
            set { _nome = Formatar(value, 20); }
        }

        public float ValorVenda { get; set; }      //5   (33-37)
        public DateOnly UltimaVenda { get; set; }  //8   (38-45)
        public DateOnly DataCadastro { get; set; } //8   (46-53)
        public char Situacao { get; set; }         //1   (54)

        /// <summary>
        /// Construtor da classe Produto.
        /// </summary>
        /// <param name="codigoBarras">O código de barras do produto.</param>
        /// <param name="nome">O nome do produto.</param>
        /// <param name="valorVenda">O valor de venda do produto.</param>
        public Produto(string codigoBarras, string nome, float valorVenda)
        {
            CodigoBarras = codigoBarras;
            Nome = nome;
            ValorVenda = valorVenda;
            UltimaVenda = DateOnly.FromDateTime(DateTime.Now);
            DataCadastro = DateOnly.FromDateTime(DateTime.Now);
            Situacao = 'A';
        }

        /// <summary>
        /// Construtor da classe Produto.
        /// </summary>
        /// <param name="data">Uma string contendo os dados do produto.</param>
        public Produto(string data)
        {
            CodigoBarras = data.Substring(0, 13);

            Nome = data.Substring(13, 20);
            ValorVenda = RecuperarValorVenda(data);

            UltimaVenda = DateOnly.ParseExact(data.Substring(38, 8), "ddMMyyyy", null);
            DataCadastro = DateOnly.ParseExact(data.Substring(46, 8), "ddMMyyyy", null);

            Situacao = char.Parse(data.Substring(54, 1));
        }



        /// <summary>
        /// Formata os dados do produto para serem salvos em um arquivo.
        /// </summary>
        /// <returns>Uma string contendo os dados formatados do produto.</returns>
        public string FormatarParaArquivo()
        {
            string data = "";
            string valorStr = $"{ValorVenda:000.00}";
            valorStr = valorStr.Replace(",", "");

            data += CodigoBarras;
            data += Nome;
            data += valorStr;

            data += UltimaVenda.ToString().Replace("/", "");
            data += DataCadastro.ToString().Replace("/", "");
            data += Situacao;

            return data;
        }

        /// <summary>
        /// Retorna uma string contendo as informações do produto formatadas para exibição.
        /// </summary>
        /// <returns>Uma string contendo as informações do produto.</returns>
        public string Print()
        {
            string situacao = Situacao == 'A' ? "Ativo" : "Inativo";

            string data = "";
            data += $"Codigo de Barras: {CodigoBarras}\n";
            data += $"Nome............: {Nome}\n";
            data += $"Valor de Venda..: {ValorVenda}\n";
            data += $"Ultima Venda....: {UltimaVenda}\n";
            data += $"Data de Cadastro: {DataCadastro}\n";
            data += $"Situacao........: {situacao}\n";

            return data;
        }



        /// <summary>
        /// Recupera o valor de venda do produto a partir dos dados formatados.
        /// </summary>
        /// <param name="data">Uma string contendo os dados formatados do produto.</param>
        /// <returns>O valor de venda do produto.</returns>
        private float RecuperarValorVenda(string data)
        {
            string valorVendaStr = data.Substring(33, 5);
            valorVendaStr = valorVendaStr.Insert(3, ",");
            return float.Parse(valorVendaStr);
        }



        /// <summary>
        /// Verifica se o código de barras é válido.
        /// </summary>
        /// <param name="cod">O código de barras a ser verificado.</param>
        /// <returns>True se o código de barras é válido, False caso contrário.</returns>
        public static bool VerificarCodigoBarras(string cod)
        {
            if (cod.Length != 13)
                return false;

            bool resultadoTry = int.TryParse(cod.Substring(0, 3), out int inicio);

            if (!resultadoTry)
                return false;

            return inicio == 789;
        }

        /// <summary>
        /// Formata uma string para ter um tamanho específico.
        /// </summary>
        /// <param name="n">A string a ser formatada.</param>
        /// <param name="tamanho">O tamanho desejado da string formatada.</param>
        /// <returns>A string formatada.</returns>
        private string Formatar(string n, int tamanho)
        {
            string nomeFormatado = n;
            // caso o nome tenha menos que tamanho
            while (nomeFormatado.Length < tamanho)
            {
                nomeFormatado += ' ';
            }

            // caso o nome tenha mais que tamanho
            nomeFormatado = nomeFormatado.Substring(0, tamanho);

            return nomeFormatado;
        }
    }
}