using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BILTIFUL.Modulo4.Entidades
{
    internal class MPrima
    {
        public string Id { get; set; }              //6  (0-5)
        public string Nome { get; set; }            //20 (6-25)
        public DateOnly UltimaCompra { get; set; }  //8 (26-33)
        public DateOnly DataCadastro { get; set; }  //8 (34-41)
        public char Situacao { get; set; }          //1 (42)

        public MPrima(string id, string nome)
        {
            Id = id;
            Nome = FormatarNome(nome);
            UltimaCompra = DateOnly.FromDateTime(DateTime.Now);
            DataCadastro = DateOnly.FromDateTime(DateTime.Now);
            Situacao = 'A';
        }

        public MPrima(string data)
        {
            Id = data.Substring(0, 6);
            Nome = data.Substring(6, 20);
            UltimaCompra = DateOnly.ParseExact(data.Substring(26, 8), "ddMMyyyy", null);
            DataCadastro = DateOnly.ParseExact(data.Substring(34, 8), "ddMMyyyy", null);
            Situacao = char.Parse(data.Substring(42, 1));
        }

        // Metodos 
        public string FormatarParaArquivo()
        {
            string data = "";

            data += Id;
            data += Nome;
            data += UltimaCompra.ToString().Replace("/", "");
            data += DataCadastro.ToString().Replace("/", "");
            data += Situacao;

            return data;
        }

        private string FormatarNome(string n)
        {
            string nomeFormatado = n;

            // caso o nome tenha menos que 20
            while (nomeFormatado.Length < 20)
            {
                nomeFormatado += ' ';
            }

            // caso o nome tenha mais que 20
            nomeFormatado = nomeFormatado.Substring(0, 20);

            return nomeFormatado;
        }

        public static bool VerificarId(string id)
        {
            if (id.Length != 6)
                return false;

            string mp = id.Substring(0, 2);
            if (mp[0] != 'M' || mp[1] != 'P')
                return false;

            bool conversao = int.TryParse(id.Substring(2, 4), out _);
            if (!conversao)
                return false;

            return true;
        }
    }
}
