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

        public List<string> VerificarFornecedoresBloqueados()
        {
            List<string> fornecedoresBloqueados = new();
            string path = @"C:\Teste\";
            string file = "Bloqueado.dat";
            string[] linhas = File.ReadAllLines(path + file);

            foreach (var linha in linhas)
            {
                fornecedoresBloqueados.Add(linha);
            }
            return fornecedoresBloqueados;
        }

        public List<DateOnly> VerificarFornecedoresDeSeisMeses()
        {
            List<string> fornecedoresComMenosDeSeisMeses = new();
            string path = @"C:\Teste\";
            string file = "Fornecedor.dat";
            int indice = 71;
            string linha = "";
            string[] linhas = File.ReadAllLines(path + file);
            List<DateOnly> dataAbertura = new();

            for (int i = 0; i < linhas.Length; i++)
            {
                linha = linhas[i];

                if (indice < linha.Length)
                {
                    string data = linha.Substring(64, 8);
                    fornecedoresComMenosDeSeisMeses.Add(data);
                    //Pego a lista de fornecedores e transformo de string para dateOnly
                    dataAbertura.Add(DateOnly.ParseExact(fornecedoresComMenosDeSeisMeses[i].Substring(0, 8), "ddMMyyyy"));
                }
            }
            return dataAbertura;
        }

        public override string? ToString()
        {
            return id.ToString().PadLeft(4, '0') + "" + dataCompra.ToString("ddMMyyyy") + cnpjFornecedor + valorTotal;
        }
    }
}
