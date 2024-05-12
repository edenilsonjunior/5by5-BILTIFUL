using BILTIFUL.Modulo4.Entidades;

namespace BILTIFUL.Modulo4.ManipuladorArquivos
{
    internal class ManipuladorProducao
    {
        private string _caminho;
        private string _arquivo;

        /// <summary>
        /// Construtor da classe Producao.
        /// </summary>
        /// <param name="caminho">O caminho do diretório.</param>
        /// <param name="arquivo">O nome do arquivo.</param>
        public ManipuladorProducao(string caminho, string arquivo)
        {
            _caminho = caminho;
            _arquivo = arquivo;
            CriarDiretorioArquivo();
        }

        /// <summary>
        /// Recupera a lista de produção do arquivo.
        /// </summary>
        /// <returns>A lista de produção.</returns>
        public List<Producao> Recuperar()
        {
            List<Producao> producao = new();

            foreach (string linha in File.ReadAllLines(_caminho + _arquivo))
            {
                Producao aux = new(linha);
                producao.Add(aux);
            }

            return producao;
        }
        /// <summary>
        /// Cria o diretório e o arquivo se não existirem.
        /// </summary>
        private void CriarDiretorioArquivo()
        {
            if (!Directory.Exists(_caminho))
                Directory.CreateDirectory(_caminho);

            if (!File.Exists(_caminho + _arquivo))
            {
                var file = File.Create(_caminho + _arquivo);
                file.Close();
            }
        }
    }
}
