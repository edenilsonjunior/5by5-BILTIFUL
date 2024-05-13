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
            MainModulo1.CriarDiretorioArquivo(_caminho, _arquivo);
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
                var aux = new Cliente(linha);
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


        /// <summary>m
        /// Cadastra um novo cliente.
        /// </summary>
        public void Cadastrar()
        {
            Console.Clear();
            Console.WriteLine("======Cadastrar Cliente======");

            var clientes = Recuperar();
            string cpf;
            bool cpfExiste = true;
            do
            {
                cpf = LerCpf();

                if (clientes.Exists(clientes => clientes.Cpf == cpf))
                {
                    Console.WriteLine("CPF já cadastrado!");
                }
                else
                {
                    cpfExiste = false;
                }

            } while (cpfExiste);

            string nome = MainModulo1.LerString("Digite o nome: ");
            char sexo = LerSexo();
            DateOnly dataNascimento = MainModulo1.LerData("Digite a data de nascimento: ");

            clientes.Add(new Cliente(cpf, nome, dataNascimento, sexo));
            Salvar(clientes);

            Console.WriteLine(">>>Cliente cadastrado com sucesso!<<<");
        }


        /// <summary>
        /// Edita um cliente existente.
        /// </summary>
        public void Editar()
        {
            Console.Clear();
            Console.WriteLine("======Editar Cliente======");

            var clientes = Recuperar();

            string cpf = LerCpf();

            Cliente? c = clientes.Find(c => c.Cpf.Equals(cpf));

            if (c == null)
            {
                Console.WriteLine("O CPF digitado nao coincide com nenhum cliente!");
                return;
            }

            bool terminouMenu = false;
            do
            {

                switch (MenuEditar(c.Nome))
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
                    case 0:
                        terminouMenu = true;
                        break;
                    default:
                        Console.WriteLine("Opcao invalida!");
                        break;
                }

            } while (!terminouMenu);

            Console.WriteLine("Usuario atualizado:");
            Console.WriteLine(c.Print());

            Salvar(clientes);
        }
        

        /// <summary>
        /// Localiza um cliente pelo CPF e exibe seus dados.
        /// </summary>
        public void Localizar()
        {
            Console.Clear();
            Console.WriteLine("=====Imprimir Cliente especifico");

            string cpf = LerCpf();

            Cliente? c = Recuperar().Find(c => c.Cpf.Equals(cpf));

            if (c == null)
            {
                Console.WriteLine("O CPF digitado nao coincide com nenhum cliente!");
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
            Console.Clear();
            Console.WriteLine("=====Imprimir todos os clientes=====");

            var clientes = Recuperar();

            if (clientes.Count == 0)
            {
                Console.WriteLine("Nenhum cliente cadastrado!");
                return;
            }

            int indice = 0;
            int opcao = -1;

            do
            {
                bool numeroCerto = false;
                bool opcaoValida = true;
                bool isNumero = true;

                Console.Clear();
                do
                {
                    Console.WriteLine("Cliente atual:");
                    Console.WriteLine(clientes[indice].Print() + $"\n\n");
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
                        indice = indice == clientes.Count - 1 ? 0 : indice + 1;
                        break;
                    case 2:
                        indice = indice == 0 ? clientes.Count - 1 : indice - 1;
                        break;
                    case 3:
                        indice = 0;
                        break;
                    case 4:
                        indice = clientes.Count - 1;
                        break;
                }
            } while (opcao != 0);
        }


        /// <summary>
        /// Exibe o menu de impressão dos clientes.
        /// </summary>
        /// <param name="isNumero">Se o valor digitado é um número.</param>
        /// <param name="opcaoValida">Se a opcao é valida</param>
        private void ExibirMenuImprimir(bool isNumero, bool opcaoValida)
        {
            Console.WriteLine("Navegar pelos clientes:");
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
                cpf = MainModulo1.LerString("Digite o cpf: ");

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
