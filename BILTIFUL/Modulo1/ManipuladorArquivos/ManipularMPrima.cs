namespace BILTIFUL.Modulo1.ManipuladorArquivos
{
    internal class ManipularMPrima
    {
        private string _caminho;
        private string _arquivo;

        public ManipularMPrima(string caminho, string arquivo)
        {
            _caminho = caminho;
            _arquivo = arquivo;
        }


        // Metodos principais
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

        public MPrima? BuscarPorId()
        {
            string id = LerId();

            List<MPrima> materias = Recuperar();

            MPrima? materia = materias.Find(x => x.Id == id);

            return materia;
        }

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

        public void Imprimir()
        {
            List<MPrima> materias = Recuperar();

            if (materias.Count == 0)
            {
                Console.WriteLine("Nenhuma matéria-prima cadastrada.");
                return;
            }

            Console.WriteLine("Lista de materias primas:");
            foreach (var item in materias)
            {
                Console.WriteLine(item.Print());
            }
        }


        // Metodos privados
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

        private string LerId()
        {
            string id;
            do
            {
                id = MainModulo1.LerString("Digite o id da matéria-prima: ");

                if (!MPrima.VerificarId(id))
                    Console.WriteLine("Id inválido.");

            } while (MPrima.VerificarId(id));

            return id;
        }

        private int MenuEditar(string nomeMP)
        {
            Console.Clear();
            Console.WriteLine("======Editar Materia prima======");
            Console.WriteLine($"Materia prima a ser editado: {nomeMP}");

            Console.WriteLine("==========================\n");
            Console.WriteLine("1 - Editar nome");
            Console.WriteLine("2 - Editar situação");
            Console.WriteLine("0 - Sair");

            int opcao = MainModulo1.LerInt("Digite a opção desejada: ");

            return opcao;
        }

    }
}
