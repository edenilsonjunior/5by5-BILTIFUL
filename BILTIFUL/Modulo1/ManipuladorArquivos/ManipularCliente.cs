using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BILTIFUL.Modulo1.ManipuladorArquivos
{
    internal class ManipularCliente
    {
        private readonly string _caminho;
        private readonly string _arquivo;

        public ManipularCliente(string caminho, string arquivo)
        {
            _caminho = caminho;
            _arquivo = arquivo;
            CriarDiretorioArquivo(caminho, arquivo);
        }

        public List<Cliente> Recuperar()
        {
            List<Cliente> clientes = new();

            foreach (string linha in File.ReadAllLines(_caminho + _arquivo))
            {
                Cliente aux = new(linha);
                clientes.Add(aux);
            }

            return clientes;
        }

        public void Salvar(List<Cliente> clientes)
        {
            using var sw = new StreamWriter(_caminho + _arquivo);

            foreach (var item in clientes)
            {
                string data = item.FormatarParaArquivo();
                sw.WriteLine(data);
            }
        }

        public void Cadastrar()
        {
            List<Cliente> clientes = Recuperar();
            string cpf, nome;
            char sexo;
            DateOnly dataNascimento;


            cpf = LerCpf(verificaRegistros: true);


            nome = MainModulo1.LerString("Digite o nome: ");
            sexo = LerSexo();


            Cliente c = new(cpf, nome, dataNascimento, sexo);

            clientes.Add(c);
            Salvar(clientes);
        }

        public void Editar()
        {
            List<Cliente> clientes = Recuperar();
            string cpf;

            cpf = LerCpf(verificaRegistros: false);

            Cliente achei = null;
            foreach (var item in clientes)
            {
                if (item.Cpf.Equals(cpf))
                {
                    achei = item;
                }
            }

            Cliente? c = clientes.Find(c => c.Cpf.Equals(cpf));

            if (c == null)
            {
                Console.WriteLine("O cpf digitado nao existe nos registros!");
                return;
            }


            int opcao;

            do
            {
                opcao = MenuEditar(c.Nome);

                switch (opcao)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        c.Sexo = LerSexo();
                        break;
                    case 4:
                        c.Situacao = (c.Situacao == 'A') ? 'I' : 'A';
                        break;
                    default:
                        Console.WriteLine("Opcao invalida!");
                        break;
                }

            } while (opcao != 0);




        }

        private int MenuEditar(string nomeCliente)
        {
            Console.Clear();
            Console.WriteLine("======Editar Cliente======");
            Console.WriteLine($"Cliente a ser editado: {nomeCliente}");
            Console.WriteLine("==========================\n");

            Console.WriteLine("Opcoes: ");
            Console.WriteLine("1- Editar nome");
            Console.WriteLine("2- Editar Data de nascimento");
            Console.WriteLine("3- Editar o sexo");
            Console.WriteLine("4- Inverter situacao");
            Console.WriteLine("0- Parar edicao");
            Console.Write("R: ");

            if (int.TryParse(Console.ReadLine(), out int option))
            {
                return option;
            }
            else
            {
                Console.WriteLine("Voce deve digitar um numero!");
                Console.Write("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                return MenuEditar(nomeCliente);
            }
        }

        private char LerSexo()
        {
            char sexo;
            do
            {
                sexo = MainModulo1.LerChar("Digite o sexo(M-F):");

                if (sexo != 'M' && sexo != 'F') Console.WriteLine("Digito invalido!");

            } while (sexo != 'M' && sexo != 'F');

            return sexo;
        }

        private string LerCpf(bool verificaRegistros)
        {
            string cpf;


            if (verificaRegistros)
            {
                List<Cliente> clientes = Recuperar();
                do
                {
                    cpf = MainModulo1.LerString("Digite o cpf da pessoa que deseja editar: ");

                    if (!Cliente.VerificarCpf(cpf))
                        Console.WriteLine("O cpf digitado é invalido");

                    if (clientes.Exists(c => c.Cpf.Equals(cpf)))
                        Console.WriteLine("Ja existe um cadastro com esse cpf!");

                } while (!Cliente.VerificarCpf(cpf) || clientes.Exists(c => c.Cpf.Equals(cpf)));
            }
            else
            {
                do
                {
                    cpf = MainModulo1.LerString("Digite o cpf da pessoa que deseja editar: ");

                    if (!Cliente.VerificarCpf(cpf)) Console.WriteLine("O cpf digitado é invalido");

                } while (!Cliente.VerificarCpf(cpf));
            }

            return cpf;
        }



        private void CriarDiretorioArquivo(string caminho, string arquivo)
        {
            if (!Directory.Exists(caminho))
                Directory.CreateDirectory(caminho);

            if (!File.Exists(caminho + arquivo))
            {
                var file = File.Create(caminho + arquivo);
                file.Close();
            }
        }

    }
}
