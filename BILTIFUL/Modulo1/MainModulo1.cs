using BILTIFUL.Modulo1.ManipuladorArquivos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            int opcao = 0;
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
            

            throw new NotImplementedException();
        }


        private void MenuFornecedor()
        {
            throw new NotImplementedException();
        }
        private void MenuMPrima()
        {
            throw new NotImplementedException();
        }
        private void MenuProduto()
        {
            throw new NotImplementedException();
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
