namespace BILTIFUL.Modulo1
{
    internal class Cliente
    {
        private string _cpf;                            //11 (0-10)
        private string _nome;                           //50 (11-60)

        public string Cpf
        {
            get => _cpf;
            set { _cpf = RemoverCaractere(value); }
        }

        public string Nome
        {
            get => _nome;
            set { _nome = FormatarNome(value); }
        }

        public DateOnly DataNascimento { get; set; }    //8  (61-68)
        public char Sexo { get; set; }                  //1  (69-69)
        public DateOnly UltimaCompra { get; set; }      //8  (70-77)
        public DateOnly DataCadastro { get; set; }      //8  (78-85)
        public char Situacao { get; set; }              //1  (86-86)

        /// <summary>
        /// Construtor da classe Cliente.
        /// </summary>
        /// <param name="cpf">O CPF do cliente.</param>
        /// <param name="nome">O nome do cliente.</param>
        /// <param name="dataNascimento">A data de nascimento do cliente.</param>
        /// <param name="sexo">O sexo do cliente.</param>
        public Cliente(string cpf, string nome, DateOnly dataNascimento, char sexo)
        {
            Cpf = cpf;
            Nome = nome;
            DataNascimento = dataNascimento;
            Sexo = sexo;
            DataCadastro = DateOnly.FromDateTime(DateTime.Now);
            UltimaCompra = DateOnly.FromDateTime(DateTime.Now);
            Situacao = 'A';
        }

        /// <summary>
        /// Construtor da classe Cliente.
        /// </summary>
        /// <param name="data">Os dados do cliente em formato de string.</param>
        public Cliente(string data)
        {
            Cpf = data.Substring(0, 11);
            Nome = data.Substring(11, 50);

            DataNascimento = DateOnly.ParseExact(data.Substring(61, 8), "ddMMyyyy", null);
            Sexo = char.Parse(data.Substring(69, 1));

            UltimaCompra = DateOnly.ParseExact(data.Substring(70, 8), "ddMMyyyy", null);
            DataCadastro = DateOnly.ParseExact(data.Substring(78, 8), "ddMMyyyy", null);

            Situacao = char.Parse(data.Substring(86, 1));
        }


        /// <summary>
        /// Formata os dados do cliente para serem salvos em um arquivo.
        /// </summary>
        /// <returns>Os dados do cliente formatados em uma string.</returns>
        public string FormatarParaArquivo()
        {
            string data = "";

            data += Cpf;
            data += Nome;
            data += DataNascimento.ToString().Replace("/", "");
            data += Sexo;
            data += UltimaCompra.ToString().Replace("/", "");
            data += DataCadastro.ToString().Replace("/", "");
            data += Situacao;

            return data;
        }

        /// <summary>
        /// Retorna uma representação em string dos dados do cliente.
        /// </summary>
        /// <returns>Uma string contendo os dados do cliente formatados.</returns>
        public string Print()
        {
            string situacao = Situacao == 'A' ? "Ativo" : "Inativo";

            string data = "";
            data += $"CPF.............: {Cpf}\n";
            data += $"Nome............: {Nome}\n";
            data += $"Data de nasc....: {DataNascimento}\n";
            data += $"Sexo............: {Sexo}\n";
            data += $"Ultima Compra...: {UltimaCompra}\n";
            data += $"Data de cadastro: {DataCadastro}\n";
            data += $"Situacao........: {situacao}";

            return data;
        }


        // metodos privados

        /// <summary>
        /// Formata o nome do cliente.
        /// </summary>
        /// <param name="n">O nome do cliente.</param>
        /// <returns>O nome do cliente formatado.</returns>
        private string FormatarNome(string n)
        {
            string nomeFormatado = n;

            // caso o nome tenha menos que 50
            while (nomeFormatado.Length < 50)
            {
                nomeFormatado += ' ';
            }

            // caso o nome tenha mais que 50
            nomeFormatado = nomeFormatado.Substring(0, 50);

            return nomeFormatado;
        }

        // Metodos estaticos

        /// <summary>
        /// Verifica se um CPF é válido.
        /// </summary>
        /// <param name="cpf">O CPF a ser verificado.</param>
        /// <returns>True se o CPF for válido, False caso contrário.</returns>
        public static bool VerificarCpf(string cpf)
        {
            cpf = RemoverCaractere(cpf);

            if (cpf.Length != 11)
                return false;


            bool v1 = IsRepetido(cpf);
            bool v2 = ValidacaoDigitoUm(cpf);
            bool v3 = ValidacaoDigitoDois(cpf);

            Console.WriteLine(cpf);
            Console.WriteLine(v1);
            Console.WriteLine(v2);
            Console.WriteLine(v3);

            return !IsRepetido(cpf) && ValidacaoDigitoUm(cpf) && ValidacaoDigitoDois(cpf);
        }

        /// <summary>
        /// Remove caracteres especiais de um CPF.
        /// </summary>
        /// <param name="cpf">O CPF a ser formatado.</param>
        /// <returns>O CPF formatado.</returns>
        private static string RemoverCaractere(string cpf)
        {
            cpf.Replace(".", "");
            cpf.Replace("-", "");

            return cpf;
        }

        /// <summary>
        /// Verifica se um CPF possui dígitos repetidos.
        /// </summary>
        /// <param name="str">O CPF a ser verificado.</param>
        /// <returns>True se o CPF possuir dígitos repetidos, False caso contrário.</returns>
        private static bool IsRepetido(string str)
        {

            int nroRepetidos = 0;
            for (int i = 0; i < str.Length - 1; i++)
            {
                int n1 = int.Parse(str.Substring(i, 1));
                int n2 = int.Parse(str.Substring(i + 1, 1));

                if (n1 == n2)
                {
                    nroRepetidos++;
                }
            }

            return nroRepetidos == str.Length - 1;
        }

        /// <summary>
        /// Realiza a validação do primeiro dígito verificador de um CPF.
        /// </summary>
        /// <param name="str">O CPF a ser validado.</param>
        /// <returns>True se o primeiro dígito verificador for válido, False caso contrário.</returns>
        private static bool ValidacaoDigitoUm(string str)
        {
            int resultado = 0;
            int multiplica = 10;

            for (int i = 0; i < 9; i++)
            {
                int digito = int.Parse(str.Substring(i, 1));
                resultado += digito * multiplica;
                multiplica--;
            }

            // O resto deve ser igual ao primeiro digito verificador
            int resto = (resultado * 10) % 11;

            if (resto == 10)
            {
                resto = 0;
            }

            int digitoUm = int.Parse(str.Substring(9, 1));

            return resto == digitoUm;
        }

        /// <summary>
        /// Realiza a validação do segundo dígito verificador de um CPF.
        /// </summary>
        /// <param name="str">O CPF a ser validado.</param>
        /// <returns>True se o segundo dígito verificador for válido, False caso contrário.</returns>
        private static bool ValidacaoDigitoDois(string str)
        {
            int resultado = 0;
            for (int i = 0, multiplica = 11; i < 10; i++, multiplica--)
            {
                int digito = int.Parse(str.Substring(i, 1));
                resultado += digito * multiplica;
            }

            int resto = (resultado * 10) % 11;

            int digito2 = int.Parse(str.Substring(10, 1));

            return resto == digito2;
        }
    }
}
