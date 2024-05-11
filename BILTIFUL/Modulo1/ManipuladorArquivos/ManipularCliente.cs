namespace BILTIFUL.Modulo1.ManipuladorArquivos
{
    internal class ManipularCliente
    {
        private readonly string _caminho;
        private readonly string _arquivo;

        public ManipularCliente(string caminho, string arquivo)
        {
            _caminho = caminho;
            _arquivo = arquivo;
            CriarDiretorioArquivo();
        }

        /// <summary>
        /// Recupera a lista de clientes a partir do arquivo.
        /// </summary>
        /// <returns>A lista de clientes recuperada.</returns>
        public List<Cliente> Recuperar()
        {
            List<Cliente> clientes = new();

            foreach (string linha in File.ReadAllLines(_caminho + _arquivo))
            {
                Cliente aux = new(linha);
                clientes.Add(aux);
            }

            return clientes;
        }

        /// <summary>
        /// Salva a lista de clientes no arquivo.
        /// </summary>
        /// <param name="clientes">A lista de clientes a ser salva.</param>
        public void Salvar(List<Cliente> clientes)
        {
            using var sw = new StreamWriter(_caminho + _arquivo);

            foreach (var item in clientes)
            {
                string data = item.FormatarParaArquivo();
                sw.WriteLine(data);
            }
        }

        /// <summary>
        /// Cadastra um novo cliente.
        /// </summary>
        public void Cadastrar()
        {
            string cpf, nome;
            char sexo;
            DateOnly dataNascimento;
            List<Cliente> clientes = Recuperar();

            cpf = LerCpf(verificaRegistros: true);
            nome = MainModulo1.LerString("Digite o nome: ");
            sexo = LerSexo();

            clientes.Add(new(cpf, nome, dataNascimento, sexo));
            Salvar(clientes);
        }

        /// <summary>
        /// Edita um cliente existente.
        /// </summary>
        public void Editar()
        {
            List<Cliente> clientes = Recuperar();
            string cpf;

            cpf = LerCpf(verificaRegistros: false);

            Cliente? c = clientes.Find(c => c.Cpf.Equals(cpf));

            if (c == null)
            {
                Console.WriteLine("O cpf digitado nao existe nos registros!");
                return;
            }

            int opcao;

            do
            {
                opcao = MenuEditar(c.Nome);

                switch (opcao)
                {
                    case 1:
                        c.Nome = MainModulo1.LerString("Digite o novo nome: ");
                        break;
                    case 2:
                        c.DataNascimento = MainModulo1.LerData("Digite a nova data de nascimento: ");
                        break;
                    case 3:
                        c.Sexo = LerSexo();
                        break;
                    case 4:
                        c.Situacao = (c.Situacao == 'A') ? 'I' : 'A';
                        break;
                    default:
                        Console.WriteLine("Opcao invalida!");
                        break;
                }

            } while (opcao != 0);

            Console.WriteLine("Usuario atualizado:");
            Console.WriteLine(c.Print());

        }

        /// <summary>
        /// Busca um cliente pelo CPF.
        /// </summary>
        /// <returns>O cliente encontrado ou null se não existir.</returns>
        public Cliente? BuscarPorCpf()
        {
            string cpf = LerCpf();

            var clientes = Recuperar();
            Cliente? c = clientes.Find(c => c.Cpf.Equals(cpf));

            if (c == null)
            {
                Console.WriteLine("O cpf digitado nao existe nos registros!");
                return null;
            }

            return c;
        }

        /// <summary>
        /// Localiza um cliente pelo CPF e exibe seus dados.
        /// </summary>
        public void Localizar()
        {
            string cpf = LerCpf(verificaRegistros: true);

            var clientes = Recuperar();
            Cliente? c = clientes.Find(c => c.Cpf.Equals(cpf));

            if (c == null)
            {
                Console.WriteLine("O cpf digitado nao existe nos registros!");
                return;
            }
            Console.WriteLine("Dados do cliente localizado: ");
            Console.WriteLine(c.Print());
        }

        /// <summary>
        /// Imprime a lista de clientes.
        /// </summary>
        public void Imprimir()
        {
            var clientes = Recuperar();

            if (clientes.Count == 0)
            {
                Console.WriteLine("Nenhum cliente cadastrado!");
                return;
            }

            Console.WriteLine("Lista de Clientes:");
            foreach (var item in clientes)
            {
                Console.WriteLine(item.Print());
                Console.WriteLine();
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

        /// <summary>
        /// Lê o sexo do cliente.
        /// </summary>
        /// <returns>O sexo do cliente.</returns>
        private char LerSexo()
        {
            char sexo;
            do
            {
                sexo = MainModulo1.LerChar("Digite o sexo(M-F):");

                if (sexo != 'M' && sexo != 'F') Console.WriteLine("Digito invalido!");

            } while (sexo != 'M' && sexo != 'F');

            return sexo;
        }

        /// <summary>
        /// Lê o CPF do cliente.
        /// </summary>
        /// <returns>O CPF do cliente.</returns>
        private string LerCpf()
        {
            string cpf;

            do
            {
                cpf = MainModulo1.LerString("Digite o cpf da pessoa que deseja editar: ");

                if (!Cliente.VerificarCpf(cpf)) Console.WriteLine("O cpf digitado é invalido");

            } while (!Cliente.VerificarCpf(cpf));

            return cpf;
        }

        /// <summary>
        /// Exibe o menu de edição do cliente.
        /// </summary>
        /// <param name="nomeCliente">O nome do cliente.</param>
        /// <returns>A opção selecionada.</returns>
        private int MenuEditar(string nomeCliente)
        {
            Console.Clear();
            Console.WriteLine("======Editar Cliente======");
            Console.WriteLine($"Cliente a ser editado: {nomeCliente}");
            Console.WriteLine("==========================\n");

            Console.WriteLine("Opcoes: ");
            Console.WriteLine("1- Editar nome");
            Console.WriteLine("2- Editar Data de nascimento");
            Console.WriteLine("3- Editar o sexo");
            Console.WriteLine("4- Inverter situacao");
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
                return MenuEditar(nomeCliente);
            }
        }

    }
}
