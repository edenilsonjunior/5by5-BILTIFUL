namespace BILTIFUL.Modulo4.Entidades
{
    internal class Produto
    {
        public string CodigoBarras { get; set; }   //13  (0-12)
        public string Nome { get; set; }           //20  (13-32)
        public float ValorVenda { get; set; }      //5   (33-37)
        public DateOnly UltimaVenda { get; set; }  //8   (38-45)
        public DateOnly DataCadastro { get; set; } //8   (46-53)
        public char Situacao { get; set; }         //1   (54)

        public Produto(string codigoBarras, string nome, float valorVenda)
        {
            CodigoBarras = codigoBarras;
            Nome = FormatarNome(nome);
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


        // Metodos
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

        private float RecuperarValorVenda(string data)
        {
            string valorVendaStr = data.Substring(33, 5);
            valorVendaStr = valorVendaStr.Insert(3, ",");
            return float.Parse(valorVendaStr);
        }

        private string FormatarNome(string n)
        {
            string nomeFormatado = n;
            // caso o nome tenha menos que 20
            while (nomeFormatado.Length < 20)
            {
                nomeFormatado += ' ';
            }

            // caso o nome tenha mais que 20
            nomeFormatado = nomeFormatado.Substring(0, 20);

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
