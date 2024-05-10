using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BILTIFUL.Modulo3
{
    internal class Compra
    {
        public int id { get; }
        public DateOnly dataCompra { get; }
        public string cnpjFornecedor { get; }
        public int valorTotal { get; }

        public Compra()
        {
        }

        public Compra(int id, DateOnly dataCompra, string cnpjFornecedor, int valorTotal)
        {
            this.id = id;
            this.dataCompra = dataCompra;
            this.cnpjFornecedor = cnpjFornecedor;
            this.valorTotal = valorTotal;
        }

        public List<string> VerificarFornecedoresExistentes()
        {
            //Funcao separa os CNPJ dos fornecedores existentes e colocam em uma lista
            List<string> fornecedoresExistentes = new();
            string path = @"C:\Teste\";
            string file = "Fornecedor.dat";
            int indice = 14;
            string linha = "";
            string[] linhas = File.ReadAllLines(path + file);

            for (int i = 0; i < linhas.Length; i++)
            {
                linha = linhas[i];

                if (indice < linha.Length)
                {
                    string substring = linha.Substring(0, indice);
                    fornecedoresExistentes.Add(substring);
                }
            }
            return fornecedoresExistentes;
        }

        public void VerificarFornecedorBloqueado()
        {
            string path = @"C:\Teste\";
            string file = "Bloqueado.dat";
            int indice = 14;
            string[] linhas = File.ReadAllLines(path + file);

            foreach (var linha in linhas)
            {
                Console.WriteLine(linha);
            }
        }

        public void VerificarFornecedorDeSeisMeses()
        {
        }

        public override string? ToString()
        {
            return id.ToString().PadLeft(4, '0') + "" + dataCompra.ToString("ddMMyyyy") + cnpjFornecedor + valorTotal;
        }
    }
}
