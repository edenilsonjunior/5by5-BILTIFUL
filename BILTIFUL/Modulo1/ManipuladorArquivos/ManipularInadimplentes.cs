namespace BILTIFUL.Modulo1.ManipuladorArquivos
{
    internal class ManipularInadimplentes
    {
        private string _caminho;
        private string _arquivo;

        /// <summary>
        /// Inicializa uma nova instância da classe ManipularInadimplentes.
        /// </summary>
        /// <param name="caminho">O caminho do diretório.</param>
        /// <param name="arquivo">O nome do arquivo.</param>
        public ManipularInadimplentes(string caminho, string arquivo)
        {
            _caminho = caminho;
            _arquivo = arquivo;
            MainModulo1.CriarDiretorioArquivo(_caminho, _arquivo);
        }



        /// <summary>
        /// Recupera a lista de inadimplentes do arquivo.
        /// </summary>
        /// <returns>A lista de inadimplentes.</returns>
        public List<string> Recuperar()
        {
            List<string> risco = new();

            foreach (string linha in File.ReadAllLines(_caminho + _arquivo))
            {
                risco.Add(linha);
            }

            return risco;
        }


        /// <summary>
        /// Salva a lista de inadimplentes no arquivo.
        /// </summary>
        /// <param name="risco">A lista de inadimplentes.</param>
        public void Salvar(List<string> risco)
        {
            using var sw = new StreamWriter(_caminho + _arquivo);

            foreach (var item in risco)
            {
                sw.WriteLine(item);
            }
        }


        /// <summary>
        /// Adiciona um cliente à lista de inadimplentes.
        /// </summary>
        /// <param name="cpf">O CPF do cliente a ser adicionado.</param>
        public void Adicionar()
        {
            Console.Clear();
            Console.WriteLine("=====Adicionar na lista de risco=====");

            string cpf = LerCpf();

            List<string> risco = Recuperar();

            // Adiciona o cliente a lista de risco
            if (!risco.Contains(cpf))
            {
                risco.Add(cpf);
                Console.WriteLine(">>>Cliente adicionado na lista de risco!<<<");
                Salvar(risco);
                return;
            }

            Console.WriteLine("Cliente já está na lista de risco!");
        }


        /// <summary>
        /// Remove um cliente da lista de inadimplentes.
        /// </summary>
        /// <param name="cpf">O CPF do cliente a ser removido.</param>
        public void Remover()
        {
            Console.Clear();
            Console.WriteLine("=====Remover da lista de risco=====");

            string cpf = LerCpf();
            List<string> risco = Recuperar();

            // Se o cliente estiver na lista de risco, remove
            if (risco.Contains(cpf))
            {
                risco.Remove(cpf);
                Salvar(risco);
                return;
            }

            Console.WriteLine("Cliente não está na lista de risco!");
        }


        /// <summary>
        /// Busca um cliente na lista de inadimplentes pelo CPF.
        /// </summary>
        /// <returns>O cliente encontrado ou null se não encontrado.</returns>
        public Cliente? BuscarPorCpf()
        {
            var risco = Recuperar();

            string cpf = MainModulo1.LerString("Digite o CPF do cliente: ");

            // retorna nulo caso o cpf nao esteja na tabela de risco
            if (!risco.Contains(cpf))
                return null;


            List<Cliente> clientes = new ManipularCliente(_caminho, "Clientes.dat").Recuperar();

            return clientes.Find(f => f.Cpf.Equals(cpf));
        }


        /// <summary>
        /// Localiza um cliente na lista de inadimplentes e exibe suas informações.
        /// </summary>
        public void Localizar()
        {
            Console.Clear();
            Console.WriteLine("=====Imprimir Cliente especifico=====");

            Cliente? c = BuscarPorCpf();

            if (c != null)
            {
                Console.WriteLine("Dados do cliente inadimplente:");
                Console.WriteLine(c.Print());
                return;
            }

            Console.WriteLine("Cliente não encontrado!");
        }


        /// <summary>
        /// Imprime a lista de inadimplentes.
        /// </summary>
        public void Imprimir()
        {
            Console.Clear();
            Console.WriteLine("=====Lista de clientes em risco=====");

            List<string> risco = Recuperar();

            if (risco.Count != 0)
            {
                foreach (var item in risco)
                {
                    Console.WriteLine($"-->{item}");
                }
                return;
            }

            Console.WriteLine("Nenhum cliente em risco!");
        }



        /// <summary>
        ///  Le o cpf do cliente
        /// </summary>
        /// <returns>
        /// O cpf do cliente
        /// </returns>
        private string LerCpf()
        {
            string cpf;

            bool valido = false;

            do
            {
                cpf = MainModulo1.LerString("Digite o CPF do cliente: ");

                if (!Cliente.VerificarCpf(cpf))
                {
                    Console.WriteLine("CPF inválido!");
                }
                else
                {
                    valido = true;
                }
            } while (!valido);

            return cpf;
        }
    }
}
