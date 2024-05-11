
using BILTIFUL.Modulo1;
using System.Collections.Generic;
//**
namespace BILTIFUL.Modulo2
{
    internal class ManipularVenda
    {


        static public void CadastrarVenda(List<Venda> listaVenda, List<Cliente> listaCliente, List<Cliente> listaBloqueados)//+LISTA ITEM VENDA
        {
            bool autorizado = true;
            DateOnly dataatual = DateOnly.FromDateTime(DateTime.Now);
            string cpf;
            int Id = 0;

            Id = listaVenda.Last().idVenda + 1;

            do
            {
                Console.Write("Insira o CPF do cliente cadastrado: ");
                Console.Write("\nOu digite 9 para sair. ");
                cpf = Console.ReadLine();

                if (cpf != "0" && Cliente.VerificarCpf(cpf) == false)
                {
                    Console.WriteLine("CPF inválido!");
                }
            } while (cpf != "0" && Cliente.VerificarCpf(cpf) == false);

            // Bloco de bloqueadores
            // por cpf
            if (listaCliente.Find(x => x.Cpf == cpf) == null)
            {
                Console.WriteLine("Não existe cliente cadastrado com esse CPF.");
                autorizado = false;
            }
            //por menor de idade

            //inadimplentes
            if (listaBloqueados.Find(x => x.Cpf == cpf) != null)
            {
                Console.WriteLine("Venda não autorizada para este CPF.");
                autorizado = false;
            }
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


            //VALOR TOTAL
        }

        /*private bool validarIdade(string? cpf)
        {
            List<Cliente> listaCliente;
            Cliente cliente = listaCliente.FirstOrDefault(c => c.Cpf == cpf);
            DateOnly dataNascimento = listaCliente.dataNascimento;
            DateOnly dataAtual = DateOnly.FromDateTime(DateTime.Now);
            int idade = dataAtual.Year - dataNascimento.Year;
            if (idade >= 18)
            {
                return true;
            }
            return false;

        }*/
    }


}



