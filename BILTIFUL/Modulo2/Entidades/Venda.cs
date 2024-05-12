using BILTIFUL.Modulo1;
using BILTIFUL.Modulo1.ManipuladorArquivos;
using BILTIFUL.Modulo4.Entidades;

//****
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

        public string Imprimir(List<Venda> listaVenda)
        {
            string texto = "";
            texto = $"Id: [ {idVenda} ]";
            texto += $" [ Data da venda: {dataVenda} ]\n";
            texto += $"    [ Venda: {listaVenda} - ";
            if (listaVenda.Find(x => x.idVenda == Venda) != null)
            {
                texto += (listaProduto.Find(x => x.CodigoBarras == Produto).Nome).Trim();
            }
            texto += $" ]  [ QTDE PRODUZIDA: {Quantidade.ToString("N2")} ]";
            return texto;
        }
    }
}
 

