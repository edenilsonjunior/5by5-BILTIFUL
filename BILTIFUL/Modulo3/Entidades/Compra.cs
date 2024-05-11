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

        public Compra(string data)
        {
            Id = int.Parse(data.Substring(0, 5));
            DataCompra = DateOnly.ParseExact(data.Substring(5, 8), "ddMMyyyy");
            CnpjFornecedor = data.Substring(13, 14);
            ValorTotal = int.Parse(data.Substring(27, 7));
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
                ValorTotal.ToString().PadLeft(7, '0');
        }
    }
}
