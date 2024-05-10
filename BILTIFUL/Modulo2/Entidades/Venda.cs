/*VENDA - Classe
Atributos
1) ID - int núm sequencial de 1 a 99999 indice 0-4 vetor?
*não pode ter 2 registros
2) Data - DateOnly / data atual
3) Cliente - armazena apenas o CPF, mas mostra nome e data de nasc
*não pode vender pra menor de 18 anos
* não vender inadimplente(inativo no Cliente)
4) Valor total - int núm máx 99.999,99 vetor?

Função/Método
1) cadastrar:
2) localizar: uma venda e seus itens -percorrer lista cliente e imprimir?
3) excluir: apaga a venda e seus respectivos itens -percorrer lista e excluir?
4) impressão por registro: podendo ir para o próximo/anterior ou para as extremidades 

salvar: FormatarParaArquivo (): string  Vendas.dat

Venda - string line
Venda (int i, DataVenda, string Cliente, int valorTotal*/
namespace BILTIFUL.Modulo2
{
    internal class Venda
    {
        public int idVenda;
        public DateOnly dataVenda;
        public int valorTotal;
        public string cliente;

        public Venda(int idVenda, DateOnly dataVenda, int valorTotal, string cliente)
        {
            this.idVenda = idVenda;//chave
            this.dataVenda = dataVenda;
            this.cliente = cliente;
            this.valorTotal = valorTotal;

        }

        bool ValidarVenda()
        {
            return true;
        }

        public void CadastrarVenda()
        {
            //não pode ter 2 com o mesmo id
        }

        public void LocalizarVenda()
        {

        }

        public void ExcluirVenda()
        {

        }

        public void ImprimirRegisVenda()
        {

        }



        public void FormatarParaArquivo()
        {

        }

        public override string? ToString()
        {
            return base.ToString();
        }
    }
}
