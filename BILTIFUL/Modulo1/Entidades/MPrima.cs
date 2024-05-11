namespace BILTIFUL.Modulo1
{
    internal class MPrima
    {
        private string _id;                         //6  (0-5)
        private string _nome;                       //20 (6-25)

        public string Id
        {
            get => _id;
            set { _id = Formatar(value, 6); }
        }
        public string Nome
        {
            get => _nome;
            set { _nome = Formatar(value, 20); }
        }

        public DateOnly UltimaCompra { get; set; }  //8 (26-33)
        public DateOnly DataCadastro { get; set; }  //8 (34-41)
        public char Situacao { get; set; }          //1 (42)

        public MPrima(string id, string nome)
        {
            Id = id;
            Nome = nome;
            UltimaCompra = DateOnly.FromDateTime(DateTime.Now);
            DataCadastro = DateOnly.FromDateTime(DateTime.Now);
            Situacao = 'A';
        }

        public MPrima(string data)
        {
            Id = data.Substring(0, 6);
            Nome = data.Substring(6, 20);
            UltimaCompra = DateOnly.ParseExact(data.Substring(26, 8), "ddMMyyyy", null);
            DataCadastro = DateOnly.ParseExact(data.Substring(34, 8), "ddMMyyyy", null);
            Situacao = char.Parse(data.Substring(42, 1));
        }

        // Metodos 
        public string FormatarParaArquivo()
        {
            string data = "";

            data += Id;
            data += Nome;
            data += UltimaCompra.ToString().Replace("/", "");
            data += DataCadastro.ToString().Replace("/", "");
            data += Situacao;

            return data;
        }

        public string Print()
        {
            string data = "";
            data += $"Id...........: {Id}\n";
            data += $"Nome.........: {Nome}\n";
            data += $"Ultima Compra: {UltimaCompra:dd/MM/yyyy}\n";
            data += $"Data Cadastro: {DataCadastro:dd/MM/yyyy}\n";
            data += $"Situacao.....: {Situacao}";

            return data;
        }


        // Metodos privados
        private string Formatar(string n, int tamanho)
        {
            string formatado = n;

            while (formatado.Length < tamanho)
            {
                formatado += ' ';
            }

            formatado = formatado.Substring(0, tamanho);

            return formatado;
        }


        // Metodos estaticos
        public static bool VerificarId(string id)
        {
            if (id.Length != 6)
                return false;

            string mp = id.Substring(0, 2);
            if (mp[0] != 'M' || mp[1] != 'P')
                return false;

            bool conversao = int.TryParse(id.Substring(2, 4), out _);
            if (!conversao)
                return false;

            return true;
        }
    }
}
