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

        /// <summary>
        /// Inicializa uma nova instância da classe MPrima com o ID e nome especificados.
        /// </summary>
        /// <param name="id">O ID.</param>
        /// <param name="nome">O nome.</param>
        public MPrima(string id, string nome)
        {
            Id = id;
            Nome = nome;
            UltimaCompra = DateOnly.FromDateTime(DateTime.Now);
            DataCadastro = DateOnly.FromDateTime(DateTime.Now);
            Situacao = 'A';
        }

        /// <summary>
        /// Inicializa uma nova instância da classe MPrima com base nos dados fornecidos.
        /// </summary>
        /// <param name="data">Os dados.</param>
        public MPrima(string data)
        {
            Id = data.Substring(0, 6);
            Nome = data.Substring(6, 20);
            UltimaCompra = DateOnly.ParseExact(data.Substring(26, 8), "ddMMyyyy", null);
            DataCadastro = DateOnly.ParseExact(data.Substring(34, 8), "ddMMyyyy", null);
            Situacao = char.Parse(data.Substring(42, 1));
        }


        /// <summary>
        /// Formata os dados da instância para serem gravados em um arquivo.
        /// </summary>
        /// <returns>Os dados formatados.</returns>
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

        /// <summary>
        /// Retorna uma representação em string da instância.
        /// </summary>
        /// <returns>A representação em string da instância.</returns>
        public string Print()
        {
            string situacao = Situacao == 'A' ? "Ativo" : "Inativo";
            string data = "";

            data += $"Id...........: {Id}\n";
            data += $"Nome.........: {Nome}\n";
            data += $"Ultima Compra: {UltimaCompra:dd/MM/yyyy}\n";
            data += $"Data Cadastro: {DataCadastro:dd/MM/yyyy}\n";
            data += $"Situacao.....: {situacao}";

            return data;
        }




        /// <summary>
        /// Formata uma string para o tamanho especificado.
        /// </summary>
        /// <param name="n">A string a ser formatada.</param>
        /// <param name="tamanho">O tamanho desejado.</param>
        /// <returns>A string formatada.</returns>
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



        /// <summary>
        /// Verifica se o ID fornecido é válido.
        /// </summary>
        /// <param name="id">O ID a ser verificado.</param>
        /// <returns>True se o ID for válido, caso contrário, False.</returns>
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
