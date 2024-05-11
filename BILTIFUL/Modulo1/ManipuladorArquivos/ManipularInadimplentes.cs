using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            CriarDiretorioArquivo();
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
        public void Adicionar()
        {
            List<Cliente> clientes = new ManipularCliente(_caminho, "Clientes.dat").Recuperar();
            List<string> risco = Recuperar();
            string cpf;

            cpf = MainModulo1.LerString("Digite o CPF do cliente: ");

            // Verifica se o cliente existe
            if (!clientes.Exists(c => c.Cpf.Equals(cpf)))
            {
                Console.WriteLine("Cliente não encontrado!");
                return;
            }

            // Se a lista de risco já contém o cpf, não adiciona
            if (risco.Contains(cpf))
            {
                Console.WriteLine("Cliente já está na tabela de risco!");
            }
            else
            {
                risco.Add(cpf);
                Salvar(risco);
            }
        }

        /// <summary>
        /// Remove um cliente da lista de inadimplentes.
        /// </summary>
        public void Remover()
        {
            List<Cliente> clientes = new ManipularCliente(_caminho, "Clientes.dat").Recuperar();
            List<string> risco = Recuperar();

            string cpf = MainModulo1.LerString("Digite o CPF do cliente: ");

            // Verifica se o cliente existe
            if (!clientes.Exists(f => f.Cpf.Equals(cpf)))
            {
                Console.WriteLine("Cliente não encontrado!");
                return;
            }

            // Verifica se o cliente está na lista de risco
            if (!risco.Contains(cpf))
            {
                Console.WriteLine("Cliente não está na lista de risco!");
            }
            else
            {
                risco.Remove(cpf);
                Salvar(risco);
            }
        }

        /// <summary>
        /// Busca um cliente na lista de inadimplentes pelo CPF.
        /// </summary>
        /// <returns>O cliente encontrado ou null se não encontrado.</returns>
        public Cliente? BuscarPorCpf()
        {
            List<string> risco = Recuperar();

            string cpf = MainModulo1.LerString("Digite o CPF do cliente: ");

            // retorna nulo caso o cpf nao esteja na tabela de risco
            if (!risco.Contains(cpf))
            {
                return null;
            }

            List<Cliente> clientes = new ManipularCliente(_caminho, "Clientes.dat").Recuperar();

            return clientes.Find(f => f.Cpf.Equals(cpf));
        }

        /// <summary>
        /// Localiza um cliente na lista de inadimplentes e exibe suas informações.
        /// </summary>
        public void Localizar()
        {
            Cliente? Cliente = BuscarPorCpf();

            if (Cliente == null)
            {
                Console.WriteLine("Cliente não encontrado!");
            }
            else
            {
                Console.WriteLine(Cliente.Print());
            }
        }

        /// <summary>
        /// Imprime a lista de inadimplentes.
        /// </summary>
        public void Imprimir()
        {
            List<string> risco = Recuperar();

            if (risco.Count == 0)
            {
                Console.WriteLine("Nenhum cliente em risco!");
            }
            else
            {
                Console.WriteLine("Lista de cpf's em risco:");
                foreach (var item in risco)
                {
                    Console.WriteLine($"-->{item}");
                }
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
