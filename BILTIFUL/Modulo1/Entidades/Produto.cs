namespace BILTIFUL.Modulo1
{
    internal class Produto
    {
        private string _codigoBarras; //13  (0-12)
        private string _nome;//20  (13-32)


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

        public Produto(string codigoBarras, string nome, float valorVenda)
        {
            CodigoBarras = codigoBarras;
            Nome = nome;
            ValorVenda = valorVenda;
            UltimaVenda = DateOnly.FromDateTime(DateTime.Now);
            DataCadastro = DateOnly.FromDateTime(DateTime.Now);
            Situacao = 'A';
        }

        public Produto(string data)
        {
            CodigoBarras = data.Substring(0, 13);

            Nome = data.Substring(13, 20);
            ValorVenda = RecuperarValorVenda(data);

            UltimaVenda = DateOnly.ParseExact(data.Substring(38, 8), "ddMMyyyy", null);
            DataCadastro = DateOnly.ParseExact(data.Substring(46, 8), "ddMMyyyy", null);

            Situacao = char.Parse(data.Substring(54, 1));
        }


        // Metodos public
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

        // Metodos private
        private float RecuperarValorVenda(string data)
        {
            string valorVendaStr = data.Substring(33, 5);
            valorVendaStr = valorVendaStr.Insert(3, ",");
            return float.Parse(valorVendaStr);
        }

        // Metodos estaticos
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

        public static bool VerificarCodigoBarras(string cod)
        {
            if (cod.Length != 13)
                return false;

            bool resultadoTry = int.TryParse(cod.Substring(0, 3), out int inicio);

            if (!resultadoTry)
                return false;

            return inicio == 789;
        }
    }
}