using BILTIFUL.Modulo1;
namespace BILTIFUL.Modulo3.ManipuladorArquivos;

internal class ManipuladorArquivoCompra
{
    public ManipuladorArquivoCompra()
    {

    }
    public static List<Fornecedor> importarFornecedor(string path, string file)
    {
        List<Fornecedor> templista = new();
        try
        {
            //string path = @"C:\BILTIFUL\", file = "Producao.dat";
            if (File.Exists(path + file))
            {
                foreach (string item in File.ReadLines(path + file))
                {
                    templista.Add(importarFornecedorAux(item));
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


    static Fornecedor importarFornecedorAux(string conteudo)
    {
        Fornecedor tempProducao = new(conteudo);
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

    public static List<string> importarFornecedorBloqueado(string path, string file)
    {
        List<string> templista = new();
        try
        {
            //string path = @"C:\BILTIFUL\", file = "Producao.dat";
            if (File.Exists(path + file))
            {
                foreach (string item in File.ReadLines(path + file))
                {
                    templista.Add(importarFornecedorBloqueadoAux(item));
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

    static string importarFornecedorBloqueadoAux(string conteudo)
    {
        string tempProducao = new(conteudo);
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

    public static List<MPrima> importarMPrima(string path, string file)
    {
        List<MPrima> templista = new();
        try
        {
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
        return templista;
    }

    static MPrima importarMPrimaAux(string conteudo)
    {
        MPrima tempMPrima = new(conteudo);
        try
        {
            tempMPrima = new(conteudo);
        }
        catch (Exception e)
        {
            Console.WriteLine("Erro inesperado.");
            Console.WriteLine(e.Message);
        }
        return tempMPrima;
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