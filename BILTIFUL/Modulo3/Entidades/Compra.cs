using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BILTIFUL.Modulo3
{
    internal class Compra
    {
        public int Id { get; }
        public DateOnly DataCompra { get; }
        public string CnpjFornecedor { get; }
        public int ValorTotal { get; }

        public Compra()
        {
        }

        public Compra(int id, DateOnly dataCompra, string cnpjFornecedor, int valorTotal)
        {
            Id = id;
            DataCompra = dataCompra;
            CnpjFornecedor = cnpjFornecedor;
            ValorTotal = valorTotal;
        }

        //public List<string> VerificarFornecedoresExistentes()
        //{
        //    //Funcao separa os CNPJ dos fornecedores existentes e colocam em uma lista
        //    List<string> fornecedoresExistentes = new();
        //    string path = @"C:\Teste\";
        //    string file = "Fornecedor.dat";
        //    int indice = 14;
        //    string linha = "";
        //    string[] linhas = File.ReadAllLines(path + file);

        //    for (int i = 0; i < linhas.Length; i++)
        //    {
        //        linha = linhas[i];

        //        if (indice < linha.Length)
        //        {
        //            string substring = linha.Substring(0, indice);
        //            fornecedoresExistentes.Add(substring);
        //        }
        //    }
        //    return fornecedoresExistentes;
        //}

        //public List<string> VerificarFornecedoresBloqueados()
        //{
        //    List<string> fornecedoresBloqueados = new();
        //    string path = @"C:\Teste\";
        //    string file = "Bloqueado.dat";
        //    string[] linhas = File.ReadAllLines(path + file);

        //    foreach (var linha in linhas)
        //    {
        //        fornecedoresBloqueados.Add(linha);
        //    }
        //    return fornecedoresBloqueados;
        //}

        //public List<string> VerificarFornecedoresComMaisDeSeisMeses()
        //{
        //    List<string> fornecedoresComMenosDeSeisMeses = new();
        //    List<string> fornecedores = new();
        //    List<string> fornecedoresComMaisDeSeisMeses = new();
        //    List<DateOnly> dataAbertura = new();

        //    string path = @"C:\Teste\";
        //    string file = "Fornecedor.dat";
        //    string linha;

        //    string[] linhas = File.ReadAllLines(path + file);

        //    int indice = 71;

        //    DateOnly dataAtual = DateOnly.FromDateTime(DateTime.Now);

        //    for (int i = 0; i < linhas.Length; i++)
        //    {
        //        linha = linhas[i];

        //        if (indice < linha.Length)
        //        {
        //            string data = linha.Substring(64, 8);
        //            string cnpj = linha.Substring(0, 14);
        //            fornecedores.Add(cnpj);
        //            fornecedoresComMenosDeSeisMeses.Add(data);
        //            //Pego a lista de data de abertura de fornecedores e transformo de string para dateOnly
        //            dataAbertura.Add(DateOnly.ParseExact(fornecedoresComMenosDeSeisMeses[i].Substring(0, 8), "ddMMyyyy"));
        //        }
        //    }
        //    //Adiciona na lista apenas os cnpj que as datas de abertura maiores ou iguais a seis meses
        //    for (int i = 0; i < fornecedores.Count; i++)
        //    {
        //        int diferencaEmMeses = ((dataAtual.Year - dataAbertura[i].Year) * 12) + dataAtual.Month - dataAbertura[i].Month;
        //        if (diferencaEmMeses >= 6)
        //        {
        //            fornecedoresComMaisDeSeisMeses.Add(fornecedores[i]);
        //        }
        //    }
        //    return fornecedoresComMaisDeSeisMeses;
        //}

        //public List<string> VerificarFornecedoresAtivos()
        //{
        //    List<string> situacaoFornecedores = new();
        //    List<string> fornecedoresAtivos = new();
        //    List<string> fornecedoresAtivosCNPJ = new();
        //    string path = @"C:\Teste\";
        //    string file = "Fornecedor.dat";
        //    string linha = "";
        //    string[] linhas = File.ReadAllLines(path + file);

        //    int indice = 88;

        //    for (int i = 0; i < linhas.Length; i++)
        //    {
        //        linha = linhas[i];

        //        if (indice < linha.Length)
        //        {
        //            string situacao = linha.Substring(88);
        //            string cnpj = linha.Substring(0, 14);
        //            situacaoFornecedores.Add(situacao);
        //            fornecedoresAtivosCNPJ.Add(cnpj);
        //        }
        //    }

        //    for (int i = 0; i < situacaoFornecedores.Count; i++)
        //    {
        //        if (situacaoFornecedores[i] == "A")
        //        {
        //            fornecedoresAtivos.Add(fornecedoresAtivosCNPJ[i]);
        //        }
        //    }

        //    return fornecedoresAtivos;
        //}

        public override string? ToString()
        {
            return Id.ToString().PadLeft(5, '0') +
                DataCompra.ToString("ddMMyyyy") +
                CnpjFornecedor +
                ValorTotal.ToString().PadLeft(7, '0');
        }
    }
}
