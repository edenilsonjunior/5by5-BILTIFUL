namespace BILTIFUL.Modulo1.ManipuladorArquivos
{
    internal class ManipularFornecedor
    {
        private readonly string _caminho;
        private readonly string _arquivo;

        public ManipularFornecedor(string caminho, string arquivo)
        {
            _caminho = caminho;
            _arquivo = arquivo;
        }


        // Metodos Principais
        public List<Fornecedor> Recuperar()
        {
            List<Fornecedor> fornecedores = new();

            foreach (string linha in File.ReadAllLines(_caminho + _arquivo))
            {
                Fornecedor aux = new(linha);
                fornecedores.Add(aux);
            }

            return fornecedores;
        }

        public void Salvar(List<Fornecedor> fornecedores)
        {
            fornecedores.Sort((f1, f2) => f1.RazaoSocial.CompareTo(f2.RazaoSocial));

            using var sw = new StreamWriter(_caminho + _arquivo);

            foreach (var item in fornecedores)
            {
                string data = item.FormatarParaArquivo();
                sw.WriteLine(data);
            }
        }

        public void Cadastrar()
        {
            string cnpj, razaoSocial;
            DateOnly dataAbertura;
            List<Fornecedor> fornecedores = Recuperar();

            do
            {
                cnpj = LerCnpj();

                if (fornecedores.Exists(c => c.Cnpj == cnpj))
                    Console.WriteLine("CNPJ já cadastrado!");

            } while (fornecedores.Exists(c => c.Cnpj == cnpj));

            razaoSocial = MainModulo1.LerString("Digite a razão social: ");
            dataAbertura = MainModulo1.LerData("Digite a data de abertura: ");

            fornecedores.Add(new(cnpj, razaoSocial, dataAbertura));
            Salvar(fornecedores);
        }

        public void Editar()
        {
            List<Fornecedor> fornecedores = Recuperar();

            Fornecedor? f = BuscarPorCnpj();

            if (f == null)
            {
                Console.WriteLine("Fornecedor não encontrado!");
                return;
            }

            bool continuar = true;
            do
            {
                switch (MenuEditar(f.RazaoSocial))
                {
                    case 1:
                        f.RazaoSocial = MainModulo1.LerString("Digite a nova razão social: ");
                        break;
                    case 2:
                        f.Situacao = f.Situacao == 'A' ? 'I' : 'A';
                        break;
                    case 0:
                        continuar = false;
                        return;
                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }
            } while (!continuar);

            Salvar(fornecedores);
        }

        public Fornecedor? BuscarPorCnpj()
        {
            List<Fornecedor> fornecedores = Recuperar();
            string cnpj = MainModulo1.LerString("Digite o CNPJ do fornecedor: ");

            return fornecedores.Find(f => f.Cnpj.Equals(cnpj));
        }

        public void Localizar()
        {
            Fornecedor? fornecedor = BuscarPorCnpj();

            if (fornecedor == null)
            {
                Console.WriteLine("Fornecedor não encontrado!");
                return;
            }

            Console.WriteLine("Fornecedor encontrado:");
            Console.WriteLine(fornecedor.Print());
        }

        public void Imprimir()
        {
            List<Fornecedor> fornecedores = Recuperar();

            if (fornecedores.Count == 0)
            {
                Console.WriteLine("Nenhum fornecedor cadastrado!");
                return;
            }

            Console.WriteLine("Lista de fornecedores");
            foreach (var item in fornecedores)
            {
                Console.WriteLine(item.Print());
                Console.WriteLine();
            }
        }


        // Metodos privados
        private string LerCnpj()
        {
            string cnpj;
            do
            {
                cnpj = MainModulo1.LerString("Digite o CNPJ: ");

                if (!Fornecedor.VerificarCnpj(cnpj))
                    Console.WriteLine("CNPJ inválido!");

            } while (!Fornecedor.VerificarCnpj(cnpj));

            return cnpj;
        }

        private int MenuEditar(string razaoSocial)
        {
            Console.Clear();
            Console.WriteLine("======Editar Fornecedor======");
            Console.WriteLine($"Fornecedor a ser editado: {razaoSocial}");
            Console.WriteLine("==========================\n");

            Console.WriteLine("Opcoes: ");
            Console.WriteLine("1- Editar razao social");
            Console.WriteLine("2- Inverter situacao");
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
                return MenuEditar(razaoSocial);
            }
        }
    }

}
