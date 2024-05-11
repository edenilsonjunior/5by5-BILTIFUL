using BILTIFUL.Modulo1.ManipuladorArquivos;

namespace BILTIFUL.Modulo1
{
    internal class MainModulo1
    {

        private readonly string caminho = @"C:\Biltiful\";
        private readonly string arquivoCliente = "Clientes.dat";
        private readonly string arquivoFornecedor = "Fornecedor.dat";
        private readonly string arquivoProduto = "Cosmetico.dat";
        private readonly string arquivoMPrima = "Materia.dat";
        private readonly string arquivoRisco = "Risco.dat";
        private readonly string arquivoBloqueado = "Bloqueado.dat";

        private ManipularCliente _cliente;
        private ManipularFornecedor _fornecedor;
        private ManipularProduto _produto;
        private ManipularMPrima _materia;
        private ManipularInadimplentes _risco;
        private ManipularBloqueados _bloqueado;

        public MainModulo1()
        {
            _cliente = new(caminho, arquivoCliente);
            _fornecedor = new(caminho, arquivoFornecedor);
            _produto = new(caminho, arquivoProduto);
            _materia = new(caminho, arquivoMPrima);
            _risco = new(caminho, arquivoRisco);
            _bloqueado = new(caminho, arquivoBloqueado);
        }

