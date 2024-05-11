using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BILTIFUL.Modulo3
{
    internal class ItemCompra
    {
        public int Id { get; set; }
        public DateOnly DataCompra { get; set; }
        public string MateriaPrimaID { get; set; }
        public int Quantidade { get; set; }
        public int ValorUnitarioItem { get; set; }
        public int ValorTotalItem { get; set; }

        public ItemCompra()
        {
        }

        public ItemCompra(int id, DateOnly dataCompra, string materiaPrimaID, int quantidade, int valorUnitarioItem, int valorTotalItem)
        {
            Id = id;
            DataCompra = dataCompra;
            MateriaPrimaID = materiaPrimaID;
            Quantidade = quantidade;
            ValorUnitarioItem = valorUnitarioItem;
            ValorTotalItem = valorTotalItem;
        }

        public ItemCompra(string data)
        {
            Id = int.Parse(data.Substring(0, 5));
            DataCompra = DateOnly.ParseExact(data.Substring(5, 8), "ddMMyyyy");
            MateriaPrimaID = data.Substring(13, 6);
            Quantidade = int.Parse(data.Substring(19, 5));
            ValorUnitarioItem = int.Parse(data.Substring(24, 5));
            ValorTotalItem = int.Parse(data.Substring(25, 6));
        }

        public string ImprimirItemCompraNaTela()
        {
            string texto = "";
            texto = $"Id: [{Id}] |";
            texto += $" DATA DE COMPRA: {DataCompra}";
            texto += $" ID DA MATERIA PRIMA: {MateriaPrimaID}";
            texto += $" QUANTIDADE DA MATERIA PRIMA: {Quantidade}";
            texto += $" VALOR UNITARIO DA MATERIA PRIMA: {ValorUnitarioItem}";
            texto += $" VALOR TOTAL DA MATERIA PRIMA: {ValorTotalItem}";
            return texto;
        }

        public override string? ToString()
        {
            return Id.ToString().PadLeft(5, '0') +
                DataCompra.ToString("ddMMyyyy") +
                MateriaPrimaID +
                Quantidade.ToString().PadLeft(5, '0') +
                ValorUnitarioItem.ToString().PadLeft(5, '0') +
                ValorTotalItem.ToString().PadLeft(6, '0');
        }
    }
}
