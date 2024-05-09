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
            Nome = nome;
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

            DataNascimento = ConverterParaData(data.Substring(61, 8));

            Sexo = char.Parse(data.Substring(69, 1));

            UltimaCompra = ConverterParaData(data.Substring(70, 8));

            DataCadastro = ConverterParaData(data.Substring(78, 8));
            Situacao = char.Parse(data.Substring(86, 1));

        }

        private DateOnly ConverterParaData(string data)
        {
            string dia = data.Substring(0, 2);
            string mes = data.Substring(2, 2);
            string ano = data.Substring(4, 4);

            return DateOnly.Parse($"{dia}/{mes}/{ano}");
        }


        public static bool VerificarCpf(string cpf)
        {
            throw new NotImplementedException();
        }

        public string FormatarParaArquivo()
        {
            throw new NotImplementedException();
        }
    }
}
