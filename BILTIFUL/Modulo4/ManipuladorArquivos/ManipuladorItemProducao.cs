using BILTIFUL.Modulo4.Entidades;
namespace BILTIFUL.Modulo4.ManipuladorArquivos
{
    internal class ManipuladorItemProducao
    {
        private string _caminho;
        private string _arquivo;

        /// <summary>
        /// Construtor da classe ItemProducao.
        /// </summary>
        /// <param name="caminho">O caminho do diretório.</param>
        /// <param name="arquivo">O nome do arquivo.</param>
        public ManipuladorItemProducao(string caminho, string arquivo)
        {
            _caminho = caminho;
            _arquivo = arquivo;
            CriarDiretorioArquivo();
        }

        /// <summary>
        /// Recupera a lista de itens de produção do arquivo.
        /// </summary>
        /// <returns>A lista de itens produção.</returns>
        public List<ItemProducao> Recuperar()
        {
            List<ItemProducao> itens = new();

            foreach (string linha in File.ReadAllLines(_caminho + _arquivo))
            {
                ItemProducao aux = new(linha);
                itens.Add(aux);
            }

            return itens;
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