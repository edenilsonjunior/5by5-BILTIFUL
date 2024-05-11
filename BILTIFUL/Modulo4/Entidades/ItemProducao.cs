using BILTIFUL.Modulo1;

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
        public ItemProducao(string data)
        {
            Id = Int32.Parse(data.Substring(0, 5));
            DataProducao = DateOnly.ParseExact(data.Substring(5, 8), "ddMMyyyy");
            MateriaPrima = data.Substring(13, 6);
            QuantidadeMateriaPrima = float.Parse((data.Substring(19, 5))) / 100;
            ItemProducao tempProducao = new(Id, DataProducao, MateriaPrima, QuantidadeMateriaPrima);
        }
        public override string? ToString()
        {
            string texto = "";
            texto = Id.ToString().PadLeft(5, '0');
            texto += DataProducao.ToString().Replace("/", "");
            texto += MateriaPrima.ToUpper().PadLeft(6, '0');
            texto += QuantidadeMateriaPrima.ToString("N2").Replace(",", "").PadLeft(5, '0');
            return texto;
        }
        public string imprimirNaTela(List<MPrima> listaMPrima)
        {
            string texto = "";
            texto += $"DATA DE PRODUÇÃO: {DataProducao}  |";
            texto += $" MATÉRIA PRIMA UTILIZADA: {MateriaPrima} ";
            if (listaMPrima.Find(x => x.Id == MateriaPrima) != null)
            {
                texto += (listaMPrima.Find(x => x.Id == MateriaPrima).Nome);
            }
            texto += $" | QTDE UTILIZADA: " + QuantidadeMateriaPrima.ToString("N2");
            return texto;
        }
    }
}