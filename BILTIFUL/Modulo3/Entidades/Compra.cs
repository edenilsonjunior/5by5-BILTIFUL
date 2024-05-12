﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Compra(string data)
        {
            Id = int.Parse(data.Substring(0, 5));
            DataCompra = DateOnly.ParseExact(data.Substring(5, 8), "ddMMyyyy");
            CnpjFornecedor = data.Substring(13, 14);
            ValorTotal = float.Parse((data.Substring(27, 7))) / 100;
        }

        public string ImprimirCompraNaTela()
        {
            string texto = "";
            texto = $"Id: [{Id}]";
            texto += $"DATA DE COMPRA: {DataCompra}";
            texto += $"CNPJ DO FORNECEDOR: {CnpjFornecedor}";
            texto += $"VALOR TOTAL COMPRA: {ValorTotal}";
            return texto;
        }
        public override string? ToString()
        {
            return Id.ToString().PadLeft(5, '0') +
                DataCompra.ToString("ddMMyyyy") +
                CnpjFornecedor +
                ValorTotal.ToString("N2").Replace(",", "").PadLeft(7, '0');
        }
    }
}