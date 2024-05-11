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
            CriarDiretorioArquivo(caminho, arquivo);
        }


        // Metodos Principais
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

        public void Salvar(List<Cliente> clientes)
        {
            using var sw = new StreamWriter(_caminho + _arquivo);

            foreach (var item in clientes)
            {
                string data = item.FormatarParaArquivo();
                sw.WriteLine(data);
            }
        }

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


        // Metodos privados
        private void CriarDiretorioArquivo(string caminho, string arquivo)
        {
            if (!Directory.Exists(caminho))
                Directory.CreateDirectory(caminho);

            if (!File.Exists(caminho + arquivo))
            {
                var file = File.Create(caminho + arquivo);
                file.Close();
            }
        }

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
