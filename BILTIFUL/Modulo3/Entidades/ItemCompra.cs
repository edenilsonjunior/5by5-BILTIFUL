using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BILTIFUL.Modulo3
{
    internal class ItemCompra
    {
        public int id { get; set; }
        public DateOnly dataCompra { get; set; }
        public string materiaPrimaID { get; set; }
        public int quantidade { get; set; }
        public int valorUnitarioItem { get; set; }
        public int valorTotalItem { get; set; }

        public ItemCompra()
        {
        }

        public ItemCompra(int id, DateOnly dataCompra, string materiaPrimaID, int quantidade, int valorUnitarioItem, int valorTotalItem)
        {
            this.id = id;
            this.dataCompra = dataCompra;
            this.materiaPrimaID = materiaPrimaID;
            this.quantidade = quantidade;
            this.valorUnitarioItem = valorUnitarioItem;
            this.valorTotalItem = valorTotalItem;
        }

        public override string? ToString()
        {
            return id.ToString().PadLeft(5, '0') +
                dataCompra.ToString("ddMMyyyy") +
                materiaPrimaID +
                quantidade.ToString().PadLeft(5, '0') +
                valorUnitarioItem.ToString().PadLeft(5, '0') +
                valorTotalItem.ToString().PadLeft(6, '0');
        }
    }
}
