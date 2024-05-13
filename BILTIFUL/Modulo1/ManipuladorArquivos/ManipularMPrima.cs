namespace BILTIFUL.Modulo1.ManipuladorArquivos
{
    internal class ManipularMPrima
    {
        private string _caminho;
        private string _arquivo;

        /// <summary>
        /// Construtor da classe ManipularMPrima.
        /// </summary>
        /// <param name="caminho">O caminho do diretório.</param>
        /// <param name="arquivo">O nome do arquivo.</param>
        public ManipularMPrima(string caminho, string arquivo)
        {
            _caminho = caminho;
            _arquivo = arquivo;
            MainModulo1.CriarDiretorioArquivo(_caminho, _arquivo);
        }


        /// <summary>
        /// Recupera a lista de matérias-primas do arquivo.
        /// </summary>
        /// <returns>A lista de matérias-primas.</returns>
        public List<MPrima> Recuperar()
        {
            var materias = new List<MPrima>();

            foreach (string linha in File.ReadAllLines(_caminho + _arquivo))
            {
                var aux = new MPrima(linha);
                materias.Add(aux);
            }

            return materias;
        }


        /// <summary>
        /// Salva a lista de matérias-primas no arquivo.
        /// </summary>
        /// <param name="listaMPrima">A lista de matérias-primas a ser salva.</param>
        public void Salvar(List<MPrima> listaMPrima)
        {
            // ordena a lista antes de salvar
            listaMPrima.Sort((x, y) => x.Id.CompareTo(y.Id));

            using var sw = new StreamWriter(_caminho + _arquivo);

            foreach (var item in listaMPrima)
            {
                string data = item.FormatarParaArquivo();
                sw.WriteLine(data);
            }
        }


        /// <summary>
        /// Cadastra uma nova matéria-prima.
        /// </summary>
        public void Cadastrar()
        {
            Console.Clear();
            Console.WriteLine("=====Cadastrar Matéria-prima=====");

            string nome = MainModulo1.LerString("Digite o nome: ");

            int idInt = GetMaiorId();
            string id = $"MP{idInt:0000}";

            var materias = Recuperar();
            materias.Add(new(id, nome));
            Salvar(materias);
            Console.WriteLine(">>>Materia prima cadastrada!<<<");
        }


        /// <summary>
        /// Edita uma matéria-prima existente.
        /// </summary>
        public void Editar()
        {
            Console.Clear();
            Console.WriteLine("======Editar Matéria-prima======");

            var materias = Recuperar();
            MPrima? materia = BuscarPorId();
            if (materia == null)
            {
                Console.WriteLine("Matéria-prima não encontrada.");
                return;
            }

            bool parada = false;

            do
            {
                switch (MenuEditar(materia.Nome))
                {
                    case 1:
                        materia.Nome = MainModulo1.LerString("Digite o novo nome: ");
                        break;

                    case 2:
                        materia.Situacao = materia.Situacao == 'A' ? 'I' : 'A';
                        break;
                    case 0:
                        Console.WriteLine("Voltando...");
                        parada = true;
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }

            } while (!parada);

            Console.WriteLine("Matéria-prima editada:");
            Console.WriteLine(materia.Print());

            Salvar(materias);
        }


        /// <summary>
        /// Busca uma matéria-prima pelo seu ID.
        /// </summary>
        /// <returns>A matéria-prima encontrada ou null se não encontrada.</returns>
        public MPrima? BuscarPorId()
        {
            string id = LerId();

            var materias = Recuperar();

            return materias.Find(x => x.Id == id);
        }


        /// <summary>
        /// Localiza uma matéria-prima pelo seu ID e exibe suas informações.
        /// </summary>
        public void Localizar()
        {
            Console.Clear();
            Console.WriteLine("=====Imprimir materia-prima especifica:");

            MPrima? materia = BuscarPorId();

            if (materia != null)
            {
                Console.WriteLine(materia.Print());
                return;
            }

            Console.WriteLine("Matéria-prima não encontrada.");
        }


        /// <summary>
        /// Imprime a lista de matérias-primas.
        /// </summary>
        public void Imprimir()
        {
            Console.Clear();
            Console.WriteLine("=====Imprimir todas as matérias-primas=====");
            List<MPrima> materias = Recuperar();

            if (materias.Count != 0)
            {
                foreach (var item in materias)
                {
                    Console.WriteLine(item.Print());
                    Console.WriteLine("---------------");
                }
                return;
            }

            Console.WriteLine("Nenhuma matéria-prima cadastrada.");
        }




        /// <summary>
        /// Obtém o maior ID das matérias-primas cadastradas.
        /// </summary>
        /// <returns>O maior ID das matérias-primas.</returns>
        private int GetMaiorId()
        {
            List<MPrima> materias = Recuperar();

            if (materias.Count == 0)
                return 1;

            int maior = 0;
            foreach (var item in materias)
            {
                int id;

                if (int.TryParse(item.Id.Substring(2), out id))
                {
                    if (id > maior)
                        maior = id;
                }
            }

            maior++;

            return maior;
        }


        /// <summary>
        /// Lê o ID da matéria-prima.
        /// </summary>
        /// <returns>O ID da matéria-prima.</returns>
        private string LerId()
        {
            string id;
            bool valido = false;
            do
            {
                id = MainModulo1.LerString("Digite o ID da matéria-prima: ");

                if (!MPrima.VerificarId(id))
                {
                    Console.WriteLine("ID inválido.");
                }
                else
                {
                    valido = true;
                }

            } while (!valido);

            return id;
        }


        /// <summary>
        /// Exibe o menu de edição da matéria-prima.
        /// </summary>
        /// <param name="nomeMP">O nome da matéria-prima.</param>
        /// <returns>A opção selecionada.</returns>
        private int MenuEditar(string nomeMP)
        {
            Console.Clear();
            Console.WriteLine("======Editar Matéria-prima======");
            Console.WriteLine($"Matéria-prima a ser editada: {nomeMP}");
            Console.WriteLine("==========================\n");

            Console.WriteLine("Opcoes:");
            Console.WriteLine("1 - Editar nome");
            Console.WriteLine("2 - Editar situação");
            Console.WriteLine("0 - Sair");

            int opcao = MainModulo1.LerInt("Digite a opção desejada: ");

            return opcao;
        }

    }
}
