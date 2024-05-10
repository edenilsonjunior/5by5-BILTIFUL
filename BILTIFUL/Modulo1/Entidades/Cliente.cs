using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BILTIFUL.Modulo1
{
    internal class Cliente
    {
        public string Cpf { get; set; }                 //11 (0-10)
        public string Nome { get; set; }                //50 (11-60)
        public DateOnly DataNascimento { get; set; }    //8  (61-68)
        public char Sexo { get; set; }                  //1  (69-69)
        public DateOnly UltimaCompra { get; set; }      //8  (70-77)
        public DateOnly DataCadastro { get; set; }      //8  (78-85)
        public char Situacao { get; set; }              //1  (86-86)

        public Cliente(string cpf, string nome, DateOnly dataNascimento, char sexo)
        {
            Cpf = cpf;
            Nome = FormatarNome(nome);

            DataNascimento = dataNascimento;
            Sexo = sexo;
            DataCadastro = DateOnly.FromDateTime(DateTime.Now);
            UltimaCompra = DateOnly.FromDateTime(DateTime.Now);
            Situacao = 'A';
        }

        public Cliente(string data)
        {
            Cpf = data.Substring(0, 11);
            Nome = data.Substring(11, 50);

            DataNascimento = DateOnly.ParseExact(data.Substring(61, 8), "ddMMyyyy", null);
            Sexo = char.Parse(data.Substring(69, 1));

            UltimaCompra = DateOnly.ParseExact(data.Substring(70, 8), "ddMMyyyy", null);
            DataCadastro = DateOnly.ParseExact(data.Substring(78, 8), "ddMMyyyy", null);

            Situacao = char.Parse(data.Substring(86, 1));
        }


        // Metodos publicos
        public string FormatarParaArquivo()
        {
            string data = "";

            data += Cpf;
            data += Nome;
            data += DataNascimento.ToString().Replace("/", "");
            data += Sexo;
            data += UltimaCompra.ToString().Replace("/", "");
            data += DataCadastro.ToString().Replace("/", "");
            data += Situacao;

            return data;
        }


        // metodos privates
        private string FormatarNome(string n)
        {
            string nomeFormatado = n;

            // caso o nome tenha menos que 50
            while (nomeFormatado.Length < 50)
            {
                nomeFormatado += ' ';
            }

            // caso o nome tenha mais que 50
            nomeFormatado = nomeFormatado.Substring(0, 50);

            return nomeFormatado;
        }


        // Metodos estaticos
        public static bool VerificarCpf(string cpf)
        {
            return !IsRepetido(cpf) && ValidacaoDigitoUm(cpf) && ValidacaoDigitoDois(cpf);
        }

        private static bool IsRepetido(string str)
        {

            int nroRepetidos = 0;
            for (int i = 0; i < str.Length - 1; i++)
            {
                int n1 = int.Parse(str.Substring(i, 1));
                int n2 = int.Parse(str.Substring(i + 1, 1));

                if (n1 == n2)
                {
                    nroRepetidos++;
                }
            }

            return nroRepetidos == str.Length - 1;
        }

        private static bool ValidacaoDigitoUm(string str)
        {
            if (str.Length < 11)
            {
                return false;
            }

            int resultado = 0;
            for (int i = 0, multiplica = 10; i < 9; i++, multiplica--)
            {
                int digito = int.Parse(str.Substring(i, 1));
                resultado += digito * multiplica;
            }

            // O resto deve ser igual ao primeiro digito verificador
            int resto = (resultado * 10) % 11;
            int digitoUm = int.Parse(str.Substring(9, 1));

            return resto == digitoUm;
        }

        private static bool ValidacaoDigitoDois(string str)
        {
            if (str.Length < 11)
            {
                return false;
            }

            int resultado = 0;
            for (int i = 0, multiplica = 11; i < 10; i++, multiplica--)
            {
                int digito = int.Parse(str.Substring(i, 1));
                resultado += digito * multiplica;
            }

            int resto = (resultado * 10) % 11;

            int digito2 = int.Parse(str.Substring(10, 1));

            return resto == digito2;
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
