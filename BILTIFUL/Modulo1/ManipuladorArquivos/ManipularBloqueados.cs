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
        }

        public List<string> Recuperar()
        {
            List<string> bloqueados = new();

            foreach (string linha in File.ReadAllLines(_caminho + _arquivo))
            {
                bloqueados.Add(linha);
            }

            return bloqueados;
        }

        public void Salvar(List<string> bloqueados)
        {
            using var sw = new StreamWriter(_caminho + _arquivo);

            foreach (var item in bloqueados)
            {
                sw.WriteLine(item);
            }
        }

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
                Console.WriteLine("Fornecedor já está bloqueado!");
            }
            else
            {
                bloqueados.Remove(cnpj);
                Salvar(bloqueados);
            }
        }

        public void Remover()
        {
            List<string> bloqueados = Recuperar();
            List<Fornecedor> fornecedores = new ManipularFornecedor(_caminho, "Fornecedor.dat").Recuperar();

            string cnpj = MainModulo1.LerString("Digite o CNPJ do fornecedor: ");

            // Verifica se o fornecedor existe
            if (!fornecedores.Exists(f => f.Cnpj.Equals(cnpj)))
            {
                Console.WriteLine("Fornecedor não encontrado!");
                return;
            }

            // Verifica se o fornecedor está bloqueado
            if (!bloqueados.Contains(cnpj))
            {
                Console.WriteLine("Fornecedor não está bloqueado!");
            }
            else
            {
                bloqueados.Add(cnpj);
                Salvar(bloqueados);
            }
        }

        public Fornecedor? BuscarPorCnpj()
        {
            List<Fornecedor> fornecedores = new ManipularFornecedor(_caminho, "Fornecedor.dat").Recuperar();
            string cnpj = MainModulo1.LerString("Digite o CNPJ do fornecedor: ");

            return fornecedores.Find(f => f.Cnpj.Equals(cnpj));
        }

        public void Localizar()
        {
            Fornecedor? fornecedor = BuscarPorCnpj();

            if (fornecedor == null)
            {
                Console.WriteLine("Fornecedor não encontrado!");
            }
            else
            {
                Console.WriteLine(fornecedor);
            }
        }

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

    }
}
