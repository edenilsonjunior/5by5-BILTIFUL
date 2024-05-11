//**
using BILTIFUL.Modulo1;
namespace BILTIFUL.Modulo2
{
    internal class ItemVenda
    {
        public int idVenda { get; } 
        public string produto; 
        public int qtd; 
        public int valorUnitario;
        public int totalItem;

        public ItemVenda(string data)
        {

            idVenda = idVenda; 
            produto = data.Substring(5, 17);
            qtd = int.Parse(data.Substring(3, 20));
            valorUnitario = int.Parse(data.Substring(5, 25));
            totalItem = int.Parse(data.Substring(6, 31));
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
