using BILTIFUL.Modulo1;
using BILTIFUL.Modulo2.ManipuladorArquivos;
using BILTIFUL.Modulo4.Entidades;
using BILTIFUL.Modulo2;
//*//

namespace BILTIFUL.Modulo2
{
    internal class ManipularVenda
    {
        private List<Cliente> listaCliente = new List<Cliente>(ArquivoVenda.importarCliente(@"C:\Biltiful\", "Cliente.dat"));
        private List<Venda> listaVenda = new List<Venda>(ArquivoVenda.importarVenda(@"C:\Biltiful\", "Venda.dat"));
        private List<Produto> listaProduto = new List<Produto>(ArquivoVenda.importarProduto(@"C:\Biltiful\", "Cosmetico.dat"));
        private List<string> listaBloqueados = new List<string>(ArquivoVenda.importarBloqueado(@"C:\Biltiful\", "Risco.dat"));
        private List<ItemVenda> listItemVenda = ArquivoVenda.importarItemVenda(@"C:\Biltiful\", "ItemVenda.dat");

        public void CadastrarVenda()
        {

            DateOnly dataatual = DateOnly.FromDateTime(DateTime.Now);
            string cpf;
            int Id = 0;

            if (listaVenda.Count == 0)
            {
                Id = 1;
            }
            else
            {
                Id = listaVenda.Last().idVenda + 1;
            }
            do
            {
                Console.Write("Insira o CPF do cliente cadastrado: ");
                cpf = Console.ReadLine();

                if (cpf != "0" && Cliente.VerificarCpf(cpf) == false)
                {
                    Console.WriteLine("CPF inválido!");
                    Console.ReadKey();
                }
            } while (cpf != "0" && Cliente.VerificarCpf(cpf) == false);

            // Bloco de bloqueadores
            // por cpf
            Cliente? ClienteVenda = listaCliente.Find(x => x.Cpf == cpf);

            if (ClienteVenda == null)
            {
                Console.WriteLine("Não existe cliente cadastrado com esse CPF.");
                Console.WriteLine("Pressione qualquer tecla para continuar.");
                Console.ReadKey();
                return;
            }

            //inadimplentes
            if (listaBloqueados.Contains(cpf))
            {
                Console.WriteLine("Venda não autorizada para este CPF.");
                Console.WriteLine("Pressione qualquer tecla para continuar.");
                Console.ReadKey();
                return;
            }

            //por menor de idade
            DateTime dataAtual = DateTime.Now;
            int idade = dataAtual.Year - ClienteVenda.DataNascimento.Year;

            if (idade < 18)
            {
                Console.WriteLine("Venda não permitida para menor de 18 anos.");
                Console.WriteLine("Pressione qualquer tecla para continuar.");
                Console.ReadKey();
                return;
            }

            // por id maximo
            if (Id > 99999)
            {
                Console.WriteLine("Excedeu o limite de vendas cadastradas no sistema.");
                Console.WriteLine("Pressione qualquer tecla para continuar.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Quantos itens deseja comprar?");
            int qtditens = retornarInt();

            while (qtditens <= 0 || qtditens > 3)
            {
                Console.WriteLine("Quantidade inválida, mín 1 e máx 3. Digite novamente!");
                qtditens = retornarInt();
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
      
            listaVenda.Add(venda);

            ArquivoVenda.salvarArquivo(listaVenda, "Venda.dat");
    
            foreach (ItemVenda item in Itens)
            {
                listItemVenda.Add(item);
            }

            ArquivoVenda.salvarArquivo(listItemVenda, "ItemVenda.dat");

            Console.WriteLine("Pressione qualquer tecla");
            Console.ReadKey();
        }


        public void LocalizarVenda()
        {

            int Id;
            Console.WriteLine("Informe o ID da venda que deseja localizar:");
            Id = retornarInt();
            ImprimirVendaAux(Id);
            Console.WriteLine("Pressione qualquer tecla para continuar.");
            Console.ReadKey();

        }

        public int retornarInt()
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

            string opcao = "";
            int Id;

            Console.WriteLine("Informe o ID da venda que deseja EXCLUIR:");
            Id = retornarInt();

            var vendaLocalizada = listaVenda.Find(x => x.idVenda == Id);
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
                    listItemVenda.RemoveAll(x => x.idVenda == Id);
                    listaVenda.Remove(vendaLocalizada);
                    ArquivoVenda.salvarArquivo(listaVenda, "Venda.dat");
                    ArquivoVenda.salvarArquivo(listItemVenda, "ItemVenda.dat");
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


        public void ImprimirRegisVenda()
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

        public void ImprimirVendaAux(int idVenda)
        {
            var vendaLocalizada = listaVenda.Find(x => x.idVenda == idVenda);
            var itemVendaLocalizado = listItemVenda.FindAll(x => x.idVenda == idVenda);

            if (vendaLocalizada != null)
            {
                Console.Clear();
                Console.WriteLine("-".PadLeft(115, '-'));
                Console.WriteLine(vendaLocalizada.Imprimir(vendaLocalizada, listaCliente, listItemVenda));
                Console.WriteLine("-".PadLeft(115, '-'));
            }

            else
            {
                Console.WriteLine("Não foi localizada uma venda com esse ID.");
            }
        }
       
    }
}




