﻿namespace BILTIFUL.Modulo1.ManipuladorArquivos
{
    internal class ManipularFornecedor
    {
        private readonly string _caminho;
        private readonly string _arquivo;

        public ManipularFornecedor(string caminho, string arquivo)
        {
            _caminho = caminho;
            _arquivo = arquivo;
            CriarDiretorioArquivo();
        }

        /// <summary>
        /// Recupera a lista de fornecedores do arquivo.
        /// </summary>
        /// <returns>A lista de fornecedores.</returns>
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

        /// <summary>
        /// Salva a lista de fornecedores no arquivo.
        /// </summary>
        /// <param name="fornecedores">A lista de fornecedores a ser salva.</param>
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

        /// <summary>
        /// Cadastra um novo fornecedor.
        /// </summary>
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

        /// <summary>
        /// Edita um fornecedor existente.
        /// </summary>
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

        /// <summary>
        /// Busca um fornecedor pelo CNPJ.
        /// </summary>
        /// <returns>O fornecedor encontrado ou null se não encontrado.</returns>
        public Fornecedor? BuscarPorCnpj()
        {
            List<Fornecedor> fornecedores = Recuperar();
            string cnpj = MainModulo1.LerString("Digite o CNPJ do fornecedor: ");

            return fornecedores.Find(f => f.Cnpj.Equals(cnpj));
        }

        /// <summary>
        /// Localiza um fornecedor pelo CNPJ e exibe suas informações.
        /// </summary>
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

        /// <summary>
        /// Imprime a lista de fornecedores.
        /// </summary>
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



        /// <summary>
        /// Lê o CNPJ do fornecedor.
        /// </summary>
        /// <returns>O CNPJ lido.</returns>
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

        /// <summary>
        /// Exibe o menu de edição do fornecedor.
        /// </summary>
        /// <param name="razaoSocial">A razão social do fornecedor.</param>
        /// <returns>A opção selecionada.</returns>
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
    }

}
