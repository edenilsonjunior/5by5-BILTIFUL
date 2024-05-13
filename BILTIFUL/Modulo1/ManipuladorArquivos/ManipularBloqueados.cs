namespace BILTIFUL.Modulo1.ManipuladorArquivos
{
    internal class ManipularBloqueados
    {
        private string _caminho;
        private string _arquivo;
        private ManipularFornecedor _fornecedores;

        public ManipularBloqueados(string caminho, string arquivo)
        {
            _caminho = caminho;
            _arquivo = arquivo;
            _fornecedores = new ManipularFornecedor(_caminho, "Fornecedor.dat");
            MainModulo1.CriarDiretorioArquivo(_caminho, _arquivo);
        }

        /// <summary>
        /// Recupera a lista de fornecedores bloqueados.
        /// </summary>
        /// <returns>A lista de fornecedores bloqueados.</returns>
        public List<string> Recuperar()
        {
            List<string> bloqueados = new();

            foreach (string linha in File.ReadAllLines(_caminho + _arquivo))
            {
                bloqueados.Add(linha);
            }

            return bloqueados;
        }


        /// <summary>
        /// Salva a lista de fornecedores bloqueados.
        /// </summary>
        /// <param name="bloqueados">A lista de fornecedores bloqueados.</param>
        public void Salvar(List<string> bloqueados)
        {
            using var sw = new StreamWriter(_caminho + _arquivo);

            foreach (var item in bloqueados)
            {
                sw.WriteLine(item);
            }
        }


        /// <summary>
        /// Adiciona um fornecedor à lista de bloqueados.
        /// </summary>
        public void Adicionar()
        {
            Console.Clear();
            Console.WriteLine("=====Adicionar cnpj bloqueado=====");

            var bloqueados = Recuperar();
            string cnpj = MainModulo1.LerString("Digite o CNPJ do fornecedor: ");

            // Se nao existe esse fornecedor, nao adiciona
            if (!ExisteFornecedor(cnpj))
            {
                Console.WriteLine("Fornecedor não encontrado!");
                return;
            }

            // se o cnpj ja esta na lista de bloqueados, nao adiciona
            if (bloqueados.Contains(cnpj))
            {
                Console.WriteLine("Fornecedor já está na lista de bloqueados!");
                return;
            }

            bloqueados.Add(cnpj);
            Salvar(bloqueados);
            Console.WriteLine(">>>>Cnpj adicionado a lista de bloqueados!<<<<");
        }


        /// <summary>
        /// Remove um fornecedor da lista de bloqueados.
        /// </summary>
        public void Remover()
        {
            Console.Clear();
            Console.WriteLine("=====Remover cnpj bloqueado=====");

            var fornecedores = _fornecedores.Recuperar();
            var bloqueados = Recuperar();

            string cnpj = MainModulo1.LerString("Digite o CNPJ do fornecedor: ");

            // se o fornecedor nao existe, nao remove
            if (!ExisteFornecedor(cnpj))
            {
                Console.WriteLine("Fornecedor não encontrado!");
                return;
            }

            // se o cnpj nao esta na lista de bloqueados, nao remove
            if (!bloqueados.Contains(cnpj))
            {
                Console.WriteLine("Fornecedor não está na lista de bloqueados!");
                return;
            }

            bloqueados.Remove(cnpj);
            Salvar(bloqueados);
            Console.WriteLine(">>>>Cnpj removido da lista de bloqueados!<<<<");
        }


        /// <summary>
        /// Busca um fornecedor pelo CNPJ na lista de bloqueados.
        /// </summary>
        /// <returns>O fornecedor encontrado ou null se não encontrado.</returns>
        public Fornecedor? BuscarPorCnpj()
        {
            var fornecedores = _fornecedores.Recuperar();
            var bloqueados = Recuperar();
            string cnpj = MainModulo1.LerString("Digite o CNPJ do fornecedor: ");

            // retorna nulo caso o cnpj nao esteja na lista de bloqueados
            if (!bloqueados.Contains(cnpj))
                return null;


            return fornecedores.Find(f => f.Cnpj.Equals(cnpj));
        }


        /// <summary>
        /// Localiza um fornecedor na lista de bloqueados e exibe suas informações.
        /// </summary>
        public void Localizar()
        {
            Console.Clear();
            Console.WriteLine("=====Imprimir fornecedor especifico=====");

            Fornecedor? fornecedor = BuscarPorCnpj();

            if (fornecedor != null)
            {
                Console.WriteLine("Fornecedor correspondente:");
                Console.WriteLine(fornecedor.Print());
                return;
            }

            Console.WriteLine("Fornecedor não encontrado!");
        }

        /// <summary>
        /// Imprime a lista de fornecedores bloqueados.
        /// </summary>
        public void Imprimir()
        {
            var bloqueados = Recuperar();

            Console.Clear();
            Console.WriteLine("=====Imprimir todos os fornecedores=====");
            Console.WriteLine("Lista de cpnj's bloqueados:");

            if (bloqueados.Count != 0)
            {
                foreach (var item in bloqueados)
                    Console.WriteLine(item);

                return;
            }

            Console.WriteLine("Nenhum fornecedor bloqueado!");
        }

        private bool ExisteFornecedor(string cnpj)
        {
            var fornecedores = _fornecedores.Recuperar();
            return fornecedores.Exists(f => f.Cnpj.Equals(cnpj));
        }

    }
}
