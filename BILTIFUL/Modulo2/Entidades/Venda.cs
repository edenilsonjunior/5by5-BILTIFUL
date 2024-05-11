using BILTIFUL.Modulo1;
using BILTIFUL.Modulo1.ManipuladorArquivos;
using BILTIFUL.Modulo4.Entidades;
using System.Collections.Generic;
//**
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


       /* public void LocalizarVenda()
        {

            int Id;
            Console.WriteLine("Informe o ID da venda que deseja localizar:");
            Id = retornarInt();
            ImprimirVendaAux(Id);
            Console.WriteLine("Pressione qualquer tecla para continuar.");
            Console.ReadKey();

        }

        static int retornarInt()
        {
            int Inteiro = 0;
            bool ex = false;

            while (!ex)
            {
                if (int.TryParse(Console.ReadLine(), out int varint))
                {
                    Inteiro = varint;
                    ex = true;
                }
                else
                {
                    Console.WriteLine("Formato inválido. Informe números inteiros apenas.");
                }
            }
            return Inteiro;
        }

        public void ExcluirVenda()
        {
            //localizar venda e excluir
        }

        public void ImprimirVenda(List<Venda> listaVenda)
        {
            int Id, IdInicial, IdFinal, opcao = 10;
            Venda listaTemporaria;
            if (listaVenda.Count == 0)
            {
                Console.WriteLine("Não há venda cadastrada!");
                Console.WriteLine("Pressione qualquer tecla para continuar.");
                Console.ReadKey();
            }
            else
            {
                IdInicial = listaVenda.First().idVenda;
                Id = IdInicial;
                IdFinal = listaVenda.Last().idVenda;
                ImprimirVendaAux(IdInicial);
                while (opcao != 9)
                {
                    Console.WriteLine("Digite:");
                    Console.WriteLine("[ 1 - Voltar ]           [ 2 - Avançar ]");
                    Console.WriteLine("[ 0 - Voltar ao Início ] [ 3 - Avançar ao Final ]");
                    Console.WriteLine("[ 9 - Sair ]");
                    opcao = retornarInt();
                    switch (opcao)
                    {
                        case 0:
                            Id = IdInicial;
                            ImprimirVendaAux(idVenda);
                            break;
                        case 1:
                            if (Id > IdInicial)
                            {
                                do
                                {
                                    Id--;
                                    listaTemporaria = listaVenda.Find(x => x.idVenda == idVenda);
                                } while (listaTemporaria == null);
                                ImprimirVendaAux(idVenda);
                            }
                            else
                            {
                                ImprimirVendaAux(IdInicial);
                                Console.WriteLine("Inicio da Lista!");
                            }
                            break;
                        case 2:
                            if (Id < IdFinal)
                            {
                                do
                                {
                                    Id++;
                                    listaTemporaria = listaVenda.Find(x => x.idVenda == idVenda);
                                } while (listaTemporaria == null);
                                ImprimirVendaAux(idVenda);
                            }
                            else
                            {
                                ImprimirVendaAux(IdFinal);
                                Console.WriteLine("Fim da Lista!");
                            }
                            break;
                        case 3:
                            Id = IdFinal;
                            ImprimirVendaAux(idVenda);
                            break;
                        case 9:
                            break;
                        default:
                            Console.WriteLine("Opção inválida.");
                            break;
                    }
                }
            }
        }

       void ImprimirVendaAux(int idVenda)
        {
            var vendaLocalizada = listaVenda.Find(x => x.idVenda == idVenda);
            var itemVendaLocalizado = listaItemVenda.FindAll(x => x.idVenda == idVenda);

            if (vendaLocalizada != null)
            {
                Console.Clear();
                Console.WriteLine("-".PadLeft(115, '-'));
                Console.WriteLine(vendaLocalizada.imprimirNaTela(listaVenda));
                Console.WriteLine("-".PadLeft(115, '-'));
                foreach (ItemProducao item in itemVendaLocalizado)
                {
                    Console.WriteLine(item.imprimirNaTela(listaItemVenda));
                }
                Console.WriteLine("-".PadLeft(115, '-'));
            }
            else
            {
                Console.WriteLine("Não foi localizada uma venda com esse ID.");
            }
        }*/
    }
}
 

