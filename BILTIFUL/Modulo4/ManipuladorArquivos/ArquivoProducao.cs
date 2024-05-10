using BILTIFUL.Modulo1;
using BILTIFUL.Modulo4.Entidades;

namespace BILTIFUL.Modulo4.ManipuladorArquivos
{
    internal class ArquivoProducao
    {
        public ArquivoProducao()
        {
            
        }
        public static List<Producao> importarProducao(string path, string file)
        {
            List<Producao> templista = new();
            try
            {
                //string path = @"C:\BILTIFUL\", file = "Producao.dat";
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
        public static List<ItemProducao> importarItemProducao(string path, string file)
        {
            List<ItemProducao> templista = new();
            try
            {
                //string path = @"C:\BILTIFUL\", file = "ItemProducao.dat";
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

        ///////////////
        ///
        public static List<MPrima> importarMPrima(string path, string file)
        {
            List<MPrima> templista = new();
            try
            {
                //string path = @"C:\BILTIFUL\", file = "Materia.dat";
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
