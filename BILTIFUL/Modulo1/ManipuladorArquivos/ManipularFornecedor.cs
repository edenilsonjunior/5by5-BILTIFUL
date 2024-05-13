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
            MainModulo1.CriarDiretorioArquivo(_caminho, _arquivo);
        }

        /// <summary>
        /// Recupera a lista de fornecedores do arquivo.
        /// </summary>
        /// <returns>A lista de fornecedores.</returns>
        public List<Fornecedor> Recuperar()
        {
            var fornecedores = new List<Fornecedor>();

            foreach (string linha in File.ReadAllLines(_caminho + _arquivo))
            {
                var aux = new Fornecedor(linha);
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
            Console.Clear();
            Console.WriteLine("=====Cadastrar Fornecedor=====");

            var fornecedores = Recuperar();

            string cnpj;

            bool existeCnpj = true;
            do
            {
                cnpj = LerCnpj();

                if (!fornecedores.Exists(c => c.Cnpj == cnpj))
                {
                    existeCnpj = false;
                }
                else
                {
                    Console.WriteLine("CNPJ já cadastrado!");
                }
            } while (existeCnpj);

            string razaoSocial = MainModulo1.LerString("Digite a razão social: ");
            var dataAbertura = MainModulo1.LerData("Digite a data de abertura: ");

            fornecedores.Add(new(cnpj, razaoSocial, dataAbertura));
            Salvar(fornecedores);
            Console.WriteLine(">>>Fornecedor adicionado!<<<");
        }


        /// <summary>
        /// Edita um fornecedor existente.
        /// </summary>
        public void Editar()
        {
            Console.Clear();
            Console.WriteLine("=====Editar Fornecedor=====");

            List<Fornecedor> fornecedores = Recuperar();
            string cnpj = MainModulo1.LerString("Digite o CNPJ do fornecedor: ");

            cnpj = Fornecedor.RemoverCaractere(cnpj);

            Fornecedor f = fornecedores.Find(f => f.Cnpj.Equals(cnpj));

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
                        break;
                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }
            } while (continuar);

            Console.WriteLine("\nFornecedor atualizado:");
            Console.WriteLine(f.Print());

            Salvar(fornecedores);
        }

        /// <summary>
        /// Localiza um fornecedor pelo CNPJ e exibe suas informações.
        /// </summary>
        public void Localizar()
        {
            Console.Clear();
            Console.WriteLine("Imprimir Fornecedor especifico");

            string cnpj = MainModulo1.LerString("Digite o CNPJ do fornecedor: ");
            Fornecedor? fornecedor = Recuperar().Find(f => f.Cnpj.Equals(cnpj));

            if (fornecedor == null)
            {
                Console.WriteLine("Fornecedor não encontrado!");
                return;
            }


            if (fornecedor != null)
            {
                Console.WriteLine("\nFornecedor encontrado:");
                Console.WriteLine(fornecedor.Print());
                return;
            }

            Console.WriteLine("Fornecedor não encontrado!");
        }


        /// <summary>
        /// Imprime a lista de fornecedores.
        /// </summary>
        public void Imprimir()
        {
            Console.Clear();
            Console.WriteLine("=====Imprimir todos os fornecedores=====");

            var fornecedores = Recuperar();

            if (fornecedores.Count == 0)
            {
                Console.WriteLine("Nenhum fornecedor cadastrado!");
                return;
            }

            int indice = 0;
            int opcao;

            do
            {
                bool numeroCerto = false;
                bool opcaoValida = true;
                bool isNumero = true;

                Console.Clear();
                do
                {
                    Console.WriteLine("Fornecedor atual:");
                    Console.WriteLine(fornecedores[indice].Print() + $"\n\n");
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
                        indice = indice == fornecedores.Count - 1 ? 0 : indice + 1;
                        break;
                    case 2:
                        indice = indice == 0 ? fornecedores.Count - 1 : indice - 1;
                        break;
                    case 3:
                        indice = 0;
                        break;
                    case 4:
                        indice = fornecedores.Count - 1;
                        break;
                }
            } while (opcao != 0);
        }


        /// <summary>
        /// Exibe o menu de impressão dos fornecedores.
        /// </summary>
        /// <param name="isNumero">Se o valor digitado é um número.</param>
        /// <param name="opcaoValida">Se a opcao é valida</param> 
        private void ExibirMenuImprimir(bool isNumero, bool opcaoValida)
        {
            Console.WriteLine("Navegar pelos fornecedores:");
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
        /// Lê o CNPJ do fornecedor.
        /// </summary>
        /// <returns>O CNPJ lido.</returns>
        private string LerCnpj()
        {
            var fornecedores = Recuperar();
            string cnpj;

            bool valido = false;
            bool existe = false;
            do
            {
                cnpj = MainModulo1.LerString("Digite o CNPJ: ");

                if (!Fornecedor.VerificarCnpj(cnpj))
                {
                    Console.WriteLine("CNPJ inválido!");
                }
                else
                {
                    valido = true;
                }

                if (fornecedores.Exists(c => c.Cnpj == cnpj))
                {
                    Console.WriteLine("CNPJ já cadastrado!");
                }
                else
                {
                    existe = true;
                }

            } while (!valido || !existe);

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

    }

}
