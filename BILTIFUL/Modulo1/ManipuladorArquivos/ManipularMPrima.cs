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
            CriarDiretorioArquivo();
        }


        /// <summary>
        /// Recupera a lista de matérias-primas do arquivo.
        /// </summary>
        /// <returns>A lista de matérias-primas.</returns>
        public List<MPrima> Recuperar()
        {
            List<MPrima> materias = new();

            foreach (string linha in File.ReadAllLines(_caminho + _arquivo))
            {
                MPrima aux = new(linha);
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
            string id, nome;
            List<MPrima> materias = Recuperar();

            int idInt = GetMaiorId();

            id = $"MP{idInt:0000}";

            nome = MainModulo1.LerString("Digite o nome: ");

            materias.Add(new(id, nome));
            Salvar(materias);
        }

        /// <summary>
        /// Edita uma matéria-prima existente.
        /// </summary>
        public void Editar()
        {
            List<MPrima> materias = Recuperar();

            string id = LerId();

            MPrima? materia = materias.Find(x => x.Id == id);

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
        }

        /// <summary>
        /// Busca uma matéria-prima pelo seu ID.
        /// </summary>
        /// <returns>A matéria-prima encontrada ou null se não encontrada.</returns>
        public MPrima? BuscarPorId()
        {
            string id = LerId();

            List<MPrima> materias = Recuperar();

            MPrima? materia = materias.Find(x => x.Id == id);

            return materia;
        }

        /// <summary>
        /// Localiza uma matéria-prima pelo seu ID e exibe suas informações.
        /// </summary>
        public void Localizar()
        {
            MPrima? materia = BuscarPorId();

            if (materia == null)
            {
                Console.WriteLine("Matéria-prima não encontrada.");
                return;
            }

            Console.WriteLine(materia);
        }

        /// <summary>
        /// Imprime a lista de matérias-primas.
        /// </summary>
        public void Imprimir()
        {
            List<MPrima> materias = Recuperar();

            if (materias.Count == 0)
            {
                Console.WriteLine("Nenhuma matéria-prima cadastrada.");
                return;
            }

            Console.WriteLine("Lista de matérias-primas:");
            foreach (var item in materias)
            {
                Console.WriteLine(item.Print());
            }
        }




        /// <summary>
        /// Obtém o maior ID das matérias-primas cadastradas.
        /// </summary>
        /// <returns>O maior ID das matérias-primas.</returns>
        private int GetMaiorId()
        {
            List<MPrima> materias = Recuperar();

            if (materias.Count == 0)
            {
                return 0;
            }
            int maior = 0;

            foreach (var item in materias)
            {
                int id = int.Parse(item.Id.Substring(2));
                if (id > maior)
                {
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
            do
            {
                id = MainModulo1.LerString("Digite o ID da matéria-prima: ");

                if (!MPrima.VerificarId(id))
                    Console.WriteLine("ID inválido.");

            } while (MPrima.VerificarId(id));

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
            Console.WriteLine("1 - Editar nome");
            Console.WriteLine("2 - Editar situação");
            Console.WriteLine("0 - Sair");

            int opcao = MainModulo1.LerInt("Digite a opção desejada: ");

            return opcao;
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
