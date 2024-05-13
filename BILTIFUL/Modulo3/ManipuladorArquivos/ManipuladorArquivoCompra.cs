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
        return templista;
    }
    static Fornecedor importarFornecedorAux(string conteudo)
    {
        Fornecedor tempFornecedor = new Fornecedor(conteudo);
        try
        {
            tempFornecedor = new(conteudo);
        }
        catch (Exception e)
        {
            Console.WriteLine("Erro inesperado.");
            Console.WriteLine(e.Message);
        }
        return tempFornecedor;
    }
    public static List<Compra> importarCompra(string path, string file)
    {
        List<Compra> templista = new();
        try
        {
            if (File.Exists(path + file))
            {
                foreach (string item in File.ReadLines(path + file))
                {
                    templista.Add(importarCompraAux(item));
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Erro inesperado!");
            Console.WriteLine(e.Message);
        }
        return templista;
    }
    static Compra importarCompraAux(string conteudo)
    {
        Compra tempCompra = new Compra(conteudo);
        try
        {
            tempCompra = new(conteudo);
        }
        catch (Exception e)
        {
            Console.WriteLine("Erro inesperado.");
            Console.WriteLine(e.Message);
        }
        return tempCompra;
    }

    public static List<ItemCompra> importarItemCompra(string path, string file)
    {
        List<ItemCompra> templista = new();
        try
        {
            if (File.Exists(path + file))
            {
                foreach (string item in File.ReadLines(path + file))
                {
                    templista.Add(importarItemCompraAux(item));
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Erro inesperado!");
            Console.WriteLine(e.Message);
        }
        return templista;
    }
    static ItemCompra importarItemCompraAux(string conteudo)
    {
        ItemCompra tempItemCompra = new ItemCompra(conteudo);
        try
        {
            tempItemCompra = new(conteudo);
        }
        catch (Exception e)
        {
            Console.WriteLine("Erro inesperado.");
            Console.WriteLine(e.Message);
        }
        return tempItemCompra;
    }
    public static List<string> importarFornecedorBloqueado(string path, string file)
    {
        List<string> tempLista = new();
        try
        {
            if (File.Exists(path + file))
            {
                foreach (string item in File.ReadLines(path + file))
                {
                    tempLista.Add(importarFornecedorBloqueadoAux(item));
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
        return tempLista;
    }
    static string importarFornecedorBloqueadoAux(string conteudo)
    {
        string tempFornecedorBloqueado = new(conteudo);
        try
        {
            tempFornecedorBloqueado = new(conteudo);
        }
        catch (Exception e)
        {
            Console.WriteLine("Erro inesperado.");
            Console.WriteLine(e.Message);
        }
        return tempFornecedorBloqueado;
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
}