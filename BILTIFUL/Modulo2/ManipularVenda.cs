

using BILTIFUL.Modulo1.ManipuladorArquivos;
using BILTIFUL.Modulo1;
using System.Threading.Channels;

namespace BILTIFUL.Modulo2
{
    internal class ManipularVenda
    {


        static public void CadastrarVenda(List<Venda> listaVenda, List<Cliente> listaCliente)
        {
            bool autorizado = true;
            DateOnly dataatual = DateOnly.FromDateTime(DateTime.Now);
            string cpf;
            int Id = 0;

            Id = listaVenda.Last().idVenda + 1;


            do
            {
                Console.Write("Insira o CPF do cliente cadstrado: ");
                Console.Write("Ou digite 0 para sair. ");
                cpf = Console.ReadLine();

                if (Cliente.VerificarCpf(cpf) == false)
                {
                    Console.WriteLine("Cpf invalido!");
                }
            } while (Cliente.VerificarCpf(cpf) == false && cpf == "0");



            // Bloco de bloqueadores

            // por cpf
            if (listaCliente.Find(x => x.Cpf == cpf) == null)
            {
                Console.WriteLine("Não existe cliente cadastrado com esse CPF.");
                autorizado = false;
            }

            //por menor de idade
            // Console.WriteLine("Não é autorizada a venda para menor de idade.");

            // por id maximo
            if (Id > 99999)
            {
                Console.WriteLine("Excedeu o limite de vendas cadastradas no sistema.");
                autorizado = false;
            }

            if (autorizado)
            {

            }
            else
            {
                Console.WriteLine("Venda não permitida...");
            }
            Console.WriteLine("Pressione qualquer tecla");
            Console.ReadKey();
        }
    }
}
