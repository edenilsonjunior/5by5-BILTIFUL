//****
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

            idVenda = idVenda; 
            produto = data.Substring(5, 17);
            qtd = int.Parse(data.Substring(3, 20));
            valorUnitario = float.Parse(data.Substring(5, 25));
            totalItem = float.Parse(data.Substring(6, 31));
        }

        public ItemVenda(int idVenda, string produto, int qtd, float valorUnitario)
        {
            this.idVenda = idVenda;
            this.produto = produto;
            this.qtd = qtd;
            this.valorUnitario = valorUnitario;
            totalItem=qtd*valorUnitario;
        }

        public override string? ToString()
        {
            string texto = "";

            texto += idVenda.ToString().PadLeft(5, '0');
            texto += produto.PadRight(17); 
            texto += qtd.ToString().PadLeft(3, '0');
            texto += valorUnitario.ToString("N2").Replace(",", "").PadLeft(7, '0');
            texto += totalItem.ToString("N2").Replace(",", "").PadLeft(7, '0');

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
