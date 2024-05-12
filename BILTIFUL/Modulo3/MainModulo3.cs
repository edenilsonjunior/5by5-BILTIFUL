using BILTIFUL.Modulo1;
using BILTIFUL.Modulo3.ManipuladorArquivos;
namespace BILTIFUL.Modulo3
{
    internal class MainModulo3
    {
        public MainModulo3()
        {
            //Variavel global
            string path = @"C:\Biltiful\";

            //Listas para acessar as propriedades das classes necessarias
            List<Fornecedor> listFornecedor = new(ManipuladorArquivoCompra.importarFornecedor(@"C:\Teste\", "Fornecedor.dat"));
            List<string> listFornecedorBloqueados = new(ManipuladorArquivoCompra.importarFornecedorBloqueado(@"C:\Teste\", "Bloqueado.dat"));
            List<MPrima> listMPrima = new(ManipuladorArquivoCompra.importarMPrima(@"C:\Teste\", "Materia.dat"));
            List<Compra> listaCompra = new(ManipuladorArquivoCompra.importarCompra(path, "Compra.dat"));
            List<ItemCompra> listaItemCompra = new(ManipuladorArquivoCompra.importarItemCompra(path, "ItemCompra.dat"));

            void RealizarCompra()
            {
                Compra compra;
                int Id = 1;
                var data = DateOnly.FromDateTime(DateTime.Now);
                string tempCNPJ, mensagem = "";

                if (listaCompra.Count != 0)
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
                    EscreverNoArquivo<Compra>(listaCompra, "Compra.dat");
                }
            }

            void PegarMateriaPrima(int idMPrima)
            {
                ItemCompra item = new();
                bool podeCadastrar = true;
                string materiaPrimaTemp = "";
                var dataAtual = DateOnly.FromDateTime(DateTime.Now);
                float valorUnitario = 0, valorTotalPorMateria = 0, quantidadeCompraMPrima = 0, valorTotal = 0;

                Console.Write("Informe quantas matérias primas deseja comprar: ");
                int quantidadeMPrima = retornarInt();

                if (quantidadeMPrima > 3)
                {
                    Console.WriteLine("Não pode comprar mais de 3 matérias primas de uma vez");
                }
                else
                {
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
                            }
                        } while (podeCadastrar == false);
                        item = new(idMPrima, dataAtual, materiaPrimaTemp, quantidadeCompraMPrima, valorUnitario, valorTotalPorMateria);
                        listaItemCompra.Add(item);
                    }
                    EscreverNoArquivo<ItemCompra>(listaItemCompra, "ItemCompra.dat");
                    listaCompra.Find(x => x.Id == idMPrima).ValorTotal = valorTotal;
                }
            }

            void LocalizarCompra()
            {
                Console.Write("Informe o ID da compra que deseja localizar: ");
                int Id = retornarInt();

                var compraLocalizada = listaCompra.Find(x => x.Id == Id);
                var itemCompraLocalizado = listaItemCompra.Find(x => x.Id == Id);

                Console.WriteLine(compraLocalizada.ImprimirCompraNaTela());

                foreach (var item in listaItemCompra)
                {
                    Console.WriteLine(itemCompraLocalizado.ImprimirItemCompraNaTela());
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

            void EscreverNoArquivo<T>(List<T> l, string file)
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
            switch (Menu())
            {
                case 1:
                    RealizarCompra();
                    break;
                case 2:
                    LocalizarCompra();
                    break;
                case 3:
                    //ExcluirCompra();
                    break;
                case 4:
                    //ImprimirCompra();
                    break;
            }
        }
    }
}