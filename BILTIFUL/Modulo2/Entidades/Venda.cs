using BILTIFUL.Modulo1;
//*//
namespace BILTIFUL.Modulo2
{
    internal class Venda
    {
        public int idVenda { get; }
        public DateOnly dataVenda;
        public string cpfCliente;
        public float valorTotal;

        public Venda(string data)
        {
            idVenda = int.Parse(data.Substring(0, 5));
            dataVenda = DateOnly.ParseExact(data.Substring(5, 8), "ddMMyyyy", null);
            cpfCliente = data.Substring(13, 11);
            valorTotal = float.Parse((data.Substring(24, 7))) / 100;
        }

        public Venda(int idVenda, DateOnly dataVenda, string cpfCliente, float valorTotal)
        {
            this.idVenda = idVenda;
            this.dataVenda = dataVenda;
            this.cpfCliente = cpfCliente;
            this.valorTotal = valorTotal;
        }

        public override string? ToString()
        {
            string texto = "";
            texto = idVenda.ToString().PadLeft(5, '0');
            texto += dataVenda.ToString("ddMMyyyy");
            texto += cpfCliente.PadLeft(11, '0');
            texto += valorTotal.ToString("N2").Replace(",", "").PadLeft(7, '0');
            return texto;
        }

        public string FormatarParaArquivo()
        {
            string data = "";

            data += idVenda;
            data += dataVenda;
            data += dataVenda.ToString().Replace("/", "");
            data += cpfCliente;
            data += valorTotal;
            return data;
        }

        
        public string Imprimir(Venda venda, List<Cliente> listaCliente, List<ItemVenda> listItemVenda)
        {
            string texto = "";
            texto += $"Id: [ {venda.idVenda} ]";
            texto += $" [ Data da venda: {venda.dataVenda} ]\n";

            Cliente clienteVenda = listaCliente.Find(x => x.Cpf == venda.cpfCliente);
            if (clienteVenda != null)
            {
                texto += $"    [Cliente: {clienteVenda.Nome}- {clienteVenda.DataNascimento} ]";
            }

            texto += $"\n[ Lista de itens ]:\n{GetItensVenda(venda, listItemVenda)}";
            return texto;
        }
    

        public string GetItensVenda(Venda venda, List<ItemVenda> listItemVenda)
        {

            string itens = "";
            foreach (ItemVenda item in listItemVenda)
            {
                if (item.idVenda == venda.idVenda)
                {
                    itens += item.ImprimirItem();
                }
            }
            return itens;
        }
    }
}
 

