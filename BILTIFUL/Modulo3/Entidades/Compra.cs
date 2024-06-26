﻿using BILTIFUL.Modulo1;

namespace BILTIFUL.Modulo3
{
    internal class Compra
    {
        public int Id { get; set; }
        public DateOnly DataCompra { get; set; }
        public string CnpjFornecedor { get; set; }
        public float ValorTotal { get; set; }

        public Compra()
        {
        }

        public Compra(int id, DateOnly dataCompra, string cnpjFornecedor, float valorTotal)
        {
            Id = id;
            DataCompra = dataCompra;
            CnpjFornecedor = cnpjFornecedor;
            ValorTotal = valorTotal;
        }

        public Compra(string conteudoArquivo)
        {
            Id = int.Parse(conteudoArquivo.Substring(0, 5));
            DataCompra = DateOnly.ParseExact(conteudoArquivo.Substring(5, 8), "ddMMyyyy");
            CnpjFornecedor = conteudoArquivo.Substring(13, 14);
            ValorTotal = float.Parse((conteudoArquivo.Substring(27, 7))) / 100;
        }

        public string ImprimirCompraNaTela(List<Fornecedor> l)
        {
            string texto = "";
            texto = $"Id: [{Id}]\n";
            texto += $"DATA DE COMPRA: {DataCompra}\n";
            texto += $"CNPJ DO FORNECEDOR: {CnpjFornecedor}\n";
            if (l.Find(x => x.Cnpj == CnpjFornecedor) != null)
            {
                texto += $"RAZAO SOCIAL: {l.Find(x => x.Cnpj == CnpjFornecedor).RazaoSocial}\n";
            }
            texto += $"VALOR TOTAL COMPRA: {ValorTotal}\n";
            return texto;
        }
        public override string? ToString()
        {
            return Id.ToString().PadLeft(5, '0') +
                DataCompra.ToString("ddMMyyyy") +
                CnpjFornecedor +
                ValorTotal.ToString("N2").Replace(",", "").Replace(".", "").PadLeft(7, '0');
        }
    }
}