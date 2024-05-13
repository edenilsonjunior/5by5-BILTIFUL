﻿using BILTIFUL.Modulo1.ManipuladorArquivos;

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
            string titulo = "Menu Modulo1";
            string[] campos = new string[] { "Menu de cliente", "Menu de fornecedor", "Menu de materia prima", "Menu de produto" };

            bool terminouMenu = false;
            do
            {
                switch (MenuGenerico(titulo, campos))
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
                    case 0:
                        terminouMenu = true;
                        break;
                    default:
                        Console.WriteLine("Opcao invalida!");
                        break;
                }

                Console.Write("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            } while (!terminouMenu);
        }

        private void MenuCliente()
        {
            string titulo = "Menu Cliente";
            string[] campos = new string[] { "Cadastrar cliente", "Editar cliente", "Imprimir cliente especifico", "Imprimir todos os clientes", "Menu dos clientes de risco" };
            bool terminouMenu = false;

            do
            {
                switch (MenuGenerico(titulo, campos))
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
                        terminouMenu = true;
                        break;
                    default:
                        Console.WriteLine("Opcao invalida!");
                        break;
                }

                Console.Write("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            } while (!terminouMenu);
        }

        private void MenuFornecedor()
        {
            string titulo = "Menu Fornecedor";
            string[] campos = new string[] { "Cadastrar fornecedor", "Editar fornecedor", "Imprimir fornecedor especifico", "Imprimir todos os fornecedores", "Menu dos fornecedores bloqueados" };

            bool terminouMenu = false;
            do
            {

                switch (MenuGenerico(titulo, campos))
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
                        terminouMenu = true;
                        break;
                    default:
                        Console.WriteLine("Opcao invalida!");
                        break;
                }

                Console.Write("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            } while (!terminouMenu);
        }

        private void MenuMPrima()
        {
            string titulo = "Menu Materia Prima";
            string[] campos = new string[] { "Cadastrar materia prima", "Editar materia prima", "Imprimir materia prima especifica", "Imprimir lista de materias primas" };

            bool terminouMenu = false;
            do
            {
                switch (MenuGenerico(titulo, campos))
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
                        terminouMenu = true;
                        break;
                    default:
                        Console.WriteLine("Opcao invalida!");
                        break;
                }

                Console.Write("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            } while (!terminouMenu);
        }

        private void MenuProduto()
        {
            string titulo = "Menu Produto";
            string[] campos = new string[] { "Cadastrar produto", "Editar produto", "Imprimir produto especifico", "Imprimir lista de produtos" };
            bool terminouMenu = false;

            do
            {
                switch (MenuGenerico(titulo, campos))
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
                        terminouMenu = true;
                        break;
                    default:
                        Console.WriteLine("Opcao invalida!");
                        break;
                }

                Console.Write("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            } while (!terminouMenu);
        }

        private void MenuRisco()
        {
            string titulo = "Menu Risco";
            string[] campos = new string[] { "Adicionar cliente de risco", "Remover cliente", "Localizar cliente especifico", "Listar todos os clientes de risco" };

            bool terminouMenu = false;
            do
            {
                switch (MenuGenerico(titulo, campos))
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
                        terminouMenu = true;
                        break;
                    default:
                        Console.WriteLine("Opcao invalida!");
                        break;
                }

                Console.Write("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            } while (!terminouMenu);
        }

        private void MenuBloqueado()
        {

            string titulo = "Menu Bloqueados";
            string[] campos = new string[] { "Adicionar fornecedor", "Remover fornecedor da lista de bloqueados", "Localizar fornecedor bloqueado especifico", "Listar todos os fornecedores bloqueados" };
            bool terminouMenu = false;

            do
            {
                switch (MenuGenerico(titulo, campos))
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
                        terminouMenu = true;
                        break;
                    default:
                        Console.WriteLine("Opcao invalida!");
                        break;
                }

                Console.Write("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            } while (!terminouMenu);
        }

        private int MenuGenerico(string titulo, string[] campos)
        {
            Console.Clear();
            Console.WriteLine($"======{titulo}======");

            for (int i = 0; i < campos.Length; i++)
            {
                Console.WriteLine($"{i + 1}- {campos[i]}");
            }
            Console.WriteLine("0- Voltar");
            Console.Write("R: ");

            if (int.TryParse(Console.ReadLine(), out int option))
            {
                Console.WriteLine($"Opcao: {option}");
                return option;
            }
            else
            {
                Console.WriteLine("Voce deve digitar um numero!");
                Console.Write("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                return MenuGenerico(titulo, campos);
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

        /// <summary>
        /// Cria o diretório e o arquivo se não existirem.
        /// </summary>
        /// <param name="arquivo" >O arquivo a ser criado.</param>
        /// <param name="caminho" >O caminho onde o arquivo será criado.</param>
        public static void CriarDiretorioArquivo(string caminho, string arquivo)
        {
            if (!Directory.Exists(caminho))
                Directory.CreateDirectory(caminho);

            if (!File.Exists(caminho + arquivo))
            {
                var file = File.Create(caminho + arquivo);
                file.Close();
            }
        }

    }
}
