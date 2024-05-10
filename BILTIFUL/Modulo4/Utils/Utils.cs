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
    internal class Utils
    {
        string pathpai = @"C:\BILTIFUL\";
        public Utils()
        {

        }
        public static List<Producao> importarProducao()
        {
            List<Producao> templista = new();
            string path = @"C:\BILTIFUL\", file = "Producao.txt";
            if (File.Exists(path + file))
            {
                foreach (string item in File.ReadLines(path + file))
                {
                    if (item.Split(';')[0] != "nome")
                    {
                        templista.Add(importarProducaoAux(item));
                    }
                }
            }
            else
            {
                Console.WriteLine($"Arquivo {path}{file} inexistente!");
            }
            return templista; // retorno essa lista para a Main e importo a minha agenda
        }
        static Producao importarProducaoAux(string conteudo)
        {
            int aux1;
            DateOnly aux2;
            string aux3;
            float aux4;

            // add tryparse trycatch
            aux1 = Int32.Parse(conteudo.Substring(0, 5));
            aux2 = DateOnly.ParseExact(conteudo.Substring(5, 8), "ddMMyyyy");
            aux3 = conteudo.Substring(13, 13);
            aux4 = float.Parse((conteudo.Substring(26, 5))) / 100;
            Producao tempProducao = new(aux1, aux2, aux3, aux4);

            return tempProducao;
        }
        public static List<ItemProducao> importarItemProducao()
        {
            List<ItemProducao> templista = new();
            string path = @"C:\BILTIFUL\", file = "ItemProducao.txt";
            if (File.Exists(path + file))
            {
                foreach (string item in File.ReadLines(path + file))
                {
                    if (item.Split(';')[0] != "nome")
                    {
                        templista.Add(importarItemProducaoAux(item));
                    }
                }
            }
            else
            {
                Console.WriteLine($"Arquivo {path}{file} inexistente!");
            }
            return templista; // retorno essa lista para a Main e importo a minha agenda
        }
        static ItemProducao importarItemProducaoAux(string conteudo)
        {
            int aux1;
            DateOnly aux2;
            string aux3;
            float aux4;

            // add tryparse trycatch
            aux1 = Int32.Parse(conteudo.Substring(0, 5));
            aux2 = DateOnly.ParseExact(conteudo.Substring(5, 8), "ddMMyyyy");
            aux3 = conteudo.Substring(13, 6);
            aux4 = float.Parse((conteudo.Substring(19, 5))) / 100;
            ItemProducao tempProducao = new(aux1, aux2, aux3, aux4);

            return tempProducao;
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
    }
}
