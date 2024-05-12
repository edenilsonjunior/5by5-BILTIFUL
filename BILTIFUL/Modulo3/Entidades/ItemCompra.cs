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
        public float Quantidade { get; set; }
        public float ValorUnitarioItem { get; set; }
        public float ValorTotalItem { get; set; }

        public ItemCompra()
        {
        }

        public ItemCompra(int id, DateOnly dataCompra, string materiaPrimaID, float quantidade, float valorUnitarioItem, float valorTotalItem)
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
            Quantidade = float.Parse(data.Substring(19, 5)) / 100;
            ValorUnitarioItem = float.Parse(data.Substring(24, 5)) / 100;
            ValorTotalItem = float.Parse(data.Substring(25, 6)) / 100;
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
                Quantidade.ToString("N2").Replace(",", "").PadLeft(5, '0') +
                ValorUnitarioItem.ToString("N2").Replace(",", "").PadLeft(6, '0') +
                ValorTotalItem.ToString("N2").Replace(",", "").PadLeft(6, '0');
        }
    }
}