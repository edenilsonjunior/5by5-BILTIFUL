﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BILTIFUL.Modulo1.ManipuladorArquivos
{
    internal class ManipularProduto
    {
        private string _caminho;
        private string _arquivo;

        public ManipularProduto(string caminho, string arquivo)
        {
            _caminho = caminho;
            _arquivo = arquivo;
        }
    }
}
