using BILTIFUL.Modulo1;
using BILTIFUL.Modulo4.Entidades;

namespace BILTIFUL.Modulo4.Utils
{
    internal class ArquivoProducao
    {
        public ArquivoProducao()
        {

        }
        /// <summary>
        /// Importa o arquivo Produção.
        /// </summary>
        public static List<Producao> importarProducao(string path, string file)
        {
            List<Producao> templista = new();
            try
            {
                if (File.Exists(path + file))
                {
                    foreach (string item in File.ReadLines(path + file))
                    {
                        Producao tempProducao = new(item);
                        templista.Add(tempProducao);
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
        /// <summary>
        /// Importa o arquivo Item Produção.
        /// </summary>
        public static List<ItemProducao> importarItemProducao(string path, string file)
        {
            List<ItemProducao> templista = new();
            try
            {
                if (File.Exists(path + file))
                {
                    foreach (string item in File.ReadLines(path + file))
                    {
                        ItemProducao tempItemProducao = new(item);
                        templista.Add(tempItemProducao);
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
        /// <summary>
        /// Importa o arquivo de Matéria Prima.
        /// </summary>
        public static List<MPrima> importarMPrima(string path, string file)
        {
            List<MPrima> templista = new();
            try
            {
                if (File.Exists(path + file))
                {
                    foreach (string item in File.ReadLines(path + file))
                    {
                        MPrima tempProducao = new(item);
                        templista.Add(tempProducao);
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
        /// <summary>
        /// Importa o arquivo de Produtos.
        /// </summary>
        public static List<Produto> importarProduto(string path, string file)
        {
            List<Produto> templista = new();
            try
            {
                if (File.Exists(path + file))
                {
                    foreach (string item in File.ReadLines(path + file))
                    {
                        Produto tempProducao = new(item);
                        templista.Add(tempProducao);
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
        /// <summary>
        /// Salva uma lista genérica em um arquivo de texto.
        /// </summary>
        public static void salvarArquivo<T>(List<T> lista, string path, string file)
        {
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
        /// <summary>
        /// Verifica se o caminho existe no computador do usuário.
        /// </summary>
        public static void ChecarCaminho(string path)
        {
            string file = "";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            for (int i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case 0:
                        file = "Producao.dat";
                        break;
                    case 1:
                        file = "ItemProducao.dat";
                        break;
                    case 2:
                        file = "Materia.dat";
                        break;
                    case 3:
                        file = "Cosmetico.dat";
                        break;
                }
                if (!File.Exists(path + file))
                {
                    var newfile = File.Create(path + file);
                    newfile.Close();
                }
            }
        }
    }
}
