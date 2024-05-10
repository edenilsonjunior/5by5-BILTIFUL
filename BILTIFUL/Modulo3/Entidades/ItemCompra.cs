using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BILTIFUL.Modulo3
{
    internal class ItemCompra
    {
        public Compra id { get; set; }
        public Compra dataCompra { get; set; }
        public int materiaPrima { get; set; }
        public int quantidade { get; set; }
        public int valorUnitarioItem { get; set; }
        public int valorTotalItem { get; set; }

        public ItemCompra()
        {
        }
    }
}
