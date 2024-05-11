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
