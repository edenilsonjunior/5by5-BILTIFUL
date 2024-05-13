//*//
using BILTIFUL.Modulo1;
namespace BILTIFUL.Modulo2
{
    internal class ItemVenda
    {
        public int idVenda { get; }
        public string produto;
        public int qtd;
        public float valorUnitario;
        public float totalItem;

        public ItemVenda(string data)
        {

            idVenda = int.Parse(data.Substring(0, 5));
            produto = data.Substring(5, 13);
            qtd = int.Parse(data.Substring(13, 3));
            valorUnitario = float.Parse(data.Substring(16, 5))/100;
            totalItem = float.Parse(data.Substring(21, 6))/100;
        }

        public ItemVenda(int idVenda, string produto, int qtd, float valorUnitario)
        {
            this.idVenda = idVenda;
            this.produto = produto;
            this.qtd = qtd;
            this.valorUnitario = valorUnitario;
            totalItem = qtd * valorUnitario;
        }

        public override string? ToString()
        {
            string texto = "";

            texto += idVenda.ToString().PadLeft(5, '0');
            texto += produto;
            texto += qtd.ToString().PadLeft(3, '0');
            texto += valorUnitario.ToString("N2").Replace(",", "").Replace(".", "").PadLeft(5, '0');
            texto += totalItem.ToString("N2").Replace(",", "").Replace(".", "").PadLeft(6, '0');

            return texto;
        }

        public string ImprimirItem()
        {
            string texto= "";
            texto += $"\nCódigo de barras: {produto}";
            texto += $" Quantidade: {qtd}";
            texto += $" Valor unitário: {valorUnitario.ToString("N2")}";
            texto += $" Valor total: {totalItem.ToString("N2")}";

            return texto;

        }

 
        public string FormatarParaArquivo()
        {
            string data = "";

            data += idVenda;
            data += produto;
            data += qtd;
            data += valorUnitario;
            data += totalItem;
          return data;
        }


    }
}
