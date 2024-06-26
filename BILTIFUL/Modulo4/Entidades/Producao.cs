﻿using BILTIFUL.Modulo1;
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
        }
        public override string? ToString()
        {
            string texto = "";
            texto = Id.ToString().PadLeft(5, '0');
            texto += DataProducao.ToString().Replace("/", "");
            texto += Produto.PadLeft(13, '0').ToUpper();
            texto += Quantidade.ToString("N2").Replace(",", "").PadLeft(5, '0');
            return texto;
        }
        public string imprimirNaTela(List<Produto> listaProduto)
        {
            string texto = "";
            texto = $"Id: [ {Id} ]";
            texto += $" [ DATA DE PRODUÇÃO: {DataProducao} ]\n";
            texto += $"    [ COSMÉTICO PRODUZIDO: {Produto} - ";
            if (listaProduto.Find(x => x.CodigoBarras == Produto) != null)
            {
                texto += (listaProduto.Find(x => x.CodigoBarras == Produto).Nome).Trim();
            }
            texto += $" ]  [ QTDE PRODUZIDA: {Quantidade.ToString("N2")} ]";
            return texto;
        }
    }
}
