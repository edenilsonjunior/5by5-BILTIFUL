using BILTIFUL.Modulo1;
using BILTIFUL.Modulo3.ManipuladorArquivos;
using System.Reflection;
namespace BILTIFUL.Modulo3
{
    internal class MainModulo3
    {
        public MainModulo3()
        {
            //Variavel global
            string path = @"C:\BILTIFUL\";

            //Listas para acessar as propriedades das classes necessarias
            List<Fornecedor> listFornecedor = new(ManipuladorArquivoCompra.importarFornecedor(path, "Fornecedor.dat"));
            List<string> listFornecedorBloqueados = new(ManipuladorArquivoCompra.importarFornecedorBloqueado(path, "Bloqueado.dat"));
            List<MPrima> listMPrima = new(ManipuladorArquivoCompra.importarMPrima(path, "Materia.dat"));
            List<Compra> listaCompra = new(ManipuladorArquivoCompra.importarCompra(path, "Compra.dat"));
            List<ItemCompra> listaItemCompra = new(ManipuladorArquivoCompra.importarItemCompra(path, "ItemCompra.dat"));

            static void RealizarCompra()
            {
                Compra compra;
                int Id = 0;
                var data = DateOnly.FromDateTime(DateTime.Now);
                string tempCNPJ, mensagem = "";

                if (listaCompra.Count == 0)
                {
                    Id = 1;
                }
                else
                {
                    Id = listaCompra.Last().Id + 1;
                }

                bool podeCadastrar = true;
                do
                {
                    Console.Write("Informe um CNPJ de fornecedor já cadastrado (14 digitos): ");
                    tempCNPJ = Console.ReadLine();

                    if (listFornecedor.Find(x => x.Cnpj == tempCNPJ) == null)
                    {
                        Console.WriteLine("Fornecedor informado não existe");
                        podeCadastrar = false;
                    }
                    else if ((listFornecedor.Find(x => x.Cnpj == tempCNPJ && x.Situacao.ToString() == "I") != null))
                    {
                        Console.WriteLine("Fornecedor está inativo, digite novamente um fornecedor que esteja ativo!!");
                        podeCadastrar = false;
                    }
                    else if (listFornecedorBloqueados.Contains(tempCNPJ))
                    {
                        Console.WriteLine("Fornecedor está bloqueado, não podemos comprar de fornecedores bloqueados.");
                        podeCadastrar = false;
                    }
                    else if (listFornecedor.Find(x => x.Cnpj == tempCNPJ && (DateTime.Now.Date - new DateTime(x.DataAbertura.Year, x.DataAbertura.Month, x.DataAbertura.Day)).Days < 180) != null)
                    {
                        Console.WriteLine("Fornecedor com menos de seis meses, para comprar dele se faz necessario que esteja aberto a mais de seis meses");
                        podeCadastrar = false;
                    }
                    else
                    {
                        var razaoSocial = listFornecedor.Find(x => x.Cnpj == tempCNPJ).RazaoSocial;
                        var dataAberturaFornecedor = listFornecedor.Find(x => x.Cnpj == tempCNPJ).DataAbertura;

                        Console.WriteLine("A razão social do fornecedor informado consta: " + razaoSocial);
                        Console.WriteLine("A data de abertura do fornecedor informado consta: " + dataAberturaFornecedor);
                        Console.Write("Informe se deseja prosseguir com as informacoes acima (S/N): ");

                        string opcaoUsuario = Console.ReadLine();

                        if (opcaoUsuario.ToLower() == "s")
                        {
                            podeCadastrar = true;
                        }
                        else
                        {
                            podeCadastrar = false;
                            Console.Clear();
                            Console.WriteLine("Voltando ao inicio do programa..");
                        }
                    }
                } while (podeCadastrar == false);

                if (podeCadastrar)
                {
                    compra = new(Id, data, tempCNPJ, 0);
                    listaCompra.Add(compra);
                    PegarMateriaPrima(Id);
                    Console.WriteLine("Compra cadastrada com sucesso!");
                    EscreverNoArquivo<Compra>(listaCompra, "Compra.dat");
                }
            }

            static void PegarMateriaPrima(int idMPrima)
            {
                ItemCompra item = new();
                bool podeCadastrar = true;
                string materiaPrimaTemp = "";
                var dataAtual = DateOnly.FromDateTime(DateTime.Now);
                float valorUnitario = 0, valorTotalPorMateria = 0, quantidadeCompraMPrima = 0, valorTotal = 0, quantidadeMPrima = 0;

                do
                {
                    Console.Write("Informe quantas matérias primas deseja comprar: ");
                    quantidadeMPrima = retornarInt();
                    if (quantidadeMPrima > 3)
                    {
                        podeCadastrar = false;
                        Console.WriteLine("Não pode comprar mais de 3 materias primas de uma vez");
                    }
                    else if (quantidadeMPrima <= 0)
                    {
                        podeCadastrar = false;
                        Console.WriteLine("Precisa compra pelo menos uma materia prima.");
                    }
                    else
                    {
                        podeCadastrar = true;
                        Console.Clear();
                    }
                } while (podeCadastrar == false);

                for (int i = 0; i < quantidadeMPrima; i++)
                {
                    do
                    {
                        Console.Write("Digite o ID da materia prima que deseja comprar: ");
                        materiaPrimaTemp = Console.ReadLine().ToUpper();
                        if (listMPrima.Find(x => x.Id == materiaPrimaTemp) == null)
                        {
                            podeCadastrar = false;
                            Console.WriteLine("Matéria prima inexistente, cadastre uma válida!!");
                        }
                        else if ((listMPrima.Find(x => x.Id == materiaPrimaTemp && x.Situacao.ToString() == "I") != null))
                        {
                            podeCadastrar = false;
                            Console.WriteLine("Matéria prima informada consta inativa!! Informe uma que esteja ativa.");
                        }
                        else
                        {
                            podeCadastrar = true;
                            Console.Clear();
                        }
                    } while (podeCadastrar == false);
                    do
                    {
                        do
                        {
                            Console.Write("Digite quantas matérias prima desse tipo voce deseja comprar: ");
                            quantidadeCompraMPrima = retornarFloat();

                            if (quantidadeCompraMPrima > 99999)
                            {
                                podeCadastrar = false;
                                Console.WriteLine("Passou da quantidade permitida de produtos comprados. Max: 99999");
                            }
                            else if (quantidadeCompraMPrima <= 0)
                            {
                                podeCadastrar = false;
                                Console.WriteLine("É preciso comprar pelo menos 1");
                            }
                            else
                            {
                                podeCadastrar = true;
                                Console.Clear();
                            }
                        }
                        while (podeCadastrar == false);
                        do
                        {
                            Console.Write("Digite o valor da matéria prima escolhida: ");
                            valorUnitario = retornarFloat();

                            if (valorUnitario > 99999)
                            {
                                podeCadastrar = false;
                                Console.WriteLine("Valor Excedido!! Max: 99999 por item.");
                            }
                            else if (valorUnitario <= 0)
                            {
                                podeCadastrar = false;
                                Console.WriteLine("O valor unitario precisa ser acima de 0.");
                            }
                            else
                            {
                                podeCadastrar = true;
                                valorTotalPorMateria = quantidadeCompraMPrima * valorUnitario;
                                valorTotal += valorTotalPorMateria;
                                Console.Clear();
                            }
                        } while (podeCadastrar == false);
                        if (valorTotalPorMateria > 999999)
                        {
                            podeCadastrar = false;
                            Console.WriteLine("O valor total ultrapassou o limite, digite valor e/ou quantidade menores.");
                        }
                        else
                        {
                            podeCadastrar = true;
                            Console.Clear();
                        }
                    } while (podeCadastrar == false);
                    item = new(idMPrima, dataAtual, materiaPrimaTemp, quantidadeCompraMPrima, valorUnitario, valorTotalPorMateria);
                    listaItemCompra.Add(item);
                }
                EscreverNoArquivo<ItemCompra>(listaItemCompra, "ItemCompra.dat");
                listaCompra.Find(x => x.Id == idMPrima).ValorTotal = valorTotal;
            }

            static void LocalizarCompra()
            {
                int opc;
                do
                {
                    Console.Clear();
                    Console.Write("Informe o ID da compra que deseja localizar: ");
                    int Id = retornarInt();

                    var compraLocalizada = listaCompra.Find(x => x.Id == Id);
                    var itemCompraLocalizado = listaItemCompra.FindAll(x => x.Id == Id);

                    if (compraLocalizada != null)
                    {
                        Console.WriteLine("======Compra======");
                        Console.WriteLine(compraLocalizada.ImprimirCompraNaTela(listFornecedor));

                        Console.WriteLine("======Item Compra======");
                        foreach (var itemCompra in itemCompraLocalizado)
                        {
                            Console.WriteLine(itemCompra.ImprimirItemCompraNaTela());
                            Console.WriteLine("-".PadLeft(115, '-'));
                        }
                    }
                    else
                    {
                        Console.WriteLine("Nao foi possivel localizar o Id informado.");
                    }
                    Console.Write("Informe 1 para localizar mais algum Id ou 0 para sair: ");
                    opc = retornarInt();
                } while (opc != 0);
            }

            static void ExcluirCompra()
            {
                int opc = 0;
                if (listaCompra.Count == 0 || listaItemCompra.Count == 0)
                {
                    Console.WriteLine("Não há compra/item compra cadastrado.");
                }
                else
                {
                    do
                    {
                        Console.Write("Informe o ID da compra que deseja excluir: ");
                        int Id = retornarInt();

                        var compraLocalizada = listaCompra.RemoveAll(x => x.Id == Id);
                        var itemCompraLocalizado = listaItemCompra.RemoveAll(x => x.Id == Id);

                        if (compraLocalizada != null)
                        {
                            Console.WriteLine($"Compra do Id {Id} excluida com sucesso!");

                            EscreverNoArquivo<Compra>(listaCompra, "Compra.dat");
                            EscreverNoArquivo<ItemCompra>(listaItemCompra, "ItemCompra.dat");
                        }
                        else
                        {
                            Console.WriteLine("Nao foi possivel localizar o Id informado.");
                        }
                        Console.Write("Informe 1 para excluir mais algum Id ou 0 para sair: ");
                        opc = retornarInt();
                    } while (opc != 0);
                }
            }

            static void ImprimirCompra()
            {
                int opc, Id = 0;

                if (listaCompra.Count == 0 || listaItemCompra.Count == 0)
                {
                    Console.WriteLine("Não há compra/item compra cadastrado.");
                }
                else
                {

                    Console.Write("Informe o ID da compra que deseja localizar: ");
                    Id = retornarInt();
                    MostrarDetalhesCompra(Id);
                    Compra tempIdLista;
                    do
                    {
                        Console.WriteLine("Escolha uma opcão:");
                        Console.WriteLine("1 - Voltar para o o ID anterior");
                        Console.WriteLine("2 - Ir para o proximo ID");
                        Console.WriteLine("3 - Voltar para o primeiro ID");
                        Console.WriteLine("4 - Ir para o ultimo ID");
                        Console.WriteLine("0 - Sair");
                        Console.Write("R: ");
                        opc = retornarInt();
                        int IdInicial = listaCompra.First().Id;
                        int IdFinal = listaCompra.Last().Id;
                        switch (opc)
                        {
                            case 1:
                                if (Id > IdInicial)
                                {
                                    do
                                    {
                                        Id -= 1;
                                        tempIdLista = listaCompra.Find(x => x.Id == Id);
                                    } while (tempIdLista == null);
                                    //enquanto o anterior nao existir, ele decrementa ate achar
                                    MostrarDetalhesCompra(Id);
                                }
                                else
                                {
                                    Console.WriteLine("Voce ja esta no primeiro Id.");
                                    Console.Clear();
                                }
                                break;
                            case 2:
                                if (Id < IdFinal)
                                {
                                    do
                                    {
                                        Id += 1;
                                        tempIdLista = listaCompra.Find(x => x.Id == Id);
                                    } while (tempIdLista == null);
                                    //enquanto o proximo nao existir, ele vai incrementar ate achar
                                    MostrarDetalhesCompra(Id);
                                }
                                else
                                {
                                    Console.WriteLine("Voce ja esta no ultimo ID.");
                                    Console.Clear();
                                }
                                break;
                            case 3:
                                if (IdInicial != null)
                                {
                                    Id = IdInicial;
                                    MostrarDetalhesCompra(Id);
                                }
                                break;
                            case 4:
                                if (IdFinal != null)
                                {
                                    Id = IdFinal;
                                    MostrarDetalhesCompra(Id);
                                }
                                break;
                            case 0:
                                Console.WriteLine("Saindo...");
                                break;
                            default:
                                Console.WriteLine("Opção inválida.");
                                break;
                        }
                    } while (opc != 0);
                }
            }

            static void MostrarDetalhesCompra(int Id)
            {
                var compraLocalizada = listaCompra.Find(x => x.Id == Id);
                var itemCompraLocalizado = listaItemCompra.FindAll(x => x.Id == Id);

                if (compraLocalizada != null)
                {
                    Console.WriteLine("======Compra======");
                    Console.WriteLine(compraLocalizada.ImprimirCompraNaTela(listFornecedor));

                    Console.WriteLine("======Item Compra======");
                    foreach (var itemCompra in itemCompraLocalizado)
                    {
                        Console.WriteLine(itemCompra.ImprimirItemCompraNaTela());
                        Console.WriteLine("-".PadLeft(115, '-'));
                    }
                }
                else
                {
                    Console.WriteLine("Nao foi possivel localizar o Id informado.");
                }
            }

            static int Menu()
            {
                Console.Clear();
                Console.WriteLine("======Modulo de Compras======");

                Console.WriteLine("Opcoes: ");
                Console.WriteLine("1- Cadastrar uma compra");
                Console.WriteLine("2- Localizar uma compra");
                Console.WriteLine("3- Excluir uma compra");
                Console.WriteLine("4- Imprimir uma compra");
                Console.WriteLine("5- Voltar ao inicio do programa");
                Console.WriteLine("0- Exit");
                Console.Write("R: ");

                int option = retornarInt();
                return option;
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

            static float retornarFloat()
            {
                float Quantidade = 0;
                bool valor = false;

                while (!valor)
                {
                    if (float.TryParse(Console.ReadLine(), out float qtde))
                    {
                        Quantidade = qtde;
                        valor = true;
                    }
                    else
                    {
                        Console.WriteLine("Formato inválido. Preencha por exemplo 0,01 1,00 10,00 100,00");
                    }
                }
                return Quantidade;
            }

            static void EscreverNoArquivo<T>(List<T> l, string file)
            {
                string path = @"C:\BILTIFUL\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                StreamWriter conteudoArquivo = new(path + file);
                foreach (var item in l)
                {
                    conteudoArquivo.WriteLine(item.ToString());
                }
                conteudoArquivo.Close();
            }

            //Programa em si
            do
            {
                switch (Menu())
                {
                    case 1:
                        RealizarCompra();
                        break;
                    case 2:
                        LocalizarCompra();
                        break;
                    case 3:
                        ExcluirCompra();
                        break;
                    case 4:
                        ImprimirCompra();
                        break;
                }
                Console.WriteLine("Deseja fazer mais alguma operacao?");
                Console.WriteLine("Se sim digite 1");
                Console.WriteLine("Se nao digite 0");
                Console.Write("R: ");
            } while (Menu() != 0);
        }
    }
}