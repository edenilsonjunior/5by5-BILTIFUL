using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BILTIFUL.Modulo4.Entidades
{
    internal class Producao
    {
        public int Id { get; set; }
        public DateOnly DataProducao { get; set; }
        public string Produto { get; set; }
        public float Quantidade { get; set; }
        public Producao()
        {
            
        }

        public Producao(int id, DateOnly dataProducao, string produto, float quantidade)
        {
            Id = id;
            DataProducao = dataProducao;
            Produto = produto;
            Quantidade = quantidade;
        }
        public override string? ToString()
        {
            string texto = "";
            texto = Id.ToString().PadLeft(5, '0');
            texto += DataProducao.ToString().Replace("/", "");
            texto += Produto.PadLeft(13, '0').ToUpper();
            texto += Quantidade.ToString("N2").Replace(",","").PadLeft(5, '0');
            return texto;
        }
    }
}
