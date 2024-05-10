/*ITEMVENDA
Atributos
1) ID - mesmo id de venda como usar a mesma variável em classes !=?
2) Produto - ID do produto na compra (produto cadastrado em outro módulo)
3) Quantidade - int, máx de 999
*não pode mais de 03 unidades por venda
4) Valor unitário - de 1 a 999,99
vem do cadastro, junto com o ID, deve haver um histórico de vendas
5) Total item: quantidade*valor unitário, máx 9.999,99
*só é gerado após a venda, com valor e quantidade daquele produto
salvar FormatarParaArquivo() string ItemVenda.dat

retorna o objeto: ItemVenda(int idVenda, atring produto, 
int qtd, int valorUnitario, int totalItem)
string line ItemVenda
 */
namespace BILTIFUL.Modulo2
{
    internal class ItemVenda
    {
        public int idVenda; //puxar da venda, deve ser o mesmo
        public string produto; //puxar do cadastro
        public int qtd; //máx 999
        public int valorUnitario;//1 a 99 999 VEM DO PRODUTO + HISTÓRICO
        public int totalItem;

        public void CalcularTotalItem()
        {
            totalItem = qtd * valorUnitario;
        }
        public string FormatarParaArquivo()
        {
            return ($"{idVenda}{produto}{qtd}{valorUnitario}{totalItem}");
        }

        public ItemVenda(int idVenda, string produto, int qtd, int valorUnitario) //construtor
        {
            this.idVenda = idVenda;
            this.produto = produto;
            if (qtd > 3)
            {
                Console.WriteLine("A quantidade não pode ser superior a 3 unidades por venda.");
            }
            this.qtd = qtd;
            valorUnitario = valorUnitario;
            CalcularTotalItem();
        }


        public override string? ToString()
        {
            return base.ToString();
        }
    }
}
