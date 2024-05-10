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
            string linha;
            string[] linhas = File.ReadAllLines(path + file);
            DateOnly dataAtual = DateOnly.FromDateTime(DateTime.Now);
            List<DateOnly> dataAbertura = new();
            List<DateOnly> dataMaisSeisMeses = new();

            for (int i = 0; i < linhas.Length; i++)
            {
                linha = linhas[i];

                if (indice < linha.Length)
                {
                    string data = linha.Substring(64, 8);
                    fornecedoresComMenosDeSeisMeses.Add(data);
                    //Pego a lista de data de abertura de fornecedores e transformo de string para dateOnly
                    dataAbertura.Add(DateOnly.ParseExact(fornecedoresComMenosDeSeisMeses[i].Substring(0, 8), "ddMMyyyy"));
                }
            }
            //Adiciona na lista apenas as datas de aberturas maiores ou iguais a seis meses
            for (int i = 0; i < dataAbertura.Count; i++)
            {
                int diferencaEmMeses = ((dataAtual.Year - dataAbertura[i].Year) * 12) + dataAtual.Month - dataAbertura[i].Month;
                if(diferencaEmMeses >= 6)
                {
                    dataMaisSeisMeses.Add(dataAbertura[i]);
                }
            }

            return dataMaisSeisMeses;
        }

        public override string? ToString()
        {
            return id.ToString().PadLeft(4, '0') + "" + dataCompra.ToString("ddMMyyyy") + cnpjFornecedor + valorTotal;
        }
    }
}
