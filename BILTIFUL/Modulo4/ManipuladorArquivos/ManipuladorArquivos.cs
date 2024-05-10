using BILTIFUL.Modulo4.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BILTIFUL.Modulo4.Utils
{
    internal class ManipuladorArquivos
    {
        public ManipuladorArquivos()
        {

        }
        public static List<Producao> importarProducao()
        {
            List<Producao> templista = new();
            try
            {
                string path = @"C:\BILTIFUL\", file = "Producao.dat";
                if (File.Exists(path + file))
                {
                    foreach (string item in File.ReadLines(path + file))
                    {
                        templista.Add(importarProducaoAux(item));
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
        static Producao importarProducaoAux(string conteudo)
        {
            int Id;
            DateOnly DataProducao;
            string Produto;
            float Quantidade;
            Producao tempProducao = new();
            try
            {
                tempProducao = new(conteudo); // constructor que recebe a linha do arquivo
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro inesperado.");
                Console.WriteLine(e.Message);
            }
            return tempProducao;
        }
        public static List<ItemProducao> importarItemProducao()
        {
            List<ItemProducao> templista = new();
            try
            {
                string path = @"C:\BILTIFUL\", file = "ItemProducao.dat";
                if (File.Exists(path + file))
                {
                    foreach (string item in File.ReadLines(path + file))
                    {
                        templista.Add(importarItemProducaoAux(item));
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
        static ItemProducao importarItemProducaoAux(string conteudo)
        {
            int Id;
            DateOnly DataProducao;
            string MateriaPrima;
            float QuantidadeMateriaPrima;

            ItemProducao tempItemProducao = new();
            try
            {
                tempItemProducao = new(conteudo); // constructor que recebe a linha do arquivo
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro inesperado.");
                Console.WriteLine(e.Message);
            }
            return tempItemProducao;
        }
        public static void salvarArquivo<T>(List<T> lista, string file)
        {
            string path = @"C:\BILTIFUL\";//, file = "Producao.txt";
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

        ///////////////
        ///
        public static List<MPrima> importarMPrima()
        {
            List<MPrima> templista = new();
            try
            {
                string path = @"C:\BILTIFUL\", file = "Materia.dat";
                if (File.Exists(path + file))
                {
                    foreach (string item in File.ReadLines(path + file))
                    {
                        templista.Add(importarMPrimaAux(item));
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
        static MPrima importarMPrimaAux(string conteudo)
        {
            MPrima tempProducao = new(conteudo);
            return tempProducao;
        }

        public static List<Produto> importarProduto()
        {
            List<Produto> templista = new();
            try
            {
                string path = @"C:\BILTIFUL\", file = "Cosmetico.dat";
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
        //////////////
        ///

    }
}
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