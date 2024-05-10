/*VENDA
Atributos
3) Cliente - armazena apenas o CPF, mas mostra nome e data de nasc
*não pode vender pra menor de 18 anos
* não vender inadimplente(inativo no Cliente)
4) Valor total - int núm máx 99.999,99 vetor?

Função/Método
1) cadastrar:
2) localizar: uma venda e seus itens -percorrer lista cliente e imprimir?
3) excluir: apaga a venda e seus respectivos itens -percorrer lista e excluir?
4) impressão por registro: podendo ir para o próximo/anterior ou para as extremidades */
using BILTIFUL.Modulo1;
using BILTIFUL.Modulo1.ManipuladorArquivos;

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



        public void LocalizarImprimirVenda()
        {
            //localizar e imprimir venda específica com todos os seus itens
        }

        public void ExcluirVenda()
        {
            //localizar venda e excluir
        }

        public void ImprimirVenda()
        {
            //usuário navega pelos registros, podendo ir próx/ant/prim/últm
        }

        public void FormatarParaArquivo()
        {
            //deixar bonitinho
        }


        public static void ChecarCaminho(string CaminhoDiretorio, string CaminhoArquivo)
        {
            if (!Directory.Exists(CaminhoDiretorio))
            {
                Directory.CreateDirectory(CaminhoDiretorio);
            }
            if (!File.Exists(CaminhoArquivo))
            {
                var file = File.Create(CaminhoArquivo);
                file.Close();
            }
        }

    }
}