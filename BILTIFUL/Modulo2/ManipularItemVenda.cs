using BILTIFUL.Modulo1;
using BILTIFUL.Modulo2;
using BILTIFUL.Modulo4.Entidades;

namespace BILTIFUL.Modulo2
{
    internal class ManipularItemVenda
    {
        static public ItemVenda CriarItemVenda(int idVenda, List<Produto> listaProduto)
        {
            Console.WriteLine("Digite o código de barras do produto desejado: ");
            string codigoBarras = Console.ReadLine();

            Produto produto = listaProduto.Find(x => x.CodigoBarras == codigoBarras);
            if (produto == null)
            {
                Console.WriteLine("Produto não encontrado.");
                return null;
            }
            Console.WriteLine("Digite a quantidade do produto desejado: ");
            int qtd = int.Parse(Console.ReadLine());

            if (qtd <= 0 || qtd > 999)
            {
                Console.WriteLine("Quantidade inválida.");
            }

          ItemVenda item = new ItemVenda(idVenda, produto.CodigoBarras,qtd,produto.ValorVenda);

            return item;
        }

       
    }
}


