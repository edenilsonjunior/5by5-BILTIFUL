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
        public Producao(string data)
        {
            Id = Int32.Parse(data.Substring(0, 5));
            DataProducao = DateOnly.ParseExact(data.Substring(5, 8), "ddMMyyyy");
            Produto = data.Substring(13, 13);
            Quantidade = float.Parse((data.Substring(26, 5))) / 100;
            Producao tempProducao = new(Id, DataProducao, Produto, Quantidade);
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
        public string imprimirNaTela()
        {
            string texto = "";
            texto = $"Id: [ {Id} ] |";
            texto += $" DATA DE PRODUÇÃO: {DataProducao} |";
            texto += $" COSMÉTICO PRODUZIDO: {Produto} |";
            texto += $" QUANTIDADE UTILIZADA: {Quantidade.ToString("N2")}";
            return texto;
        }
    }
}
