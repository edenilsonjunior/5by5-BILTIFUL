namespace BILTIFUL.Modulo3
{
    internal class MainModulo3
    {
        public MainModulo3()
        {
            Compra c = new Compra();
            //foreach (var cnpj in c.CNPJFornecedoresExistentes())
            //{
            //    Console.WriteLine(cnpj);
            //}
            //c.VerificarFornecedorBloqueado();

            Compra CadastrarCompra()
            {
                int id = 1;
                DateOnly dataCompra = DateOnly.FromDateTime(DateTime.Today);
                string cnpjFornecedor = "12345678987643";
                int valorTotal = 9998;
                return new(id, dataCompra, cnpjFornecedor, valorTotal);
            }

            void EscreverNoArquivo(Compra c)
            {
                string path = @"C:\Teste\";
                string file = "teste.dat";

                StreamWriter sw = new(path + file);

                sw.WriteLine(c);

                sw.Close();
            }
        }

    }
}