        public void Executar()
        {
            int opcao;
            do
            {
                opcao = MenuPrincipal();
                switch (opcao)
                {
                    case 1:
                        MenuCliente();
                        break;
                    case 2:
                        MenuFornecedor();
                        break;
                    case 3:
                        MenuMPrima();
                        break;
                    case 4:
                        MenuProduto();
                        break;
                    default:
                        Console.WriteLine("Opcao invalida!");
                        break;
                }

                Console.Write("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            } while (opcao != 0);
        }



        private void MenuCliente()
        {
            int opcao;
            do
            {
                Console.Clear();
                Console.WriteLine("======Menu Cliente======");

                Console.WriteLine("Opcoes: ");
                Console.WriteLine("1- Cadastrar cliente");
                Console.WriteLine("2- Editar cliente");
                Console.WriteLine("3- Imprimir cliente especifico");
                Console.WriteLine("4- Imprimir todos os clientes");
                Console.WriteLine("5- Menu dos clientes de risco");
                Console.WriteLine("0- Voltar");
                Console.Write("R: ");

                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.WriteLine("Voce deve digitar um numero!");
                }
                else
                {
                    switch (opcao)
                    {
                        case 1:
                            _cliente.Cadastrar();
                            break;
                        case 2:
                            _cliente.Editar();
                            break;
                        case 3:
                            _cliente.Localizar();
                            break;
                        case 4:
                            _cliente.Imprimir();
                            break;
                        case 5:
                            MenuRisco();
                            break;
                        case 0:
                            Console.WriteLine("Opcao Voltar selecionada");
                            break;
                        default:
                            Console.WriteLine("Opcao invalida!");
                            break;
                    }
                }

                Console.Write("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            } while (opcao != 0);
        }

        private void MenuRisco()
        {
            int opcao = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("======Menu Risco======");

                Console.WriteLine("Opcoes: ");
                Console.WriteLine("1- Adicionar cliente de risco");
                Console.WriteLine("2- Remover cliente");
                Console.WriteLine("3- Localizar cliente especifico");
                Console.WriteLine("4- Listar todos os clientes de risco");
                Console.WriteLine("0- Voltar");
                Console.Write("R: ");

                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.WriteLine("Voce deve digitar um numero!");
                }
                else
                {
                    switch (opcao)
                    {
                        case 1:
                            _risco.Adicionar();
                            break;
                        case 2:
                            _risco.Remover();
                            break;
                        case 3:
                            _risco.Localizar();
                            break;
                        case 4:
                            _risco.Imprimir();
                            break;
                        case 0:
                            Console.WriteLine("Opcao Voltar selecionada");
                            break;
                        default:
                            Console.WriteLine("Opcao invalida!");
                            break;
                    }
                }

                Console.Write("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            } while (opcao != 0);
        }


        private void MenuFornecedor()
        {
            int opcao = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("======Menu Fornecedor======");

                Console.WriteLine("Opcoes: ");
                Console.WriteLine("1- Cadastrar fornecedor");
                Console.WriteLine("2- Editar fornecedor");
                Console.WriteLine("3- Imprimir fornecedor especifico");
                Console.WriteLine("4- Imprimir todos os fornecedores");
                Console.WriteLine("5- Menu dos fornecedores bloqueados");
                Console.WriteLine("0- Voltar");
                Console.Write("R: ");

                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.WriteLine("Voce deve digitar um numero!");
                }
                else
                {
                    switch (opcao)
                    {
                        case 1:
                            _fornecedor.Cadastrar();
                            break;
                        case 2:
                            _cliente.Editar();
                            break;
                        case 3:
                            _cliente.Localizar();
                            break;
                        case 4:
                            _fornecedor.Imprimir();
                            break;
                        case 5:
                            MenuBloqueado();
                            break;
                        case 0:
                            Console.WriteLine("Opcao Voltar selecionada");
                            break;
                        default:
                            Console.WriteLine("Opcao invalida!");
                            break;
                    }
                }

                Console.Write("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            } while (opcao != 0);
        }

        private void MenuBloqueado()
        {
            int opcao;
            do
            {
                Console.Clear();
                Console.WriteLine("======Menu Bloqueados======");

                Console.WriteLine("Opcoes: ");
                Console.WriteLine("1- Adicionar fornecedor");
                Console.WriteLine("2- Remover fornecedor da lista de bloqueados");
                Console.WriteLine("3- Localizar fornecedor bloqueado especifico");
                Console.WriteLine("4- Listar todos os fornecedores bloqueados");
                Console.WriteLine("0- Voltar");
                Console.Write("R: ");

                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.WriteLine("Voce deve digitar um numero!");
                }
                else
                {
                    switch (opcao)
                    {
                        case 1:
                            _bloqueado.Adicionar();
                            break;
                        case 2:
                            _bloqueado.Remover();
                            break;
                        case 3:
                            _bloqueado.Localizar();
                            break;
                        case 4:
                            _bloqueado.Imprimir();
                            break;
                        case 0:
                            Console.WriteLine("Opcao Voltar selecionada");
                            break;
                        default:
                            Console.WriteLine("Opcao invalida!");
                            break;
                    }
                }

                Console.Write("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            } while (opcao != 0);
        }




        private void MenuMPrima()
        {
            int opcao;
            do
            {
                Console.Clear();
                Console.WriteLine("======Menu Materia Prima======");

                Console.WriteLine("Opcoes: ");
                Console.WriteLine("1- Cadastrar materia prima");
                Console.WriteLine("2- Editar materia prima");
                Console.WriteLine("3- Imprimir uma materia-prima especifica");
                Console.WriteLine("4- Imprimir lista de matérias-primas");
                Console.WriteLine("0- Voltar");
                Console.Write("R: ");

                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.WriteLine("Voce deve digitar um numero!");
                }
                else
                {
                    switch (opcao)
                    {
                        case 1:
                            _materia.Cadastrar();
                            break;
                        case 2:
                            _materia.Editar();
                            break;
                        case 3:
                            _materia.Localizar();
                            break;
                        case 4:
                            _materia.Imprimir();
                            break;
                        case 0:
                            Console.WriteLine("Opcao Voltar selecionada");
                            break;
                        default:
                            Console.WriteLine("Opcao invalida!");
                            break;
                    }
                }

                Console.Write("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            } while (opcao != 0);
        }

        private void MenuProduto()
        {
            int opcao;
            do
            {
                Console.Clear();
                Console.WriteLine("======Menu Produto======");

                Console.WriteLine("Opcoes: ");
                Console.WriteLine("1- Cadastrar produto");
                Console.WriteLine("2- Editar produto");
                Console.WriteLine("3- Imprimir um produto especifica");
                Console.WriteLine("4- Imprimir lista de produtos");
                Console.WriteLine("0- Voltar");
                Console.Write("R: ");

                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.WriteLine("Voce deve digitar um numero!");
                }
                else
                {
                    switch (opcao)
                    {
                        case 1:
                            _produto.Cadastrar();
                            break;
                        case 2:
                            _produto.Editar();
                            break;
                        case 3:
                            _produto.Localizar();
                            break;
                        case 4:
                            _produto.Imprimir();
                            break;
                        case 0:
                            Console.WriteLine("Opcao Voltar selecionada");
                            break;
                        default:
                            Console.WriteLine("Opcao invalida!");
                            break;
                    }
                }

                Console.Write("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            } while (opcao != 0);
        }

        private int MenuPrincipal()
        {
            Console.Clear();
            Console.WriteLine("======Menu Modulo1======");

            Console.WriteLine("Opcoes: ");
            Console.WriteLine("1- Menu de cliente");
            Console.WriteLine("2- Menu de fornecedor");
            Console.WriteLine("3- Menu de materia prima");
            Console.WriteLine("4- Menu de produto");
            Console.WriteLine("0- Voltar");
            Console.Write("R: ");

            if (int.TryParse(Console.ReadLine(), out int option))
            {
                return option;
            }
            else
            {
                Console.WriteLine("Voce deve digitar um numero!");
                Console.Write("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                return MenuPrincipal();
            }
        }

        public static string LerString(string texto)
        {
            Console.WriteLine(texto);
            Console.Write("R: ");

            string? s;

            do
            {
                s = Console.ReadLine();
                if (s == null) Console.WriteLine("Texto invalido!");

            } while (s == null || s.Length == 0);

            return s;
        }

        public static int LerInt(string texto)
        {
            Console.WriteLine(texto);
            Console.Write("R: ");

            int numero;
            string? s = Console.ReadLine();

            while (!int.TryParse(s, out numero))
            {
                Console.WriteLine("Numero invalido!");
                Console.WriteLine(texto);
                Console.Write("R: ");
                s = Console.ReadLine();
            }

            return numero;
        }

        public static float LerFloat(string texto)
        {
            Console.WriteLine(texto);
            Console.Write("R: ");

            float numero;
            string? s = Console.ReadLine();

            while (!float.TryParse(s, out numero))
            {
                Console.WriteLine("Numero invalido!");
                Console.WriteLine(texto);
                Console.Write("R: ");
                s = Console.ReadLine();
            }

            return numero;
        }

        public static DateOnly LerData(string texto)
        {
            Console.WriteLine(texto);
            Console.Write("R: ");

            DateOnly dateOnly;
            string? s = Console.ReadLine();

            while (DateOnly.TryParse(s, out dateOnly) == false || dateOnly > DateOnly.FromDateTime(DateTime.Now))
            {
                Console.WriteLine("Data invalida!");
                Console.WriteLine(texto);
                Console.Write("R: ");
                s = Console.ReadLine();
            }

            return dateOnly;
        }

        public static char LerChar(string texto)
        {
            Console.WriteLine(texto);
            Console.Write("R: ");

            char caractere;
            string? s = Console.ReadLine();

            while (!char.TryParse(s, out caractere))
            {
                Console.WriteLine("char invalido!");
                Console.WriteLine(texto);
                Console.Write("R: ");
                s = Console.ReadLine();
            }

            return caractere;
        }

    }
}
