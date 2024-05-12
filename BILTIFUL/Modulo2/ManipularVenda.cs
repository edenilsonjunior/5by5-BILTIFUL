
using BILTIFUL.Modulo1;
using BILTIFUL.Modulo2.ManipuladorArquivos;
using BILTIFUL.Modulo4.Entidades;

//****
namespace BILTIFUL.Modulo2
{
    internal class ManipularVenda
    {

        static public void CadastrarVenda(List<Venda> listaVenda, List<Cliente> listaCliente, List<Cliente> listaBloqueados)
        {

            DateOnly dataatual = DateOnly.FromDateTime(DateTime.Now);
            string cpf;
            int Id = 0;
            List<Produto> listaProduto = new List<Produto>(ArquivoVenda.importarProduto(@"C:\Biltiful\", "Cosmetico.dat"));
            Id = listaVenda.Last().idVenda + 1;

            do
            {
                Console.Write("Insira o CPF do cliente cadastrado: ");
                cpf = Console.ReadLine();

                if (cpf != "0" && Cliente.VerificarCpf(cpf) == false)
                {
                    Console.WriteLine("CPF inválido!");
                }
            } while (cpf != "0" && Cliente.VerificarCpf(cpf) == false);

            // Bloco de bloqueadores
            // por cpf
            Cliente? ClienteVenda = listaCliente.Find(x => x.Cpf == cpf);

            if (ClienteVenda == null)
            {
                Console.WriteLine("Não existe cliente cadastrado com esse CPF.");
                return;
            }

            //inadimplentes
            if (listaBloqueados.Find(x => x.Cpf == cpf) != null)
            {
                Console.WriteLine("Venda não autorizada para este CPF.");
                return;
            }

            //por menor de idade
            DateTime dataAtual = DateTime.Now;
            int idade = dataAtual.Year - ClienteVenda.DataNascimento.Year;

            if (idade < 18)
            {
                Console.WriteLine("Venda não permitida para menor de 18 anos.");
                return;
            }

            // por id maximo
            if (Id > 99999)
            {
                Console.WriteLine("Excedeu o limite de vendas cadastradas no sistema.");
                return;
            }

            Console.WriteLine("Quantos itens deseja comprar?");
            int qtditens = int.Parse(Console.ReadLine());
            while (qtditens <= 0 || qtditens > 3)
            {
                Console.WriteLine("Quantidade inválida, mín 1 e máx 3. Digite novamente!");
                qtditens = int.Parse(Console.ReadLine());
            }

            ItemVenda[] Itens = new ItemVenda[qtditens];
            for (int i = 0; i < qtditens; i++)
            {
                Itens[i] = ManipularItemVenda.CriarItemVenda(Id, listaProduto);
            }

            float total = 0;
            foreach (ItemVenda item in Itens)
            {
                total += item.totalItem;
            }
            if (total > 99999.99)
            {
                Console.WriteLine("Venda excede o limite de valor permitido.");
                return;
            }

            Venda venda = new Venda(Id, DateOnly.FromDateTime(DateTime.Now), ClienteVenda.Cpf, total);

            List<Venda> listdeVenda = ArquivoVenda.importarVenda(@"C:\Biltiful\", "Venda.dat");
            listdeVenda.Add(venda);

            ArquivoVenda.salvarArquivo(listdeVenda, "Venda.dat");

            List<ItemVenda> listItemVenda = ArquivoVenda.importarItemVenda(@"C:\Biltiful\", "ItemVenda.dat");

            foreach (ItemVenda item in Itens)
            {
                listItemVenda.Add(item);
            }

            ArquivoVenda.salvarArquivo(listItemVenda, "ItemVenda.dat");

            Console.WriteLine("Pressione qualquer tecla");
            Console.ReadKey();
        }


        static public void LocalizarVenda()
        {

            int Id;
            Console.WriteLine("Informe o ID da venda que deseja localizar:");
            Id = retornarInt();
            ImprimirVendaAux(Id);
            Console.WriteLine("Pressione qualquer tecla para continuar.");
            Console.ReadKey();

        }

        static public int retornarInt()
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

        static public void ExcluirVenda()
        {
           
            string opcao = "";
            int Id;

            Console.WriteLine("Informe o ID da venda que deseja EXCLUIR:");
            Id = retornarInt();

            var vendaLocalizada = listaVenda.Find(x => x.Id == Id);
            if (vendaLocalizada != null)
            {
                ImprimirVendaAux(Id);
                Console.WriteLine("Pressione qualquer tecla para continuar.");
                Console.ReadKey();
                Console.WriteLine("Confirma exclusão da venda?");
                Console.WriteLine("[ S - Sim ] [ Qualquer tecla - Não ]");
                opcao = Console.ReadLine();
                if (opcao.ToLower() == "s")
                {
                    listItemVenda.RemoveAll(x => x.Id == Id);
                    listaVenda.Remove(vendaLocalizada);
                    ArquivoVenda.salvarArquivo(listaVenda, @"C:\Biltiful\", "Venda.dat");
                    ArquivoVenda.salvarArquivo(listItemVenda, @"C:\Biltiful\", "ItemVenda.dat");
                    Console.WriteLine($"Exclusão realizada com sucesso!");
                }
            }
            else
            {
                Console.WriteLine("Não foi localizado uma venda com esse ID, tente novamente.");
            }
            Console.WriteLine("Pressione qualquer tecla para continuar.");
            Console.ReadKey();
        }
    }

    static public void ImprimirRegisVenda(List<Venda> listaVenda)
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
                        ImprimirVendaAux(Id);
                        break;
                    case 1:
                        if (Id > IdInicial)
                        {
                            do
                            {
                                Id--;
                                listaTemporaria = listaVenda.Find(x => x.idVenda == Id);
                            } while (listaTemporaria == null);
                            ImprimirVendaAux(Id);
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
                                listaTemporaria = listaVenda.Find(x => x.idVenda == Id);
                            } while (listaTemporaria == null);
                            ImprimirVendaAux(Id);
                        }
                        else
                        {
                            ImprimirVendaAux(IdFinal);
                            Console.WriteLine("Fim da Lista!");
                        }
                        break;
                    case 3:
                        Id = IdFinal;
                        ImprimirVendaAux(Id);
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

    static void ImprimirVendaAux(int idVenda)
    {
        var vendaLocalizada = listaVenda.Find(x => x.idVenda == idVenda);
        var itemVendaLocalizado = listItemVenda.FindAll(x => x.idVenda == idVenda);

        if (vendaLocalizada != null)
        {
            Console.Clear();
            Console.WriteLine("-".PadLeft(115, '-'));
            Console.WriteLine(vendaLocalizada.Imprimir(listaVenda));
            Console.WriteLine("-".PadLeft(115, '-'));
            foreach (ItemProducao item in itemVendaLocalizado)
            {
                Console.WriteLine(item.Imprimir(listItemVenda));
            }
            Console.WriteLine("-".PadLeft(115, '-'));
        }
        else
        {
            Console.WriteLine("Não foi localizada uma venda com esse ID.");
        }
    }
}




