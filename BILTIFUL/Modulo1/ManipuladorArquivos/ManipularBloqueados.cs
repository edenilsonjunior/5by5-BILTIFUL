using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BILTIFUL.Modulo1.ManipuladorArquivos
{
    internal class ManipularBloqueados
    {
        private string _caminho;
        private string _arquivo;

        public ManipularBloqueados(string caminho, string arquivo)
        {
            _caminho = caminho;
            _arquivo = arquivo;
            CriarDiretorioArquivo();
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
            List<Fornecedor> fornecedores = new ManipularFornecedor(_caminho, "Fornecedor.dat").Recuperar();
            List<string> bloqueados = Recuperar();
            string cnpj;

            cnpj = MainModulo1.LerString("Digite o CNPJ do fornecedor: ");

            if (!fornecedores.Exists(f => f.Cnpj.Equals(cnpj)))
            {
                Console.WriteLine("Fornecedor não encontrado!");
                return;
            }

            if (bloqueados.Contains(cnpj))
            {
                Console.WriteLine("Fornecedor já está na lista de bloqueados!");
                return;
            }

            // Adiciona o cnpj na lista de bloqueados
            bloqueados.Add(cnpj);
            Salvar(bloqueados);
        }


        /// <summary>
        /// Remove um fornecedor da lista de bloqueados.
        /// </summary>
        public void Remover()
        {
            List<Fornecedor> fornecedores = new ManipularFornecedor(_caminho, "Fornecedor.dat").Recuperar();
            List<string> bloqueados = Recuperar();
            string cnpj;

            cnpj = MainModulo1.LerString("Digite o CNPJ do fornecedor: ");

            if (!fornecedores.Exists(f => f.Cnpj.Equals(cnpj)))
            {
                Console.WriteLine("Fornecedor não encontrado!");
                return;
            }

            if (!bloqueados.Contains(cnpj))
            {
                Console.WriteLine("Nao foi possivel remover.");
                Console.WriteLine("-->Fornecedor não está na lista de bloqueados!");
                return;
            }

            bloqueados.Remove(cnpj);
            Salvar(bloqueados);
        }

        /// <summary>
        /// Busca um fornecedor pelo CNPJ na lista de bloqueados.
        /// </summary>
        /// <returns>O fornecedor encontrado ou null se não encontrado.</returns>
        public Fornecedor? BuscarPorCnpj()
        {
            List<Fornecedor> fornecedores = new ManipularFornecedor(_caminho, "Fornecedor.dat").Recuperar();
            List<string> bloqueados = Recuperar();
            string cnpj = MainModulo1.LerString("Digite o CNPJ do fornecedor: ");

            // retorna nulo caso o cnpj nao esteja na lista de bloqueados
            if (!bloqueados.Contains(cnpj))
            {
                return null;
            }

            return fornecedores.Find(f => f.Cnpj.Equals(cnpj));
        }


        /// <summary>
        /// Localiza um fornecedor na lista de bloqueados e exibe suas informações.
        /// </summary>
        public void Localizar()
        {
            Fornecedor? fornecedor = BuscarPorCnpj();

            if (fornecedor == null)
            {
                Console.WriteLine("Fornecedor não encontrado!");
            }
            else
            {
                Console.WriteLine("Fornecedor correspondente:");
                Console.WriteLine(fornecedor.Print());
            }
        }

        /// <summary>
        /// Imprime a lista de fornecedores bloqueados.
        /// </summary>
        public void Imprimir()
        {
            List<string> bloqueados = Recuperar();

            if (bloqueados.Count == 0)
            {
                Console.WriteLine("Nenhum fornecedor bloqueado!");
            }
            else
            {
                Console.WriteLine("Lista de cpnj's bloqueados:");
                foreach (var item in bloqueados)
                {
                    Console.WriteLine(item);
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
