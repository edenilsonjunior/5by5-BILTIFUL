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

        public ItemCompra(string conteudoArquivo)
        {
            Id = int.Parse(conteudoArquivo.Substring(0, 5));
            DataCompra = DateOnly.ParseExact(conteudoArquivo.Substring(5, 8), "ddMMyyyy");
            MateriaPrimaID = conteudoArquivo.Substring(13, 6);
            Quantidade = float.Parse(conteudoArquivo.Substring(19, 5)) / 100;
            ValorUnitarioItem = float.Parse(conteudoArquivo.Substring(24, 5)) / 100;
            ValorTotalItem = float.Parse(conteudoArquivo.Substring(29, 6)) / 100;
        }

        public string ImprimirItemCompraNaTela()
        {
            string texto = "";

            texto = $"Id: [{Id}]\n";
            texto += $"DATA DE COMPRA: {DataCompra}\n";
            texto += $"ID DA MATERIA PRIMA: {MateriaPrimaID}\n";
            texto += $"QUANTIDADE DA MATERIA PRIMA: {Quantidade}\n";
            texto += $"VALOR UNITARIO DA MATERIA PRIMA: {ValorUnitarioItem}\n";
            texto += $"VALOR TOTAL DA MATERIA PRIMA: {ValorTotalItem}\n";

            return texto;
        }

        public override string? ToString()
        {
            string texto = "";

            texto = Id.ToString().PadLeft(5, '0') +
                DataCompra.ToString("ddMMyyyy") +
                MateriaPrimaID +
                Quantidade.ToString("N2").Replace(",", "").Replace(".", "").PadLeft(5, '0') +
                ValorUnitarioItem.ToString("N2").Replace(",", "").Replace(".", "").PadLeft(5, '0') +
                ValorTotalItem.ToString("N2").Replace(",", "").Replace(".", "").PadLeft(6, '0');

            return texto;
        }
    }
}