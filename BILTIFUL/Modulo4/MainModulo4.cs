using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using BILTIFUL.Modulo4.Entidades;
using BILTIFUL.Modulo4.Utils;

namespace BILTIFUL.Modulo4
{
    internal class MainModulo4
    {
        public MainModulo4()
        {
/*
C:\Biltiful\Producao.txt
0000102052024789111111111125020
0000203052024789111111111235000
0000304052024789111111111105035
0000405052024789111111111200020
0000506052024789111111111100120
C:\Biltiful\ItemProducao.txt
0000102052024MP000112010
0000102052024MP000200210
0000102052024MP000352100
0000102052024MP000401510
0000203052024MP000100029
0000203052024MP000210030
0000304052024MP000300510
0000304052024MP000452000
0000304052024MP000102012
0000405052024MP000242018
0000405052024MP000179283
0000506052024MP000202880
0000506052024MP000301500
0000506052024MP000150170
*/
            // carrega listas pelos arquivos da pasta
            List<Producao> listaProducao = new(Modulo4.Utils.Utils.importarProducao());
            List<ItemProducao> listaItemProducao = new(Modulo4.Utils.Utils.importarItemProducao());
            int opcao = -1;

            while (opcao != 0)
            {
                Console.WriteLine("Escolha sua opçao:");
                Console.WriteLine("1 - Cadastrar Producao");
                Console.WriteLine("2 - Localizar Producao");
                Console.WriteLine("3 - Excluir Producao");
                Console.WriteLine("4 - Impressao Producao");
                Console.WriteLine("0 - Voltar ao Menu Inicial");
                opcao = int.Parse(Console.ReadLine());
                switch (opcao)
                {
                    // sair
                    case 0:
                        break;
                    // cadastrar
                    case 1:
                        inserirProducao(listaProducao, listaItemProducao);
                        break;
                    // localizar
                    case 2:
                        break;
                    // excluir
                    case 3:
                        break;
                    // impressao
                    case 4:
                        break;
                    default:
                        Console.WriteLine("Opcao invalida");
                        break;
                }
            }
        }
        static void inserirProducao(List<Producao> listaProducao, List<ItemProducao> listaItemProducao)
        {
            int Id, contador = 0;
            DateOnly DataProducao = DateOnly.FromDateTime(DateTime.Now);
            string Produto = "", opcao = "";
            float Quantidade = 0;

            Console.WriteLine("Informe o Produto a ser produzido (Cod.Barras):");
            Produto = Console.ReadLine();
            Console.WriteLine("Informe a quantidade a ser produzida:");
            Quantidade = float.Parse(Console.ReadLine());
            Id = listaProducao.Last().Id + 1;

            // Add na lista item producao os itens de materia prima
            do
            {
                if (contador > 1)
                {
                    Console.WriteLine("Deseja inserir mais uma matéria prima?\nS - Sim\nN - Nao\nX - Anular insercao");
                    opcao = Console.ReadLine();
                    if (opcao.ToLower() == "s")
                    {
                        listaItemProducao.Add(inserirItemProducao(Id));
                    }
                }
                else
                {
                    listaItemProducao.Add(inserirItemProducao(Id));
                }
                contador++;
            } while (opcao.ToLower() != "n" && opcao.ToLower() != "x");

            // Por fim cria a producao em si
            if (opcao.ToLower() != "x")
            {
                Producao tempProducao = new(Id, DataProducao, Produto, Quantidade);
                listaProducao.Add(tempProducao);
                Modulo4.Utils.Utils.salvarArquivo(listaProducao, "Producao.txt");
                Modulo4.Utils.Utils.salvarArquivo(listaItemProducao, "ItemProducao.txt");
            } else
            {
                listaItemProducao.RemoveAll(x => x.Id == Id); // se for escolhido anular, exclui da lista producao item os itens
            }
        }
        static ItemProducao inserirItemProducao(int Id)
        {
            DateOnly DataProducao = DateOnly.FromDateTime(DateTime.Now);
            string MateriaPrima = "";
            float QuantidadeMateriaPrima = 0;

            Console.WriteLine("Informe a Matéria Prima a ser utilizada (Código da MP):");
            MateriaPrima = Console.ReadLine();
            Console.WriteLine("Informe a quantidade da matéria prima a ser inserida");
            QuantidadeMateriaPrima = float.Parse(Console.ReadLine());

            ItemProducao itemProducao = new ItemProducao(Id, DataProducao, MateriaPrima, QuantidadeMateriaPrima);
            return itemProducao;
        }
    }
}
