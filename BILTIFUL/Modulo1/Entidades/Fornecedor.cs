namespace BILTIFUL.Modulo1
{
    internal class Fornecedor
    {
        private string _cnpj;                         //14 (0-13)
        private string _razaoSocial;                  //50 (14-63)


        public string Cnpj
        {
            get => _cnpj;
            set { _cnpj = Formatar(value, 14); }
        }

        public string RazaoSocial
        {
            get => _razaoSocial;
            set { _razaoSocial = Formatar(value, 50); }
        }

        public DateOnly DataAbertura { get; set; }    //8 (64-71)
        public DateOnly UltimaCompra { get; set; }    //8 (72-79)
        public DateOnly DataCadastro { get; set; }    //8 (80-87)
        public char Situacao { get; set; }            //1 (88-88)

        /// <summary>
        /// Construtor da classe Fornecedor.
        /// </summary>
        /// <param name="cnpj">O CNPJ do fornecedor.</param>
        /// <param name="razaoSocial">A razão social do fornecedor.</param>
        /// <param name="dataAbertura">A data de abertura do fornecedor.</param>
        public Fornecedor(string cnpj, string razaoSocial, DateOnly dataAbertura)
        {
            cnpj = RemoverCaractere(cnpj);
            _cnpj = Formatar(cnpj, 14);
            _razaoSocial = Formatar(razaoSocial, 50);
            DataAbertura = dataAbertura;
            UltimaCompra = DateOnly.FromDateTime(DateTime.Now);
            DataCadastro = DateOnly.FromDateTime(DateTime.Now);
            Situacao = 'A';
        }

        /// <summary>
        /// Construtor da classe Fornecedor.
        /// </summary>
        /// <param name="data">Os dados do fornecedor em formato de string.</param>
        public Fornecedor(string data)
        {
            _cnpj = Formatar(data.Substring(0, 14), 14);
            _razaoSocial = Formatar(data.Substring(14, 50), 50);
            DataAbertura = DateOnly.ParseExact(data.Substring(64, 8), "ddMMyyyy", null);

            UltimaCompra = DateOnly.ParseExact(data.Substring(72, 8), "ddMMyyyy", null);
            DataCadastro = DateOnly.ParseExact(data.Substring(80, 8), "ddMMyyyy", null);

            Situacao = char.Parse(data.Substring(88, 1));
        }



        /// <summary>
        /// Formata os dados do fornecedor para serem salvos em um arquivo.
        /// </summary>
        /// <returns>Os dados formatados do fornecedor.</returns>
        public string FormatarParaArquivo()
        {
            string data = "";

            data += Cnpj;
            data += RazaoSocial;
            data += DataAbertura.ToString().Replace("/", "");
            data += UltimaCompra.ToString().Replace("/", "");
            data += DataCadastro.ToString().Replace("/", "");
            data += Situacao;

            return data;
        }

        /// <summary>
        /// Retorna uma string formatada com os dados do fornecedor.
        /// </summary>
        /// <returns>A string formatada com os dados do fornecedor.</returns>
        public string Print()
        {
            string situacao = Situacao == 'A' ? "Ativo" : "Inativo";
            string data = "";

            data += $"CNPJ.........: {Cnpj}\n";
            data += $"Razão Social.: {RazaoSocial}\n";
            data += $"Data Abertura: {DataAbertura:dd/MM/yyyy}\n";
            data += $"Ultima Compra: {UltimaCompra:dd/MM/yyyy}\n";
            data += $"Data Cadastro: {DataCadastro:dd/MM/yyyy}\n";
            data += $"Situação.....: {situacao}";
            return data;
        }



        /// <summary>
        /// Formata uma string para um tamanho específico.
        /// </summary>
        /// <param name="n">A string a ser formatada.</param>
        /// <param name="tamanho">O tamanho desejado da string formatada.</param>
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
        /// Verifica se o CNPJ é válido.
        /// </summary>
        /// <param name="cnpj">O CNPJ a ser verificado.</param>
        /// <returns>True se o CNPJ for válido, False caso contrário.</returns>
        public static bool VerificarCnpj(string cnpj)
        {
            cnpj = RemoverCaractere(cnpj);

            if (cnpj.Length != 14) return false;

            bool repetido = IsRepetido(cnpj);
            bool digitoUm = ValidacaoDigitoUm(cnpj);
            bool digitoDois = ValidacaoDigitoDois(cnpj);

            return !repetido && digitoUm && digitoDois;
        }

        /// <summary>
        /// Valida o primeiro dígito verificador do CNPJ.
        /// </summary>
        /// <param name="str">O CNPJ a ser validado.</param>
        /// <returns>True se o dígito for válido, False caso contrário.</returns>
        private static bool ValidacaoDigitoUm(string str)
        {
            int total = 0;
            int[] nrosMultiplicadores = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            for (int i = 0; i < 12; i++)
            {
                string digitoStr = str.Substring(i, 1);
                int digito = int.Parse(digitoStr);

                total += digito * nrosMultiplicadores[i];
            }

            int resto = total % 11;
            int digitoUm = int.Parse(str.Substring(12, 1));

            if ((resto == 0 || resto == 1) && digitoUm == 0)
                return true;

            if ((resto >= 2 && resto <= 10) && digitoUm == 11 - resto)
                return true;

            return false;
        }

        /// <summary>
        /// Valida o segundo dígito verificador do CNPJ.
        /// </summary>
        /// <param name="cnpj">O CNPJ a ser validado.</param>
        /// <returns>True se o dígito for válido, False caso contrário.</returns>
        private static bool ValidacaoDigitoDois(string cnpj)
        {
            int total = 0;
            int[] nrosMultiplicadores = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            for (int i = 0; i < 13; i++)
            {
                string digitoStr = cnpj.Substring(i, 1);
                int digito = int.Parse(digitoStr);

                total += digito * nrosMultiplicadores[i];
            }

            int resto = total % 11;
            int digitoDois = int.Parse(cnpj.Substring(13, 1));

            if ((resto == 0 || resto == 1) && digitoDois == 0)
                return true;

            if ((resto >= 2 && resto <= 10) && digitoDois == 11 - resto)
                return true;

            return false;
        }

        /// <summary>
        /// Verifica se há dígitos repetidos no CNPJ.
        /// </summary>
        /// <param name="cnpj">O CNPJ a ser verificado.</param>
        /// <returns>True se houver dígitos repetidos, False caso contrário.</returns>
        private static bool IsRepetido(string cnpj)
        {
            cnpj = RemoverCaractere(cnpj);
            int nroRepetidos = 0;
            for (int i = 0; i < cnpj.Length - 1; i++)
            {
                int n1 = int.Parse(cnpj.Substring(i, 1));
                int n2 = int.Parse(cnpj.Substring(i + 1, 1));

                if (n1 == n2)
                {
                    nroRepetidos++;
                }
            }
            return nroRepetidos == cnpj.Length - 1;
        }

        /// <summary>
        /// Remove caracteres especiais do CNPJ.
        /// </summary>
        /// <param name="cnpj">O CNPJ a ser formatado.</param>
        /// <returns>O CNPJ formatado.</returns>
        public static string RemoverCaractere(string cnpj)
        {
            cnpj = cnpj.Replace(".", "");
            cnpj = cnpj.Replace("/", "");
            cnpj = cnpj.Replace("-", "");
            return cnpj;
        }
    }
}
