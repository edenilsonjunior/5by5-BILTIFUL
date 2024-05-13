using BILTIFUL.Modulo4.Entidades;
using BILTIFUL.Modulo1;
//*//
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
            return templista; 
        }
        static Cliente importarClienteAux(string conteudo)
        {
            Cliente tempCliente = new Cliente(conteudo);
            return tempCliente;
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
            return templista; 
        }
        static Venda importarVendaAux(string conteudo)
        {
            Venda tempProducao = new Venda(conteudo);
            return tempProducao;
        }

        public static List<string> importarBloqueado(string path, string file)
        {
            List<string> templista = new();
            try
            {
                if (File.Exists(path + file))
                {
                    foreach (string item in File.ReadLines(path + file))
                    {
                        templista.Add(importarBloqueado(item));
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
        static string importarBloqueado(string conteudo)
        {
            string tempbloqueado = new string(conteudo);
            return tempbloqueado;
        }
        public static List<Produto> importarProduto(string path, string file)
        {
            List<Produto> templista = new();
            try
            {
                
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
            Produto tempProduto = new(conteudo);
            return tempProduto;
        }

        public static List<ItemVenda> importarItemVenda(string path, string file)
        {
            List<ItemVenda> templista = new();
            try
            {
                if (File.Exists(path + file))
                {
                    foreach (string item in File.ReadLines(path + file))
                    {
                        ItemVenda aux=new (item);
                        templista.Add(aux);
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
