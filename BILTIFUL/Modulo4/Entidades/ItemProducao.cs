using BILTIFUL.Modulo1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BILTIFUL.Modulo4.Entidades
{
    internal class ItemProducao
    {
        public int Id { get; set; }
        public DateOnly DataProducao { get; set; }
        public string MateriaPrima { get; set; }
        public float QuantidadeMateriaPrima { get; set; }
        public ItemProducao()
        {
            
        }

        public ItemProducao(int id, DateOnly dataProducao, string materiaPrima, float quantidadeMateriaPrima)
        {
            Id = id;
            DataProducao = dataProducao;
            MateriaPrima = materiaPrima;
            QuantidadeMateriaPrima = quantidadeMateriaPrima;
        }
        public override string? ToString()
        {
            string texto = "";
            texto = Id.ToString().PadLeft(5, '0');
            texto += DataProducao.ToString().Replace("/","");
            texto += MateriaPrima.ToUpper().PadLeft(6, '0');
            texto += QuantidadeMateriaPrima.ToString("N2").Replace(",", "").PadLeft(5, '0');
            return texto;
        }
    }
}
