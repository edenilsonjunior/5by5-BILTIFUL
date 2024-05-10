using BILTIFUL.Modulo4.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BILTIFUL.Modulo1;

namespace BILTIFUL.Modulo2.ManipuladorArquivos
{
    internal class ArquivoVenda
    {
        public ArquivoVenda()
        {

        }
        public static List<Cliente> importarCliente(string path, string file)
        {
            List<Cliente> templista = new();
            try
            {
                if (File.Exists(path + file))
                {
                    foreach (string item in File.ReadLines(path + file))
                    {
                        templista.Add(importarClienteAux(item));
                    }
                }
                else
                {
                    Console.WriteLine($"Arquivo {path}{file} inexistente!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro inesperado!");
                Console.WriteLine(e.Message);
            }
            return templista; // retorno essa lista para a Main e importo a minha agenda
        }
        static Cliente importarClienteAux(string conteudo)
        {
            Cliente tempProducao = new Cliente(conteudo);
            return tempProducao;
        }
        public static List<Venda> importarVenda(string path, string file)
        {
            List<Venda> templista = new();
            try
            {
                if (File.Exists(path + file))
                {
                    foreach (string item in File.ReadLines(path + file))
                    {
                        templista.Add(importarVendaAux(item));
                    }
                }
                else
                {
                    Console.WriteLine($"Arquivo {path}{file} inexistente!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro inesperado!");
                Console.WriteLine(e.Message);
            }
            return templista; // retorno essa lista para a Main e importo a minha agenda
        }
        static Venda importarVendaAux(string conteudo)
        {
            Venda tempProducao = new Venda(conteudo);
            return tempProducao;
        }
        public static List<Produto> importarProduto(string path, string file)
        {
            List<Produto> templista = new();
            try
            {
                //string path = @"C:\BILTIFUL\", file = "Cosmetico.dat";
                if (File.Exists(path + file))
                {
                    foreach (string item in File.ReadLines(path + file))
                    {
                        templista.Add(importarProdutoAux(item));
                    }
                }
                else
                {
                    Console.WriteLine($"Arquivo {path}{file} inexistente!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro inesperado!");
                Console.WriteLine(e.Message);
            }
            return templista;
        }
        static Produto importarProdutoAux(string conteudo)
        {
            Produto tempProducao = new(conteudo);
            return tempProducao;
        }
        public static void salvarArquivo<T>(List<T> lista, string file)
        {
            string path = @"C:\BILTIFUL\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            StreamWriter filecontent = new(path + file);
            foreach (var item in lista)
            {
                filecontent.WriteLine(item.ToString());
            }
            filecontent.Close();
        }
    }
}
