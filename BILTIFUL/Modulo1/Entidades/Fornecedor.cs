using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BILTIFUL.Modulo1
{
    internal class Fornecedor
    {
        public string Cnpj { get; set; }             //14 (0-13)
        public string RazaoSocial { get; set; }      //50 (14-63)
        public DateOnly DataAbertura { get; set; }   //8 (64-71)
        public DateOnly UltimaCompra { get; set; }   //8 (72-79)
        public DateOnly DataCadastro { get; set; }   //8 (80-87)
        public char Situacao { get; set; }           //1 (88-88)

        public Fornecedor(string cnpj, string razaoSocial, DateOnly dataAbertura)
        {
            Cnpj = cnpj;
            RazaoSocial = FormatarRazaoSocial(razaoSocial);
            DataAbertura = dataAbertura;
            UltimaCompra = DateOnly.FromDateTime(DateTime.Now);
            DataCadastro = DateOnly.FromDateTime(DateTime.Now);
            Situacao = 'A';
        }

        public Fornecedor(string data)
        {
            Cnpj = data.Substring(0, 14);
            RazaoSocial = data.Substring(14, 50);
            DataAbertura = DateOnly.ParseExact(data.Substring(64, 8), "ddMMyyyy", null);

            UltimaCompra = DateOnly.ParseExact(data.Substring(72, 8), "ddMMyyyy", null);
            DataCadastro = DateOnly.ParseExact(data.Substring(80, 8), "ddMMyyyy", null);

            Situacao = char.Parse(data.Substring(88, 1));
        }

        // Metodos publicos
        public string FormatarParaArquivo()
        {
            string data = "";

            data += Cnpj;
            data += RazaoSocial;
            data += DataAbertura.ToString().Replace("/", "");
            data += UltimaCompra.ToString().Replace("/", "");
            data += DataCadastro.ToString().Replace("/", "");
            data += Situacao;

            return data;
        }


        // Metodos privados
        private string FormatarRazaoSocial(string n)
        {
            string formatado = n;

            while (formatado.Length < 50)
            {
                formatado += ' ';
            }

            formatado = formatado.Substring(0, 50);

            return formatado;
        }


        // Metodos Estaticos
        public static bool VerificarCnpj(string cnpj)
        {
            cnpj = RemoverCaractere(cnpj);

            if (cnpj.Length != 14) return false;

            bool repetido = IsRepetido(cnpj);
            bool digitoUm = ValidacaoDigitoUm(cnpj);
            bool digitoDois = ValidacaoDigitoDois(cnpj);

            return !repetido && digitoUm && digitoDois;
        }

        private static bool ValidacaoDigitoUm(string str)
        {
            int total = 0;
            int[] nrosMultiplicadores = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            for (int i = 0; i < 12; i++)
            {
                string digitoStr = str.Substring(i, 1);
                int digito = int.Parse(digitoStr);

                total += digito * nrosMultiplicadores[i];
            }

            int resto = total % 11;
            int digitoUm = int.Parse(str.Substring(12, 1));

            if ((resto == 0 || resto == 1) && digitoUm == 0)
                return true;

            if ((resto >= 2 && resto <= 10) && digitoUm == 11 - resto)
                return true;

            return false;
        }

        private static bool ValidacaoDigitoDois(string cnpj)
        {
            int total = 0;
            int[] nrosMultiplicadores = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            for (int i = 0; i < 13; i++)
            {
                string digitoStr = cnpj.Substring(i, 1);
                int digito = int.Parse(digitoStr);

                total += digito * nrosMultiplicadores[i];
            }

            int resto = total % 11;
            int digitoDois = int.Parse(cnpj.Substring(13, 1));

            if ((resto == 0 || resto == 1) && digitoDois == 0)
                return true;

            if ((resto >= 2 && resto <= 10) && digitoDois == 11 - resto)
                return true;

            return false;
        }

        private static bool IsRepetido(string cnpj)
        {
            cnpj = RemoverCaractere(cnpj);
            int nroRepetidos = 0;
            for (int i = 0; i < cnpj.Length - 1; i++)
            {
                int n1 = int.Parse(cnpj.Substring(i, 1));
                int n2 = int.Parse(cnpj.Substring(i + 1, 1));

                if (n1 == n2)
                {
                    nroRepetidos++;
                }
            }
            return nroRepetidos == cnpj.Length-1;
        }

        static private string RemoverCaractere(string cnpj)
        {
            cnpj = cnpj.Replace(".", "");
            cnpj = cnpj.Replace("/", "");
            cnpj = cnpj.Replace("-", "");
            return cnpj;
        }
    }
}
