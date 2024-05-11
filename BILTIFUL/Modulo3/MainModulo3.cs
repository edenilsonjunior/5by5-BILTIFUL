using BILTIFUL.Modulo1;
using BILTIFUL.Modulo3;
using BILTIFUL.Modulo3.ManipuladorArquivos;
namespace BILTIFUL.Modulo3
{
    internal class MainModulo3
    {
        public MainModulo3()
        {
            int id = 0, valorTotal = 0;

            List<Fornecedor> listFornecedor = new List<Fornecedor>(ManipuladorArquivoCompra.importarFornecedor(@"C:\Teste\", "Fornecedor.dat"));
            List<string> listFornecedorBloqueados = new List<string>(ManipuladorArquivoCompra.importarFornecedorBloqueado(@"C:\Teste\", "Bloqueado.dat"));
            List<MPrima> listMPrima = new List<MPrima>(ManipuladorArquivoCompra.importarMPrima(@"C:\Teste\", "Materia.dat"));

            foreach (var linha in RealizarCompra())
            {
                Console.WriteLine(linha);
            }

            List<Compra> RealizarCompra()
            {
                Compra compra;
                List<Compra> lista = new();
                int idCompra = 1;
                var data = DateOnly.FromDateTime(DateTime.Now);
                string tempCNPJ, mensagem = "";
                bool podeCadastrar = true;

                Console.Write("Informe um CNPJ de fornecedor já cadastrado (14 digitos): ");
                tempCNPJ = Console.ReadLine();

                if (listFornecedor.Find(x => x.Cnpj == tempCNPJ) == null)
                {
                    mensagem = "Fornecedor informado não existe";
                    podeCadastrar = false;
                }
                else if ((listFornecedor.Find(x => x.Cnpj == tempCNPJ && x.Situacao.ToString() == "I") != null))
                {
                    mensagem = "Fornecedor está inativo";
                    podeCadastrar = false;
                }
                else if (listFornecedorBloqueados.Contains(tempCNPJ) == null)
                {
                    mensagem = "Fornecedor está bloqueado";
                    podeCadastrar = false;
                }
                else if (listFornecedor.Find(x => x.Cnpj == tempCNPJ && (DateTime.Now.Date - new DateTime(x.DataAbertura.Year, x.DataAbertura.Month, x.DataAbertura.Day)).Days < 180) != null)
                {
                    mensagem = "Fornecedor com menos de seis meses";
                    podeCadastrar = false;
                }

                if (podeCadastrar)
                {
                    PegarMPrima();
                    compra = new(idCompra, data, tempCNPJ, valorTotal);
                    lista.Add(compra);
                    return lista;
                }
                else
                {
                    Console.WriteLine(mensagem);
                }
                return null;
            }

            List<ItemCompra> PegarMPrima()
            {
                ItemCompra item = new();
                List<ItemCompra> temp = new();
                bool podeCadastrar = true;
                string mensagem = "", materiaPrimaTemp = "";
                var dataAtual = DateOnly.FromDateTime(DateTime.Now);
                int valorMateriaPrima = 0, valorTotalPorMateria = 0, idMPrima = 1, quantidadeCompraMPrima = 0;

                Console.Write("Informe quantas matérias primas deseja comprar: ");
                int quantidadeMPrima = int.Parse(Console.ReadLine());

                if (quantidadeMPrima > 3)
                {
                    Console.WriteLine("Não pode comprar mais de 3 matérias primas de uma vez");
                }
                else
                {
                    for (int i = 0; i < quantidadeMPrima; i++)
                    {
                        Console.Write("Digite o ID da materia prima que deseja comprar: ");
                        materiaPrimaTemp = Console.ReadLine();

                        if (listMPrima.Find(x => x.Id == materiaPrimaTemp) == null)
                        {
                            mensagem = "Matéria Prima informada não existe na base de dados";
                            podeCadastrar = false;
                        }

                        Console.Write("Digite quantas matérias prima desse tipo voce deseja comprar: ");
                        quantidadeCompraMPrima = int.Parse(Console.ReadLine());

                        if (quantidadeCompraMPrima <= 99999 && quantidadeCompraMPrima > 0)
                        {
                            Console.Write("Digite o valor da matéria prima escolhida: ");
                            valorMateriaPrima = int.Parse(Console.ReadLine());

                            valorTotalPorMateria = quantidadeCompraMPrima * valorMateriaPrima;
                            valorTotal += valorTotalPorMateria;
                        }
                        else
                        {
                            Console.WriteLine("Ultrapassou o valor máximo de itens dessa matéria prima.");
                        }
                        item = new(idMPrima, dataAtual, materiaPrimaTemp, quantidadeCompraMPrima, valorMateriaPrima, valorTotalPorMateria);
                        temp.Add(item);
                    }
                }
                return temp;
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